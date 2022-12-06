using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    PlayerInput playerInput;
    public PlayerStateMachine _playerMachine;
    public Transform _interactionPoint;
    public float _interactionPointRadius;
    public LayerMask _interactableMask;
    public InteractionPromptUI _interactionPromptUI;

    private readonly Collider[] _colliders = new Collider[3];
    public int _numFound;

    private IInteractable _interactable;

    void Update() {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);

        if (_numFound> 0) {
            _interactable = _colliders[0].GetComponent<IInteractable>();

            if (_interactable != null) {
                if (!_interactionPromptUI.IsDisplayed) {
                    _interactionPromptUI.SetUp(_interactable.InteractionPrompt);
                }
                if (_playerMachine.IsSelectPressed) {
                    _interactable.Interact(this);

                    if(!_interactable.IsOpen) {
                        _interactable.Open();
                    }
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
    }
}
