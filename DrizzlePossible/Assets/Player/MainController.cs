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
    private string _itemName;
    private bool _isGrabbing;
    [SerializeField] private PlayerStateMachine _player;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private ItemTracker _itemTracker;
    [SerializeField] private Apple _apple;
    [SerializeField] private Bacon _bacon;
    [SerializeField] private Pizza _pizza;
    [SerializeField] private Cupcake _cupcake;
    [SerializeField] private float _damage;
    public string ItemName { set { _itemName = value; } }
    public bool IsGrabbing { set { _isGrabbing = value; } }

    void Awake() {
        while (usedNums.Count < _items.Length) {
            UniqueNums();
        }

        foreach (int num in usedNums) {
            itemList.Add(Instantiate(_items[num], _spawnPoints[_count].position, Quaternion.identity, _spawnPoints[_count]));
            _count++;
        }

        _healthBar.SetHealth();
    }

    void UniqueNums() {
        int ranNum = Random.Range(0, _items.Length);
        if (!usedNums.Contains(ranNum)) 
            usedNums.Add(ranNum);
    }

    void Update() {

        CheckItem();
        _healthBar.SetHealth();
        _itemTracker.SetItemCount();
    }

    void CheckItem() {

        if (_isGrabbing) {
            Debug.Log("[MAIN] Grabbing " + _itemName);

            ApplyModifier();

            _isGrabbing = false;
        }

    }

    void ApplyModifier() {

        switch (_itemName) {

            case "Apple":
                _player.Health += _apple.ApplyPowerUp;
                break;
            case "Bacon":
                _player.Speed += _player.Speed * _bacon.ApplyPowerUp;
                _itemTracker.NumBacon += 1;
                break;
            case "Cupcake":
                _player.FireRate *= _cupcake.ApplyPowerUp;
                _itemTracker.NumCupcake += 1;
                break;
            case "Pizza":
                _player.Damage += _pizza.ApplyPowerUp;
                _itemTracker.NumPizza += 1;
                break;
            default:
                break;
        }
    }

}
