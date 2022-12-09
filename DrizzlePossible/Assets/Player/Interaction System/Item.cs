using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    public Animator _animator;
    public GameObject _item;
    public string _interactionPrompt;
    
    private bool _isOpen = false;

    public string InteractionPrompt => _interactionPrompt;
    public bool IsOpen => _isOpen;

    void Start() {
        _item.SetActive(false);
    }

    public void Open() {
        _animator.SetBool("isOpen", true);
        _item.SetActive(true);
        _isOpen = true;
    }

    public void Grab() {
        _item.SetActive(false);
    }

    public bool Interact (Interactor interactor) {
        Debug.Log("Interacting");
        return true;
    }
}
