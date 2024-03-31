using System.Collections;
using System.Collections.Generic;
using Creatures.Player;
using UnityEngine;

public class GravityPanel : MonoBehaviour
{
    [ContextMenu(nameof(InvertGravity))]
    public void InvertGravity()
    {
        GameManager.Instance.InvertGravity();
    }
}