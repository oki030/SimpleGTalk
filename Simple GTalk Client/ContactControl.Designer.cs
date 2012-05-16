namespace Simple_GTalk_Client
{
    partial class ContactControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContactControl));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.NameLabel = new System.Windows.Forms.Label();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.StatusTextLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Enabled = false;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(5, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(80, 5);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(35, 13);
            this.NameLabel.TabIndex = 1;
            this.NameLabel.Text = "Name";
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.ForeColor = System.Drawing.Color.Gray;
            this.StatusLabel.Location = new System.Drawing.Point(43, 5);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(37, 13);
            this.StatusLabel.TabIndex = 2;
            this.StatusLabel.Text = "Offline";
            // 
            // StatusTextLabel
            // 
            this.StatusTextLabel.AutoSize = true;
            this.StatusTextLabel.ForeColor = System.Drawing.Color.Gray;
            this.StatusTextLabel.Location = new System.Drawing.Point(50, 22);
            this.StatusTextLabel.Name = "StatusTextLabel";
            this.StatusTextLabel.Size = new System.Drawing.Size(0, 13);
            this.StatusTextLabel.TabIndex = 3;
            // 
            // ContactControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.StatusTextLabel);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.pictureBox1);
            this.Name = "ContactControl";
            this.Size = new System.Drawing.Size(207, 43);
            this.DoubleClick += new System.EventHandler(this.ContactControl_DoubleClick);
            this.MouseEnter += new System.EventHandler(this.ContactControl_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.ContactControl_MouseLeave);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label NameLabel;
        public System.Windows.Forms.Label StatusLabel;
        public System.Windows.Forms.Label StatusTextLabel;
        public System.Windows.Forms.PictureBox pictureBox1;
    }
}
