using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScalar : MonoBehaviour
{
    Light lg;
    float difficultyScalar = .05f;
    float timeScalar = 1000000;
    float intensityValue = 0;
    float maxIntensity = 1.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        lg = GetComponent<Light>();
        lg.intensity = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (lg.intensity < maxIntensity)
        {
            intensityValue = (Time.time / timeScalar) * difficultyScalar;
            lg.intensity += intensityValue;
        }
    }
}
