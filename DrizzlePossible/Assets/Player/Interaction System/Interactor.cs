using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    PlayerInput playerInput;

    public MainController _mainController;
    public PlayerStateMachine _playerMachine;
    public Transform _interactionPoint;
    public float _interactionPointRadius;
    public LayerMask _interactableMask;
    public LayerMask _coinMask;
    public InteractionPromptUI _interactionPromptUI;

    public float waitTime = 1f;
    float nextTime;

    private readonly Collider[] _colliders = new Collider[3];
    public int _numFound;
    public int _coinFound;

    private IInteractable _interactable;

    void Update() {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);

        if (_numFound> 0) {
            _interactable = _colliders[0].GetComponent<IInteractable>();

            if (_interactable != null) {
                if (!_interactionPromptUI.IsDisplayed) {
                    _interactionPromptUI.SetUp(_interactable.InteractionPrompt + _interactable.Payout.ToString());
                }
                if (_playerMachine.IsSelectPressed && _interactable.GetObjectName == "chest") {
                    _interactable.Interact(this);

                        if(!_interactable.IsOpen) {
                            _interactable.Open();
                            nextTime = Time.time + waitTime;
                        }
                    
                        if (_playerMachine.IsSelectPressed && _interactable.IsOpen && Time.time > nextTime) {
                            _interactable.Grab();
                            _mainController.ItemName = _interactable.GetItemName;
                        }
                }
                else if (_interactable.GetObjectName == "atm" && _playerMachine.IsSelectPressed) {
                    _interactable.Grab();
                    _mainController.ItemName = _interactable.GetObjectName;
                }
            }
        } else {
            
            if (_interactable != null) {
                _interactable = null;
            }

            if (_interactionPromptUI.IsDisplayed) {
                _interactionPromptUI.Close();
            }
        }

        _coinFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _coinMask);

        if (_coinFound > 0) {

            _mainController.AddCoin();
            FindObjectOfType<AudioManager>().Play("Coin");
            Destroy(_colliders[0].gameObject);

        }
    }
}
