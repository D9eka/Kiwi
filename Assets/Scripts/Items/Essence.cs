using System;
using System.Collections;
using System.Collections.Generic;
using Creatures.Player;
using UnityEngine;

public class Essence : MonoBehaviour
{
    public void GetEssence()
    {
        GameManager.Instance.GetEssence(1);
        Destroy(gameObject);
    }
}