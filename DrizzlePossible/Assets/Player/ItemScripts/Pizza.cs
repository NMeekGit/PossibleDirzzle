using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour
{
    [SerializeField] private float _newDamage;

    public float ApplyPowerUp => _newDamage;
}
