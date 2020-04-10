using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTutorial : MonoBehaviour
{
    public GameObject player;
    public GameObject Tutorial;
    public GameObject airTank;
    public Transform camara;
    // Start is called before the first frame update
    void Start()
    {
    }
    private void Update()
    {
        gameObject.transform.LookAt(camara);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(player.tag)) {
            if (Input.GetButtonDown("Fire1")){
                airTank.SetActive(true);
                Tutorial.GetComponent<Tutorial>().SetState(2); 
            }
        }
    }

}
