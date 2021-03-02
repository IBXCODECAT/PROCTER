using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChunkLoader : MonoBehaviour
{
	public Transform parent;

	public const int renderDistance = 5;

	public float updateSpeed;

	public Transform reference;

	public static Vector2 viewerPosition;

	public static MapGenerator mapGenerator;
	private int chunkDiameter;

	public float elevation = 0f;

	private int chunksGenerated = 0;

	private	static List<GameObject> loadedChunks = new List<GameObject>();
	private static List<GameObject> dumpedChunks = new List<GameObject>();

	private static List<Vector3> chunksPos = new List<Vector3>();

	private void Start() { StartCoroutine(Updater()); }

	
	IEnumerator Updater()
    {
		chunksPos.Clear();
		loadedChunks.Clear();
		dumpedChunks.Clear();

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
		for (int x = 0; x < renderDistance * 2; x++)
		{
			for (int z = 0; z < renderDistance * 2; z++)
			{
				int posX = Mathf.RoundToInt(x * chunkDiameter) + xAprox;
				int posZ = Mathf.RoundToInt(z * chunkDiameter) + zAprox;

				Debug.Log("Want to generate chunk at X = " + posX +  " Z = " + posZ);

				if(!chunksPos.Contains(new Vector3(posX, elevation, posZ)))
                {
					GameObject thisChunk = GenerateChunk(new Vector3(posX, elevation, posZ));
					chunksPos.Add(thisChunk.transform.position);

					Debug.Log("Generated new chunk " + thisChunk);
					chunksGenerated++;
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

		foreach(GameObject chunk in dumpedChunks)
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

		chunkObject.transform.position = pos;

		chunkGenerator.data = chunkData;
		chunkGenerator.GenerateMap();

		chunkGenerator.GenerateMesh();
		chunkDiameter = chunkGenerator.mapChunkSize;

		return chunkObject;
	}

	private void Load(GameObject chunk)
    {
		loadedChunks.Add(chunk);
		dumpedChunks.Remove(chunk);
		chunk.SetActive(true);
    }

	private void Dump(GameObject chunk)
	{
		loadedChunks.Remove(chunk);
		dumpedChunks.Add(chunk);
		chunk.SetActive(false);
	}
	
}