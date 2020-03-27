using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Data.SQLite;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DecryptPluralSightVideos.Encryption;
using DecryptPluralSightVideos.Model;
using DecryptPluralSightVideos.Option;
using static System.String;
using static DecryptPluralSightVideos.Option.Utils;
using SearchOption = System.IO.SearchOption;

namespace DecryptPluralSightVideos
{
    public class Decryptor
    {
        #region Fields

        //private VirtualFileStream playingFileStream;
        List<char> InvalidPathCharacters = new List<char>(), InvalidFileCharacters = new List<char>();
        private SQLiteConnection DatabaseConnection;
        public DecryptorOptions Options = new DecryptorOptions();
        private ConsoleColor color_default;
        List<Task> TaskList = new List<Task>();
        SemaphoreSlim Semaphore = new SemaphoreSlim(5);
        object SemaphoreLock = new object();

        #endregion


        /// <summary>
        /// Constructor Of Decryptor Class. Init invalid characters of path and console colors.
        /// </summary>
        public Decryptor()
        {
            InvalidPathCharacters.AddRange(Path.GetInvalidPathChars());
            //InvalidPathCharacters.AddRange(new char[] {':', '?', '"', '\\', '/'});

            InvalidFileCharacters.AddRange(Path.GetInvalidFileNameChars());
            //InvalidFileCharacters.AddRange(new char[] {':', '?', '"', '\\', '/'});

            color_default = Console.ForegroundColor;
        }

        /// <summary>
        /// Constructor Of Decryptor Class. Init options from the console.
        /// </summary>
        /// <param name="options">Decryptor Options</param>
        public Decryptor(DecryptorOptions options) : this()
        {
            Options = options;

            if (options.UseDatabase)
                Options.UseDatabase = InitDB(options.DatabasePath);
        }

        /// <summary>
        /// Clean the input string and remove all invalid chars
        /// </summary>
        /// <param name="path">input path</param>
        /// <returns></returns>
        public string CleanName(string path)
        {
            foreach (var invalidChar in InvalidFileCharacters)
                path = path.Replace(invalidChar, '-');

            return path;
        }


        /// <summary>
        /// Encrypt two string to become folder name.
        /// </summary>
        /// <param name="moduleName">Name of Module</param>
        /// <param name="moduleAuthorName">Name of Author in the course</param>
        /// <returns>String has been encrypted</returns>
        public string ModuleHash(string moduleName, string moduleAuthorName)
        {
            string s = moduleName + "|" + moduleAuthorName;
            using (MD5 md5 = MD5.Create())
                return Convert.ToBase64String(md5.ComputeHash(Encoding.UTF8.GetBytes(s))).Replace('/', '_');
        }

