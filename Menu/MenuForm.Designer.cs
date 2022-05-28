namespace Ui
{
    partial class MenuForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuForm));
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonSmallSize = new System.Windows.Forms.RadioButton();
            this.radioButtonMediumSize = new System.Windows.Forms.RadioButton();
            this.radioButtonLargeSize = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxPlayer1Name = new System.Windows.Forms.TextBox();
            this.checkBoxIsPlayingAgainstFriend = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxPlayer2Name = new System.Windows.Forms.TextBox();
            this.buttonDoneConfiguration = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(41, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Board Size: ";
            // 
            // radioButtonSmallSize
            // 
            this.radioButtonSmallSize.AutoSize = true;
            this.radioButtonSmallSize.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonSmallSize.Checked = true;
            this.radioButtonSmallSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.radioButtonSmallSize.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.radioButtonSmallSize.Location = new System.Drawing.Point(59, 109);
            this.radioButtonSmallSize.Name = "radioButtonSmallSize";
            this.radioButtonSmallSize.Size = new System.Drawing.Size(75, 29);
            this.radioButtonSmallSize.TabIndex = 1;
            this.radioButtonSmallSize.TabStop = true;
            this.radioButtonSmallSize.Text = "6 x 6";
            this.radioButtonSmallSize.UseVisualStyleBackColor = false;
            this.radioButtonSmallSize.CheckedChanged += new System.EventHandler(this.radioButtonSmallSize_CheckedChanged);
            // 
            // radioButtonMediumSize
            // 
            this.radioButtonMediumSize.AutoSize = true;
            this.radioButtonMediumSize.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonMediumSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.radioButtonMediumSize.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.radioButtonMediumSize.Location = new System.Drawing.Point(173, 109);
            this.radioButtonMediumSize.Name = "radioButtonMediumSize";
            this.radioButtonMediumSize.Size = new System.Drawing.Size(75, 29);
            this.radioButtonMediumSize.TabIndex = 2;
            this.radioButtonMediumSize.Text = "8 x 8";
            this.radioButtonMediumSize.UseVisualStyleBackColor = false;
            this.radioButtonMediumSize.CheckedChanged += new System.EventHandler(this.radioButtonMediumSize_CheckedChanged);
            // 
            // radioButtonLargeSize
            // 
            this.radioButtonLargeSize.AutoSize = true;
            this.radioButtonLargeSize.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonLargeSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.radioButtonLargeSize.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.radioButtonLargeSize.Location = new System.Drawing.Point(286, 109);
            this.radioButtonLargeSize.Name = "radioButtonLargeSize";
            this.radioButtonLargeSize.Size = new System.Drawing.Size(97, 29);
            this.radioButtonLargeSize.TabIndex = 3;
            this.radioButtonLargeSize.Text = "10 x 10";
            this.radioButtonLargeSize.UseVisualStyleBackColor = false;
            this.radioButtonLargeSize.CheckedChanged += new System.EventHandler(this.radioButtonLargeSize_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(41, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 29);
            this.label2.TabIndex = 4;
            this.label2.Text = "Players:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(63, 214);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "Player 1: ";
            // 
            // textBoxPlayer1Name
            // 
            this.textBoxPlayer1Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.textBoxPlayer1Name.Location = new System.Drawing.Point(200, 214);
            this.textBoxPlayer1Name.Name = "textBoxPlayer1Name";
            this.textBoxPlayer1Name.Size = new System.Drawing.Size(173, 30);
            this.textBoxPlayer1Name.TabIndex = 6;
            this.textBoxPlayer1Name.TextChanged += new System.EventHandler(this.textBoxPlayer1Name_TextChanged);
            // 
            // checkBoxIsPlayingAgainstFriend
            // 
            this.checkBoxIsPlayingAgainstFriend.AutoSize = true;
            this.checkBoxIsPlayingAgainstFriend.BackColor = System.Drawing.Color.Black;
            this.checkBoxIsPlayingAgainstFriend.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.checkBoxIsPlayingAgainstFriend.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.checkBoxIsPlayingAgainstFriend.Location = new System.Drawing.Point(68, 266);
            this.checkBoxIsPlayingAgainstFriend.Name = "checkBoxIsPlayingAgainstFriend";
            this.checkBoxIsPlayingAgainstFriend.Size = new System.Drawing.Size(18, 17);
            this.checkBoxIsPlayingAgainstFriend.TabIndex = 7;
            this.checkBoxIsPlayingAgainstFriend.UseVisualStyleBackColor = false;
            this.checkBoxIsPlayingAgainstFriend.CheckedChanged += new System.EventHandler(this.checkBoxIsPlayingAgainstFriend_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Location = new System.Drawing.Point(92, 262);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 25);
            this.label4.TabIndex = 8;
            this.label4.Text = "Player 2:";
            // 
            // textBoxPlayer2Name
            // 
            this.textBoxPlayer2Name.Enabled = false;
            this.textBoxPlayer2Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.textBoxPlayer2Name.Location = new System.Drawing.Point(200, 259);
            this.textBoxPlayer2Name.Name = "textBoxPlayer2Name";
            this.textBoxPlayer2Name.Size = new System.Drawing.Size(173, 30);
            this.textBoxPlayer2Name.TabIndex = 9;
            this.textBoxPlayer2Name.Text = "CPU";
            this.textBoxPlayer2Name.TextChanged += new System.EventHandler(this.textBoxPlayer2Name_TextChanged);
            // 
            // buttonDoneConfiguration
            // 
            this.buttonDoneConfiguration.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonDoneConfiguration.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonDoneConfiguration.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.buttonDoneConfiguration.Location = new System.Drawing.Point(290, 326);
            this.buttonDoneConfiguration.Name = "buttonDoneConfiguration";
            this.buttonDoneConfiguration.Size = new System.Drawing.Size(148, 42);
            this.buttonDoneConfiguration.TabIndex = 10;
            this.buttonDoneConfiguration.Text = "Done";
            this.buttonDoneConfiguration.UseVisualStyleBackColor = false;
            this.buttonDoneConfiguration.Click += new System.EventHandler(this.buttonDoneConfiguration_Click);
            // 
            // MenuForm
            // 
            this.AcceptButton = this.buttonDoneConfiguration;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.BackgroundImage = global::Ui.Properties.Resources.checkersBackground2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(489, 404);
            this.Controls.Add(this.buttonDoneConfiguration);
            this.Controls.Add(this.textBoxPlayer2Name);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBoxIsPlayingAgainstFriend);
            this.Controls.Add(this.textBoxPlayer1Name);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.radioButtonLargeSize);
            this.Controls.Add(this.radioButtonMediumSize);
            this.Controls.Add(this.radioButtonSmallSize);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MenuForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu";
            this.Load += new System.EventHandler(this.MenuForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButtonMediumSize;
        private System.Windows.Forms.RadioButton radioButtonSmallSize;
        private System.Windows.Forms.RadioButton radioButtonLargeSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxPlayer1Name;
        private System.Windows.Forms.CheckBox checkBoxIsPlayingAgainstFriend;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxPlayer2Name;
        private System.Windows.Forms.Button buttonDoneConfiguration;
    }
}

