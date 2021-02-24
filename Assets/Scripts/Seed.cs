using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Seed
{
    public static int finalSeed; public static string seed;

    public static List<int> seedContainer = new List<int>(); public static int[] generatedComponents;

    public static float InitStateGenerator(float min, float max) { return Random.Range(min, max); }
    public static void SetInternalSeed(string rawSeed) { seed = rawSeed; }
    public static void SetExternalSeed(string rawSeed) 
    {
        SetInternalSeed(rawSeed);
        Debug.Log(seed);
        SplitSeed();
    }
    
    public static void SplitSeed()
    {
        int charIndex = 0;

        foreach(char seedComponent in seed)
        {
            seedContainer.Add(seedComponent);

            Debug.Log("Sucsessfully converted " + seedComponent + " to " + seedContainer[charIndex] + " at index " + charIndex);

            charIndex++;
        }

        generatedComponents = seedContainer.ToArray();

        foreach(byte component in seedContainer) { finalSeed = finalSeed * 10 + component; }

        Debug.Log("Final Seed Generated: " + finalSeed);
    }
}
