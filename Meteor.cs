using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
	public class Meteor
	{
		public struct direction
		{
			float x;

			public float X { get => x; set => x = value; }

			public direction(float x)
			{
				this.x = x;
			}
		}

		private float x;
		private float y;
		private int speed;
		private direction direct;

		public float X { get => x; set => x = value; }
		public float Y { get => y; set => y = value; }
		public int Speed { get => speed; set => speed = value; }
		public direction Direct { get => direct; set => direct = value; }

		public Meteor(int maxWidth)
		{
			X = new Random().Next(0, maxWidth + 1);
			Y = 0;
			Speed = new Random().Next(10, 21);
			Direct = new direction(X > (maxWidth + 1) / 2 ? (float)(Math.PI * new Random().Next(180, 271) / 180.0) : (float)(Math.PI * new Random().Next(270, 361) / 180.0));
		}
	}
}
