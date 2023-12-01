﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
	public class Bullet
	{
		private int x;
		private int y;

		public int X { get => x; set => x = value; }
		public int Y { get => y; set => y = value; }

		public Bullet(int x, int y)
		{
			X = x;
			Y = y;
		}
	}
}
