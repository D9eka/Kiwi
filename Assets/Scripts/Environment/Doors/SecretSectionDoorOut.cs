using System;
using System.Collections;
using System.Collections.Generic;
using Sections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SecretSectionDoorOut : Door
{

    protected override void Enter()
    {
        SectionManager.Instance.ExitSecretSection();
    }
}