﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementShip : MonoBehaviour
{
    // Start is called before the first frame update
    public float velocity;
    public GameObject player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newTransform = new Vector3 (0,0,velocity);
        transform.position += newTransform;
        
    }
}
