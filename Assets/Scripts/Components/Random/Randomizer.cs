using System.Collections;
using System.Collections.Generic;
using Sections;
using UnityEngine;

public static class Randomiser
{
    public static bool Succeed(float chance)
    {
        if (chance == 0) return false;
        return chance >= Random.Range(0f, 1f);
    }

    public static T GetRandomElement<T>(List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }
    public static SectionSO GetRandomNotSecretSection(List<SectionSO> list)
    {
        var section = GetRandomElement(list);
        while (section.ContainsSecret)
        {
            section = GetRandomElement(list);
        }
        return section;
    }
}