using System;
using System.Collections;
using System.Collections.Generic;
using Components.Interactables;
using Creatures.Player;
using UnityEngine;

public class InteractiveComponent : MonoBehaviour
{
    private List<InteractableComponent> _interactableComponents = new();

    private void Start()
    {
        var inputReader = GetComponentInParent<PlayerInputReader>();
        inputReader.OnInteract += PlayerInputReader_OnInteract;
    }

    private void PlayerInputReader_OnInteract(object sender, EventArgs e)
    {
        TryInteract();
    }

    private void TryInteract()
    {
        if (_interactableComponents.Count == 0) return;
        _interactableComponents[^1].Interact();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out InteractableComponent interactableComponent))
        {
            _interactableComponents.Add(interactableComponent);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out InteractableComponent interactableComponent))
        {
            _interactableComponents.Remove(interactableComponent);
        }
    }
}