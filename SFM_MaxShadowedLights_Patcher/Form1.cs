using System.Diagnostics;

namespace SFM_MaxShadowedLights_Patcher
{
    public partial class Form1 : Form
    {
        private string[] hex_client_0 = { "0100008B4DFC0FB6C3F7D81BC083E0", "3F" }; // 07 >> 3F
        private string[] hex_client_1 = { "E8BF4D31008B45B88B4DB0408945B8", "81F84000" }; // 3B818401 >> 81F84000
        private string[] hex_client_2 = { "420C688CA34E10FFD0F7D81BC083E0", "3F" }; // 07 >> 3F

        private string[] hex_ifm_0 = { "859CFEFFFF4089859CFEFFFF83F8", "40" }; // 08 >> 40
        private string[] hex_ifm_1 = { "8558FFFFFF8B859CFEFFFF5F83F8", "40" }; // 08 >> 40
        private string[] hex_ifm_2 = { "FF8B859CFEFFFF5F83F8407E2C6A", "40" }; // 08 >> 40

        private string sfm_dir = @"C:\Program Files (x86)\Steam\steamapps\common\SourceFilmmaker\";


        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // if SFM directory not found, set the button to select folder
            if (!Directory.Exists(sfm_dir) || !Directory.Exists(sfm_dir + "game\\"))
            {
                btn_SFM.Visible = true;
                btn_SFM.Text = "Select the steam \"SourceFilmmaker\" app folder";

                return;
            }

            // if SFM dll's backup files exist, change button to "Uninstall SFM patch"
            if (File.Exists(sfm_dir + @"game\tf\bin\client.dll.bak") && File.Exists(sfm_dir + @"game\bin\tools\ifm.dll.bak"))
            {
                btn_SFM.Text = "Uninstall SFM patch";
            }
        }

        private bool check_if_SFM_is_runnning()
        {
            Process[] processlist = Process.GetProcesses();
            foreach (Process theprocess in processlist)
            {
                if (theprocess.ProcessName == "sfm")
                {
                    return true;
                }
            }

            return false;
        }

        // popupt dialog to select SMF directory
        private void select_SFM_dir()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    if (!fbd.SelectedPath.Contains("SourceFilmmaker"))
                    {
                        MessageBox.Show("Invalid folder, select the 'SourceFilmmaker' folder that should contain a \"game\" folder.");
                        return;
                    }
                    sfm_dir = (fbd.SelectedPath + "\\").Replace("\\\\", "\\");

