using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.LevelGenerator
{
	public class FieldTile
	{
		public TileType Type { set; get; }
		public Vector3 Position { set; get; }
		public int IndexX { set; get; }
		public int IndexY { set; get; }
	}
}
