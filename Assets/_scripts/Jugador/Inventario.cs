using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
	[SerializeField]
	private List<GameObject> piezasColectadas = new List<GameObject>();
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void agregarAInventario(GameObject obj)
	{
		piezasColectadas.Add(obj);
	}
}
