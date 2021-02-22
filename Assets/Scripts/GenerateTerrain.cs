using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrain : MonoBehaviour
{
    public Terrain terrain;
    public TerrainData data;

    public float scale;

    private int heightMapWidth;
    private int heightMapHeight;

    [System.Obsolete]
    void Start()
    {
        Generate();
    }

    [System.Obsolete]
    void Generate()
    {
        heightMapWidth = data.heightmapWidth;
        heightMapHeight = data.heightmapHeight;
        float[,] heights = data.GetHeights(0, 0, heightMapWidth, heightMapHeight);

        for(int z = 0; z < heightMapHeight; z++)
        {
            for(int x = 0; x < heightMapWidth; x++)
            {
                heights[x, z] = CalculateHeight(x, z);
            }
        }

        data.SetHeights(0, 0, heights);
    }

    private float CalculateHeight(int x, int z)
    {
        float xCoord = (float)x / heightMapWidth * scale;
        float zCoord = (float)z / heightMapHeight * scale;

        return Mathf.PerlinNoise(xCoord, zCoord);
    }

}
