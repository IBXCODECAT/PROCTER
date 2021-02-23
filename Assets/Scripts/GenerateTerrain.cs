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

    private int heightMapWidth;
    private int heightMapHeight;

    public int seed;

    private int seedX;
    private int seedZ;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            Random.InitState(seed);

            seedX = Random.Range(-32767314, 32767314);
            seedZ = Random.Range(-32767314, 32767314);

            Debug.Log("X: " + seedX + " Z: " + seedZ);
            
        }
    }
}
