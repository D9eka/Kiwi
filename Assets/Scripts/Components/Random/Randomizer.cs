using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Randomiser
{
    public static bool Succeed(float chance)
    {
        if (chance == 0) return false;
        return chance >= Random.Range(0f, 1f);
    }
}