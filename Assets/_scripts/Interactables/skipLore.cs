using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skipLore : MonoBehaviour
{
	[SerializeField]
	private GameObject miniMapUI;

	[SerializeField]
	private GameObject gasTankUI;
	void Update()
    {
		if (Input.anyKey)
		{
			miniMapUI.SetActive(true);
			gasTankUI.SetActive(true);
			gameObject.SetActive(false);
		}
    }
}
