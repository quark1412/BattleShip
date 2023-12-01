using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShip
{
	public partial class GameOver : Form
	{
		public GameOver(int score)
		{
			InitializeComponent();
			lb_score.Text = "Điểm của bạn: " + score;
		}

		private void btn_restart_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
