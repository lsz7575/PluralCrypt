using DecryptPluralSightVideosGUI.Encryption;
using DecryptPluralSightVideosGUI.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using Directory = Pri.LongPath.Directory;
using DirectoryInfo = Pri.LongPath.DirectoryInfo;
using File = Pri.LongPath.File;
using Path = Pri.LongPath.Path;
using Ookii.Dialogs.WinForms;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.ListView;
using System.Diagnostics;

namespace DecryptPluralSightVideosGUI
{
    public partial class frmMain : Form
    {
        private List<CourseItem> listCourse;
        private readonly char[] InvalidFileCharacters = Path.GetInvalidFileNameChars();
        private SQLiteConnection DatabaseConnection;
        private AppSetting appSetting;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public frmMain()
        {
            InitializeComponent();

            #region Apply setting
            if (File.Exists("setting.json"))
            {
                appSetting = JsonConvert.DeserializeObject<AppSetting>(File.ReadAllText("setting.json"));

                txtCoursePath.Text = Directory.Exists(appSetting.CoursePath) ? appSetting.CoursePath : "";
                txtDBPath.Text = Directory.Exists(appSetting.CoursePath) ? appSetting.CoursePath : "";
                txtOutputPath.Text = Directory.Exists(appSetting.OutputPath) ? appSetting.OutputPath : "";

                chkDecrypt.Checked = appSetting.Decrypt;
                chkCreateSub.Checked = appSetting.CreateSub;
                chkDelete.Checked = appSetting.DeleteAfterDecrypt;
                chkShowErrOnly.Checked = appSetting.ShowErrorOnly;
                chkCopyImage.Checked = appSetting.CopyImage;
                chkStartClipIndexAt1.Checked = appSetting.ClipIndexAtOne;
                chkStartModuleIndexAt1.Checked = appSetting.ModuleIndexAtOne;
            }
            else
            {
                appSetting = new AppSetting();
            }
            
            #endregion

            imgList.ImageSize = new Size(170, 100);
            imgList.ColorDepth = ColorDepth.Depth32Bit;
            lsvCourse.LargeImageList = imgList;

            bgwDecrypt.DoWork += BgwDecrypt_DoWork;
            bgwDecrypt.ProgressChanged += BgwDecrypt_ProgressChanged;
            bgwDecrypt.RunWorkerCompleted += BgwDecrypt_RunWorkerCompleted;

            bgwGetCourse.DoWork += BgwGetCourse_DoWork;
            bgwGetCourse.ProgressChanged += BgwGetCourse_ProgressChanged;
            bgwGetCourse.RunWorkerCompleted += BgwGetCourse_RunWorkerCompleted;

            tslToolVersion.Text = $"Tool version: {System.Windows.Forms.Application.ProductVersion}";
            if (File.Exists(txtDBPath.Text.Replace("pluralsight.db", "pluralsight.exe")))
            {
                FileVersionInfo fvInfo = FileVersionInfo.GetVersionInfo(txtDBPath.Text.Replace("pluralsight.db", "pluralsight.exe"));
                tslPOPVersion.Text = $"Pluralsight Offline Player Version: {fvInfo.FileVersion}";
            }
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
                Log log = (Log)(e.UserState);
                AddText(log.Text, log.TextColor, log.NewLine, true);
            }
            else
            {
                dynamic obj = e.UserState;
                AddListView(obj.Item as ListViewItem, obj.Image as Image);
            }
        }

        private void BgwGetCourse_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            ReadCourse(txtCoursePath.Text, txtDBPath.Text);

