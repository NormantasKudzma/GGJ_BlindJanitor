using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using UnityEngine;

namespace Assets.LevelGenerator
{
	public class PropsPattern
	{
		private List<PropData> m_Props = new List<PropData>();

		public List<PropData> Props { get { return m_Props; } }

		public void Load(string file)
		{
			// "LevelGenerator/SmallRoom.xml"
			TextAsset asset = Resources.Load<TextAsset>(file);
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(asset.text);
			XmlElement root = doc.DocumentElement;
			foreach (XmlNode node in root.ChildNodes)
			{
				if (node.NodeType != XmlNodeType.Element)
				{
					continue;
				}
				if (node.Name.Equals("Prop"))
				{
					XmlAttribute spriteAttribute = node.Attributes["Sprite"];
					XmlAttribute offsetXAttribute = node.Attributes["OffsetX"];
					XmlAttribute offsetYAttribute = node.Attributes["OffsetY"];

					PropData prop = new PropData();
					if (spriteAttribute != null && !string.IsNullOrEmpty(spriteAttribute.Value))
					{
						prop.SpriteName = spriteAttribute.Value;
					}
					Vector2 offset = new Vector2(0.0f, 0.0f);
					if (offsetXAttribute != null && !string.IsNullOrEmpty(offsetXAttribute.Value))
					{
						offset.x = float.Parse(offsetXAttribute.Value);
					}
					if (offsetYAttribute != null && !string.IsNullOrEmpty(offsetYAttribute.Value))
					{
						offset.y = float.Parse(offsetYAttribute.Value);
					}
					prop.SpriteOffset = offset;

					m_Props.Add(prop);
				}
			}

		}
	}
}
