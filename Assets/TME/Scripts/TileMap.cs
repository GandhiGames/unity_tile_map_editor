using UnityEngine;
using System.Collections;

namespace TME
{
	public class TileMap : MonoBehaviour
	{
		private static readonly int DEFAULT_MAP_WIDTH = 20;
		private static readonly int DEFAULT_MAP_HEIGHT = 20;

		private Vector2 mapSize = new Vector2 (DEFAULT_MAP_WIDTH, DEFAULT_MAP_HEIGHT);

		public Vector2 MapSize { get { return this.mapSize; } set { mapSize = value; } }

		private Texture2D mapTexture;

		public Texture2D MapTexture { get { return this.mapTexture; } set { mapTexture = value; } }

		private Vector2 tileSize = Vector2.zero;

		public Vector2 TileSize { get { return this.tileSize; } set { tileSize = value; } }

		private object[] spriteRefs;

		public object[] SpriteRefs { get { return this.spriteRefs; } set { spriteRefs = value; } }

		private Vector2 gridSize;

		public Vector2 GridSize { get { return this.gridSize; } set { gridSize = value; } }

		private int pixelsToUnits = 100;

		public int PixelsToUnits { get { return this.pixelsToUnits; } set { pixelsToUnits = value; } }

		private int tileID = 1;

		public int TileID {
			get {
				return this.tileID;
			}
			set {
				if (value != tileID) {
					tileBrushUpdated = true;
					tileID = value;
				}
			}
		}

		private bool tileBrushUpdated;

		public bool TileBrushUpdated {
			get {
				if (tileBrushUpdated) {
					tileBrushUpdated = false;
					return true;
				}
				return false;
			}
		}

		public Sprite CurrentTileBrush { get { return (Sprite)spriteRefs [tileID]; } }

		private GameObject tiles;

		public GameObject Tiles {
			get {
				return this.tiles;
			}
			set {
				tiles = value;
			}
		}

		void OnDrawGizmosSelected ()
		{
			var pos = transform.position;

			if (mapTexture != null) {

				Gizmos.color = Color.gray;
				int row = 0;
				int maxColumns = (int)mapSize.x;
				int total = (int)(mapSize.x * mapSize.y);
				var tile = new Vector3 (tileSize.x / pixelsToUnits, tileSize.y / pixelsToUnits);
				var offset = new Vector2 (tile.x / 2, tile.y / 2);

				for (int i = 0; i < total; i++) {

					int column = i % maxColumns;

					float newX = (column * tile.x) + offset.x + pos.x;
					float newY = -(row * tile.y) - offset.y + pos.y;

					Gizmos.DrawWireCube (new Vector2 (newX, newY), tile);

					if (column == maxColumns - 1) {
						row++;
					}
				}

				Gizmos.color = Color.white;
				var centreX = pos.x + (gridSize.x * 0.5f);
				var centreY = pos.y - (gridSize.y * 0.5f);

				Gizmos.DrawWireCube (new Vector2 (centreX, centreY), gridSize);
			}
		}
	}
}
