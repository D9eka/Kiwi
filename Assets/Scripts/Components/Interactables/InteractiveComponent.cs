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
        var inputReader = PlayerInputReader.Instance;
        inputReader.OnInteract += PlayerInputReader_OnInteract;
    }

    private void PlayerInputReader_OnInteract(object sender, EventArgs e)
    {
        TryInteract();
    }

    private void TryInteract()
    {
        if (_interactableComponents.Count == 0) return;
        _interactableComponents[^1].TryInteract();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out InteractableComponent interactableComponent))
        {
            if (_interactableComponents.Count > 0) interactableComponent.TryShowHintUI(false);
            _interactableComponents.Add(interactableComponent);
            interactableComponent.TryShowHintUI();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out InteractableComponent interactableComponent))
        {
            _interactableComponents.Remove(interactableComponent);
            interactableComponent.TryShowHintUI(false);
        }
    }
}