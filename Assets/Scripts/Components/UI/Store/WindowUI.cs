using System;
using System.Collections;
using System.Collections.Generic;
using Creatures.Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class WindowUI : MonoBehaviour
{
    [SerializeField] private GameObject _content;
    [SerializeField] private UnityEvent _spacebarAction;
    [SerializeField] private UnityEvent _enterAction;
    [SerializeField] private bool _isClosable;
    public bool IsClosable => _isClosable;


    public void Show()
    {
        _content.SetActive(true);
        Activate();
    }

    public void Hide()
    {
        _content.SetActive(false);
        Activate(false);
    }

    private void Activate(bool value = true)
    {
        if (value)
        {
            PlayerInputReader.Instance.OnSpacebarPressed += InvokeSpaceAction;
            PlayerInputReader.Instance.OnEnterPressed += InvokeEnterAction;
            // Debug.Log("Activated");
        }
        else
        {
            PlayerInputReader.Instance.OnSpacebarPressed -= InvokeSpaceAction;
            PlayerInputReader.Instance.OnEnterPressed -= InvokeEnterAction;
            // Debug.Log("Deactivated");

        }
    }

    private void InvokeSpaceAction(object sender, EventArgs args)
    {
        _spacebarAction?.Invoke();
    }

    private void InvokeEnterAction(object sender, EventArgs args)
    {
        _enterAction?.Invoke();
    }
}