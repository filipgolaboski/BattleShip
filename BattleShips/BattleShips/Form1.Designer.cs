namespace BattleShips
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
            this.opponentSea = new System.Windows.Forms.Panel();
            this.playerSea = new System.Windows.Forms.Panel();
            this.tbStatus = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbShipLength = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rbtnHorizontal = new System.Windows.Forms.RadioButton();
            this.rbtnVertical = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // opponentSea
            // 
            this.opponentSea.Location = new System.Drawing.Point(10, 74);
            this.opponentSea.Name = "opponentSea";
            this.opponentSea.Size = new System.Drawing.Size(413, 413);
            this.opponentSea.TabIndex = 1;
            this.opponentSea.Click += new System.EventHandler(this.opponentSea_Click);
            this.opponentSea.Paint += new System.Windows.Forms.PaintEventHandler(this.opponentSea_Paint);
            this.opponentSea.MouseLeave += new System.EventHandler(this.opponentSea_MouseLeave);
            this.opponentSea.MouseMove += new System.Windows.Forms.MouseEventHandler(this.opponentSea_MouseMove);
            // 
            // playerSea
            // 
            this.playerSea.Location = new System.Drawing.Point(432, 74);
            this.playerSea.Name = "playerSea";
            this.playerSea.Size = new System.Drawing.Size(413, 413);
            this.playerSea.TabIndex = 2;
            this.playerSea.Paint += new System.Windows.Forms.PaintEventHandler(this.playerSea_Paint);
            this.playerSea.MouseClick += new System.Windows.Forms.MouseEventHandler(this.playerSea_MouseClick);
            this.playerSea.MouseLeave += new System.EventHandler(this.playerSea_MouseLeave);
            this.playerSea.MouseMove += new System.Windows.Forms.MouseEventHandler(this.playerSea_MouseMove);
            // 
            // tbStatus
            // 
            this.tbStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbStatus.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tbStatus.Enabled = false;
            this.tbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbStatus.Location = new System.Drawing.Point(10, 508);
            this.tbStatus.Name = "tbStatus";
            this.tbStatus.ReadOnly = true;
            this.tbStatus.Size = new System.Drawing.Size(413, 22);
            this.tbStatus.TabIndex = 4;
            this.tbStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbShipLength);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.rbtnHorizontal);
            this.groupBox1.Controls.Add(this.rbtnVertical);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(432, 493);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(411, 54);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ship properties";
            // 
            // cbShipLength
            // 
            this.cbShipLength.FormattingEnabled = true;
            this.cbShipLength.Items.AddRange(new object[] {
            "2",
            "3",
            "4"});
            this.cbShipLength.Location = new System.Drawing.Point(103, 20);
            this.cbShipLength.Name = "cbShipLength";
            this.cbShipLength.Size = new System.Drawing.Size(50, 21);
            this.cbShipLength.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(217, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Ship orientation:";
            // 
            // rbtnHorizontal
            // 
            this.rbtnHorizontal.AutoSize = true;
            this.rbtnHorizontal.Location = new System.Drawing.Point(286, 31);
            this.rbtnHorizontal.Name = "rbtnHorizontal";
            this.rbtnHorizontal.Size = new System.Drawing.Size(72, 17);
            this.rbtnHorizontal.TabIndex = 3;
            this.rbtnHorizontal.TabStop = true;
            this.rbtnHorizontal.Text = "Horizontal";
            this.rbtnHorizontal.UseVisualStyleBackColor = true;
            // 
            // rbtnVertical
            // 
            this.rbtnVertical.AutoSize = true;
            this.rbtnVertical.Location = new System.Drawing.Point(209, 31);
            this.rbtnVertical.Name = "rbtnVertical";
            this.rbtnVertical.Size = new System.Drawing.Size(60, 17);
            this.rbtnVertical.TabIndex = 2;
            this.rbtnVertical.TabStop = true;
            this.rbtnVertical.Text = "Vertical";
            this.rbtnVertical.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(7, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ship length:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Montserrat Extra Bold", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(78, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(276, 43);
            this.label3.TabIndex = 6;
            this.label3.Text = "Opponent Sea";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Montserrat Extra Bold", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(550, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(177, 43);
            this.label4.TabIndex = 7;
            this.label4.Text = "Your Sea";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 559);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbStatus);
            this.Controls.Add(this.playerSea);
            this.Controls.Add(this.opponentSea);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BATTLESHIPS";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel opponentSea;
        private System.Windows.Forms.Panel playerSea;
        private System.Windows.Forms.TextBox tbStatus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbtnHorizontal;
        private System.Windows.Forms.RadioButton rbtnVertical;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbShipLength;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

