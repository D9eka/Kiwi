using System;
using System.Collections;
using System.Collections.Generic;
using Creatures.Player;
using UnityEngine;

public class KeyCard : MonoBehaviour
{
    private void Start()
    {
        if (GameManager.Instance._wasKeyCardGenerated) Destroy(gameObject);
    }

    public void GetKeyCard()
    {
        GameManager.Instance.GetKeyCard(1);
        Destroy(gameObject);
    }
}