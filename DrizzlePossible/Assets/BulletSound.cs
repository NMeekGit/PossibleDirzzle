using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSound : MonoBehaviour
{

    void Start()
    {
        SoundManagerScript.PlaySound("fire");
        Debug.Log("BulletShot");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
