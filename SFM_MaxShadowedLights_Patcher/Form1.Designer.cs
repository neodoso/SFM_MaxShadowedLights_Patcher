namespace SFM_MaxShadowedLights_Patcher
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btn_SFM = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label_about = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_SFM
            // 
            this.btn_SFM.Location = new System.Drawing.Point(75, 63);
            this.btn_SFM.Name = "btn_SFM";
            this.btn_SFM.Size = new System.Drawing.Size(274, 40);
            this.btn_SFM.TabIndex = 0;
            this.btn_SFM.Text = "Patch SFM";
            this.btn_SFM.UseVisualStyleBackColor = true;
            this.btn_SFM.Click += new System.EventHandler(this.btn_SFM_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(421, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "This tool will patch the SFM files in order to allow for up to 64 shadowed lights" +
    ".";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(62, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(306, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Please close SFM before applying or removing the patch.";
            // 
            // label_about
            // 
            this.label_about.AutoSize = true;
            this.label_about.Location = new System.Drawing.Point(398, 108);
            this.label_about.Name = "label_about";
            this.label_about.Size = new System.Drawing.Size(40, 15);
            this.label_about.TabIndex = 2;
            this.label_about.Text = "About";
            this.label_about.Click += new System.EventHandler(this.label_about_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 126);
            this.Controls.Add(this.label_about);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_SFM);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "SFM Max Shadowed Lights Patcher";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btn_SFM;
        private Label label1;
        private Label label2;
        private Label label_about;
    }
}