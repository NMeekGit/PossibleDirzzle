using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] private MainController _mainController;
	public GameObject Enemy;
	public float SpawnerScalar = 1f;
	public float spawningLimit = 10;
	public float enemiesAlive = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawningLimit = (Time.time/100000)*SpawnerScalar + spawningLimit;
        if (_mainController.EnemiesAlive < spawningLimit){
        	_mainController.EnemiesAlive += 1;
        	Instantiate(Enemy, this.gameObject.transform.position, Quaternion.identity);
        }
    }
}