        /// <summary>
        /// Decryption and rename course, module path.
        /// </summary>
        /// <param name="folderPath">Source path contains all courses</param>
        /// <param name="outputFolder">Destination of output course</param>
        public void DecryptAllFolders(string folderPath, string outputFolder = "")
        {
            if (!Directory.Exists(folderPath))
            {
                throw new DirectoryNotFoundException();
            }

            if (IsNullOrWhiteSpace(outputFolder))
            {
                outputFolder = folderPath;
            }
            else
            {
                if (!Directory.Exists(outputFolder))
                {
                    System.IO.Directory.CreateDirectory(outputFolder);
                }
            }

            foreach (string coursePath in Directory.GetDirectories(folderPath, "*",
                SearchOption.TopDirectoryOnly))
            {
                var course = GetCourseFromDb(coursePath);

                if (course != null)
                {
                    // Create new course path with the output path
                    var newCoursePath = Path.Combine(outputFolder, CleanName(course.CourseTitle));

                    DirectoryInfo courseInfo = Directory.Exists(newCoursePath)
                        ? new DirectoryInfo(newCoursePath)
                        : Directory.CreateDirectory(newCoursePath);

                    // Move all folders and its contents to newCoursePath

                    #region Move file

                    /*
                     
                    if (Path.GetPathRoot(newCoursePath) != Path.GetPathRoot(coursePath))
                    {
                        Microsoft.VisualBasic.FileIO.FileSystem.MoveDirectory(coursePath, newCoursePath);
                    }
                    else
                    {
                        Directory.Move(coursePath, newCoursePath);
                    }

                    */

                    #endregion

                    // Get list all modules in current course
                    List<Module> listModules = course.Modules;
                    
                    if (listModules.Count > 0)
                    {
                        // Get each module
                        foreach (Module module in listModules)
                        {
                            // Generate module hash name
                            string moduleHash = ModuleHash(module.ModuleName, module.AuthorHandle);
                            // Generate module path
                            string moduleHashPath = Path.Combine(coursePath, moduleHash);
                            // Create new module path with decryption name
                            string newModulePath = Path.Combine(courseInfo.FullName,
                                module.ModuleIndex + ". " + module.ModuleTitle);
                            // If length too long, rename it
                            if (newModulePath.Length > 240)
                            {
                                newModulePath = Path.Combine(courseInfo.FullName,
                                    module.ModuleIndex + "");
                            }

							if (Directory.Exists(moduleHashPath))
                            {
                                DirectoryInfo moduleInfo = Directory.Exists(newModulePath)
                                    ? new DirectoryInfo(newModulePath)
                                    : Directory.CreateDirectory(newModulePath);
                                // Decrypt all videos in current module folder
                                DecryptAllVideos(moduleHashPath, module, moduleInfo.FullName);
                            }
                            else
                            {
                                WriteToConsole(
                                    "Foler " + moduleHash +
                                    " cannot be found in the current course path.",
                                    ConsoleColor.Red);
                            }
                        }
                    }
                    WriteToConsole("Decryption " + course.CourseTitle + " has been completed!", ConsoleColor.Magenta);
                }
            }
        }

        public bool RemoveCourseInDb(string coursePath)
        {
            string courseName = GetFolderName(coursePath);

            var cmd = DatabaseConnection.CreateCommand();
            cmd.CommandText = @"DELETE FROM Course 
                                WHERE Name = @courseName";
            cmd.Parameters.Add(new SQLiteParameter("@courseName", courseName));

            var reader = cmd.ExecuteNonQuery();

            return reader > 0;
        }

        public bool RemoveCourseInDisk(string coursePath)
        {
            try
            {
                if (Directory.Exists(coursePath))
                {
                    Directory.Delete(coursePath,true);
                }
                return true;
            }
            catch (Exception ex)
            {
                WriteToConsole(ex.Message, ConsoleColor.White);
                return false;
            }
        }

        /// <summary>
        /// Check, if the path or file name contains any illegal characters
        /// </summary>
        /// <param name="path">The path to the file or directory</param>
        /// <returns>A path without any illegal characters</returns>
        private string EscapeIllegalCharacters(string path)
		{
			// Windows NTFS (macOS is only ':')
			string illegalCharacters = "/?<>:*|\"";
			foreach (char c in illegalCharacters)
			{
				path = path.Replace(c.ToString(), String.Empty);
			}

			return path;
		}

