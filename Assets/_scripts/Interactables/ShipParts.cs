using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipParts : Interactable
{

    [Header("Custom Variables")]
    [SerializeField]
    protected GameObject Ship;

    override protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(player.tag))
        {
            player.GetComponent<Pickup>().SetItem(gameObject.GetComponent<ShipParts>());
        }
    }

    override public void Interact()
    {
        BuildShip();
        base.Interact();
        
    }

    private protected void BuildShip() {
        //Ship.GetComponent<armarNave>().armar(itemInfo.name);
    }

}
