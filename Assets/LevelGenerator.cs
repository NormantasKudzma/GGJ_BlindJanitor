using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.LevelGenerator
{
	public class LevelGenerator : MonoBehaviour
	{
		//private List<PropsPattern> m_PropsPatterns = new List<PropsPattern>();
		//private List<RoomPattern> m_RoomPatterns = new List<RoomPattern>();
		public Transform[] lalala;
		public Transform OutCornerTopLeftTile;
		public Transform OutCornerTopRightTile;
		public Transform OutCornerBottomRightTile;
		public Transform OutCornerBottomLeftTile;
		public Transform InCornerTopLeftTile;
		public Transform InCornerTopRightTile;
		public Transform InCornerBottomRightTile;
		public Transform InCornerBottomLeftTile;
		public Transform WallLeftTile;
		public Transform WallTopTile;
		public Transform WallRightTile;
		public Transform WallBottomTile;
		public Transform EmptyTile;

		void Start()
		{
			//LoadRoomPatters();
			//LoadPropPatterns();
			GenerateMap();
		}

		public void GenerateMap()
		{
			float cellSize = 2.0f;
			int roomWidth = 6;
			int roomHeight = 6;
			float startX = -cellSize * (roomWidth / 2.0f);
			float startY = -cellSize * (roomHeight / 2.0f);
			for (int i = 0; i < roomWidth; ++i)
			{
				for (int j = 0; j < roomHeight; ++j)
				{
					Vector3 pos = new Vector3(startX + cellSize * i, 0.0f, startY + cellSize * j);
					if (i == 0)
					{
						if (j  == (roomHeight - 1))
						{
							Instantiate(OutCornerTopLeftTile, pos, Quaternion.identity);
						}
						else if (j == 0)
						{
							Instantiate(OutCornerBottomLeftTile, pos, Quaternion.identity);
						}
						else
						{
							Instantiate(WallLeftTile, pos, Quaternion.identity);
						}
					}
					else if (i == (roomWidth - 1))
					{
						if (j == (roomHeight - 1))
						{
							Instantiate(OutCornerTopRightTile, pos, Quaternion.identity);
						}
						else if (j == 0)
						{
							Instantiate(OutCornerBottomRightTile, pos, Quaternion.identity);
						}
						else
						{
							Instantiate(WallRightTile, pos, Quaternion.identity);
						}
					}
					else if (j == (roomHeight - 1))
					{
						Instantiate(WallTopTile, pos, Quaternion.identity);
					}
					else if (j == 0)
					{
						Instantiate(WallBottomTile, pos, Quaternion.identity);
					}
					else
					{
						Instantiate(EmptyTile, pos, Quaternion.identity);
					}
				}
			}
		}

//		public void LoadRoomPatters()
//		{
//			string[] files =
//			{
//				"SmallRoom",
//				//"LevelGenerator/BigRoom.xml",
//				//"LevelGenerator/CorridorRoom.xml"
//			};
//			foreach (string patternFile in files)
//			{
//				RoomPattern pattern = new RoomPattern();
//				pattern.Load(patternFile);
//				m_RoomPatterns.Add(pattern);
//			}
//		}
//
//		public void LoadPropPatterns()
//		{
//			string[] files =
//			{
//				//"LevelGenerator/TrianglePattern.xml",
//				//"LevelGenerator/CirclePattern.xml",
//				"SinglePattern",
//			};
//			foreach (string patternFile in files)
//			{
//				PropsPattern pattern = new PropsPattern();
//				pattern.Load(patternFile);
//				m_PropsPatterns.Add(pattern);
//			}
//		}
	}
}