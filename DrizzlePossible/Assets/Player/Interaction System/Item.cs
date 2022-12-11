using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    public Animator _animator;
    public GameObject _item;
    public MainController _mainController;
    public string _interactionPrompt;
    
    private bool _isOpen = false;
    private bool _grabbed = false;
    private string _itemName;

    public string InteractionPrompt => _interactionPrompt;
    public bool IsOpen => _isOpen;
    public string GetItemName => _itemName;

    void Start() {
        _item.SetActive(false);
        _itemName = _item.transform.GetChild(0).gameObject.tag;
    }

    public void Open() {
        _animator.SetBool("isOpen", true);
        _item.SetActive(true);
        _isOpen = true;
    }

    public void Grab() {
        _item.SetActive(false);
        if (!_grabbed) {
            _mainController.IsGrabbing = true;
            _grabbed = true;
        }
    }

    public bool Interact (Interactor interactor) {
        Debug.Log("Interacting");
        return true;
    }
}
