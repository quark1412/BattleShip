using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShip
{
	public partial class Screen : Form
	{
		List<Bullet> bullets = new List<Bullet>();
		List<Meteor> meteors = new List<Meteor>();

		private Bitmap explosion;
		private int explosionX;
		private int explosionY;
		private Bitmap player;
		private Bitmap bullet;
		private Bitmap meteor;
		private int playerX;
		private int playerY;
		private Bitmap backBuffer;
		private Timer explosionTimer;
		private Timer playerTimer;
		private Timer spawnBulletTimer;
		private Timer spawnMeteorTimer;
		public Graphics graphics;
		private int index;
		private int curFrameColumn;
		private int curFrameRow;
		private bool isExplosionActive = false;
		public Graphics g;
		private int spawnInterval = 10;
		private bool gameOver = false;
		private int spawnTime = 2050;

		private int score = 0;

		public Screen()
		{
			InitializeComponent();
			graphics = this.CreateGraphics();
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			backBuffer = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
			playerX = this.ClientRectangle.Width / 2 - 50;
			playerY = this.ClientRectangle.Height - 100;
			ExplosionConstructor();
			PlayerConstructor();
			BulletConstructor();
			MeteorConstructor();
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{

		}
		
		private void PlayerConstructor()
		{
			player = ResizeBitmap(new Bitmap("ship.png"), 250, 250);
			playerTimer = new Timer();
			playerTimer.Enabled = true;
			playerTimer.Interval = 60;
			playerTimer.Tick += new EventHandler(playerTimer_Tick);
		}

		private void ExplosionConstructor()
		{
			explosion = ResizeBitmap(new Bitmap("explosion.png"), 250, 250);
			index = 0;
			explosionTimer = new Timer();
			explosionTimer.Enabled = true;
			explosionTimer.Interval = 60;
			explosionTimer.Tick += new EventHandler(explosionTimer_Tick);
		}

		private void BulletConstructor()
		{
			bullet = ResizeBitmap(new Bitmap("bullet.png"), 250, 250);
			spawnBulletTimer = new Timer();
			spawnBulletTimer.Enabled = true;
			spawnBulletTimer.Interval = 60;
			spawnBulletTimer.Tick += new EventHandler(spawnBulletTimer_Tick);
		}

		private void MeteorConstructor()
		{
			meteor = ResizeBitmap(new Bitmap("meteor.png"), 250, 250);
			spawnMeteorTimer = new Timer();
			spawnMeteorTimer.Enabled = true;
			spawnMeteorTimer.Interval = 60;
			spawnMeteorTimer.Tick += new EventHandler(spawnMeteorTimer_Tick);
		}

		private void PlayerRender()
		{
			player = ResizeBitmap(new Bitmap("ship.png"), 100, 100);
		}

		private void BulletRender()
		{
			bullet = ResizeBitmap(new Bitmap("bullet.png"), 50, 50);
		}

		private void MeteorRender()
		{
			meteor = ResizeBitmap(new Bitmap("meteor.png"), 50, 50);
		}

		private void ExplosionRender()
		{
			g = Graphics.FromImage(backBuffer);
			g.Clear(Color.White);
			lb_score.Text = "Điểm: " + score;
			if (!gameOver)
			{
				g.DrawImage(player, playerX, playerY);
			}
			foreach (var item in bullets)
			{
				g.DrawImage(bullet, item.X, item.Y);
			}
			foreach (var obstacle in meteors)
			{
				g.DrawImage(meteor, obstacle.X, obstacle.Y);
			}
			if (isExplosionActive)
			{
				curFrameColumn = index % 5;
				curFrameRow = index / 5;
				g.DrawImage(explosion, explosionX, explosionY, new Rectangle(curFrameColumn * 50, curFrameRow * 64, 55, 55), GraphicsUnit.Pixel);

				index++;
				if (index > 20)
				{
					index = 0;
					isExplosionActive = false;
					if (gameOver)
					{
						GameOver();
					}
				}
			}
			g.Dispose();
			graphics.DrawImageUnscaled(backBuffer, 0, 0);
		}

		private void GameOver()
		{
			playerTimer.Stop();
			spawnBulletTimer.Stop();
			spawnMeteorTimer.Stop();
			explosionTimer.Stop();
			GameOver gO = new GameOver(score);
			gO.ShowDialog();
			this.Close();
		}

		private void explosionTimer_Tick(object sender, EventArgs e)
		{
			ExplosionRender();
			CheckBulletMeteorIntersection();
			CheckPlayerMeteorIntersection();
		}

		private void playerTimer_Tick(object sender, EventArgs e)
		{
			PlayerRender();
		}

		private void spawnBulletTimer_Tick(object sender, EventArgs e)
		{
			BulletRender();
			foreach (var item in bullets)
			{
				item.Y -= 20;
			}
		}

		private void spawnMeteorTimer_Tick(object sender, EventArgs e)
		{
			MeteorRender();
			if (spawnInterval % spawnTime != 0)
			{
				spawnInterval += 60;
			}
			else
			{
				meteors.Add(new Meteor(ClientRectangle.Width));
				spawnInterval = 10;
				spawnTime = spawnTime <= 1750 ? 1750 : spawnTime - 60;
			}
			for (int i = meteors.Count - 1; i >= 0; i--)
			{
				meteors[i].X = (meteors[i].X + (float)Math.Cos(meteors[i].Direct.X) * meteors[i].Speed) % ClientRectangle.Width;
				meteors[i].Y = (meteors[i].Y - (float)Math.Sin(meteors[i].Direct.X) * meteors[i].Speed) % ClientRectangle.Height;
				if (meteors[i].X >= ClientRectangle.Width - 50 || meteors[i].X <= 0 || meteors[i].Y >= ClientRectangle.Height - 50)
				{
					meteors.Remove(meteors[i]);
				}
			}
		}

		public Bitmap ResizeBitmap(Bitmap bmp, int width, int height)
		{
			Bitmap result = new Bitmap(width, height);
			using (Graphics g = Graphics.FromImage(result))
			{
				g.DrawImage(bmp, 0, 0, width, height);
			}
			return result;
		}

		private void checkWall()
		{
			if (playerX <= 0) playerX = 0;
			if (playerX >= ClientRectangle.Width - 100) playerX = ClientRectangle.Width - 100;
			if (playerY <= 0) playerY = 0;
			if (playerY >= ClientRectangle.Height - 100) playerY = ClientRectangle.Height - 100;
		}

		private void Form1_MouseClick(object sender, MouseEventArgs e)
		{
			Bullet item = new Bullet(playerX + 26, playerY);
			bullets.Add(item);
		}

		private void CheckBulletMeteorIntersection()
		{
			for (int i = bullets.Count - 1; i >= 0; i--)
			{
				Rectangle bulletRect = new Rectangle(bullets[i].X, bullets[i].Y, bullet.Width, bullet.Height);

				for (int j = meteors.Count - 1; j >= 0; j--)
				{
					Rectangle meteorRect = new Rectangle((int)meteors[j].X, (int)meteors[j].Y, meteor.Width, meteor.Height);

					if (bulletRect.IntersectsWith(meteorRect))
					{
						if (!isExplosionActive)
						{
							isExplosionActive = true;
							ExplosionStart(bullets[i].X, bullets[i].Y);
							score++;
						}
						bullets.RemoveAt(i);
						meteors.RemoveAt(j);
					}
				}
			}
		}

		private void CheckPlayerMeteorIntersection()
		{
			Rectangle playerRect = new Rectangle(playerX, playerY, player.Width, player.Height);

			for (int j = meteors.Count - 1; j >= 0; j--)
			{
				Rectangle meteorRect = new Rectangle((int)meteors[j].X, (int)meteors[j].Y, meteor.Width, meteor.Height);

				if (playerRect.IntersectsWith(meteorRect))
				{
					if (!isExplosionActive)
					{
						isExplosionActive = true;
						ExplosionStart(playerX, playerY);
					}
					gameOver = true;
					meteors.RemoveAt(j);
				}
			}
		}

		private void ExplosionStart(int x, int y)
		{
			explosionX = x;
			explosionY = y;
			explosionTimer.Start();
		}

		private void Form1_MouseMove(object sender, MouseEventArgs e)
		{
			playerX = e.X - 50;
			playerY = e.Y - 50;
			checkWall();
		}
	}
}
