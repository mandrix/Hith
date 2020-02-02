using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class treeMusic : MonoBehaviour
{
	[SerializeField]
	private AudioSource treeAudio;
	[SerializeField]
	private AudioSource ambientAudio;
	[SerializeField]
	private GameObject player;

	private float distanceToPlayerInitial;
	private float distanceToPlayer;
	private bool inRadius = false;

	[SerializeField]
	[Range(0, 50)]
	private int oxygenToAdd = 25;
	[SerializeField]
	private bool inRadiusToGetOxygen = false;
	// Start is called before the first frame update
	void Start()
    {
		
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag(player.tag) && !inRadius)
		{
			distanceToPlayerInitial = Vector3.Distance(player.transform.position, transform.position);
			inRadius = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag(player.tag))
		{
			inRadius = false;
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag(player.tag))
		{
			distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
			manejarMusica();

			manejarOxigeno();
		}
	}

	private void manejarOxigeno()
	{
		// Definir que tan dentro debe estar del sphere collider
		if (distanceToPlayer <= distanceToPlayerInitial/2 && !inRadiusToGetOxygen)
		{
			player.GetComponent<airTankLevel>().addAir(oxygenToAdd);
			inRadiusToGetOxygen = true;

			//Saving
			Debug.Log("Saved");
			ES3.Save<Vector3>("user-v3", player.transform.position);
			ES3.Save<float>("user-oxygen", player.GetComponent<airTankLevel>().getAirLevel());
			List<sePuedeRecojer.nombreDePiezas> piezas = new List<sePuedeRecojer.nombreDePiezas>();
			foreach(GameObject obj in player.GetComponent<Inventario>().getShipParts())
			{
				piezas.Add(obj.GetComponent<sePuedeRecojer>().GetNombreDePieza());
			}
			ES3.Save<List<sePuedeRecojer.nombreDePiezas>>("user-ship-parts", piezas);

		}
	}

	private void manejarMusica()
	{

		float percentageAmbient = treeMusic.percentageVolAmbient(distanceToPlayerInitial, distanceToPlayer);
		ambientAudio.volume = percentageAmbient / 100;

		float percentageTree = treeMusic.percentageVolTree(distanceToPlayerInitial, distanceToPlayer);
		treeAudio.volume = percentageTree / 100;
	}

	static float percentageVolAmbient(float initialDistance, float currentDistance)
	{
		float result = (currentDistance / initialDistance) * 100;
		return result;
	}

	static float percentageVolTree(float initialDistance, float currentDistance)
	{
		float result = 100 - ((currentDistance / initialDistance) * 100);
		return result;
	}
}
