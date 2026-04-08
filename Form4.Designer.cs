namespace septica
{
    partial class Form4
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.meniuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jocNouToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.homeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnMasaJ1 = new System.Windows.Forms.Button();
            this.btnTrageCarte = new System.Windows.Forms.Button();
            this.flowLayoutPanelJ1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblScor = new System.Windows.Forms.Label();
            this.lblJucator = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRand = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.meniuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // meniuToolStripMenuItem
            // 
            this.meniuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem,
            this.jocNouToolStripMenuItem,
            this.homeToolStripMenuItem});
            this.meniuToolStripMenuItem.Name = "meniuToolStripMenuItem";
            this.meniuToolStripMenuItem.Size = new System.Drawing.Size(64, 24);
            this.meniuToolStripMenuItem.Text = "Meniu";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(142, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // jocNouToolStripMenuItem
            // 
            this.jocNouToolStripMenuItem.Name = "jocNouToolStripMenuItem";
            this.jocNouToolStripMenuItem.Size = new System.Drawing.Size(142, 26);
            this.jocNouToolStripMenuItem.Text = "Joc nou";
            this.jocNouToolStripMenuItem.Click += new System.EventHandler(this.jocNouToolStripMenuItem_Click);
            // 
            // homeToolStripMenuItem
            // 
            this.homeToolStripMenuItem.Name = "homeToolStripMenuItem";
            this.homeToolStripMenuItem.Size = new System.Drawing.Size(142, 26);
            this.homeToolStripMenuItem.Text = "Home";
            this.homeToolStripMenuItem.Click += new System.EventHandler(this.homeToolStripMenuItem_Click);
            // 
            // btnMasaJ1
            // 
            this.btnMasaJ1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMasaJ1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMasaJ1.Location = new System.Drawing.Point(45, 69);
            this.btnMasaJ1.Name = "btnMasaJ1";
            this.btnMasaJ1.Size = new System.Drawing.Size(130, 160);
            this.btnMasaJ1.TabIndex = 6;
            this.btnMasaJ1.UseVisualStyleBackColor = true;
            // 
            // btnTrageCarte
            // 
            this.btnTrageCarte.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTrageCarte.BackgroundImage")));
            this.btnTrageCarte.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTrageCarte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrageCarte.Location = new System.Drawing.Point(623, 69);
            this.btnTrageCarte.Name = "btnTrageCarte";
            this.btnTrageCarte.Size = new System.Drawing.Size(130, 160);
            this.btnTrageCarte.TabIndex = 7;
            this.btnTrageCarte.UseVisualStyleBackColor = true;
            this.btnTrageCarte.Click += new System.EventHandler(this.btnTrageCarte_Click_1);
            // 
            // flowLayoutPanelJ1
            // 
            this.flowLayoutPanelJ1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.flowLayoutPanelJ1.AutoScroll = true;
            this.flowLayoutPanelJ1.Location = new System.Drawing.Point(45, 248);
            this.flowLayoutPanelJ1.Name = "flowLayoutPanelJ1";
            this.flowLayoutPanelJ1.Size = new System.Drawing.Size(708, 190);
            this.flowLayoutPanelJ1.TabIndex = 9;
            this.flowLayoutPanelJ1.WrapContents = false;
            // 
            // lblScor
            // 
            this.lblScor.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblScor.AutoSize = true;
            this.lblScor.BackColor = System.Drawing.Color.Transparent;
            this.lblScor.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScor.ForeColor = System.Drawing.SystemColors.Control;
            this.lblScor.Location = new System.Drawing.Point(312, 104);
            this.lblScor.Name = "lblScor";
            this.lblScor.Size = new System.Drawing.Size(177, 23);
            this.lblScor.TabIndex = 10;
            this.lblScor.Text = "scorJ1: 0 | scorJ2: 0";
            // 
            // lblJucator
            // 
            this.lblJucator.AutoSize = true;
            this.lblJucator.BackColor = System.Drawing.Color.Transparent;
            this.lblJucator.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJucator.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblJucator.Location = new System.Drawing.Point(331, 59);
            this.lblJucator.Name = "lblJucator";
            this.lblJucator.Size = new System.Drawing.Size(0, 22);
            this.lblJucator.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(328, 150);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 16);
            this.label1.TabIndex = 12;
            // 
            // lblRand
            // 
            this.lblRand.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblRand.AutoSize = true;
            this.lblRand.BackColor = System.Drawing.Color.Transparent;
            this.lblRand.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRand.ForeColor = System.Drawing.SystemColors.Control;
            this.lblRand.Location = new System.Drawing.Point(312, 143);
            this.lblRand.Name = "lblRand";
            this.lblRand.Size = new System.Drawing.Size(30, 23);
            this.lblRand.TabIndex = 13;
            this.lblRand.Text = "    ";
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblRand);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblJucator);
            this.Controls.Add(this.lblScor);
            this.Controls.Add(this.flowLayoutPanelJ1);
            this.Controls.Add(this.btnTrageCarte);
            this.Controls.Add(this.btnMasaJ1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form4";
            this.Text = "Septica online";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem meniuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button btnMasaJ1;
        private System.Windows.Forms.Button btnTrageCarte;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelJ1;
        private System.Windows.Forms.Label lblScor;
        private System.Windows.Forms.Label lblJucator;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblRand;
        private System.Windows.Forms.ToolStripMenuItem jocNouToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem homeToolStripMenuItem;
    }
}