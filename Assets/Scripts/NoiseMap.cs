using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Obsolete]
public static class NoiseMap
{
    private static List<float[,]> octaves;

    public static void Generate(TerrainData data, int octaves, float octaveScale, int seed)
    {
        for(int i = 0; i < octaves; i++)
        {
            GenerateOctave(data, octaveScale, seed);
        }
    }

    private static void GenerateOctave(TerrainData data, float scale, int seed)
    {
        Random.seed = seed;

        int width = data.heightmapWidth * Mathf.RoundToInt(Random.Range(-scale, scale));
        int height = data.heightmapHeight * Mathf.RoundToInt(Random.Range(-scale, scale));

        float[,] currentOctave = data.GetHeights(0, 0, width, height);

        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                float xPerlin = (float)x / width * scale;
                float zPerlin = (float)z / height * scale;

                currentOctave[x, z] = Mathf.PerlinNoise(xPerlin, zPerlin);
            }
        }
    }

    public static float GetHeight(int x, int y)
    {

    }
}
