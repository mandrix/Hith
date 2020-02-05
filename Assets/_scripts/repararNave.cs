using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class repararNave : MonoBehaviour
{
	private List<GameObject> partesDeNave;
	[SerializeField]
	private string tagPartesDeNave;
    // Start is called before the first frame update
    void Start()
    {

        foreach (Transform child in transform)
         {
             if (child.tag == tagPartesDeNave)
             {
                 partesDeNave.Add(child.gameObject);
             }
         }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