                    if (!check_SFM_dll_files())
                    {
                        btn_SFM.Text = "Select the steam \"SourceFilmmaker\" app folder";
                        return;
                    } else {
                        install_SFM_patch();
                    }

                    
                }
            }
        }

        private bool check_SFM_dll_files()
        {
            if (!File.Exists(sfm_dir + @"game\tf\bin\client.dll"))
            {
                //MessageBox.Show("Error: could not find file 'client.dll' in \n\n" + sfm_dir + @"game\tf\bin\");
                return false;
            }

            if (!File.Exists(sfm_dir + @"game\bin\tools\ifm.dll"))
            {
               // MessageBox.Show("Error: could not find file 'ifm.dll' in \n\n" + sfm_dir + @"game\bin\tools\");
                return false;
            }

            return true;
        }

        private void install_SFM_patch()
        {
            // if SFM is running return
            if (check_if_SFM_is_runnning())
            {
                MessageBox.Show("SFM is running, please close SFM before installing the patch.");
                return;
            }

            // check if SFM files exist
            if (!check_SFM_dll_files())
            {
                btn_SFM.Text = "Select the steam \"SourceFilmmaker\" app folder";
                MessageBox.Show("Error: could not find the SFM files to patch in the SFM directory provided,\nplease select the Source Filmmaker folder, it should contain a \"game\" folder.");   
                    return;
            }

            #region backup dll files before patching

            // backup files before patching
            try
            {
                if(File.Exists(sfm_dir + @"game\tf\bin\client.dll"))
                    File.Copy(sfm_dir + @"game\tf\bin\client.dll", sfm_dir + @"game\tf\bin\client.dll.bak", true);
            }
            catch
            {
                MessageBox.Show("Error: could not backup file 'cilent.dll'");
                return;
            }

            try
            {
                if (File.Exists(sfm_dir + @"game\bin\tools\ifm.dll"))
                    File.Copy(sfm_dir + @"game\bin\tools\ifm.dll", sfm_dir + @"game\bin\tools\ifm.dll.bak", true);
            }
            catch
            {
                MessageBox.Show("Error: could not backup file 'ifm.dll'");
                return;
            }

            #endregion

           Functions.BinaryPatcher bp = new Functions.BinaryPatcher();
            // client.dll in "game\tf\bin\"
            bool patch_0 = bp.search_and_replace_HexCode(sfm_dir + @"game\tf\bin\client.dll", hex_client_0);
            bool patch_1 = bp.search_and_replace_HexCode(sfm_dir + @"game\tf\bin\client.dll", hex_client_1);
            bool patch_2 = bp.search_and_replace_HexCode(sfm_dir + @"game\tf\bin\client.dll", hex_client_2);

            // ifm.dll in "game\bin\tools\"
            bool patch_3 = bp.search_and_replace_HexCode(sfm_dir + @"game\bin\tools\ifm.dll", hex_ifm_0);
            bool patch_4 = bp.search_and_replace_HexCode(sfm_dir + @"game\bin\tools\ifm.dll", hex_ifm_1);
            bool patch_5 = bp.search_and_replace_HexCode(sfm_dir + @"game\bin\tools\ifm.dll", hex_ifm_2);

            // if all patches were applied succesfully
            if(patch_0 && patch_1 && patch_2 && patch_3 && patch_4 && patch_5)
            {
                MessageBox.Show("Patch succeeded, you can now start SFM.");
                System.Media.SystemSounds.Asterisk.Play();

                btn_SFM.Text = "Uninstall SFM patch";
            }    
        }

        public void uninstall_SFM_patch()
        {
            // if SFM is running return
            if (check_if_SFM_is_runnning())
            {
                MessageBox.Show("SFM is running, please close SFM before uninstalling the patch.");
                return;
            }

            if (!File.Exists(sfm_dir + @"game\tf\bin\client.dll.bak") || !File.Exists(sfm_dir + @"game\bin\tools\ifm.dll.bak"))
            {
                MessageBox.Show("Error: could not find backup files \n\nIn order to restore the original SFM files, go on steam and in the game library," +
                                "\nright click on Source Filmmaker >> Properties, go to \"Local Files\" and click \"Verify integrity of software files\",\nSteam will check for corrupted or modified files and will re-download those only.");

                btn_SFM.Text = "Patch SFM";
                return;
            }

            bool restored_files_succeed = true;
            // restore original dll files from .bak files
            try
            {
                if (File.Exists(sfm_dir + @"game\tf\bin\client.dll.bak"))
                {
                    File.Copy(sfm_dir + @"game\tf\bin\client.dll.bak", sfm_dir + @"game\tf\bin\client.dll", true);
                }

            }
            catch
            {
                restored_files_succeed = false;
                MessageBox.Show("Error: could not restore backup file 'cilent.dll.bak'");
                return;
            }

            // restore ifm dll
            try
            {
                if (File.Exists(sfm_dir + @"game\bin\tools\ifm.dll.bak"))
                {
                    File.Copy(sfm_dir + @"game\bin\tools\ifm.dll.bak", sfm_dir + @"game\bin\tools\ifm.dll", true);
                }     
            }
            catch
            {
                restored_files_succeed = false;
                MessageBox.Show("Error: could not restore backup file 'ifm.dll.bak'");
                return;
            }


            try
            {   // try removing the .bak files
                if(restored_files_succeed)
                {
                    File.Delete(sfm_dir + @"game\bin\tools\ifm.dll.bak");
                    File.Delete(sfm_dir + @"game\tf\bin\client.dll.bak");
                }
            }
            catch
            {
                restored_files_succeed = false;
                MessageBox.Show("Error: could not remove backup files('ifm.dll.bak' and 'cilent.dll.bak')");
                return;
            }

            if(restored_files_succeed)
            {
                btn_SFM.Text = "Patch SFM";
                MessageBox.Show("SFM max shadowed lights patch succesfully uninstalled.");
                System.Media.SystemSounds.Asterisk.Play();
            }
        }

        private void btn_SFM_Click(object sender, EventArgs e)
        {
            if (btn_SFM.Text == "Select the steam \"SourceFilmmaker\" app folder")
            {
                select_SFM_dir();
                return;
            }

            if(btn_SFM.Text == "Patch SFM")
            {
                install_SFM_patch();
                return;
            }

            if (btn_SFM.Text == "Uninstall SFM patch")
            {
                uninstall_SFM_patch();
                return;
            }
        }

        private void label_about_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.FileName = "www.github.com/neodoso/SFM_MaxShadowedLights_Patcher";
            p.Start();

            Process.Start("explorer", "www.github.com/neodoso/SFM_MaxShadowedLights_Patcher");
        }
    }
}