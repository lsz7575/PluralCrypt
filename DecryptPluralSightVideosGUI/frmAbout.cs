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
            strb.AppendLine("- Select course path, database path, and output path.");
            strb.AppendLine("- Press Read course button to read course downloaded (only the downloaded course completed is displayed)");
            strb.AppendLine("- Choose course want to decrypt or press select all to select all courses.");
            strb.AppendLine("- Make sure the Decrypt checkbox is checked. If not, and Delete checkbox is checked. The tool will delete all selected course.");
            strb.AppendLine("- Check to Create sub if you want to create the subtitle file.");
            strb.AppendLine("- Check to the Delete checkbox if you want to delete the course after decrypt.");
            strb.AppendLine("- Press run to start to process decrypt and wait.");

            textBox1.Text = strb.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
