using UnityEngine;

public class KeyCard : MonoBehaviour
{
    private void Start()
    {
        if (MyGameManager.WasKeyCardGenerated) Destroy(gameObject);
    }

    public void GetKeyCard()
    {
        MyGameManager.GetKeyCard();
        Destroy(gameObject);
    }
}