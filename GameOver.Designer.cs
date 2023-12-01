namespace BattleShip
{
	partial class GameOver
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
			this.lb_score = new System.Windows.Forms.Label();
			this.btn_restart = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lb_score
			// 
			this.lb_score.AutoSize = true;
			this.lb_score.Font = new System.Drawing.Font("Cambria", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lb_score.Location = new System.Drawing.Point(57, 56);
			this.lb_score.Name = "lb_score";
			this.lb_score.Size = new System.Drawing.Size(218, 33);
			this.lb_score.TabIndex = 0;
			this.lb_score.Text = "Điểm của bạn là: ";
			// 
			// btn_restart
			// 
			this.btn_restart.Font = new System.Drawing.Font("Cambria", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_restart.Location = new System.Drawing.Point(96, 159);
			this.btn_restart.Name = "btn_restart";
			this.btn_restart.Size = new System.Drawing.Size(163, 54);
			this.btn_restart.TabIndex = 2;
			this.btn_restart.Text = "Chơi lại";
			this.btn_restart.UseVisualStyleBackColor = true;
			this.btn_restart.Click += new System.EventHandler(this.btn_restart_Click);
			// 
			// GameOver
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(374, 268);
			this.Controls.Add(this.btn_restart);
			this.Controls.Add(this.lb_score);
			this.Name = "GameOver";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "GameOver";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label lb_score;
		private System.Windows.Forms.Button btn_restart;
	}
}