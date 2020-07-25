using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skipLore : MonoBehaviour
{
	[SerializeField]
	private GameObject inventoryUI;

	[SerializeField]
	private GameObject gasTankUI;
	void Update()
    {
		if (Input.anyKey)
		{
			inventoryUI.SetActive(true);
			gasTankUI.SetActive(true);
			gameObject.SetActive(false);
		}
    }
}
