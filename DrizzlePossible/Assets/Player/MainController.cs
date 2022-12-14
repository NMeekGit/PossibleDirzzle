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
    [SerializeField] private CoinTracker _coinTracker;
    [SerializeField] private Apple _apple;
    [SerializeField] private Bacon _bacon;
    [SerializeField] private Pizza _pizza;
    [SerializeField] private Cupcake _cupcake;
    [SerializeField] private float _enemy1Damage;
    [SerializeField] private float _enemy2Damage;
    [SerializeField] private float _enemiesAlive;
    [SerializeField] private float _enemyDamage = 10;
    public string ItemName { set { _itemName = value; } }
    public bool IsGrabbing { set { _isGrabbing = value; } }
    public float Enemy1Damage { get { return _enemy1Damage; } }
    public float Enemy2Damage { get { return _enemy2Damage; } }
    public float Coin { get { return _coinTracker.NumCoin; } set { _coinTracker.NumCoin = value; } }
    public float EnemiesAlive { get { return _enemiesAlive; } set { _enemiesAlive = value; } }
    public float PlayerHealth { get { return _player.Health;} set {_player.Health = value;}}
    public float EnemyDamage { get { return _enemyDamage;} set {_enemyDamage = value;}}

    

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
        _coinTracker.SetCoinTracker();
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

    public void AddCoin() {

        _coinTracker.NumCoin += 1;

    }

}
