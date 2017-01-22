using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.LevelGenerator
{
	public class LevelMap : MonoBehaviour
	{
		private List<PropsPattern> m_PropsPatterns = new List<PropsPattern>();
		private List<RoomPattern> m_RoomPatterns = new List<RoomPattern>();
		private Dictionary<string, TileType> m_TileTypeMap = new Dictionary<string, TileType>();
		private Dictionary<TileType, Transform> m_Prefabs = new Dictionary<TileType, Transform>();
		private static int m_FieldWidth = 100;
		private static int m_FieldHeight = 100;
		private FieldTile[,] m_Field = new FieldTile[m_FieldWidth, m_FieldHeight];
		private float m_CellSize = 2f;

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

		public Transform[] PropPrefabs;

		//public Dictionary<string, Transform> Prefabs { get { return m_Prefabs; } }

		//public Dictionary<string, Transform> Prefabs2;
		//public Transform[] Prefabs3;

		void Start()
		{
			InitField();
			InitPrefabs();
			LoadRoomPatters();
			LoadPropPatterns();
			GenerateMap();
			CreateTiles();
			CreateProps();
		}

		private void InitField()
		{
			float startX = -m_CellSize * (m_FieldWidth / 2.0f);
			float startY = m_CellSize * (m_FieldHeight / 2.0f);
			for (int i = 0; i < m_FieldHeight; ++i)
			{
				for (int j = 0; j < m_FieldWidth; ++j)
				{
					Vector3 position = new Vector3(startX + m_CellSize * j, 0.0f, startY - m_CellSize * i);
					//Debug.Log (position);
					FieldTile tile = new FieldTile();
					tile.Type = TileType.Void;
					tile.Position = position;
					tile.IndexX = j;
					tile.IndexY = i;
					m_Field[j, i] = tile;
				}
			}
		}

		private void InitPrefabs()
		{
			m_TileTypeMap.Add("OutCornerTopLeft", TileType.OutCornerTopLeft);
			m_TileTypeMap.Add("OutCornerTopRight", TileType.OutCornerTopRight);
			m_TileTypeMap.Add("OutCornerBottomRight", TileType.OutCornerBottomRight);
			m_TileTypeMap.Add("OutCornerBottomLeft", TileType.OutCornerBottomLeft);
			m_TileTypeMap.Add("InCornerTopLeft", TileType.InCornerTopLeft);
			m_TileTypeMap.Add("InCornerTopRight", TileType.InCornerTopRight);
			m_TileTypeMap.Add("InCornerBottomRight", TileType.InCornerBottomRight);
			m_TileTypeMap.Add("InCornerBottomLeft", TileType.InCornerBottomLeft);
			m_TileTypeMap.Add("WallLeft", TileType.WallLeft);
			m_TileTypeMap.Add("WallTop", TileType.WallTop);
			m_TileTypeMap.Add("WallRight", TileType.WallRight);
			m_TileTypeMap.Add("WallBottom", TileType.WallBottom);
			m_TileTypeMap.Add("Empty", TileType.Empty);
			m_TileTypeMap.Add("Void", TileType.Void);

			m_Prefabs.Add(TileType.OutCornerTopLeft, OutCornerTopLeftTile);
			m_Prefabs.Add(TileType.OutCornerTopRight, OutCornerTopRightTile);
			m_Prefabs.Add(TileType.OutCornerBottomRight, OutCornerBottomRightTile);
			m_Prefabs.Add(TileType.OutCornerBottomLeft, OutCornerBottomLeftTile);
			m_Prefabs.Add(TileType.InCornerTopLeft, InCornerTopLeftTile);
			m_Prefabs.Add(TileType.InCornerTopRight, InCornerTopRightTile);
			m_Prefabs.Add(TileType.InCornerBottomRight, InCornerBottomRightTile);
			m_Prefabs.Add(TileType.InCornerBottomLeft, InCornerBottomLeftTile);
			m_Prefabs.Add(TileType.WallLeft, WallLeftTile);
			m_Prefabs.Add(TileType.WallTop, WallTopTile);
			m_Prefabs.Add(TileType.WallRight, WallRightTile);
			m_Prefabs.Add(TileType.WallBottom, WallBottomTile);
			m_Prefabs.Add(TileType.Empty, EmptyTile);
		}

		private void CreateTiles()
		{
			for (int i = 0; i < m_FieldHeight; ++i)
			{
				for (int j = 0; j < m_FieldWidth; ++j)
				{
					FieldTile tile = m_Field[i, j];
					if (tile.Type != TileType.Void)
					{
						Instantiate(m_Prefabs[tile.Type], tile.Position, Quaternion.identity, transform);
					}
				}
			}
		}

		private void CreateProps()
		{
			if (PropPrefabs == null || PropPrefabs.Length == 0)
			{
				return;
			}
			// Random random = new System.Random();
			for (int i = 0; i < m_FieldHeight; ++i)
			{
				for (int j = 0; j < m_FieldWidth; ++j)
				{
					FieldTile tile = m_Field[i, j];
					if (tile.Type == TileType.Empty)
					{
						if (UnityEngine.Random.value <= 0.3f)
						{
							float minVal = 0.0f;
							float maxVal = PropPrefabs.Length - 0.5f;
							int randomIndex = (int)Math.Round(UnityEngine.Random.Range(minVal, maxVal));
							Instantiate(PropPrefabs[randomIndex], tile.Position, Quaternion.identity, transform);
							//Debug.Log("Creating prop: " + randomIndex.ToString());
						}
					}
				}
			}
		}

		private void TestLevel()
		{
			float cellSize = 0.7f;
			int roomWidth = 12;
			int roomHeight = 6;
			float startX = -cellSize * (roomWidth / 2.0f);
			float startY = -cellSize * (roomHeight / 2.0f);
			for (int i = 0; i < roomWidth; ++i)
			{
				for (int j = 0; j < roomHeight; ++j)
				{
					Vector3 pos = new Vector3(startX + cellSize * i, startY + cellSize * j, 0.0f);
					if (i == 0)
					{
						if (j == 0)
						{
							Instantiate(OutCornerTopLeftTile, pos, Quaternion.identity, transform);
						}
						else if (j == (roomHeight - 1))
						{
							Instantiate(OutCornerBottomLeftTile, pos, Quaternion.identity, transform);
						}
						else
						{
							Instantiate(WallLeftTile, pos, Quaternion.identity, transform);
						}
					}
					else if (i == (roomWidth - 1))
					{
						if (j == 0)
						{
							Instantiate(OutCornerTopRightTile, pos, Quaternion.identity, transform);
						}
						else if (j == (roomHeight - 1))
						{
							Instantiate(OutCornerBottomRightTile, pos, Quaternion.identity, transform);
						}
						else
						{
							Instantiate(WallRightTile, pos, Quaternion.identity, transform);
						}
					}
					else if (j == 0)
					{
						Instantiate(WallTopTile, pos, Quaternion.identity, transform);
					}
					else if (j == (roomHeight - 1))
					{
						Instantiate(WallBottomTile, pos, Quaternion.identity, transform);
					}
					else
					{
						Instantiate(EmptyTile, pos, Quaternion.identity, transform);
					}
				}
			}
		}

		public void GenerateMap()
		{
			RoomPattern room = m_RoomPatterns[3];
			int roomWidth = room.Width;
			int roomHeight = room.Height;
			int indexDeltaX = (m_FieldWidth / 2) - (roomWidth / 2);
			int indexDeltaY = (m_FieldHeight / 2) - (roomHeight / 2);
			foreach (FloorTile tile in room.Tiles)
			{
				if (m_TileTypeMap.ContainsKey(tile.FloorSprite))
				{
					FieldTile fieldTile = m_Field[indexDeltaX + tile.XIndex, indexDeltaY + tile.YIndex];
					fieldTile.Type = m_TileTypeMap[tile.FloorSprite];
				}
			}
		}

		public void LoadRoomPatters()
		{
			string[] files =
			{
				"SmallRoom",
				"BigRoom",
				"CrossSection",
				"LargeCrossSection",
				"LargeRoom",
				//"CorridorRoom"
			};
			foreach (string patternFile in files)
			{
				RoomPattern pattern = new RoomPattern();
				pattern.Load(patternFile);
				m_RoomPatterns.Add(pattern);
			}
		}

		public void LoadPropPatterns()
		{
			string[] files =
			{
				//"LevelGenerator/TrianglePattern.xml",
				//"LevelGenerator/CirclePattern.xml",
				"SinglePattern",
			};
			foreach (string patternFile in files)
			{
				PropsPattern pattern = new PropsPattern();
				pattern.Load(patternFile);
				m_PropsPatterns.Add(pattern);
			}
		}
	}
}
