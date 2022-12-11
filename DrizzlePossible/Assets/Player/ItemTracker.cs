using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemTracker : MonoBehaviour
{
    public TextMeshProUGUI _baconCount;
    public TextMeshProUGUI _pizzaCount;
    public TextMeshProUGUI _cupcakeCount;
    private float _numBacon = 0f;
    private float _numPizza = 0f;
    private float _numCupcake = 0f;

    public float NumBacon { get { return _numBacon; } set { _numBacon = value; } }
    public float NumPizza { get { return _numPizza; } set { _numPizza = value; } }
    public float NumCupcake { get {return _numCupcake; } set { _numCupcake = value; } }

    public void SetItemCount() {
        _baconCount.text = _numBacon.ToString();
        _pizzaCount.text = _numPizza.ToString();
        _cupcakeCount.text = _numCupcake.ToString();
    }
}
