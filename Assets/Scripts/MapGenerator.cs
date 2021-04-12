using UnityEngine;
using System.Collections;

public static class MapGenerator
{
	private static int levelOfDetail;
	public static float noiseScale;
	public static int octaves;
	public static float persistance;
	public static float lacunarity;

	public static int seed;
	public static Vector2 offset;
	public static float meshHeightMultiplier;
	
	public static TerrainData data;
	static float[,] noiseMap = { };

	public static int mapChunkSize;

	public static void GenerateMesh(Vector2 offset, TerrainData meshData)
    {
		data = meshData;

		noiseMap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, seed, noiseScale, octaves, persistance, lacunarity, offset);

		data.SetHeights(0, 0, noiseMap);
	}

	static void OnValidate() {
		if (lacunarity < 1) {
			lacunarity = 1;
		}
		if (octaves < 0) {
			octaves = 0;
		}
	}
}