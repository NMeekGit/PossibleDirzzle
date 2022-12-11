using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField] private float _addHealth = 25f;

    public float ApplyPowerUp => _addHealth;
}
