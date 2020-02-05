using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> piezasColectadas = new List<GameObject>();
    [SerializeField]
    private List<GameObject> loreColectado = new List<GameObject>();
    [SerializeField]
    private GameObject nave;

    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 position = ES3.Load<Vector3>("user-v3", transform.position);
        float oxygen = ES3.Load<float>("user-oxygen", 100);
        List<sePuedeRecojer.nombreDePiezas> ship_parts = ES3.Load<List<sePuedeRecojer.nombreDePiezas>>("user-ship-parts", new List<sePuedeRecojer.nombreDePiezas>());
        List<string> collectables = ES3.Load<List<string>>("user-collectables", new List<string>());
        //Debug.Log(position);
        //Debug.Log(oxygen);
        //Debug.Log(collectables);
        foreach(sePuedeRecojer.nombreDePiezas part in ship_parts)
        {
            //Debug.Log(string.Format("part: {0}  nombre: {1}", part, part));
            nave.GetComponent<armarNave>().armar(part);
        }

        if (position != Vector3.zero)
        {
            transform.position = position;
        }
        GetComponent<airTankLevel>().setAirLevel(oxygen);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void agregarAInventario(GameObject obj)
    {
        piezasColectadas.Add(obj);
    }

    public int getPiezasColectadasConteo()
    {
        return piezasColectadas.Count;
    }

    public void agregarAInventarioLore(GameObject obj)
    {
        piezasColectadas.Add(obj);
    }

    public List<GameObject> getShipParts()
    {
        return piezasColectadas;
    }
    public List<GameObject> getLore()
    {
        return loreColectado;
    }
}
