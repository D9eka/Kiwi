using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotDestroyable : MonoBehaviour
{
    // Start is called before the first frame update
    public static NotDestroyable Instance { get; private set; }

    private void Awake()
    {
        if (Instance is not null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }
}