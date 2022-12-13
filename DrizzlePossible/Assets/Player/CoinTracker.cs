using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinTracker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinTracker;
    [SerializeField] private float _numCoin = 0f;

    public float NumCoin { get { return _numCoin; } set { _numCoin = value; } }

    public void SetCoinTracker() {

        _coinTracker.text = _numCoin.ToString();

    }
}
