using System.Collections.Generic;
using UnityEngine;
using System;

public class Formations
{
    public static IEnumerable<Vector3> Radial(float a, float stepSize)
    {
        int i = 0;
        while (true)
        {
            float t = stepSize * i++;
            float x = a * t * (float)Math.Cos(t);
            float y = a * t * (float)Math.Sin(t);
            Vector3 offsets = new Vector3(Mathf.PerlinNoise(x, y), 0, Mathf.PerlinNoise(x, y));
            yield return new Vector3(x, 0, y) + offsets;
        }
    }
}
