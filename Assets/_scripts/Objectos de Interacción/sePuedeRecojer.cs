using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class sePuedeRecojer : MonoBehaviour
{
	// Este script ayuda en que si el jugador está en el rango del collider de este objeto
	// le permite presionar espacio y poder recolectar la pieza de la nave espacial

	public enum nombreDePiezas // your custom enumeration
	{
		motor,
		ala1,
		ala2,
		rampa
	};

	[SerializeField]
	private nombreDePiezas nombreDePieza;
	[SerializeField]
	private GameObject jugador;

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag(jugador.tag))
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				Debug.Log("Item se recojió");
				GameObject objeto_copia = Instantiate(gameObject, transform.position, transform.rotation);
				objeto_copia.SetActive(false);

				jugador.GetComponent<Inventario>().agregarAInventario(objeto_copia);
				Destroy(gameObject, 0.10f);
			}
		}
	}
}
