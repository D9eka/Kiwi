using UnityEngine;

public class Essence : MonoBehaviour
{
    public void GetEssence()
    {
        MyGameManager.GetEssence(1);
        Destroy(gameObject);
    }
}