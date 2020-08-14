using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField]
    private Transform Camera;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(Camera);
    }
}
