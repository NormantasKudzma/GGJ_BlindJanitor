using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using UnityEngine;

namespace Assets.LevelGenerator
{
	public class RoomPattern
	{
		private int m_Width = 1;
		private int m_Height = 1;
		private List<FloorTile> m_Tiles = new List<FloorTile>();

		public int Width { get { return m_Width; } }
		public int Height { get { return m_Height; } }
		public List<FloorTile> Tiles { get { return m_Tiles; } }

		public void Load(string file)
		{
			// "LevelGenerator/SmallRoom.xml"
			TextAsset asset = Resources.Load<TextAsset>(file);
			Debug.Log("loadinam - " + file);
			Debug.Log(asset != null ? "asset not null" : "asset is null!");
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(asset.text);

			int minXIndex = 0;
			int minYIndex = 0;
			int maxXIndex = 0;
			int maxYIndex = 0;
			XmlElement root = doc.DocumentElement;
			foreach (XmlNode node in root.ChildNodes)
			{
				if (node.NodeType != XmlNodeType.Element)
				{
					continue;
				}
				if (node.Name.Equals("Tile"))
				{
					XmlAttribute floorSpriteAttribute = node.Attributes["FloorSprite"];
					XmlAttribute wallSpriteAttribute = node.Attributes["WallSprite"];
					XmlAttribute xIndexAttribute = node.Attributes["XIndex"];
					XmlAttribute yIndexAttribute = node.Attributes["YIndex"];

					FloorTile tile = new FloorTile();
					if (floorSpriteAttribute != null && !string.IsNullOrEmpty(floorSpriteAttribute.Value))
					{
						tile.FloorSprite = floorSpriteAttribute.Value;
					}
					if (wallSpriteAttribute != null && !string.IsNullOrEmpty(wallSpriteAttribute.Value))
					{
						tile.WallSprite = wallSpriteAttribute.Value;
					}
					if (xIndexAttribute != null && !string.IsNullOrEmpty(xIndexAttribute.Value))
					{
						tile.XIndex = int.Parse(xIndexAttribute.Value);
					}
					if (yIndexAttribute != null && !string.IsNullOrEmpty(yIndexAttribute.Value))
					{
						tile.YIndex = int.Parse(yIndexAttribute.Value);
					}
					m_Tiles.Add(tile);
					if (minXIndex > tile.XIndex)
					{
						minXIndex = tile.XIndex;
					}
					if (minYIndex > tile.YIndex)
					{
						minYIndex = tile.YIndex;
					}
					if (maxXIndex < tile.XIndex)
					{
						maxXIndex = tile.XIndex;
					}
					if (maxYIndex < tile.YIndex)
					{
						maxYIndex = tile.YIndex;
					}
				}
			}
			m_Width = maxXIndex - minXIndex + 1;
			m_Height = maxYIndex - minYIndex + 1;
			Debug.Log("Room size: " + m_Width.ToString() + " x " + m_Height.ToString());
		}
	}
}
