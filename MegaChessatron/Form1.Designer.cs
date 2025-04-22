namespace MegaChessatron
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setupBoardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadTranspositionTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTranspositionTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.turnOnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.turnOffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.currentDepthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decreaseSeachDepthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnBeli = new System.Windows.Forms.Button();
            this.btnCrni = new System.Windows.Forms.Button();
            this.lblFocus = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.lblSeachDeapth = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(157, 33);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(0, 0);
            this.panel1.TabIndex = 0;
            this.panel1.ClientSizeChanged += new System.EventHandler(this.panel1_ClientSizeChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem,
            this.setupBoardToolStripMenuItem,
            this.otherToolStripMenuItem,
            this.aIToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(225, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(93, 24);
            this.newGameToolStripMenuItem.Text = "&New game";
            this.newGameToolStripMenuItem.Click += new System.EventHandler(this.newGameToolStripMenuItem_Click);
            // 
            // setupBoardToolStripMenuItem
            // 
            this.setupBoardToolStripMenuItem.Name = "setupBoardToolStripMenuItem";
            this.setupBoardToolStripMenuItem.Size = new System.Drawing.Size(103, 24);
            this.setupBoardToolStripMenuItem.Text = "&Setup board";
            this.setupBoardToolStripMenuItem.Click += new System.EventHandler(this.setupBoardToolStripMenuItem_Click);
            // 
            // otherToolStripMenuItem
            // 
            this.otherToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iconsToolStripMenuItem,
            this.loadTranspositionTableToolStripMenuItem,
            this.saveTranspositionTableToolStripMenuItem});
            this.otherToolStripMenuItem.Name = "otherToolStripMenuItem";
            this.otherToolStripMenuItem.Size = new System.Drawing.Size(58, 24);
            this.otherToolStripMenuItem.Text = "&Other";
            // 
            // iconsToolStripMenuItem
            // 
            this.iconsToolStripMenuItem.Name = "iconsToolStripMenuItem";
            this.iconsToolStripMenuItem.Size = new System.Drawing.Size(248, 26);
            this.iconsToolStripMenuItem.Text = "Icons";
            this.iconsToolStripMenuItem.Click += new System.EventHandler(this.iconsToolStripMenuItem_Click);
            // 
            // loadTranspositionTableToolStripMenuItem
            // 
            this.loadTranspositionTableToolStripMenuItem.Name = "loadTranspositionTableToolStripMenuItem";
            this.loadTranspositionTableToolStripMenuItem.Size = new System.Drawing.Size(248, 26);
            this.loadTranspositionTableToolStripMenuItem.Text = "Load Transposition Table";
            this.loadTranspositionTableToolStripMenuItem.Click += new System.EventHandler(this.loadTranspositionTableToolStripMenuItem_Click);
            // 
            // saveTranspositionTableToolStripMenuItem
            // 
            this.saveTranspositionTableToolStripMenuItem.Name = "saveTranspositionTableToolStripMenuItem";
            this.saveTranspositionTableToolStripMenuItem.Size = new System.Drawing.Size(248, 26);
            this.saveTranspositionTableToolStripMenuItem.Text = "Save Transposition Table";
            this.saveTranspositionTableToolStripMenuItem.Click += new System.EventHandler(this.saveTranspositionTableToolStripMenuItem_Click);
            // 
            // aIToolStripMenuItem
            // 
            this.aIToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.turnOnToolStripMenuItem,
            this.turnOffToolStripMenuItem,
            this.currentDepthToolStripMenuItem,
            this.decreaseSeachDepthToolStripMenuItem});
            this.aIToolStripMenuItem.Name = "aIToolStripMenuItem";
            this.aIToolStripMenuItem.Size = new System.Drawing.Size(35, 24);
            this.aIToolStripMenuItem.Text = "&AI";
            // 
            // turnOnToolStripMenuItem
            // 
            this.turnOnToolStripMenuItem.Name = "turnOnToolStripMenuItem";
            this.turnOnToolStripMenuItem.Size = new System.Drawing.Size(229, 26);
            this.turnOnToolStripMenuItem.Text = "Turn on";
            this.turnOnToolStripMenuItem.Click += new System.EventHandler(this.turnOnToolStripMenuItem_Click);
            // 
            // turnOffToolStripMenuItem
            // 
            this.turnOffToolStripMenuItem.Name = "turnOffToolStripMenuItem";
            this.turnOffToolStripMenuItem.Size = new System.Drawing.Size(229, 26);
            this.turnOffToolStripMenuItem.Text = "Turn off";
            this.turnOffToolStripMenuItem.Click += new System.EventHandler(this.turnOffToolStripMenuItem_Click_1);
            // 
            // currentDepthToolStripMenuItem
            // 
            this.currentDepthToolStripMenuItem.Name = "currentDepthToolStripMenuItem";
            this.currentDepthToolStripMenuItem.Size = new System.Drawing.Size(229, 26);
            this.currentDepthToolStripMenuItem.Text = "&Increase SeachDepth";
            this.currentDepthToolStripMenuItem.Click += new System.EventHandler(this.currentDepthToolStripMenuItem_Click);
            // 
            // decreaseSeachDepthToolStripMenuItem
            // 
            this.decreaseSeachDepthToolStripMenuItem.Name = "decreaseSeachDepthToolStripMenuItem";
            this.decreaseSeachDepthToolStripMenuItem.Size = new System.Drawing.Size(229, 26);
            this.decreaseSeachDepthToolStripMenuItem.Text = "&Decrease SeachDepth";
            this.decreaseSeachDepthToolStripMenuItem.Click += new System.EventHandler(this.decreaseSeachDepthToolStripMenuItem_Click);
            // 
            // btnBeli
            // 
            this.btnBeli.BackColor = System.Drawing.SystemColors.Control;
            this.btnBeli.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnBeli.FlatAppearance.BorderSize = 10;
            this.btnBeli.Image = global::MegaChessatron.Properties.Resources.whitePawn;
            this.btnBeli.Location = new System.Drawing.Point(16, 33);
            this.btnBeli.Margin = new System.Windows.Forms.Padding(4);
            this.btnBeli.Name = "btnBeli";
            this.btnBeli.Size = new System.Drawing.Size(133, 123);
            this.btnBeli.TabIndex = 3;
            this.btnBeli.TabStop = false;
            this.btnBeli.UseVisualStyleBackColor = false;
            // 
            // btnCrni
            // 
            this.btnCrni.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCrni.FlatAppearance.BorderSize = 10;
            this.btnCrni.Image = global::MegaChessatron.Properties.Resources.blackPawn;
            this.btnCrni.Location = new System.Drawing.Point(16, 164);
            this.btnCrni.Margin = new System.Windows.Forms.Padding(4);
            this.btnCrni.Name = "btnCrni";
            this.btnCrni.Size = new System.Drawing.Size(133, 123);
            this.btnCrni.TabIndex = 2;
            this.btnCrni.TabStop = false;
            this.btnCrni.UseVisualStyleBackColor = true;
            // 
            // lblFocus
            // 
            this.lblFocus.AutoSize = true;
            this.lblFocus.Location = new System.Drawing.Point(159, 202);
            this.lblFocus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFocus.Name = "lblFocus";
            this.lblFocus.Size = new System.Drawing.Size(0, 17);
            this.lblFocus.TabIndex = 4;
            this.lblFocus.Visible = false;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "tabela";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "XML file | *.xml";
            // 
            // lblSeachDeapth
            // 
            this.lblSeachDeapth.AutoSize = true;
            this.lblSeachDeapth.Location = new System.Drawing.Point(16, 295);
            this.lblSeachDeapth.Name = "lblSeachDeapth";
            this.lblSeachDeapth.Size = new System.Drawing.Size(90, 17);
            this.lblSeachDeapth.TabIndex = 5;
            this.lblSeachDeapth.Text = "SeachDepth:";
            this.lblSeachDeapth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(225, 62);
            this.Controls.Add(this.lblSeachDeapth);
            this.Controls.Add(this.lblFocus);
            this.Controls.Add(this.btnBeli);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCrni);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "MegaChessatron";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setupBoardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem otherToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iconsToolStripMenuItem;
        private System.Windows.Forms.Button btnCrni;
        private System.Windows.Forms.Button btnBeli;
        private System.Windows.Forms.Label lblFocus;
        private System.Windows.Forms.ToolStripMenuItem aIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem turnOnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem turnOffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadTranspositionTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveTranspositionTableToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem currentDepthToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem decreaseSeachDepthToolStripMenuItem;
        private System.Windows.Forms.Label lblSeachDeapth;
    }
}

