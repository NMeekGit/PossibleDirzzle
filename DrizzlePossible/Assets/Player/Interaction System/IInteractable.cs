using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public string InteractionPrompt { get; }
    public bool IsOpen { get; }

    public void Open();
    public void Grab();
    public bool Interact (Interactor interactor);
}
