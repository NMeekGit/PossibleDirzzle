using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public string InteractionPrompt { get; }
    public bool IsOpen { get; }
    public bool Grabbed { get; }
    public string GetItemName { get; }
    public string GetObjectName { get; }
    public float Payout { get; }

    public void Open();
    public void Grab();
    public bool Interact (Interactor interactor);
}
