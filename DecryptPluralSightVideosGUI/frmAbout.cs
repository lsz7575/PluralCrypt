using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DecryptPluralSightVideosGUI
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();

            StringBuilder strb = new StringBuilder();
            strb.AppendLine("This program helps you decrypt the video download from PluralSight. It's free software, so please using it for yourself, don't use it for commercial purposes.");
            strb.AppendLine("");
            strb.AppendLine("Author:");
            strb.AppendLine("- Loc Nguyen");
            strb.AppendLine("- Mr.VoGiaCu (Mr.HomeLess :D)");
            strb.AppendLine("- sitiom");
            strb.AppendLine("");
            strb.AppendLine("");
            strb.AppendLine("Option:");
            strb.AppendLine("- Decrypt: decrypt video");
            strb.AppendLine("- Create sub: create the subtitle if has");
            strb.AppendLine("- Delete: delete course after decrypt");
            strb.AppendLine("- Show error only: display error only in console log");
            strb.AppendLine("- Copy image: copy course image to the decrypted folder");
            strb.AppendLine("- Clip index at 1: Meaning the index of clips will start at 1 instead of 0");
            strb.AppendLine("- Module index at 1: Meaning the index of Modules will start at 1 instead of 0");
            strb.AppendLine("");
            strb.AppendLine("");
            strb.AppendLine("Use:");
            strb.AppendLine("- Step 1: Select the course path and DB file. Note: The course path always include \"\\courses\" at the end of the path.");
            strb.AppendLine("- Step 2: After select exactly all paths, press the Read course button.");
            strb.AppendLine("- Step 3: Select the courses want to decrypt or press the Select all button to select all courses.");
            strb.AppendLine("- Step 4: Choose the option you want");
            strb.AppendLine("   + Decrypt: Decrypt the course.");
            strb.AppendLine("   + Create sub: Create the subtitle file.");
            strb.AppendLine("   + Delete: Delete course after decrypt.");
            strb.AppendLine("- Step 5: After choosing the option, press Run and waiting for the decrypt process finish.");
            strb.AppendLine("");
            strb.AppendLine("");
            strb.AppendLine("Notes:");
            strb.AppendLine("");
            strb.AppendLine("- Please don't remove the course from POP before decrypt. You can check the Delete checkbox to remove the course after the course decrypted.");
            strb.AppendLine("- You can delete all courses select all courses, check the Delete checkbox only, and press Run.");
            strb.AppendLine("- Some courses don't have subtitles, so the subtitle file will not be generated.");

            textBox1.Text = strb.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
