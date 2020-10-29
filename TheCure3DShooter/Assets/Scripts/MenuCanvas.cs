using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCanvas : MonoBehaviour
{

    public GameObject gameOverBackground;
    private Image backgroundImage;
    public GameObject GameOverText;

    float currentAlpha = 0;
    public float fadeinSpeed;
    private Color tempColor;

    private void OnEnable()
    {
        backgroundImage = gameOverBackground.GetComponent<Image>();
        currentAlpha = 0;
        tempColor = new Color(0, 0, 0, currentAlpha);
        backgroundImage.color = tempColor;
        GameOverText.gameObject.SetActive(false);

    }

    void Update()
    {
        //Debug.Log(currentAlpha);
        if (currentAlpha < 1)
        {
            FadeInBackGround();
        }
        else
        {
            GameOverText.gameObject.SetActive(true);
        }


    }

    void FadeInBackGround()
    {
        currentAlpha += fadeinSpeed * Time.deltaTime;
        tempColor.a = currentAlpha;
        backgroundImage.color = tempColor;
    }
}
