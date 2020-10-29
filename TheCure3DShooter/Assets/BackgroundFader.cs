using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundFader : MonoBehaviour {

    public static BackgroundFader instance;

    float currentAlpha = 0;
    float desiredFadeValue;
    public float fadeSpeed;
    public Color backgroundColor;
    public Image backgroundImage;

    void Start() {

        if( instance == null ) {

            instance = this;
        } else {

            Destroy(this.gameObject);
        }
    }

    public bool DoFade() {

        if( currentAlpha < desiredFadeValue ) {

            currentAlpha -= fadeSpeed * Time.deltaTime;
        } else if( currentAlpha > desiredFadeValue ) {

            currentAlpha += fadeSpeed * Time.deltaTime;
        } else {

            return true;
        }

        return false;
    }

    public void Fade( float fadeValue ) {

        desiredFadeValue = fadeValue;
    }
}
