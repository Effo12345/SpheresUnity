using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    CanvasRenderer r;
    float alphaValue = 1.0f;

    public bool isMenu;
    public bool hasTitleText;
    bool isFading = false;
    public bool shouldWaitToFade;
    float menuAlphaValue;

    //public Text[] titleText;
    public TMP_Text[] titleText;

    // Start is called before the first frame update
    void Start()
    {
        r = gameObject.GetComponent<Image>().canvasRenderer;
        if (shouldWaitToFade)
        {
            StartCoroutine(Wait());
        }
        else
        {
            isFading = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMenu && isFading)
        {
            if (alphaValue > 0.0f)
            {
                alphaValue -= 0.01f;
            }
            else
            {
                gameObject.SetActive(false);

                foreach (var text in titleText)
                {
                    text.gameObject.SetActive(false);
                }
            }

        }
        else
        {
                menuAlphaValue += 0.0001f;
        }

        /*Color newColor = r.GetMaterial().color;
        newColor.a = alphaValue;
        r.GetMaterial().color = newColor;*/

        gameObject.GetComponent<CanvasGroup>().alpha = alphaValue;
        //r.SetAlpha(alphaValue);

        foreach (var text in titleText)
        {
            text.GetComponent<CanvasGroup>().alpha = alphaValue;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        isFading = true;
    }
}
