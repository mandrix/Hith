using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armarNave : MonoBehaviour
{
    public void armar(sePuedeRecojer.nombreDePiezas pieza)

	{
		// Debug.Log(string.Format("pieza: {0}", pieza));
		foreach (Transform child in transform)
		{
			// Debug.Log(child.gameObject.GetComponent<tipoDeParte>().nombreDePieza);
			if (child.GetComponent<tipoDeParte>().nombreDePieza.Equals(pieza))
			{
				child.gameObject.SetActive(true);
			  break;
			}
		}
	}

    public bool estaCompleta()
    {
        bool isComplete = true;
        foreach (Transform child in transform)
        {
            if (child.gameObject.active)
            {
                isComplete = true;
            }
            else
            {
                isComplete = false;
                break;
            }
        }
        return isComplete;
    }
}
