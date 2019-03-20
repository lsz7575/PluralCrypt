using DecryptPluralSightVideosGUI.Encryption;
using DecryptPluralSightVideosGUI.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.ListView;
using DecryptPluralSightVideosGUI.Properties;

namespace DecryptPluralSightVideosGUI
{
    public partial class frmMain : Form
    {
        private List<CourseItem> listCourse;
        private char[] InvalidFileCharacters = Path.GetInvalidFileNameChars();
        private SQLiteConnection DatabaseConnection;
        SemaphoreSlim Semaphore = new SemaphoreSlim(5);
        object SemaphoreLock = new object();

        public frmMain()
        {
            InitializeComponent();

            string strPluralsightPath = "";

            if (string.IsNullOrEmpty(Settings.Default.PluralsightPath))
            {
                strPluralsightPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Pluralsight";
                strPluralsightPath = Directory.Exists(strPluralsightPath) ? strPluralsightPath : "";
                Settings.Default.PluralsightPath = strPluralsightPath;
            }

            txtCoursePath.Text = Directory.Exists(strPluralsightPath) ? strPluralsightPath + @"\courses" : "";
            txtDBPath.Text = Directory.Exists(strPluralsightPath) ? strPluralsightPath + @"\pluralsight.db" : "";
            txtOutputPath.Text = Directory.Exists(Settings.Default.DecryptPath) ? Settings.Default.DecryptPath : "";

            imgList.ImageSize = new Size(170, 100);
            imgList.ColorDepth = ColorDepth.Depth32Bit;
            listView1.LargeImageList = imgList;

            bgwDecrypt.DoWork += BgwDecrypt_DoWork;
            bgwDecrypt.ProgressChanged += BgwDecrypt_ProgressChanged;
            bgwDecrypt.RunWorkerCompleted += BgwDecrypt_RunWorkerCompleted;

            bgwGetCourse.DoWork += BgwGetCourse_DoWork;
            bgwGetCourse.ProgressChanged += BgwGetCourse_ProgressChanged;
            bgwGetCourse.RunWorkerCompleted += BgwGetCourse_RunWorkerCompleted;
        }

        

        #region BackgroundWorker
        private void BgwGetCourse_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 100)
            {
                bgwDecrypt.CancelAsync();
                return;
            }

