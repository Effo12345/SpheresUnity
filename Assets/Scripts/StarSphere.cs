using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSphere : MonoBehaviour
{
    bool scale = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StopScale());
    }

    // Update is called once per frame
    void Update()
    {
        if (scale)
        {
            gameObject.transform.localScale += new Vector3(0.007f, 0.007f, 0.007f);
        }

    }

    IEnumerator StopScale()
    {
        yield return new WaitForSeconds(2f);
        scale = false;
    }
}
