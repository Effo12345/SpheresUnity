﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private Vector3 center;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        center = new Vector3(0.0f, 0.5f, 0.0f);
        transform.RotateAround(center, Vector3.up, 45 * Time.deltaTime);
    }

}
