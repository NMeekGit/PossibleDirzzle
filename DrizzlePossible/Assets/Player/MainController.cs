using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public GameObject[] _items;
    public Transform[] _spawnPoints;

    private List<int> usedNums = new List<int>();
    private List<GameObject> itemList = new List<GameObject>();
    private int _count = 0;

    void Start() {
        while (usedNums.Count < _items.Length) {
            UniqueNums();
        }

        foreach (int num in usedNums) {
            itemList.Add(Instantiate(_items[num], _spawnPoints[_count].position, Quaternion.identity, _spawnPoints[_count]));
            _count++;
        }
    }

    void UniqueNums() {
        int ranNum = Random.Range(0, _items.Length);
        if (!usedNums.Contains(ranNum)) 
            usedNums.Add(ranNum);
    }
}