        /// <summary>
        /// Decrypt all videos in current module folder.
        /// </summary>
        /// <param name="folderPath">Current module folder</param>
        /// <param name="moduleId">Module Id</param>
        /// <param name="outputPath">Destination of output video</param>
        public void DecryptAllVideos(string folderPath, Module module, string outputPath)
        {
            
            // Get all clips of this module from database
            List<Clip> listClips = module.Clips;

			if (listClips.Count > 0)
            {
                foreach (Clip clip in listClips)
                {
                    // Get current path of the encrypted video
                    string currPath = Path.Combine(folderPath, clip.ClipName + ".psv");
                    if (File.Exists(currPath))
                    {
                        // Create new path with output folder
                        string newPath = "";
                        if (clip.ClipIndex < 10)
                        {
                            newPath = Path.Combine(outputPath,
                                 "0" + clip.ClipIndex + ". " + clip.ClipTitle + ".mp4");
                        }
                        else
                        {
                            newPath = Path.Combine(outputPath,
                                clip.ClipIndex + ". " + clip.ClipTitle + ".mp4");
                        }

                        // If length too long, rename it
                        if (newPath.Length > 240)
                        {
                            newPath = Path.Combine(outputPath,
                                clip.ClipIndex + ".mp4");
                        }

                        // Init video and get it from istream
                        IStream iStream;
						var playingFileStream = new VirtualFileStream(currPath);
                        playingFileStream.Clone(out iStream);

                        string fileName = Path.GetFileName(currPath);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Start to Decrypt File \"{0}\"", fileName);
                        Console.ForegroundColor = color_default;

                        DecryptVideo(iStream, newPath);
                        if (Options.CreateTranscript)
                        {
                            // Generate transcript file if user ask
                            WriteTranscriptFile(clip, newPath);
                        }
                        //Semaphore.Wait();
                        //TaskList.Add(Task.Run(() =>
                        //{
                        //    // Write the decrypted video from istream to new file mp4
                        //    DecryptVideo(iStream, newPath);
                        //    if (Options.CreateTranscript)
                        //    {
                        //        // Generate transcript file if user ask
                        //        WriteTranscriptFile(clip, newPath);
                        //    }
                        //    lock (SemaphoreLock)
                        //    {
                        //        Semaphore.Release();
                        //    }
                        //}));

                        WriteToConsole($"Decryption File \"{Path.GetFileName(newPath)}\" successfully ", ConsoleColor.Green, true);
                        playingFileStream.Dispose();

                    }
                    else
                    {
                        WriteToConsole($"File \"{Path.GetFileName(currPath)}\"  cannot be found ", ConsoleColor.Gray, true);
                    }
                }
            }
        }

        /// <summary>
        /// Write transcript for the clip if it available.
        /// </summary>
        /// <param name="clipId">Clip Id</param>
        /// <param name="clipPath">Path of current clip</param>
        public void WriteTranscriptFile(Clip clip, string clipPath)
        {
            // Get all transcript to list
            List<ClipTranscript> clipTranscripts = clip.Subtitle;

            if (clipTranscripts.Count > 0)
            {
                // Create transcript path with the same name of the clip
                string transcriptPath = Path.Combine(Path.GetDirectoryName(clipPath),
                    Path.GetFileNameWithoutExtension(clipPath) + ".srt");
                if (!File.Exists(transcriptPath))
                {
                    // Write it to file with stream writer
                    StreamWriter writer = new StreamWriter(transcriptPath);
                    int i = 1;
                    foreach (var clipTranscript in clipTranscripts)
                    {
                        var start = TimeSpan.FromMilliseconds(clipTranscript.StartTime).ToString(@"hh\:mm\:ss\,fff");
                        var end = TimeSpan.FromMilliseconds(clipTranscript.EndTime).ToString(@"hh\:mm\:ss\,fff");
                        writer.WriteLine(i++);
                        writer.WriteLine(start + " --> " + end);
                        writer.WriteLine(clipTranscript.Text);
                        writer.WriteLine();
                    }
                    writer.Close();
                    WriteToConsole("Transcript of " + Path.GetFileName(clipPath) + " has been generated scucessfully.",
                        ConsoleColor.Red);
                }
            }
        }

        public string RenameIfDuplicated(string path)
        {
            string newFullPath = Empty;
            int count = 1;

            // If path is file
            if (Path.HasExtension(path))
            {
                string fileName = Path.GetFileNameWithoutExtension(path);
                string extension = Path.GetExtension(path);
                string currPath = Path.GetDirectoryName(path);
                newFullPath = path;

                while (File.Exists(newFullPath))
                {
                    string tempFileName = $"{fileName} ({count++})";
                    newFullPath = Path.Combine(currPath, tempFileName + extension);
                }
            }
            // Else path is directory
            else
            {
                string folderName = GetFolderName(path);
                string currPath = Path.GetDirectoryName(path);
                newFullPath = path;

                while (Directory.Exists(newFullPath))
                {
                    string tempFileName = $"{folderName} ({count++})";
                    newFullPath = Path.Combine(currPath, tempFileName);
                }
            }
            return newFullPath;
        }

