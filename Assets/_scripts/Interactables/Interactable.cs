using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Interactable Variables")]

    protected bool canPickUp = true;
    
    public GameObject player;

    [SerializeField]
    private Item itemInfo;

    
    virtual protected void Start()
    {
        
    }

    virtual protected void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(player.tag))
        {
			player.GetComponent<movement>().setCanPick(true);  // canPick is a Variable to prevent spam
        }
    }

    virtual protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(player.tag)) {
            player.GetComponent<Pickup>().SetItem(gameObject.GetComponent<Interactable>());
        }
    }

    virtual protected void OnDestroy()
    {
        if (player)
        {
            player.GetComponent<movement>().setCanPick(false);
        }
    }

    virtual protected void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(player.tag))
        {
            player.GetComponent<movement>().setCanPick(false);
        }
    }

    virtual public GameObject GetPlayer()
    {
        return player;
    }

    virtual protected void AddToInventory()
    {
        Inventory.instance.Add(itemInfo);
    }

    virtual public void Interact()
    {
        if (canPickUp)
        {
            AddToInventory();
            Destroy(gameObject, 0.1f);
            canPickUp = false;
        }

    }
}
