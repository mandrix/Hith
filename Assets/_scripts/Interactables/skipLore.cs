using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skipLore : MonoBehaviour
{
    void Update()
    {
		if (Input.anyKey)
		{
			gameObject.SetActive(false);
		}
    }
}
