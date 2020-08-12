using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSphere : MonoBehaviour
{
    private Material thisMat;
    Renderer r;
    float alphaValue = 1.0f;
    float intensity = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        r = gameObject.GetComponent<Renderer>();
        thisMat = r.material;
        thisMat.EnableKeyword("_EMISSION");
    }

    // Update is called once per frame
    void Update()
    {
        DecreaseEmision();
        
        IncreaseTransparency();

        gameObject.transform.localScale += new Vector3(0.007f, 0.007f, 0.007f);
    }

    void IncreaseTransparency()
    {
        if (alphaValue > 0.0f)
        {
            alphaValue -= 0.01f;
        }
        /*else
        {
            gameObject.SetActive(false);
        }*/
        Color newColor = r.material.color;
        newColor.a = alphaValue;
        r.material.color = newColor;
    }

    void DecreaseEmision()
    {
        if (intensity > 0.0f)
        {
            intensity -= .007f;
        }
        else
        {
            gameObject.SetActive(false);
        }

        thisMat.SetColor("_EmissionColor", new Color(1.0f, 1.0f, 1.0f, 1.0f) * intensity);
    }
}
