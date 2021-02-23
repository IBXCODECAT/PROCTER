using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Obsolete]
public class GenerateTerrain : MonoBehaviour
{
    public static GenerateTerrain instance;

    public Terrain terrain;
    public TerrainData data;

    public float scale;

    public string seed;
    public int octaves;

    private int seedX;
    private int seedZ;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            Generate();
        }
    }

    public void Generate()
    {
        Seed.SetExternalSeed(seed);
        data.SetHeights(0, 0, NoiseMap.GenerateMap(data, scale, octaves)); ;
    }
}
