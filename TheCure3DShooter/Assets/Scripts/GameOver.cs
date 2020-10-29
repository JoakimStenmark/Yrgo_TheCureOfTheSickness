using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{

    public GameObject gameOverBackground;
    private Image backgroundImage;
    
    float currentAlpha = 0;
    public float fadeinSpeed;
    private Color tempColor;

    void Start()
    {
        backgroundImage = gameOverBackground.GetComponent<Image>();
        tempColor = new Color(0, 0, 0, currentAlpha);
    }

    void Update()
    {
        if (currentAlpha < 255)
        {
            currentAlpha += fadeinSpeed * Time.deltaTime;
            tempColor.a = currentAlpha;
        }

        backgroundImage.color = tempColor;
    }
}
