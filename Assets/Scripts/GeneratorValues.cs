using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorValues : MonoBehaviour
{
	[Header("Noise Map Settings")]
	[SerializeField] [Range(0, 6)] [Tooltip("How acurate the mesh renderer should be in subpixels.")] private static int levelOfDetail;
	[SerializeField] [Range(100, 500)] [Tooltip("Adjusts noise focus.")] private float noiseScale;
	[SerializeField] [Range(1, 10)] [Tooltip("How many noise maps are instanced to create the master noise map.")] private int octaves;
	[SerializeField] [Range(0.01f, 0.11f)] [Tooltip("How much submaps overide the minor noise maps and thier octaves.")] private float persistance;
	[SerializeField] [Range(5, 10)] [Tooltip("How much submaps overide the major key maps and thier octaves.")] private float lacunarity;

	[Header("RNG Setting")]
	public static int seed;

	public static float meshHeightMultiplier;


	[Header("Physical Terrain")]

	[HideInInspector] public static TerrainData data;

	float[,] noiseMap = { };

	public int mapChunkSize;

    private void Awake()
    {
		MapGenerator.noiseScale = noiseScale;
		MapGenerator.octaves = octaves;
		MapGenerator.persistance = persistance;
		MapGenerator.lacunarity = lacunarity;
		MapGenerator.seed = seed;
		MapGenerator.meshHeightMultiplier = meshHeightMultiplier;
		MapGenerator.mapChunkSize = mapChunkSize;
    }
}
