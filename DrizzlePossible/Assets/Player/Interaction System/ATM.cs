using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATM : MonoBehaviour, IInteractable
{
    [SerializeField] private MainController _mainController;
    [SerializeField] private GameObject _obstacle;
    [SerializeField] private string _prompt;
    [SerializeField] private float _payout;

    private string _itemName;
    private string _objectName;
    private bool _isOpen;
    private bool _grabbed = false;

    public string InteractionPrompt => _prompt;
    public string GetItemName => _itemName;
    public string GetObjectName => _objectName;
    public bool IsOpen => _isOpen;
    public bool Grabbed => _grabbed;
    public float Payout => _payout;

    void Start() {
        _isOpen = true;
        _itemName = "";
        _objectName = this.gameObject.tag;
    }
    public void Open() {}

    public void Grab() {

        if (_mainController.Coin >= _payout && !_grabbed) {
            Destroy(_obstacle);
            _mainController.Coin -= _payout;
            _grabbed = true;
        }
    }

    public bool Interact (Interactor interactor) { return true; }
}
