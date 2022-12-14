using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnemyDamage
{
    [SerializeField] private MainController _mainController;
     private string _prompt;
    private float _payout;

    private string _itemName;
    private string _objectName;
    private bool _isOpen = false;
    private bool _grabbed = false;

    public string InteractionPrompt => _prompt;
    public string GetItemName => _itemName;
    public string GetObjectName => _objectName;
    public bool IsOpen => _isOpen;
    public bool Grabbed => _grabbed;
    public float Payout => _payout;

    void Start(){
     _objectName = this.gameObject.tag;
     itemName = "";
     prompt = "";
     _payout = 0;


    }
    public void Grab() {

        if (_objectName == "Player") {
            Destroy(this.gameObject);
            _mainController.EnemiesAlive -= 1;
            _mainController.PlayerHealth -= _mainController.EnemyDamage;
            
        }
    }

    public bool Interact (Interactor interactor) { return true; }
}