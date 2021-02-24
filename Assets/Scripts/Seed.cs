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
        Debug.Log("Seed String " + seed);
        SplitSeed();
    }
    
    public static void SplitSeed()
    {
        seedContainer.Clear();
        int charIndex = 0;
        finalSeed = 0;

        foreach(char seedComponent in seed)
        {
            seedContainer.Add(seedComponent.GetHashCode());

            //Debug.Log("Sucsessfully converted " + seedComponent + " to " + seedContainer[charIndex] + " at index " + charIndex);

            charIndex++;
        }

        foreach(byte component in seedContainer)
        {
            Debug.Log(component);
            finalSeed += component;
        }

        Debug.Log("Final Seed Generated: " + finalSeed);
    }
}
