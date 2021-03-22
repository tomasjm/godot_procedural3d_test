using Godot;
using System;


public static class Noise {
    public static float[,] NormalizeNoiseMap(float[,] noiseMap) {
        for (int x = 0; x < noiseMap.GetLength(0); x++)
        {
            for (int y = 0; y < noiseMap.GetLength(1); y++)
            {
                noiseMap[x,y] = NormalizeNoiseValue(noiseMap[x,y]);
            }
        }
        return noiseMap;
    }
    public static float NormalizeNoiseValue(float noiseValue) {
        return ((noiseValue-(-1))/(1-(-1)));
    }
    public static float GenerateNoiseValue(OpenSimplexNoise noise, float x, float y, float scale) {
        return NormalizeNoiseValue(noise.GetNoise2d(x/scale, y/scale));
    }
    public static float[,] GenerateNoiseMap(OpenSimplexNoise noise,int chunkSize, float scale) {
        float[,] noiseMap = new float[chunkSize, chunkSize];
        for (int y = 0; y < chunkSize; y++)
        {
            for (int x = 0; x < chunkSize; x++)
            {
                float sampleX = x / scale;
                float sampleY = y / scale;
                float simplexValue = noise.GetNoise2d(sampleX, sampleY);
                noiseMap[x,y] = simplexValue;
            }
        }
        return NormalizeNoiseMap(noiseMap);
    }

}