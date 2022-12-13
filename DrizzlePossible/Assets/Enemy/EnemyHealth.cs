using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private PlayerStateMachine _player;
    [SerializeField] private GameObject _coin;
    [SerializeField] private float _health;

    public float Health { get { return _health; } set { _health = value; } }
    public float healthScalar = 1f;
    public float startingHealth = 10f;
    public float enemyDamage = 5f;

    void Start()
    {
        _health = startingHealth + (Time.time / 100000 * healthScalar);
    }

    void Update() {
        
        if (_health <= 0 ) {

            Destroy(this.gameObject);
            Instantiate(_coin, this.gameObject.transform.position, Quaternion.identity);

        }
    }

    void OnTriggerEnter(Collider coll) {

        if (coll.gameObject.tag == "bullet") {

            Debug.Log("[Enemy] Hit");
            SoundManagerScript.PlaySound("enemyhit");
            _health -= _player.Damage;

        }
        if (coll.gameObject.tag == "Player")
        {

            Debug.Log("[player] Hit");
            SoundManagerScript.PlaySound("death");
            _player.Health -= enemyDamage;

        }
    }
}
