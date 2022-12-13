using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadlightController : MonoBehaviour
{
    public float timeDelay = 1.0f;
    public float timeBetweenFlickers = 1.0f;
    float timebeforeFlicker = 0f;
    bool flickering = false;
    Light lg;
    // Start is called before the first frame update
    void Start()
    {
        lg = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!flickering)
        {
            flickerLight();
        }
    }

    void flickerLight()
    {
        flickering = true; 
        lg.enabled = false;
        while (timebeforeFlicker > timeBetweenFlickers)
        {
            timebeforeFlicker += Time.deltaTime;
        }
        timebeforeFlicker= 0.0f;
        lg.enabled = true;
        flickering = false;
        while (timebeforeFlicker > timeBetweenFlickers)
        {
            timebeforeFlicker += Time.deltaTime;
        }

    }
}
