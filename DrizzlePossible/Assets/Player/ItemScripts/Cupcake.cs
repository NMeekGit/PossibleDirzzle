using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cupcake : MonoBehaviour, IItem
{
    [SerializeField] private float _increaseFireRate;

    public float ApplyPowerUp => _increaseFireRate;
}
