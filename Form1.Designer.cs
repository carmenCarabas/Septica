namespace septica
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
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_UmanVsUman = new System.Windows.Forms.Button();
            this.btn_Instr = new System.Windows.Forms.Button();
            this.btn_Despre = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_start
            // 
            this.btn_start.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_start.Location = new System.Drawing.Point(85, 281);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(179, 88);
            this.btn_start.TabIndex = 0;
            this.btn_start.Text = "Joacă offline";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // btn_UmanVsUman
            // 
            this.btn_UmanVsUman.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_UmanVsUman.Location = new System.Drawing.Point(303, 281);
            this.btn_UmanVsUman.Name = "btn_UmanVsUman";
            this.btn_UmanVsUman.Size = new System.Drawing.Size(179, 88);
            this.btn_UmanVsUman.TabIndex = 1;
            this.btn_UmanVsUman.Text = "Joacă online";
            this.btn_UmanVsUman.UseVisualStyleBackColor = true;
            this.btn_UmanVsUman.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_Instr
            // 
            this.btn_Instr.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Instr.Location = new System.Drawing.Point(520, 281);
            this.btn_Instr.Name = "btn_Instr";
            this.btn_Instr.Size = new System.Drawing.Size(179, 88);
            this.btn_Instr.TabIndex = 2;
            this.btn_Instr.Text = "Instrucțiuni";
            this.btn_Instr.UseVisualStyleBackColor = true;
            this.btn_Instr.Click += new System.EventHandler(this.btn_Instr_Click);
            // 
            // btn_Despre
            // 
            this.btn_Despre.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Despre.Location = new System.Drawing.Point(644, 398);
            this.btn_Despre.Name = "btn_Despre";
            this.btn_Despre.Size = new System.Drawing.Size(120, 30);
            this.btn_Despre.TabIndex = 3;
            this.btn_Despre.Text = "Despre";
            this.btn_Despre.UseVisualStyleBackColor = true;
            this.btn_Despre.Click += new System.EventHandler(this.btn_Despre_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(259, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(270, 68);
            this.label1.TabIndex = 4;
            this.label1.Text = "ȘEPTICĂ";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(21, 398);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 30);
            this.button1.TabIndex = 5;
            this.button1.Text = "Exit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::septica.Properties.Resources.fundalMasaJoc;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Despre);
            this.Controls.Add(this.btn_Instr);
            this.Controls.Add(this.btn_UmanVsUman);
            this.Controls.Add(this.btn_start);
            this.Name = "Form1";
            this.Text = "Septica";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Button btn_UmanVsUman;
        private System.Windows.Forms.Button btn_Instr;
        private System.Windows.Forms.Button btn_Despre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}

