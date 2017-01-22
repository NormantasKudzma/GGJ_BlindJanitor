using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.LevelGenerator
{
	public enum TileType
	{
		OutCornerTopLeft,
		OutCornerTopRight,
		OutCornerBottomRight,
		OutCornerBottomLeft,
		InCornerTopLeft,
		InCornerTopRight,
		InCornerBottomLeft,
		InCornerBottomRight,
		WallLeft,
		WallTop,
		WallRight,
		WallBottom,
		Empty,
		Void
	}
}
