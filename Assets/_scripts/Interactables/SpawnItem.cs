using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private void Start()
    {
        player = GetComponentInParent<Interactable>().GetPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(player.tag))
        {
            transform.parent.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(player.tag))
        {
            transform.parent.gameObject.SetActive(false);
        }
    }
}
