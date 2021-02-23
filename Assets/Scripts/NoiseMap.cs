using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Obsolete]
public static class NoiseMap
{
    public static int heightMapWidth;
    public static int heightMapHeight;

    private static float[,] heights;
    private static int scale;

    public static float[,] GenerateMap(TerrainData data, int x, int z, int scale, int octaves, int seed)
    {
        Random.InitState(seed);
        heightMapWidth = data.heightmapWidth;
        heightMapHeight = data.heightmapHeight;

        GenerateOctaves(data, octaves);
    }

    private static float[,] AverageNoiseFromOctaves(TerrainData data, int count)
    {
        float[,] altitudes = data.GetHeights(0, 0, heightMapWidth, heightMapHeight); //Reset height data
        List<float[,]> octaves = null; //Create a list of octaves
        octaves.Clear(); //Clear the octave list so a new list can be generated

        for (int i = 0; i < count; i++) //For each octave
        {
            for (int z = 0; z < heightMapHeight; z++) //For each Z coord
            {
                for (int x = 0; x < heightMapWidth; x++) //For each X coord
                {
                    float xCoord = ((float)x / heightMapWidth * scale);
                    float zCoord = ((float)z / heightMapHeight * scale);

                    altitudes[x, z] = Mathf.PerlinNoise(xCoord, zCoord);
                }
            }

            octaves.Add(altitudes);
        }

        float[][,] octaveData = octaves.ToArray();

        float[,] averageNoise = null; //Create 2D float array to be returned

        for(int i = 0; i < count; i++)
        {
            for (int z = 0; z < octaves.Count; z++)
            {
                for (int x = 0; x < octaves.Count; x++)
                {
                    averageNoise[x, z] += octaveData[i][x, z]; //Add octaves starting average operation
                }
            }
        }

        for (int z = 0; z < octaves.Count; z++)
        {
            for (int x = 0; x < octaves.Count; x++)
            {
                averageNoise[x, z] = averageNoise[x / count, z / count]; //Divide Indexes finishing average operation
            }
        }

        return averageNoise;
    }
}
