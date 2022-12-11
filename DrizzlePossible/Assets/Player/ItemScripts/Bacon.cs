using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacon : MonoBehaviour, IItem
{
    [SerializeField] private float _addSpeed = .5f;

    public float ApplyPowerUp => _addSpeed;
}
