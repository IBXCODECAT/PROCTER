using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChunkLoader : MonoBehaviour {
	public float renderDistance;
	public Transform centerReference;
	[SerializeField] private GameObject chunkObject;

	[SerializeField] private short chunkSize;

	public static Vector2 center;
	int chunksLoaded;

	Dictionary<Vector2, TerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk>();
	List<TerrainChunk> terrainChunksVisibleLastUpdate = new List<TerrainChunk>();

	void Start() { chunksLoaded = Mathf.RoundToInt(renderDistance / chunkSize); }

	void Update()
	{
		center = new Vector2 (centerReference.position.x, centerReference.position.z);
		UpdateRenderedOnly();
	}
		
	void UpdateRenderedOnly()
	{
		for (int i = 0; i < terrainChunksVisibleLastUpdate.Count; i++) { terrainChunksVisibleLastUpdate [i].Loader (false); }

		terrainChunksVisibleLastUpdate.Clear ();
			
		int currentChunkCoordX = Mathf.RoundToInt (center.x / chunkSize);
		int currentChunkCoordY = Mathf.RoundToInt (center.y / chunkSize);

		for (int yOffset = -chunksLoaded; yOffset <= chunksLoaded; yOffset++) {
			for (int xOffset = -chunksLoaded; xOffset <= chunksLoaded; xOffset++) {
				Vector2 viewedChunkCoord = new Vector2 (currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);

				if (terrainChunkDictionary.ContainsKey (viewedChunkCoord)) {
					terrainChunkDictionary [viewedChunkCoord].UpdateChunkState (renderDistance);
					if (terrainChunkDictionary [viewedChunkCoord].IsRendering ()) {
						terrainChunksVisibleLastUpdate.Add (terrainChunkDictionary [viewedChunkCoord]);
					}
				} else {
					terrainChunkDictionary.Add (viewedChunkCoord, new TerrainChunk (viewedChunkCoord, chunkSize, transform, chunkObject));
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
			position = coord * size;
			bounds = new Bounds(position,Vector2.one * size);
			Vector3 positionV3 = new Vector3(position.x,0,position.y);

			meshObject =  Instantiate(chunk, Vector3.zero, Quaternion.identity);
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