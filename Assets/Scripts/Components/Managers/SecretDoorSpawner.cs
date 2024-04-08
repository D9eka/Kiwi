using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sections;
using UnityEngine;

public class SecretDoorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _secretDoor;

    private void Start()
    {
        if (SectionManager.Instance._wasSecretSectionFound) return;
        if (Randomiser.Succeed(0.3f)) _secretDoor.SetActive(true);
    }

}