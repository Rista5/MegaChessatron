namespace MegaChessatron
{
    partial class FormIcons
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
            this.cbxWhiteBishop = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.cbxWhiteKing = new System.Windows.Forms.ComboBox();
            this.cbxBlackKing = new System.Windows.Forms.ComboBox();
            this.pbxBlackKing = new System.Windows.Forms.PictureBox();
            this.pbxWhiteKing = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbxBlackKing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxWhiteKing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxWhiteBishop
            // 
            this.cbxWhiteBishop.FormattingEnabled = true;
            this.cbxWhiteBishop.Location = new System.Drawing.Point(12, 41);
            this.cbxWhiteBishop.Name = "cbxWhiteBishop";
            this.cbxWhiteBishop.Size = new System.Drawing.Size(121, 21);
            this.cbxWhiteBishop.TabIndex = 1;
            this.cbxWhiteBishop.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(12, 12);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // cbxWhiteKing
            // 
            this.cbxWhiteKing.FormattingEnabled = true;
            this.cbxWhiteKing.Location = new System.Drawing.Point(140, 40);
            this.cbxWhiteKing.Name = "cbxWhiteKing";
            this.cbxWhiteKing.Size = new System.Drawing.Size(121, 21);
            this.cbxWhiteKing.TabIndex = 3;
            this.cbxWhiteKing.SelectedIndexChanged += new System.EventHandler(this.cbxWhiteKing_SelectedIndexChanged);
            // 
            // cbxBlackKing
            // 
            this.cbxBlackKing.FormattingEnabled = true;
            this.cbxBlackKing.Location = new System.Drawing.Point(268, 40);
            this.cbxBlackKing.Name = "cbxBlackKing";
            this.cbxBlackKing.Size = new System.Drawing.Size(121, 21);
            this.cbxBlackKing.TabIndex = 5;
            this.cbxBlackKing.SelectedIndexChanged += new System.EventHandler(this.cbxBlackKing_SelectedIndexChanged);
            // 
            // pbxBlackKing
            // 
            this.pbxBlackKing.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbxBlackKing.Location = new System.Drawing.Point(268, 68);
            this.pbxBlackKing.Name = "pbxBlackKing";
            this.pbxBlackKing.Size = new System.Drawing.Size(75, 75);
            this.pbxBlackKing.TabIndex = 6;
            this.pbxBlackKing.TabStop = false;
            // 
            // pbxWhiteKing
            // 
            this.pbxWhiteKing.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbxWhiteKing.Location = new System.Drawing.Point(140, 68);
            this.pbxWhiteKing.Name = "pbxWhiteKing";
            this.pbxWhiteKing.Size = new System.Drawing.Size(75, 75);
            this.pbxWhiteKing.TabIndex = 4;
            this.pbxWhiteKing.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(12, 68);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(75, 75);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(94, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // FormIcons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 166);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.pbxBlackKing);
            this.Controls.Add(this.cbxBlackKing);
            this.Controls.Add(this.pbxWhiteKing);
            this.Controls.Add(this.cbxWhiteKing);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cbxWhiteBishop);
            this.Controls.Add(this.pictureBox1);
            this.Name = "FormIcons";
            this.Text = "FormIcons";
            this.Load += new System.EventHandler(this.FormIcons_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbxBlackKing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxWhiteKing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox cbxWhiteBishop;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ComboBox cbxWhiteKing;
        private System.Windows.Forms.PictureBox pbxWhiteKing;
        private System.Windows.Forms.ComboBox cbxBlackKing;
        private System.Windows.Forms.PictureBox pbxBlackKing;
        private System.Windows.Forms.Button btnCancel;
    }
}