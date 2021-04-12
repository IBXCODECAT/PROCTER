using UnityEngine;
using System.Collections.Generic;

public class ChunkLoader : MonoBehaviour {
	public float renderDistance;
	public Transform centerReference;
	[SerializeField] private GameObject chunkObject;


	public static Vector2 center;
	int chunksLoaded;

	Dictionary<Vector2, TerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk>();
	List<TerrainChunk> terrainChunksVisibleLastUpdate = new List<TerrainChunk>();

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
					terrainChunkDictionary.Add (viewedChunkCoord, new TerrainChunk (viewedChunkCoord, MapGenerator.mapChunkSize, transform, chunkObject));
				}

			}
		}
	}

	public class TerrainChunk
	{
		GameObject meshObject;
		Vector2 position;
		Bounds bounds;

		public TerrainChunk(Vector2 coord, int size, Transform parent, GameObject chunk) {

			Terrain mesh;
			TerrainData meshData;

			position = coord * size;
			bounds = new Bounds(position,Vector2.one * size);
			Vector3 positionV3 = new Vector3(position.x,0,position.y);

			meshObject = Instantiate(chunk, Vector3.zero, Quaternion.identity);

			if (meshObject.GetComponent<Terrain>() == null)
			{
				mesh = meshObject.AddComponent<Terrain>();
			}
			else
            {
				mesh = meshObject.GetComponent<Terrain>();
            }

			meshData = mesh.terrainData;

			MapGenerator.GenerateMesh(coord, meshData);

			meshObject.transform.position = positionV3;
			meshObject.transform.parent = parent;
			Loader(false);
		}

		public void UpdateChunkState(float renderDistance)
		{
			float viewerDstFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance (center));
			bool visible = viewerDstFromNearestEdge <= renderDistance;
			Loader(visible);
		}

		public void Loader(bool visible) { meshObject.SetActive (visible); }
		public bool IsRendering() { return meshObject.activeSelf; }

	}
}