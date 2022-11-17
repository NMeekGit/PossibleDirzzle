using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public GameObject bulletDecal;

    public float speed = 50f;
    
    float timeToDestroy = 3f;

    public Vector3 target { get; set; }
    public bool hit { get; set; }

    void OnEnable() {
        Destroy(gameObject, timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (!hit && Vector3.Distance(transform.position, target) < .01f) {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision coll) {
        ContactPoint contact = coll.GetContact(0);
        GameObject.Instantiate(bulletDecal, contact.point + contact.normal * .0001f, Quaternion.LookRotation(contact.normal));
        Destroy(this.gameObject);
    }
}
