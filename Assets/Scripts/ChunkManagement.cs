using UnityEngine;
using System.Collections.Generic;

public class ChunkManagement : MonoBehaviour {
	public float renderDistance;
	public Transform centerReference;
	[SerializeField] private GameObject chunkObject;


	public static Vector2 center;
	int chunksLoaded;

	Dictionary<Vector2, Chunk> terrainChunkDictionary = new Dictionary<Vector2, Chunk>();
	List<Chunk> terrainChunksVisibleLastUpdate = new List<Chunk>();

	void Start() { chunksLoaded = Mathf.RoundToInt(renderDistance / MapGenerator.mapChunkSize); }

	void Update()
	{
		center = new Vector2 (centerReference.position.x, centerReference.position.z);
		UpdateRenderedOnly();
	}
		
	void UpdateRenderedOnly()
	{
		for (int i = 0; i < terrainChunksVisibleLastUpdate.Count; i++) { terrainChunksVisibleLastUpdate [i].Loader (false); }

		terrainChunksVisibleLastUpdate.Clear ();
			
		int currentChunkCoordX = Mathf.RoundToInt (center.x / MapGenerator.mapChunkSize);
		int currentChunkCoordY = Mathf.RoundToInt (center.y / MapGenerator.mapChunkSize);

		for (int yOffset = -chunksLoaded; yOffset <= chunksLoaded; yOffset++) {
			for (int xOffset = -chunksLoaded; xOffset <= chunksLoaded; xOffset++) {
				Vector2 viewedChunkCoord = new Vector2 (currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);

				if (terrainChunkDictionary.ContainsKey (viewedChunkCoord)) {
					terrainChunkDictionary [viewedChunkCoord].UpdateChunkState (renderDistance);
					if (terrainChunkDictionary [viewedChunkCoord].IsRendering ()) {
						terrainChunksVisibleLastUpdate.Add (terrainChunkDictionary [viewedChunkCoord]);
					}
				} else {
					terrainChunkDictionary.Add (viewedChunkCoord, new Chunk (viewedChunkCoord, MapGenerator.mapChunkSize, transform, chunkObject));
				}

			}
		}
	}

	public class Chunk
	{
		public GameObject chunkObject;
		public Vector2 position;
		public static Bounds bounds;

		public Chunk(Vector2 coord, int size, Transform parent, GameObject chunk)
		{

			Terrain mesh;

			position = coord * size;
			bounds = new Bounds(position, Vector2.one * size);
			Vector3 positionV3 = new Vector3(position.x, 0, position.y);

			chunkObject = Instantiate(chunk, Vector3.zero, Quaternion.identity);

			if (chunkObject.GetComponent<Terrain>() == null)
			{
				mesh = chunkObject.AddComponent<Terrain>();
			}
			else
			{
				mesh = chunkObject.GetComponent<Terrain>();
			}

			Data.Chunks.AddData(this);
			Data.Terrains.AddData(mesh);
			Data.Terrains.AddData(new TerrainData());

			MapGenerator.GenerateMesh(position, new TerrainData());

			chunkObject.transform.position = positionV3;
			chunkObject.transform.parent = parent;
			ChunkLoader.instance.Loader(false, chunkObject);
		}
	}
}