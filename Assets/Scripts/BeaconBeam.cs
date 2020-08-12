using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconBeam : MonoBehaviour
{
    float scrollSpeed = 0.5f;
    float offset;
    float rotate;

    Renderer renderer;
    float yOffset;

    bool isCollected = false;
    Vector3 scaleDecrease = new Vector3(0, 0.01f, 0)
;
    // Start is called before the first frame update
    void Start()
    {
        renderer = gameObject.GetComponent<Renderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        offset += (Time.deltaTime * scrollSpeed) / 10.0f;
        yOffset += -(Time.deltaTime * scrollSpeed) / 10.0f;
        renderer.material.SetTextureOffset("_MainTex", new Vector2(offset, yOffset));


        if (isCollected)
        {
            if (gameObject.transform.localScale.y > 0)
            {
                gameObject.transform.localScale -= scaleDecrease;
                gameObject.transform.position -= scaleDecrease;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void PyramidIsCollected()
    {
        gameObject.transform.parent = null;
        isCollected = true;

        GameObject mask = gameObject.transform.Find("Mask").gameObject;
        mask.SetActive(false);
    }
}
