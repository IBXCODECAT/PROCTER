using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChunkLoader : MonoBehaviour
{
	public Transform parent;
	public int renderDistance;
	public float updateSpeed;

	public Transform reference;

	public static Vector2 viewerPosition;

	public static MapGenerator mapGenerator;
	private int chunkDiameter;

	public float elevation = 0f;

	private int chunksGenerated = 0;

	private	static List<GameObject> loadedChunks = new List<GameObject>();
	private static List<GameObject> unLoadedChunks = new List<GameObject>();

	private bool[,] ChunkPosFetchable;

	private void Start()
    {
		StartCoroutine(Updater());
    }

	IEnumerator Updater()
    {
		while(true)
        {
			ChunkSystem();
			yield return new WaitForSecondsRealtime(updateSpeed);
		}
    }

	private void ChunkSystem()
    {

		int viewerPositionX = Mathf.RoundToInt(viewerPosition.x);
		int viewerPositionZ = Mathf.RoundToInt(viewerPosition.y);

		GenerateNewChunks(viewerPositionX, viewerPositionZ);

		UpdateChunkStates();
    }

	private void GenerateNewChunks(int xAprox, int zAprox)
	{
		for (int x = -renderDistance + xAprox; x < renderDistance + xAprox; x++)
		{
			for (int z = -renderDistance + zAprox; z < renderDistance + zAprox; z++)
			{
				int posX = Mathf.RoundToInt(x * chunkDiameter);
				int posZ = Mathf.RoundToInt(z * chunkDiameter);

				if (!ChunkPosFetchable[posX, posZ])
                {
					Debug.Log("Generated New Chunk");
					loadedChunks.Add(GenerateChunk(new Vector3(posX, elevation, posZ)));

					ChunkPosFetchable[posX, posZ] = true;
				}
			}
		}
	}

	private void UpdateChunkStates()
    {
		foreach (GameObject chunk in loadedChunks)
        {
			if(Vector3.Distance(viewerPosition, chunk.transform.position) > renderDistance)
            {
				Dump(chunk);
				Debug.Log("Dumped Chunk " + chunk, chunk);
            }
        }

		foreach(GameObject chunk in unLoadedChunks)
        {
			if(Vector3.Distance(viewerPosition, chunk.transform.position) < renderDistance)
            {
				Load(chunk);
				Debug.Log("Loaded Chunk " + chunk, chunk);
            }
        }
    }

	private GameObject GenerateChunk(Vector3 pos)
	{
		TerrainData chunkData = new TerrainData();
		chunkData.name = "Chunk Data (" + chunksGenerated + ")";
		GameObject chunkObject = Terrain.CreateTerrainGameObject(chunkData);
		MapGenerator chunkGenerator = chunkObject.AddComponent<MapGenerator>();

		chunkGenerator.data = chunkData;

		chunkGenerator.GenerateMesh();
		chunkDiameter = chunkGenerator.mapChunkSize;

		return chunkObject;
	}

	private void Load(GameObject chunk)
    {
		loadedChunks.Add(chunk);
		unLoadedChunks.Remove(chunk);
		ChunkPosFetchable[Mathf.RoundToInt(chunk.transform.position.x), Mathf.RoundToInt(chunk.transform.position.z)] = true;
		chunk.SetActive(true);
    }

	private void Dump(GameObject chunk)
	{
		loadedChunks.Remove(chunk);
		unLoadedChunks.Add(chunk);
		chunk.SetActive(false);
	}
	
}