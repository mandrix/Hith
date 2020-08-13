using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private GameObject parent;

    private void Start()
    {
        if (player == null) { }
            //player = GetComponentInParent<Collector>().GetPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(player.tag))
        {
            if (parent)
            {
                parent.gameObject.SetActive(true);
            }
            else
            {
                Destroy(this);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(player.tag))
        {
            if (parent)
            {
                parent.gameObject.SetActive(false);
            }
            else {
                Destroy(this);
            }
        }
    }

    public void SetParent(GameObject newParent) {
        parent = newParent;
    }
}