            if (e.UserState.GetType() == typeof(Log))
            {
                Log log = e.UserState as Log;
                AddText(log.Text, log.TextColor, log.NewLine);
            }
            else
            {
                dynamic obj = (dynamic)e.UserState;
                AddListView(obj.Item as ListViewItem, obj.Image as Image);
            }
        }

        private void BgwGetCourse_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            ReadCourse(txtCoursePath.Text,txtDBPath.Text);

            if (bgwGetCourse.CancellationPending)
            {
                // Set the e.Cancel flag so that the WorkerCompleted event
                // knows that the process was cancelled.
                e.Cancel = true;
                bgwGetCourse.ReportProgress(0);
                return;
            }
        }

        private void BgwGetCourse_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            pnlMain.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void BgwDecrypt_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 100)
            {
                bgwDecrypt.CancelAsync();
                return;
            }

            dynamic obj = (dynamic)e.UserState;
            AddText(obj.Text, obj.Color, obj.newLine);
        }

        private void BgwDecrypt_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            DecryptCourse((List<ListViewItem>)e.Argument);

            if (bgwDecrypt.CancellationPending)
            {
                // Set the e.Cancel flag so that the WorkerCompleted event
                // knows that the process was cancelled.
                e.Cancel = true;
                bgwDecrypt.ReportProgress(0);
                return;
            }
        }

        private void BgwDecrypt_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            pnlMain.Enabled = true;
            Cursor.Current = Cursors.Default;
        }
        #endregion

        #region Event
        private void btnCoursePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtCoursePath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtOutputPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnDBPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() { Multiselect = false };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtDBPath.Text = openFileDialog.FileName;
            }
        }

        private void btnReadCourse_Click(object sender, EventArgs e)
        {
            try
            {
                pnlMain.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                listView1.Items.Clear();
                imgList.Images.Clear();
                rtbLog.Clear();

                bgwGetCourse.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDecypt_Click(object sender, EventArgs e)
        {
            try
            {
                pnlMain.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;

                List<ListViewItem> lstCourse = listView1.CheckedItems.Cast<ListViewItem>().ToList();
                bgwDecrypt.RunWorkerAsync(lstCourse);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            SelectedListViewItemCollection list = (sender as ListView).SelectedItems;
            if (list.Count == 0) return;
            list[0].Checked = list[0].Checked ? false : true;
        }
        #endregion

        #region Method
        public void ReadCourse(string coursePath, string dbPath)
        {
            try
            {
                if (Directory.Exists(coursePath) && this.InitDB(dbPath))
                {
                    bgwGetCourse.ReportProgress(1, new Log() { Text = "Getting courses ....", TextColor = Color.Green, NewLine = true });

                    List<string> folderList = Directory.GetDirectories(coursePath, "*", SearchOption.TopDirectoryOnly).ToList();

                    folderList = folderList.Where(r => Directory.GetDirectories(r, "*", SearchOption.TopDirectoryOnly).Length > 0).ToList();

                    listCourse = folderList.Select(r => new CourseItem() { coursePath = r, course = this.GetCourseFromDb(r) }).Where(r => r.course != null).OrderBy(r => r.course.CourseTitle).ToList();

                    listCourse = listCourse.Where(r => Directory.GetFiles(r.coursePath,"*.psv",SearchOption.AllDirectories).Length == r.course.NumOfVideo).ToList();

                    foreach (CourseItem item in listCourse)
                    {
                        Image img;
                        if (File.Exists(item.coursePath + @"\image.jpg"))
                            img = Image.FromFile(item.coursePath + @"\image.jpg");
                        else
                            img = new Bitmap(100, 100);

                        ListViewItem listItem = new ListViewItem() { ImageKey = item.course.CourseName, Name = item.course.CourseName, Text = item.course.CourseTitle };

                        bgwGetCourse.ReportProgress(1, new { Item = listItem, Image = img });
                    }

                    bgwGetCourse.ReportProgress(1, new Log() { Text = "Get courses Completed.", TextColor = Color.Green, NewLine = true });

                    bgwGetCourse.ReportProgress(100);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DecryptCourse(List<ListViewItem> list)
        {
            if (string.IsNullOrWhiteSpace(txtCoursePath.Text))
            {
                MessageBox.Show("Please select courses path", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrWhiteSpace(txtDBPath.Text))
            {
                MessageBox.Show("Please select database path", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrWhiteSpace(txtOutputPath.Text))
            {
                MessageBox.Show("Please select output path", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            foreach (ListViewItem item in list)
            {
                CourseItem courseItem = listCourse.Where(r => r.course.CourseName == item.Name).Select(r => r).FirstOrDefault();

                bgwDecrypt.ReportProgress(1, new { Text = "Starting decrcypt course " + courseItem.course.CourseTitle, Color = Color.Magenta, newLine = true });

                //Create new course path with the output path
                var newCoursePath = Path.Combine(txtOutputPath.Text, this.CleanName(courseItem.course.CourseTitle));

                DirectoryInfo courseInfo = Directory.Exists(newCoursePath)
                    ? new DirectoryInfo(newCoursePath)
                    : Directory.CreateDirectory(newCoursePath);


                //Get list all modules in current course
                List<Module> listModules = courseItem.course.Modules;

                if (listModules.Count > 0)
                {
                    //Get each module
                    foreach (Module module in listModules)
                    {
                        //Generate module hash name
                        string moduleHash = this.ModuleHash(module.ModuleName, module.AuthorHandle);
                        //Generate module path
                        string moduleHashPath = Path.Combine(courseItem.coursePath, moduleHash);
                        //Create new module path with decryption name
                        string newModulePath = Path.Combine(courseInfo.FullName,
                            module.ModuleIndex + ". " + module.ModuleTitle);
                        //If length too long, rename it
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
                            //Decrypt all videos in current module folder
                            this.DecryptAllVideos(moduleHashPath, module, moduleInfo.FullName);
                        }
                        else
                        {
                            //this.AddText("Folder " + moduleHash + " cannot be found in the current course path.", Color.Red, true);
                            bgwDecrypt.ReportProgress(1, new { Text = "Folder " + moduleHash + " cannot be found in the current course path.", Color = Color.Red, newLine = true });
                        }
                    }
                }
                //this.AddText("Decryption " + courseItem.course.CourseTitle + " has been completed!", Color.Magenta, true);
                bgwDecrypt.ReportProgress(1, new { Text = "Decryption " + courseItem.course.CourseTitle + " has been completed!", Color = Color.Magenta, newLine = true });

                if (chkDelete.Checked)
                {
                    Directory.Delete(courseItem.coursePath,true);
                    RemoveCourseInDb(courseItem.coursePath);

                    bgwDecrypt.ReportProgress(1, new { Text = "Delete course " + courseItem.course.CourseTitle + " success!", Color = Color.Magenta, newLine = true });
                }
            }

            bgwDecrypt.ReportProgress(100);
        }

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
                        string newPath = Path.Combine(outputPath,
                            clip.ClipIndex + ". " + clip.ClipTitle + ".mp4");



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

                        //this.AddText($"Start to Decrypt File \"{fileName}\"", Color.Yellow);
                        bgwDecrypt.ReportProgress(1, new { Text = $"Start to Decrypt File \"{fileName}\"", Color = Color.Yellow, newLine = false });

                        this.DecryptVideo(iStream, newPath);
                        if (chkCreateSub.Checked)
                        {
                            // Generate transcript file if user ask
                            this.WriteTranscriptFile(clip, newPath);
                        }

                        //this.AddText($"Decryption File \"{Path.GetFileName(newPath)}\" successfully", Color.Green, true);
                        bgwDecrypt.ReportProgress(1, new { Text = $"Decryption File \"{Path.GetFileName(newPath)}\" successfully", Color = Color.Green, newLine = true });
                        playingFileStream.Dispose();

                    }
                    else
                    {
                        //this.AddText($"File \"{Path.GetFileName(currPath)}\"  cannot be found ", Color.Gray, true);
                        bgwDecrypt.ReportProgress(1, new { Text = $"File \"{Path.GetFileName(currPath)}\"  cannot be found", Color = Color.Gray, newLine = true });

                    }
                }
            }
        }

        public void DecryptVideo(IStream curStream, string newPath)
        {
            STATSTG stat;
            curStream.Stat(out stat, 0);
            IntPtr myPtr = (IntPtr)0;
            int strmSize = (int)stat.cbSize;
            byte[] strmInfo = new byte[strmSize];
            curStream.Read(strmInfo, strmSize, myPtr);
            File.WriteAllBytes(newPath, strmInfo);
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
                    //this.AddText("Transcript of " + Path.GetFileName(clipPath) + " has been generated scucessfully.", Color.Purple);
                    bgwDecrypt.ReportProgress(1, new { Text = $"Transcript of " + Path.GetFileName(clipPath) + " has been generated scucessfully.", Color = Color.Purple, newLine = false });
                }
            }
        } 
        #endregion

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

                course.NumOfVideo = course.Modules.Sum(r => r.Clips.Count);
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
                    //this.AddText("The Database Connection has been open completely.", Color.Green, true);
                    bgwGetCourse.ReportProgress(1, new Log() {Text = "The Database Connection has been open completely.", TextColor =  Color.Green, NewLine = true });
                    return true;
                }
                //this.AddText("The database file isn't corrected.", Color.Red);
                bgwGetCourse.ReportProgress(1, new Log() { Text = "The database file isn't corrected.", TextColor =  Color.Red});
                return false;
            }
            //this.AddText("Cannot find the database path.", Color.Red);
            bgwGetCourse.ReportProgress(1, new Log() {Text = "Cannot find the database path.", TextColor =  Color.Red });
            return false;
        }

        public bool RemoveCourseInDb(string coursePath)
        {
            string courseName = GetFolderName(coursePath);

            var cmd = DatabaseConnection.CreateCommand();
            cmd.CommandText = @"pragma foreign_keys=on;DELETE FROM Course WHERE Name = @courseName";
            cmd.Parameters.Add(new SQLiteParameter("@courseName", courseName));

            var reader = cmd.ExecuteNonQuery();

            return reader > 0;
        }
        #endregion
        #region Util
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

        public void AddText(string text, Color color, bool newLine = false)
        {
            rtbLog.SelectionColor = color;
            rtbLog.AppendText(text + "\n");
            if (newLine) rtbLog.AppendText("\n");

            rtbLog.SelectionStart = rtbLog.Text.Length;
            rtbLog.ScrollToCaret();
        }

        public void AddListView(ListViewItem item, Image img)
        {
            imgList.Images.Add(item.ImageKey,img);
            listView1.Items.Add(item);

            listView1.SuspendLayout();
            listView1.Refresh();
            listView1.ResumeLayout();
        }
        #endregion
    }

    class CourseItem
    {
        public Course course { get; set; }
        public string coursePath { get; set; }
    }

    class Log
    {
        public string Text { get; set; }
        public Color TextColor { get; set; }
        public bool NewLine { get; set; }
    }
}
