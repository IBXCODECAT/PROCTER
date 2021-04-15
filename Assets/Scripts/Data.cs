using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class Data 
{
    #region
    static List<ChunkManagement.Chunk> chunksMaster = new List<ChunkManagement.Chunk>();
    #endregion

    #region
    static List<Terrain> terrainsMaster = new List<Terrain>();
    static List<TerrainData> terrainDataMaster = new List<TerrainData>();
    static List<TerrainCollider> terrainCollidersMaster = new List<TerrainCollider>();
    #endregion


    public struct Chunks
    {
        public static ChunkManagement.Chunk[] chunks;

        public static void AddData(ChunkManagement.Chunk chunk) { chunksMaster.Add(chunk); }
        public static void RemoveData(ChunkManagement.Chunk chunk) { chunksMaster.Add(chunk); }
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
