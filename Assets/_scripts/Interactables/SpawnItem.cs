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
                Destroy(gameObject);
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
                Destroy(gameObject);
            }
        }
    }

    public void SetParent(GameObject newParent) {
        parent = newParent;
    }
}
