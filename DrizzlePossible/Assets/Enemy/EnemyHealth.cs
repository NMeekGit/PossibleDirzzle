using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private PlayerStateMachine _player;
    [SerializeField] private GameObject _coin;
    [SerializeField] private float _health;

    public float Health { get { return _health; } set { _health = value; } }

    void Update() {
        
        if (_health <= 0 ) {

            Destroy(this.gameObject);
            Instantiate(_coin, this.gameObject.transform.position, Quaternion.identity);

        }
    }

    void OnTriggerEnter(Collider coll) {

        if (coll.gameObject.tag == "bullet") {

            Debug.Log("[Enemy] Hit");

            _health -= _player.Damage;

        }
    }
}
