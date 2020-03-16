using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingsToSaved : MonoBehaviour
{
	private ES3File es3File;

	public void Saving()
    {
		es3File = new ES3File("save.es3");

		es3File.Save<Vector3>("user-v3", transform.position);
		es3File.Save<float>("user-oxygen", GetComponent<airTankLevel>().getAirLevel());
        List<sePuedeRecojer.nombreDePiezas> piezas = new List<sePuedeRecojer.nombreDePiezas>();
        foreach (GameObject obj in GetComponent<Inventario>().getShipParts())
        {
            piezas.Add(obj.GetComponent<sePuedeRecojer>().GetNombreDePieza());
        }
		es3File.Save<List<sePuedeRecojer.nombreDePiezas>>("user-ship-parts", piezas);
    }

}
