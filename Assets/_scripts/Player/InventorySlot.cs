using UnityEngine;
using UnityEngine.UI;

// Creado por JZ1999

public class InventorySlot : MonoBehaviour
{
	#region Variables
	public Image icon;          // Reference to the Icon image
	Item item;  // Current item in the slot
	#endregion

	#region Unity Methods
	#endregion

	#region Custom methods
	// Add item to the slot
	public void AddItem(Item newItem)
	{
		item = newItem;

		icon.sprite = item.icon;
		icon.enabled = true;
	}

	// Clear the slot
	public void ClearSlot()
	{
		item = null;

		icon.sprite = null;
		icon.enabled = false;
	}

	// Called when the remove button is pressed
	public void OnRemoveButton()
	{
		Inventory.instance.Remove(item);
	}

	// Called when the item is pressed
	public void UseItem()
	{
		if (item != null)
		{
			item.Use();
		}
	}
	#endregion
}
