using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armarNave : MonoBehaviour
{
    public void armar(sePuedeRecojer.nombreDePiezas pieza)
	{
		foreach (Transform child in transform)
		{
			//Debug.Log(child.gameObject.GetComponent<tipoDeParte>().nombreDePieza);
			if (child.GetComponent<tipoDeParte>().nombreDePieza.Equals(pieza))
			{
				child.gameObject.SetActive(true);
			  break;
			}
		}
	}
}
