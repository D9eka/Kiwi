using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotDestroyable : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}