using System.Collections.Generic;
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

    public static List<T> GetRandomElements<T>(List<T> list, int count)
    {
        var newList = list.GetRange(0, list.Count);
        var answerList = new List<T>();
        while (answerList.Count < count && newList.Count != 0)
        {
            var element = GetRandomElement(newList);
            answerList.Add(element);
            newList.Remove(element);
        }
        return answerList;
    }
}