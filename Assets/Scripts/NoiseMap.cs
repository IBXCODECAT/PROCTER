using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Obsolete]
public static class NoiseMap
{
    public static int heightMapWidth;
    public static int heightMapHeight;

    public static float[,] GenerateMap(TerrainData data, float noiseScale, int octaves, float octaveLimit)
    {
        Random.InitState(Seed.finalSeed);
        heightMapWidth = data.heightmapWidth;
        heightMapHeight = data.heightmapHeight;

        float[,] noise = AverageNoiseFromOctaves(data, octaves, noiseScale, octaveLimit);
        return noise;
    }

    private static float[,] AverageNoiseFromOctaves(TerrainData data, int count, float scale, float octaveLimit)
    {
        float[,] altitudes = data.GetHeights(0, 0, heightMapWidth, heightMapHeight); //Reset height data
        List<float[,]> octaves = new List<float[,]>() ; //Create a list of octaves

        octaves.Clear(); //Clear the octave list so a new list can be generated

        for (int i = 0; i < count; i++) //For each octave
        {
            float offset = Seed.InitStateGenerator(-octaveLimit, octaveLimit);

            for (int z = 0; z < heightMapHeight; z++) //For each Z coord
            {
                for (int x = 0; x < heightMapWidth; x++) //For each X coord
                {
                    float xCoord = ((float)x / heightMapWidth * scale);
                    float zCoord = ((float)z / heightMapHeight * scale);

                    altitudes[x, z] = Mathf.PerlinNoise(xCoord + offset, zCoord + offset);
                }
            }
            octaves.Add(altitudes);
        }

        float[][,] octaveData = octaves.ToArray();
        float[,] averageNoise = new float[heightMapHeight, heightMapWidth];

        for(int i = 0; i < octaves.Count; i++)
        {
            for (int z = 0; z < heightMapHeight; z++)
            {
                for (int x = 0; x < heightMapWidth; x++)
                {
                    averageNoise[x, z] += octaveData[i][x, z]; //Add octaves starting average operation
                }
            }
        }

        for (int z = 0; z < heightMapHeight; z++)
        {
            for (int x = 0; x < heightMapWidth; x++)
            {
                averageNoise[x, z] = averageNoise[x , z] / octaves.Count; //Divide Indexes finishing average operation
            }
        }

        //Debug.Log("Start Point: " + averageNoise[0, 0] + "End Point: " + averageNoise[heightMapHeight - 1, heightMapWidth - 1]);

        //Debug.Log(averageNoise.Length);
        return averageNoise;
    }
}
