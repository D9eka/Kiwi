using Creatures.Player;
using UnityEngine;

public class LookAtPlayerController : MonoBehaviour
{
    [SerializeField] private bool invertScale;

    private Transform player;

    private void Start()
    {
        player = PlayerController.Instance.transform;
    }

    private void Update()
    {
        float scaleX = invertScale ? -Mathf.Abs(transform.localScale.x) : Mathf.Abs(transform.localScale.x);
        transform.localScale = new Vector2(
            transform.position.x < player.position.x ? scaleX : -scaleX,
            transform.localScale.y);
    }
}
