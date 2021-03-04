using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChunkLoader : MonoBehaviour {
	public float renderDistance;
	public Transform viewer;
	[SerializeField] private GameObject chunk;
	[SerializeField] private int chunkSize;
	public static Vector2 viewerPosition;
	int chunksVisableInRenderDistance;

	Dictionary<Vector2, TerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk>();
	List<TerrainChunk> terrainChunksVisibleLastUpdate = new List<TerrainChunk>();

	void Start() {
		chunksVisableInRenderDistance = Mathf.RoundToInt(renderDistance / chunkSize);
	}

	void Update() {
		viewerPosition = new Vector2 (viewer.position.x, viewer.position.z);
		UpdateVisibleChunks ();
	}
		
	void UpdateVisibleChunks() {

		for (int i = 0; i < terrainChunksVisibleLastUpdate.Count; i++) {
			terrainChunksVisibleLastUpdate [i].SetVisible (false);
		}
		terrainChunksVisibleLastUpdate.Clear ();
			
		int currentChunkCoordX = Mathf.RoundToInt (viewerPosition.x / chunkSize);
		int currentChunkCoordY = Mathf.RoundToInt (viewerPosition.y / chunkSize);

		for (int yOffset = -chunksVisableInRenderDistance; yOffset <= chunksVisableInRenderDistance; yOffset++) {
			for (int xOffset = -chunksVisableInRenderDistance; xOffset <= chunksVisableInRenderDistance; xOffset++) {
				Vector2 viewedChunkCoord = new Vector2 (currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);

				if (terrainChunkDictionary.ContainsKey (viewedChunkCoord)) {
					terrainChunkDictionary [viewedChunkCoord].UpdateTerrainChunk (renderDistance);
					if (terrainChunkDictionary [viewedChunkCoord].IsVisible ()) {
						terrainChunksVisibleLastUpdate.Add (terrainChunkDictionary [viewedChunkCoord]);
					}
				} else {
					terrainChunkDictionary.Add (viewedChunkCoord, new TerrainChunk (viewedChunkCoord, chunkSize, transform, chunk));
				}

			}
		}
	}

	public class TerrainChunk {
		GameObject meshObject;
		Vector2 position;
		Bounds bounds;
		public TerrainChunk(Vector2 coord, int size, Transform parent, GameObject chunk) {
			position = coord * size;
			bounds = new Bounds(position,Vector2.one * size);
			Vector3 positionV3 = new Vector3(position.x,0,position.y);

			//meshObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
			meshObject =  Instantiate(chunk, Vector3.zero, Quaternion.identity);
			meshObject.transform.position = positionV3;
			//meshObject.transform.localScale = Vector3.one * size /10f;
			meshObject.transform.parent = parent;
			SetVisible(false);
		}

		public void UpdateTerrainChunk(float renderDistance) {
			float viewerDstFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance (viewerPosition));
			bool visible = viewerDstFromNearestEdge <= renderDistance;
			SetVisible (visible);
		}

		public void SetVisible(bool visible) {
			meshObject.SetActive (visible);
		}

		public bool IsVisible() {
			return meshObject.activeSelf;
		}

	}
}