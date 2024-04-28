using UnityEngine;

public class GravityPanel : MonoBehaviour
{
    [ContextMenu(nameof(InvertGravity))]
    public void InvertGravity()
    {
        MyGameManager.InvertGravity();
    }
}