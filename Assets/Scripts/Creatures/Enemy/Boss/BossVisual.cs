using UnityEngine;

public class BossVisual : MonoBehaviour
{
    [SerializeField] SpawnComponents _attack2Spawner;
    [SerializeField] SpawnComponents _attack5Spawner;

    public void Attack2Spawn()
    {
        _attack2Spawner.Spawn();
    }

    public void Attack3Invoke()
    {
        MyGameManager.InvertGravity();
    }

    public void Attack5Spawn()
    {
        _attack5Spawner.Spawn();
    }
}