        public string GetFileNameIfItExisted(string filePath, bool checkExisted = false)
        {
            if (checkExisted)
            {
                if (File.Exists(filePath))
                {
                    return filePath.Substring(filePath.LastIndexOf(@"\") + 1);
                }

                throw new FileNotFoundException();
            }
            return filePath.Substring(filePath.LastIndexOf(@"\") + 1);
        }

        /// <summary>
        /// Get current folder name in the full path.
        /// </summary>
        /// <param name="folderPath">The full folder path.</param>
        /// <param name="checkExisted">Determine if user need to check folder is existed.</param>
        /// <returns>Folder name of full path.</returns>
        public string GetFolderName(string folderPath, bool checkExisted = false)
        {
            if (checkExisted)
            {
                if (Directory.Exists(folderPath))
                {
                    return folderPath.Substring(folderPath.LastIndexOf(@"\") + 1);
                }
                throw new DirectoryNotFoundException();
            }
            return folderPath.Substring(folderPath.LastIndexOf(@"\") + 1);
        }

        /// <summary>
        /// Compare two files
        /// </summary>
        /// <param name="fileOne">Path of the first file</param>
        /// <param name="fileTwo">Path of the second file</param>
        /// <returns>Boolean value determine two files is the same.</returns>
        public bool CompareTwoFiles(string fileOne, string fileTwo)
        {
            return File.ReadAllBytes(fileOne).LongLength == File.ReadAllBytes(fileTwo).LongLength;
        }

        /// <summary>
        /// Decrypt video.
        /// </summary>
        /// <param name="curStream">IStream</param>
        /// <param name="newPath">Output path of the clip</param>
        public void DecryptVideo(IStream curStream, string newPath)
        {
            STATSTG stat;
            curStream.Stat(out stat, 0);
            IntPtr myPtr = (IntPtr) 0;
            int strmSize = (int) stat.cbSize;
            byte[] strmInfo = new byte[strmSize];
            curStream.Read(strmInfo, strmSize, myPtr);
            File.WriteAllBytes(newPath, strmInfo);
        }

        #region DB
        /// <summary>
        /// Get transcript text of specified clip from database.
        /// </summary>
        /// <param name="clipId">Clip Id</param>
        /// <returns>List of transcript text of the current clip.</returns>
        public List<ClipTranscript> GetTrasncriptFromDb(int clipId)
        {
            List<ClipTranscript> list = new List<ClipTranscript>();

            var cmd = DatabaseConnection.CreateCommand();
            cmd.CommandText = @"SELECT StartTime, EndTime, Text
                                FROM ClipTranscript
                                WHERE ClipId = @clipId
                                ORDER BY Id ASC";
            cmd.Parameters.Add(new SQLiteParameter("@clipId", clipId));

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ClipTranscript clipTranscript = new ClipTranscript
                {
                    StartTime = reader.GetInt32(reader.GetOrdinal("StartTime")),
                    EndTime = reader.GetInt32(reader.GetOrdinal("EndTime")),
                    Text = reader.GetString(reader.GetOrdinal("Text"))
                };
                list.Add(clipTranscript);
            }

            return list;
        }

        /// <summary>
        /// Get all clips information of specified module from database.
        /// </summary>
        /// <param name="moduleId">Module Id</param>
        /// <returns>List of information about clips belong to specifed module.</returns>
        public List<Clip> GetClipsFromDb(int moduleId)
        {
            List<Clip> list = new List<Clip>();

            var cmd = DatabaseConnection.CreateCommand();
            cmd.CommandText = @"SELECT Id, Name, Title, ClipIndex
                                FROM Clip 
                                WHERE ModuleId = @moduleId";
            cmd.Parameters.Add(new SQLiteParameter("@moduleId", moduleId));

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Clip clip = new Clip
                {
                    ClipId = reader.GetInt32(reader.GetOrdinal("Id")),
                    ClipName = reader.GetString(reader.GetOrdinal("Name")),
                    ClipTitle = CleanName(reader.GetString(reader.GetOrdinal("Title"))),
                    ClipIndex = reader.GetInt32(reader.GetOrdinal("ClipIndex")),
                    Subtitle = GetTrasncriptFromDb(reader.GetInt32(reader.GetOrdinal("Id")))
                };
                list.Add(clip);
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// Get all modules information of specified course from database.
        /// </summary>
        /// <param name="courseName">Name of course</param>
        /// <returns>List of modules information of specified course.</returns>
        public List<Module> GetModulesFromDb(string courseName)
        {
            List<Module> list = new List<Module>();

            var cmd = DatabaseConnection.CreateCommand();
            cmd.CommandText = @"SELECT Id, Name, Title, AuthorHandle, ModuleIndex
                                FROM Module 
                                WHERE CourseName = @courseName";
            cmd.Parameters.Add(new SQLiteParameter("@courseName", courseName));

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Module module = new Module
                {
                    ModuleId = reader.GetInt32(reader.GetOrdinal("Id")),
                    AuthorHandle = reader.GetString(reader.GetOrdinal("AuthorHandle")),
                    ModuleName = reader.GetString(reader.GetOrdinal("Name")),
                    ModuleTitle = CleanName(reader.GetString(reader.GetOrdinal("Title"))),
                    ModuleIndex = reader.GetInt32(reader.GetOrdinal("ModuleIndex")),
                    Clips = GetClipsFromDb(reader.GetInt32(reader.GetOrdinal("Id")))
                };
                list.Add(module);
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// Get course information from database.
        /// </summary>
        /// <param name="folderCoursePath">Folder contains all courses</param>
        /// <returns>Course information</returns>
        public Course GetCourseFromDb(string folderCoursePath)
        {
            Course course = null;

            string courseName = GetFolderName(folderCoursePath, true).Trim().ToLower();

            var cmd = DatabaseConnection.CreateCommand();
            cmd.CommandText = @"SELECT Name, Title, HasTranscript 
                                FROM Course 
                                WHERE Name = @courseName";
            cmd.Parameters.Add(new SQLiteParameter("@courseName", courseName));

            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                course = new Course
                {
                    CourseName = reader.GetString(reader.GetOrdinal("Name")),
                    CourseTitle = CleanName(reader.GetString(reader.GetOrdinal("Title"))),
                    HasTranscript = reader.GetInt32(reader.GetOrdinal("HasTranscript")),
                    Modules = GetModulesFromDb(reader.GetString(reader.GetOrdinal("Name")))
                };
            }

            reader.Close();

            return course;
        } 
        

        /// <summary>
        /// Init database connection.
        /// </summary>
        /// <param name="dbPath">Database file path</param>
        /// <returns>Boolean value determine the database is open successful or not</returns>
        public bool InitDB(string dbPath)
        {
            if (File.Exists(dbPath))
            {
                if (Path.GetExtension(dbPath).Equals(".db"))
                {
                    DatabaseConnection = new SQLiteConnection($"Data Source={dbPath}; Version=3;FailIfMissing=True");
                    DatabaseConnection.Open();
                    WriteToConsole("The Database Connection has been open completely." + Environment.NewLine,
                        ConsoleColor.Green);

                    return true;
                }
                WriteToConsole("The database file isn't corrected.", ConsoleColor.Red);
                return false;
            }
            WriteToConsole("Cannot find the database path.", ConsoleColor.Red);
            return false;
        }
        #endregion
    }
}