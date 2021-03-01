using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

	
	public enum DrawMode {NoiseMap, ColourMap, Mesh, Physical};
	public DrawMode drawMode;
	
	[Header("Noise Map Settings")]
	[SerializeField] [Range(0,6)] [Tooltip("How acurate the mesh renderer should be in subpixels.")] private int levelOfDetail;
	[SerializeField] [Range(100, 500)][Tooltip("Adjusts noise focus.")] private float noiseScale;
	[SerializeField] [Range(1, 10)] [Tooltip("How many noise maps are instanced to create the master noise map.")] private int octaves;
	[SerializeField] [Range(0.01f, 0.11f)] [Tooltip("How much submaps overide the minor noise maps and thier octaves.")] private float persistance;
	[SerializeField] [Range(5, 10)] [Tooltip("How much submaps overide the major key maps and thier octaves.")]private float lacunarity;

	[Header("RNG Setting")]
	public int seed;

	[Header("Generation Settings")]
	public Vector2 offset;

	public float meshHeightMultiplier;
	public AnimationCurve meshHeightCurve;

	public bool autoUpdate;

	public TerrainType[] regions;

	[Header("Physical Terrain")]
	public TerrainData data;

	float[,] noiseMap = { };

	int mapChunkSize = 10;

    private void Awake()
    {
		mapChunkSize = data.heightmapResolution;
	}

    public void GenerateMap() {

		noiseMap = Noise.GenerateNoiseMap (mapChunkSize, mapChunkSize, seed, noiseScale, octaves, persistance, lacunarity, offset);

		data.SetHeights(0, 0, noiseMap);

		Color[] colourMap = new Color[mapChunkSize * mapChunkSize];
		for (int y = 0; y < mapChunkSize; y++) {
			for (int x = 0; x < mapChunkSize; x++) {
				float currentHeight = noiseMap [x, y];
				for (int i = 0; i < regions.Length; i++) {
					if (currentHeight <= regions [i].height) {
						colourMap [y * mapChunkSize + x] = regions [i].colour;
						break;
					}
				}
			}
		}

		MapDisplay display = FindObjectOfType<MapDisplay> ();
		if (drawMode == DrawMode.NoiseMap) {
			display.DrawTexture (TextureGenerator.TextureFromHeightMap (noiseMap));
		} else if (drawMode == DrawMode.ColourMap) {
			display.DrawTexture (TextureGenerator.TextureFromColourMap (colourMap, mapChunkSize, mapChunkSize));
		} else if (drawMode == DrawMode.Mesh) {
			display.DrawMesh (MeshGenerator.GenerateTerrainMesh (noiseMap, meshHeightMultiplier, meshHeightCurve, levelOfDetail), TextureGenerator.TextureFromColourMap (colourMap, mapChunkSize, mapChunkSize));
		} else if (drawMode == DrawMode.Physical) {

        }
	}

	public void GeneratePhysical()
	{
		data.SetHeights(0, 0, noiseMap);
	}

	void OnValidate() {
		if (lacunarity < 1) {
			lacunarity = 1;
		}
		if (octaves < 0) {
			octaves = 0;
		}
	}
}

[System.Serializable]
public struct TerrainType {
	public string name;
	public float height;
	public Color colour;
}