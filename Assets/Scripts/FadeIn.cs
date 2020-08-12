using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    CanvasRenderer r;
    float alphaValue = 1.0f;

    public bool isMenu;
    float menuAlphaValue;

    // Start is called before the first frame update
    void Start()
    {
        r = gameObject.GetComponent<Image>().canvasRenderer;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMenu)
        {
            if (alphaValue > 0.0f)
            {
                alphaValue -= 0.01f;
            }
            else
            {
                gameObject.SetActive(false);
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
    }
}
