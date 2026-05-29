namespace ProjectDarkness
{
    partial class MainForm
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
            TerkepPictureBox = new PictureBox();
            SzovegTextBox = new TextBox();
            HelyszinPictureBox = new PictureBox();
            MenuListBox = new ListBox();
            QuitButton = new Button();
            ResetButton = new Button();
            VerzioLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)TerkepPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)HelyszinPictureBox).BeginInit();
            SuspendLayout();
            // 
            // TerkepPictureBox
            // 
            TerkepPictureBox.Location = new Point(73, 9);
            TerkepPictureBox.Name = "TerkepPictureBox";
            TerkepPictureBox.Size = new Size(350, 350);
            TerkepPictureBox.TabIndex = 0;
            TerkepPictureBox.TabStop = false;
            // 
            // SzovegTextBox
            // 
            SzovegTextBox.Location = new Point(509, 365);
            SzovegTextBox.Multiline = true;
            SzovegTextBox.Name = "SzovegTextBox";
            SzovegTextBox.ScrollBars = ScrollBars.Vertical;
            SzovegTextBox.Size = new Size(739, 376);
            SzovegTextBox.TabIndex = 1;
            // 
            // HelyszinPictureBox
            // 
            HelyszinPictureBox.Location = new Point(509, 9);
            HelyszinPictureBox.Name = "HelyszinPictureBox";
            HelyszinPictureBox.Size = new Size(450, 350);
            HelyszinPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            HelyszinPictureBox.TabIndex = 3;
            HelyszinPictureBox.TabStop = false;
            // 
            // MenuListBox
            // 
            MenuListBox.FormattingEnabled = true;
            MenuListBox.ItemHeight = 15;
            MenuListBox.Location = new Point(73, 365);
            MenuListBox.Name = "MenuListBox";
            MenuListBox.Size = new Size(350, 379);
            MenuListBox.TabIndex = 4;
            MenuListBox.KeyDown += MenuListBox_KeyDown;
            // 
            // QuitButton
            // 
            QuitButton.Location = new Point(1108, 9);
            QuitButton.Name = "QuitButton";
            QuitButton.Size = new Size(140, 30);
            QuitButton.TabIndex = 5;
            QuitButton.Text = "Kilépés";
            QuitButton.UseVisualStyleBackColor = true;
            QuitButton.Click += QuitButton_Click;
            // 
            // ResetButton
            // 
            ResetButton.Location = new Point(1108, 86);
            ResetButton.Name = "ResetButton";
            ResetButton.Size = new Size(140, 30);
            ResetButton.TabIndex = 6;
            ResetButton.Text = "Újrakezdés";
            ResetButton.UseVisualStyleBackColor = true;
            ResetButton.Click += ResetButton_Click;
            // 
            // VerzioLabel
            // 
            VerzioLabel.Location = new Point(1108, 329);
            VerzioLabel.Name = "VerzioLabel";
            VerzioLabel.Size = new Size(140, 30);
            VerzioLabel.TabIndex = 7;
            VerzioLabel.Text = "label1";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1260, 757);
            Controls.Add(VerzioLabel);
            Controls.Add(ResetButton);
            Controls.Add(QuitButton);
            Controls.Add(MenuListBox);
            Controls.Add(HelyszinPictureBox);
            Controls.Add(SzovegTextBox);
            Controls.Add(TerkepPictureBox);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Project Darkness";
            ((System.ComponentModel.ISupportInitialize)TerkepPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)HelyszinPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox TerkepPictureBox;
        private TextBox SzovegTextBox;
        private PictureBox HelyszinPictureBox;
        private ListBox MenuListBox;
        private Button QuitButton;
        private Button ResetButton;
        private Label VerzioLabel;
    }
}
