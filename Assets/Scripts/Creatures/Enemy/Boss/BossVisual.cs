using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossVisual : MonoBehaviour
{
    [SerializeField] SpawnComponents _attack2Spawner;
    [SerializeField] SpawnComponents _attack5Spawner;

    public void Attack2Spawn()
    {
        _attack2Spawner.Spawn();
    }

    public void Attack5Spawn()
    {
        _attack5Spawner.Spawn();
    }
 }
