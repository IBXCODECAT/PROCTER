using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class Data 
{
    private static List<Chunk> chunksMaster = new List<Chunk>();

    private static List<Terrain> terrainsMaster = new List<Terrain>();
    private static List<TerrainData> terrainDataMaster = new List<TerrainData>();
    private static List<TerrainCollider> terrainCollidersMaster = new List<TerrainCollider>();

    public struct Chunks
    {
        public static Chunk[] chunks;
        public static void AddData(Chunk chunk) { chunksMaster.Add(chunk); }
        public static void RemoveData(Chunk chunk) { chunksMaster.Add(chunk); }
        public static void UpdateData() { chunks = chunksMaster.ToArray(); }
    }

    public struct Terrains
    {
        public static Terrain[] terrain;
        public static TerrainData[] terrainData;
        public static TerrainCollider[] terrainCollider;
        public static void AddData(Terrain data) { terrainsMaster.Add(data); }
        public static void AddData(TerrainData data) { terrainDataMaster.Add(data); }
        public static void AddData(TerrainCollider data) { terrainCollidersMaster.Add(data); }
        public static void RemoveData(Terrain data) { terrainsMaster.Remove(data); }
        public static void RemoveData(TerrainData data) { terrainDataMaster.Remove(data); }
        public static void RemoveData(TerrainCollider data) { terrainCollidersMaster.Remove(data); }
        public static void UpdateData()
        {
            terrain = terrainsMaster.ToArray();
            terrainData = terrainDataMaster.ToArray();
            terrainCollider = terrainCollidersMaster.ToArray();
        }
    }

}
