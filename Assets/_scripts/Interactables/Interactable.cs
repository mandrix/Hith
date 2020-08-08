using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Interactable Variables")]
    [SerializeField]
    //public GameObject ColliderForRespawn;

    protected bool canPickUp = true;
    
    public GameObject player;

    [SerializeField]
    private Item itemInfo;

    


    virtual protected void Start()
    {
        //Instantiate(ColliderForRespawn, transform.position, new Quaternion(), transform);
    }

    virtual protected void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(player.tag))
        {
			player.GetComponent<movement>().setCanPick(true);  // canPick is a Variable to prevent spam
            if (Input.GetButtonDown("Fire1") && canPickUp)
            {
                canPickUp = false;
                Interact();
            }
        }
    }

    virtual protected void Interact() {
        AddToInventory();
        Destroy(gameObject, 0.1f);
    }

    virtual protected void AddToInventory()
    {
        Inventory.instance.Add(itemInfo);
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
    public GameObject GetPlayer()
    {
        return player;
    }
}
