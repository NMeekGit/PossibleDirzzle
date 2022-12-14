using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour, IInteractable
{
    [SerializeField] private MainController _mainController;
    [SerializeField] private PlayerStateMachine _player;
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
         _itemName = "";
         _prompt = "";
         _payout = 0;
    }
    public void Open() {}
    public void Grab() {

        Destroy(this.gameObject);
        _mainController.EnemiesAlive -= 1;
        _player.Health -= _mainController.EnemyDamage;
            
    }

    public bool Interact (Interactor interactor) { return true; }
}
