using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChunkLoader : MonoBehaviour
{
	public Transform parent;
	public float renderDistance;
	public float updateSpeed;

	public Transform reference;

	public static Vector2 viewerPosition;

	public static MapGenerator mapGenerator;
	private int chunkSize;

	public float elevation = 0f;

	private	static List<GameObject> loadedChunks = new List<GameObject>();

	IEnumerator Start()
    {
		ChunkUpdate();
		yield return new WaitForSecondsRealtime(updateSpeed);
    }

	private void ChunkUpdate()
    {
		for(float x = -renderDistance + viewerPosition.x; x < renderDistance + viewerPosition.x; x++)
        {
			for(float z = -renderDistance + viewerPosition.y; z < renderDistance + viewerPosition.y; z++)
            {
				float xPos = x * chunkSize;
				float zPos = z * chunkSize;

				loadedChunks.Add(GenerateChunk(new Vector3(xPos, elevation, zPos)));
            }
        }
    }

	private GameObject GenerateChunk(Vector3 pos)
    {
		Terrain activeChunk = Instantiate(new Terrain(), pos, Quaternion.identity, parent);
		TerrainData chunkData = activeChunk.terrainData;
		GameObject chunkObject = activeChunk.gameObject;
		MapGenerator chunkGenerator = chunkObject.AddComponent<MapGenerator>();

		chunkGenerator.GenerateMesh();
		chunkSize = chunkGenerator.mapChunkSize;

		return chunkObject;
	}

	private void LoadChunk()
    {

    }

	private void UnloadChunk()
    {

    }
	
}