            if (bgwGetCourse.CancellationPending)
            {
                // Set the e.Cancel flag so that the WorkerCompleted event
                // knows that the process was cancelled.
                e.Cancel = true;
                bgwGetCourse.ReportProgress(0);
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

            Log obj = e.UserState as Log;
            AddText(obj.Text, obj.TextColor, obj.NewLine, obj.IsError);
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
            try
            {
                VistaFolderBrowserDialog folderBrowserDialog = new VistaFolderBrowserDialog { SelectedPath = txtCoursePath.Text };
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    txtCoursePath.Text = folderBrowserDialog.SelectedPath;
                    appSetting.CoursePath  = folderBrowserDialog.SelectedPath;
                }
            }
            catch (PathTooLongException)
            {
                VistaFolderBrowserDialog folderBrowserDialog = new VistaFolderBrowserDialog();
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    txtCoursePath.Text = folderBrowserDialog.SelectedPath;
                    appSetting.CoursePath  = folderBrowserDialog.SelectedPath;
                }
            }
        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            try
            {
                VistaFolderBrowserDialog folderBrowserDialog = new VistaFolderBrowserDialog { SelectedPath = txtOutputPath.Text };
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK) txtOutputPath.Text = folderBrowserDialog.SelectedPath;
            } catch(PathTooLongException)
            {
                VistaFolderBrowserDialog folderBrowserDialog = new VistaFolderBrowserDialog();
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK) txtOutputPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnDBPath_Click(object sender, EventArgs e)
        {
            try
            {
                VistaOpenFileDialog openFileDialog = new VistaOpenFileDialog
                {
                    Multiselect = false,
                    Filter = "Database file (*.db) | *.db",
                    FileName = txtDBPath.Text
                };
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtDBPath.Text = openFileDialog.FileName;
                    appSetting.DatabasePath = openFileDialog.FileName;
                }
            }
            catch (PathTooLongException)
            {
                VistaOpenFileDialog openFileDialog = new VistaOpenFileDialog
                {
                    Multiselect = false,
                    Filter = "Database file (*.db) | *.db",
                };
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtDBPath.Text = openFileDialog.FileName;
                    appSetting.DatabasePath = openFileDialog.FileName;
                }
            }
        }

        private void btnReadCourse_Click(object sender, EventArgs e)
        {
            try
            {
                pnlMain.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                lsvCourse.Items.Clear();
                imgList.Images.Clear();
                rtbLog.Clear();

                bgwGetCourse.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            try
            {
                pnlMain.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;

                List<ListViewItem> lstCourse = lsvCourse.CheckedItems.Cast<ListViewItem>().ToList();
                bgwDecrypt.RunWorkerAsync(lstCourse);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lsvCourse_ItemActivate(object sender, EventArgs e)
        {
            SelectedListViewItemCollection list = ((ListView)sender).SelectedItems;
            if (list.Count == 0) return;
            list[0].Checked = !list[0].Checked;
        }

        private void btnOpenDB_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("explorer.exe", System.IO.Path.GetDirectoryName(txtDBPath.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOpenOutput_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("explorer.exe", txtOutputPath.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            lsvCourse.Items.CheckUncheck(true);
        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            lsvCourse.Items.CheckUncheck(false);
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)       // Ctrl-S Save
            {
                new frmAbout().ShowDialog();
                e.SuppressKeyPress = true;  // Stops other controls on the form receiving event.
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            appSetting = new AppSetting()
            {
                CoursePath = txtCoursePath.Text,
                DatabasePath = txtDBPath.Text,
                OutputPath = txtOutputPath.Text,
                Decrypt = chkDecrypt.Checked,
                CreateSub = chkCreateSub.Checked,
                DeleteAfterDecrypt = chkDelete.Checked,
                ShowErrorOnly = chkShowErrOnly.Checked,
                CopyImage = chkCopyImage.Checked,
                ClipIndexAtOne = chkStartClipIndexAt1.Checked,
                ModuleIndexAtOne = chkStartModuleIndexAt1.Checked
            };

            File.WriteAllText("setting.json", JsonConvert.SerializeObject(appSetting));
        }
        #endregion

        #region Method
        public void ReadCourse(string coursePath, string dbPath)
        {
            try
            {
                if (Directory.Exists(coursePath) && this.InitDb(dbPath))
                {
                    bgwGetCourse.ReportProgress(1, new Log() { Text = "Getting course data . . .", TextColor = Color.Green, NewLine = true });

                    List<string> folderList = Directory.GetDirectories(coursePath, "*", SearchOption.TopDirectoryOnly).ToList();
                    logger.Debug($"FolderList1: {folderList.Count}");
                    folderList = folderList.Where(r => Directory.GetDirectories(r, "*", SearchOption.TopDirectoryOnly).Length > 0).ToList();
                    logger.Debug($"FolderList2: {folderList.Count}");
                    listCourse = folderList.Select(r => new CourseItem() { CoursePath = r, Course = this.GetCourseFromDb(r) }).Where(r => r.Course != null).OrderBy(r => r.Course.Title).ToList();
                    logger.Debug($"listCourse1: {listCourse.Count}");
                    listCourse = listCourse.Where(c => c.Course.IsDownloaded).ToList();
                    logger.Debug($"listCourse2: {listCourse.Count}");
                    foreach (CourseItem item in listCourse)
                    {
                        Image img = File.Exists(item.CoursePath + @"\image.jpg") ? Image.FromFile(item.CoursePath + @"\image.jpg") : new Bitmap(100, 100);

                        ListViewItem listItem = new ListViewItem() { ImageKey = item.Course.Name, Name = item.Course.Name, Text = item.Course.Title };

                        bgwGetCourse.ReportProgress(1, new { Item = listItem, Image = img });
                    }

                    bgwGetCourse.ReportProgress(1, new Log() { Text = $"Complete! Total: {listCourse.Count} courses", TextColor = Color.Green, NewLine = true });

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
                MessageBox.Show("Please select course path", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                CourseItem courseItem = listCourse.Where(r => r.Course.Name == item.Name).Select(r => r).FirstOrDefault();

                if (chkDecrypt.Checked)
                {
                    bgwDecrypt.ReportProgress(1, new Log { Text = $"Start to decrypt course \"{courseItem.Course.Title}\"", TextColor = Color.Magenta, NewLine = true, IsError = true });

                    //Create new course path with the output path
                    var newCoursePath = Path.Combine(txtOutputPath.Text, this.CleanName(courseItem.Course.Title));

                    DirectoryInfo courseInfo = Directory.Exists(newCoursePath)
                        ? new DirectoryInfo(newCoursePath)
                        : Directory.CreateDirectory(newCoursePath);

                    if (chkCopyImage.Checked && File.Exists($"{courseItem.CoursePath}\\image.jpg"))
                    {
                        File.Copy($"{courseItem.CoursePath}\\image.jpg", $"{newCoursePath}\\image.jpg",true);
                    }


                    //Get list all modules in current course
                    List<Module> listModules = courseItem.Course.Modules;

                    if (listModules.Count > 0)
                    {
                        // integer to add 1 if index should start at 1
                        int startAt1 = Convert.ToInt16(chkStartModuleIndexAt1.Checked);
                        //Get each module
                        foreach (Module module in listModules)
                        {
                            //Generate module hash name
                            string moduleHash = this.ModuleHash(module.Name, module.AuthorHandle);
                            //Generate module path
                            string moduleHashPath = Path.Combine(courseItem.CoursePath, moduleHash);
                            //Create new module path with decryption name
                            string newModulePath = Path.Combine(courseInfo.FullName, $"{(startAt1 + module.Index):00}. {module.Title}");

                            if (Directory.Exists(moduleHashPath))
                            {
                                DirectoryInfo moduleInfo = Directory.Exists(newModulePath)
                                    ? new DirectoryInfo(newModulePath)
                                    : Directory.CreateDirectory(newModulePath);
                                //Decrypt all videos in current module folder
                                this.DecryptAllVideos(moduleHashPath, module, moduleInfo.FullName, courseItem.Course.HasTranscript);
                            }
                            else
                            {
                                bgwDecrypt.ReportProgress(1, new Log { Text = $"Folder {moduleHash} not found in the current course path", TextColor = Color.Red, NewLine = true, IsError = true });
                            }
                        }
                    }
                    bgwDecrypt.ReportProgress(1, new Log { Text = $"Decrypt \"{courseItem.Course.Title}\" complete!", TextColor = Color.Magenta, NewLine = true, IsError = true });
                }

                if (chkDelete.Checked)
                {
                    try
                    {
                        Directory.Delete(courseItem.CoursePath, true);
                    }
                    catch (Exception ex)
                    {
                        bgwDecrypt.ReportProgress(1, new Log { Text = $"Delete folder course {courseItem.Course.Title} fail\n{ex.Message}", TextColor = Color.Gray, NewLine = true, IsError = true });
                    }

                    try
                    {
                        RemoveCourseInDb(courseItem.CoursePath);
                    }
                    catch (Exception ex)
                    {
                        bgwDecrypt.ReportProgress(1, new Log { Text = $"Delete course {courseItem.Course.Title} from db fail\n{ex.Message}", TextColor = Color.Gray, NewLine = true, IsError = true });
                    }


                    bgwDecrypt.ReportProgress(1, new Log { Text = $"Delete course {courseItem.Course.Title} success!", TextColor = Color.Magenta, NewLine = true });
                }
            }

            bgwDecrypt.ReportProgress(100);
        }

        public void DecryptAllVideos(string folderPath, Module module, string outputPath, bool hasTranscript)
        {

            // Get all clips of this module from database
            List<Clip> listClips = module.Clips;

            if (listClips.Count > 0)
            {
                // integer to add 1 if index should start at 1
                int startAt1 = Convert.ToInt16(chkStartClipIndexAt1.Checked);
                foreach (Clip clip in listClips)
                {
                    // Get current path of the encrypted video
                    string currentPath = Path.Combine(folderPath, $"{clip.Name}.psv");
                    if (File.Exists(currentPath))
                    {
                        // Create new path with output folder
                        string newPath = Path.Combine(outputPath, $"{(startAt1 + clip.Index):00}. {clip.Title}.mp4");

                        // Init video and get it from iStream
                        var playingFileStream = new VirtualFileStream(currentPath);
                        playingFileStream.Clone(out IStream iStream);

                        string fileName = Path.GetFileName(currentPath);

                        bgwDecrypt.ReportProgress(1, new Log { Text = $"Start to Decrypt File \"{fileName}\"", TextColor = Color.Yellow, NewLine = false });

                        this.DecryptVideo(iStream, newPath);
                        if (chkCreateSub.Checked && hasTranscript)
                        {
                            // Generate transcript file if user ask
                            this.WriteTranscriptFile(clip, newPath);
                        }

                        bgwDecrypt.ReportProgress(1, new Log { Text = $"Decrypt File \"{Path.GetFileName(newPath)}\" success!", TextColor = Color.Green, NewLine = true });
                        playingFileStream.Dispose();

                    }
                    else
                    {
                        bgwDecrypt.ReportProgress(1, new Log { Text = $"File \"{Path.GetFileName(currentPath)}\"  cannot be found", TextColor = Color.Gray, NewLine = true, IsError = true });

                    }
                }
            }
        }

        public void DecryptVideo(IStream currentStream, string newPath)
        {
            try
            {
                currentStream.Stat(out STATSTG stat, 0);
                IntPtr myPtr = (IntPtr)0;
                int streamSize = (int)stat.cbSize;
                byte[] streamInfo = new byte[streamSize];
                currentStream.Read(streamInfo, streamSize, myPtr);
                File.WriteAllBytes(newPath, streamInfo);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Write transcript for the clip if it available.
        /// </summary>
        /// <param name="clipId">Clip Id</param>
        /// <param name="clipPath">Path of current clip</param>
        public void WriteTranscriptFile(Clip clipId, string clipPath)
        {
            // Get all transcript to list
            List<ClipTranscript> clipTranscripts = clipId.Subtitle;

            if (clipTranscripts.Count > 0)
            {
                // Create transcript path with the same name of the clip
                string transcriptPath = Path.Combine(Path.GetDirectoryName(clipPath),
                    Path.GetFileNameWithoutExtension(clipPath) + ".srt");
                if (File.Exists(transcriptPath))
                {
                    File.Delete(transcriptPath);
                }

                using (FileStream transcriptStream = File.OpenWrite(transcriptPath))
                {
                    using (StreamWriter writer = new StreamWriter(transcriptStream))
                    {
                        // Write it to file with stream writer
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
                    }
                }
                bgwDecrypt.ReportProgress(1, new Log { Text = $"Transcript of {Path.GetFileName(clipPath)} generated.", TextColor = Color.Purple, NewLine = false });
            }
        }

        #endregion

        #region DB
        /// <summary>
        /// Get transcript text of specified clip from database.
        /// </summary>
        /// <param name="clipId">Clip Id</param>
        /// <returns>List of transcript text of the current clip.</returns>
        public List<ClipTranscript> GetTranscriptFromDb(int clipId)
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
        /// <returns>List of information about clips belong to specified module.</returns>
        public List<Clip> GetClipsFromDb(int moduleId, string moduleName, string courseName)
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
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Title = CleanName(reader.GetString(reader.GetOrdinal("Title"))),
                    Index = reader.GetInt32(reader.GetOrdinal("ClipIndex")),
                    Subtitle = GetTranscriptFromDb(reader.GetInt32(reader.GetOrdinal("Id")))
                };

                clip.IsDownloaded = File.Exists($@"{appSetting.CoursePath}\{courseName}\{moduleName}\{clip.Name}.psv");
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
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    AuthorHandle = reader.GetString(reader.GetOrdinal("AuthorHandle")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Title = CleanName(reader.GetString(reader.GetOrdinal("Title"))),
                    Index = reader.GetInt32(reader.GetOrdinal("ModuleIndex"))
                };

                module.Clips = GetClipsFromDb(module.Id, this.ModuleHash(module.Name, module.AuthorHandle), courseName);
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
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Title = CleanName(reader.GetString(reader.GetOrdinal("Title"))),
                    HasTranscript = Convert.ToBoolean(reader.GetInt32(reader.GetOrdinal("HasTranscript")))
                };

                course.Modules = GetModulesFromDb(course.Name);
            }

            reader.Close();

            return course;
        }

        /// <summary>
        /// Init database connection.
        /// </summary>
        /// <param name="dbPath">Database file path</param>
        /// <returns>Boolean value determine the database is open successful or not</returns>
        public bool InitDb(string dbPath)
        {
            if (File.Exists(dbPath))
            {
                if (Path.GetExtension(dbPath).Equals(".db"))
                {
                    DatabaseConnection = new SQLiteConnection($"Data Source={dbPath}; Version=3;FailIfMissing=True");
                    DatabaseConnection.Open();
                    bgwGetCourse.ReportProgress(1, new Log() { Text = "Database connection opened.", TextColor = Color.Green, NewLine = true });
                    return true;
                }
                bgwGetCourse.ReportProgress(1, new Log() { Text = "File is not a database file.", TextColor = Color.Red });
                return false;
            }
            bgwGetCourse.ReportProgress(1, new Log() { Text = "Invalid file path.", TextColor = Color.Red });
            return false;
        }

        public bool RemoveCourseInDb(string coursePath)
        {
            string courseName = GetFolderName(coursePath);

            var cmd = DatabaseConnection.CreateCommand();
            cmd.CommandText = @"pragma foreign_keys=on;DELETE FROM Course WHERE Name = @courseName;pragma foreign_keys=off;";
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

            return path.Trim();
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
                    return folderPath.Substring(folderPath.LastIndexOf(@"\", StringComparison.Ordinal) + 1);
                }
                throw new DirectoryNotFoundException();
            }
            return folderPath.Substring(folderPath.LastIndexOf(@"\", StringComparison.Ordinal) + 1);
        }

        public void AddText(string text, Color color, bool newLine, bool IsError)
        {
            if (chkShowErrOnly.Checked && !IsError) return;
            
            rtbLog.SelectionColor = color;
            rtbLog.AppendText($"{text}\n");
            if (newLine) rtbLog.AppendText("\n");

            rtbLog.SelectionStart = rtbLog.Text.Length;
            rtbLog.ScrollToCaret();
        }

        public void AddListView(ListViewItem item, Image img)
        {
            imgList.Images.Add(item.ImageKey, img);
            lsvCourse.Items.Add(item);

            lsvCourse.SuspendLayout();
            lsvCourse.Refresh();
            lsvCourse.ResumeLayout();
        }

        #endregion
    }

    #region class
    public static class ExtensionMethod {
        public static void CheckUncheck(this ListViewItemCollection lstItem, bool selected)
        {
            foreach (ListViewItem item in lstItem)
            {
                item.Selected = selected;
                item.Checked = selected;
            }
        }
    }

    class CourseItem
    {
        public Course Course { get; set; }
        public string CoursePath { get; set; }
    }

    class Log
    {
        public string Text { get; set; }
        public Color TextColor { get; set; }
        public bool NewLine { get; set; }
        public bool IsError { get; set; }
    }

    class AppSetting
    {
        public string CoursePath { get; set; }
        public string DatabasePath { get; set; }
        public string OutputPath { get; set; }
        public bool Decrypt { get; set; }
        public bool CreateSub { get; set; }
        public bool DeleteAfterDecrypt { get; set; }
        public bool ClipIndexAtOne { get; set; }
        public bool ModuleIndexAtOne { get; set; }
        public bool ShowErrorOnly { get; set; }
        public bool CopyImage { get; set; }

        public AppSetting()
        {
            CoursePath = string.Empty;
            DatabasePath = string.Empty;
            OutputPath = string.Empty;
            Decrypt = true;
            CreateSub = true;
            DeleteAfterDecrypt = true;
            ClipIndexAtOne = false;
            ModuleIndexAtOne = false;
            ShowErrorOnly = false;
            CopyImage = false;
        }
    } 
    #endregion
}
