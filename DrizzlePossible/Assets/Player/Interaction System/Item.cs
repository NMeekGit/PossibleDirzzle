using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    public Animator _animator;
    public string _interactionPrompt;
    
    private bool _isOpen = false;

    public string InteractionPrompt => _interactionPrompt;
    public bool IsOpen => _isOpen;

    public void Open() {
        _animator.SetBool("isOpen", true);
        _isOpen = true;
    }

    public bool Interact (Interactor interactor) {
        Debug.Log("Interacting");
        return true;
    }
}
