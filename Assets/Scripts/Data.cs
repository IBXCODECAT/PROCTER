using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class Data 
{
    private static List<TerrainData> terrainDataMaster = new List<TerrainData>();

    public static TerrainData TerrainDataNew(int size, short heightMapRes, short baseMapRes, short detailRes)
    {
        TerrainData terrainData = new TerrainData();
        terrainData.heightmapResolution = size;
        terrainData.size = new Vector3(2000, 600, 2000);

        terrainData.heightmapResolution = 512;
        terrainData.baseMapResolution = 1024;
        terrainData.SetDetailResolution(1024, terrainData.detailResolutionPerPatch);

        AssetDatabase.CreateAsset(terrainData, "Assets/Data/New Terrain{0}.asset");

        return terrainData;
    }

    public static void TerrainDataAdd(TerrainData data) { terrainDataMaster.Add(data); }
    public static void TerrainDataRemove(TerrainData data) { terrainDataMaster.Remove(data); }
    public static List<TerrainData> TerrainData(TerrainData data) { return terrainDataMaster; }
}
