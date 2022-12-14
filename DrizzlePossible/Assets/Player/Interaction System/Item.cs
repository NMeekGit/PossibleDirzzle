using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    public Animator _animator;
    public GameObject _item;
    public MainController _mainController;
    public string _interactionPrompt;
    [SerializeField] private float _payout;
    
    private bool _isOpen = false;
    private bool _grabbed = false;
    private string _itemName;
    private string _objectName;

    public string InteractionPrompt => _interactionPrompt;
    public bool IsOpen => _isOpen;
    public bool Grabbed => _grabbed;
    public string GetItemName => _itemName;
    public string GetObjectName => _objectName;
    public float Payout => _payout;

    void Start() {
        _item.SetActive(false);
        _objectName = this.gameObject.tag;
        _itemName = _item.transform.GetChild(0).gameObject.tag;
    }

    public void Open() {
        _animator.SetBool("isOpen", true);
        _mainController.Coin -= _payout;
        _item.SetActive(true);
        _isOpen = true;
    }

    public void Grab() {
        
        if (_mainController.Coin >= _payout) {
            _item.SetActive(false);
            if (!_grabbed) {
                _mainController.IsGrabbing = true;
                _grabbed = true;
                _mainController.AudioManager.Play("PowerUp");
            }
        }
    }

    public bool Interact (Interactor interactor) {
        Debug.Log("Interacting");
        return true;
    }
}
