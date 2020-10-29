using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFader : MonoBehaviour {

    Light light;
    public float fadeSpeed = 0.1f;

    void Start() {

        light = GetComponent<Light>();
        light.intensity = 0;
    }

    void Update() {


        if( light.intensity < 1.22 ) {

            light.intensity += fadeSpeed * Time.deltaTime;
        }

    }
}
