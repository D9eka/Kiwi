using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChipIconUI : MonoBehaviour
{
    [SerializeField] private Image _image;

    public void SetInfo(ChipSO chipSO)
    {
        _image.sprite = chipSO.Sprite;
    }
}