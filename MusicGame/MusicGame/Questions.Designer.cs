namespace MusicGame
{
    partial class Questions
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnSong1 = new System.Windows.Forms.Button();
            this.btnSong2 = new System.Windows.Forms.Button();
            this.btnSong3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(87, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Guess the year of song?";
            // 
            // btnSong1
            // 
            this.btnSong1.Location = new System.Drawing.Point(36, 77);
            this.btnSong1.Name = "btnSong1";
            this.btnSong1.Size = new System.Drawing.Size(91, 40);
            this.btnSong1.TabIndex = 1;
            this.btnSong1.Text = "button1";
            this.btnSong1.UseVisualStyleBackColor = true;
            // 
            // btnSong2
            // 
            this.btnSong2.Location = new System.Drawing.Point(161, 76);
            this.btnSong2.Name = "btnSong2";
            this.btnSong2.Size = new System.Drawing.Size(88, 41);
            this.btnSong2.TabIndex = 2;
            this.btnSong2.Text = "v";
            this.btnSong2.UseVisualStyleBackColor = true;
            // 
            // btnSong3
            // 
            this.btnSong3.Location = new System.Drawing.Point(287, 76);
            this.btnSong3.Name = "btnSong3";
            this.btnSong3.Size = new System.Drawing.Size(88, 41);
            this.btnSong3.TabIndex = 3;
            this.btnSong3.Text = "v";
            this.btnSong3.UseVisualStyleBackColor = true;
            // 
            // Questions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 129);
            this.Controls.Add(this.btnSong3);
            this.Controls.Add(this.btnSong2);
            this.Controls.Add(this.btnSong1);
            this.Controls.Add(this.label1);
            this.Name = "Questions";
            this.Text = "NewGame";
            this.Load += new System.EventHandler(this.Questions_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSong1;
        private System.Windows.Forms.Button btnSong2;
        private System.Windows.Forms.Button btnSong3;
    }
}