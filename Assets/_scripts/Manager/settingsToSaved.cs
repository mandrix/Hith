using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingsToSaved : MonoBehaviour
{

    
    public void Saving()
    {
        ES3.Save<Vector3>("user-v3", transform.position);
        ES3.Save<float>("user-oxygen", GetComponent<airTankLevel>().getAirLevel());
        List<sePuedeRecojer.nombreDePiezas> piezas = new List<sePuedeRecojer.nombreDePiezas>();
        foreach (GameObject obj in GetComponent<Inventario>().getShipParts())
        {
            piezas.Add(obj.GetComponent<sePuedeRecojer>().GetNombreDePieza());
        }
        ES3.Save<List<sePuedeRecojer.nombreDePiezas>>("user-ship-parts", piezas);
    }

}
