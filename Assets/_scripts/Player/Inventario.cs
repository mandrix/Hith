using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> piezasColectadas = new List<GameObject>();
    [SerializeField]
    private List<string> loreColectado = new List<string>();
    [SerializeField]
    private GameObject nave;

	private ES3File es3File;


	// Start is called before the first frame update
	void Start()
    {
		es3File = new ES3File("save.es3");

		Vector3 position = es3File.Load<Vector3>("user-v3", transform.position);
        float oxygen = es3File.Load<float>("user-oxygen", 100);
        List<sePuedeRecojer.nombreDePiezas> ship_parts = es3File.Load<List<sePuedeRecojer.nombreDePiezas>>("user-ship-parts", new List<sePuedeRecojer.nombreDePiezas>());
        List<string> collectables = es3File.Load<List<string>>("user-collectables", new List<string>());
        foreach(sePuedeRecojer.nombreDePiezas part in ship_parts)
        {
            nave.GetComponent<armarNave>().armar(part);
        }

        if (position != Vector3.zero)
        {
            transform.position = position;
        }
        GetComponent<airTankLevel>().setAirLevel(oxygen);
    }

    public void agregarAInventario(GameObject obj)
    {
        piezasColectadas.Add(obj);
    }

    public int getPiezasColectadasConteo()
    {
        return piezasColectadas.Count;
    }

    public void agregarAInventarioLore(string obj)
    {
		loreColectado.Add(obj);
    }

    public List<GameObject> getShipParts()
    {
        return piezasColectadas;
    }
    public List<string> getLore()
    {
        return loreColectado;
    }

	public void saveLore()
	{
		HashSet<string> loresSaved = es3File.Load("lores", new HashSet<string>());
		HashSet<string> loresSavedSet = new HashSet<string>(loresSaved);

		HashSet<string> loresToSaveSet = new HashSet<string>(loreColectado);

		loresToSaveSet.UnionWith(loresSavedSet);

		es3File.Save<HashSet<string>>("lores", loresToSaveSet);
		es3File.Sync();
	}
}
