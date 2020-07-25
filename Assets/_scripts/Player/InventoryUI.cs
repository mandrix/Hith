using UnityEngine;
// Creado por JZ1999

public class InventoryUI : MonoBehaviour
{
	#region Variables
	[SerializeField]
	private KeyCode openInventory = KeyCode.E;

	[SerializeField]
	private GameObject inventoryUI;

	[SerializeField]
	private GameObject gasTankUI;

	[SerializeField]
	private GameObject minimapUI;

	[SerializeField]
	private Transform itemsParent;   // The parent object of all the items

	[SerializeField]
	private GameObject player;

	Inventory inventory;    // Our current inventory

	InventorySlot[] slots;  // List of all the slots

	#endregion

	#region Unity Methods
	void Start()
    {
		inventory = Inventory.instance;
		inventory.onItemChangedCallback += UpdateUI;    // Subscribe to the onItemChanged callback

		// Populate our slots array
		slots = itemsParent.GetComponentsInChildren<InventorySlot>();
	}

	private void Update()
	{
		if (Input.GetButtonDown("Inventory"))
		{
			if(!inventoryUI.activeSelf)
				player.GetComponent<pause>().pauseGame(inventoryUI);
			else
				player.GetComponent<pause>().unpauseGame(inventoryUI);

			minimapUI.SetActive(!minimapUI.activeSelf);
			gasTankUI.SetActive(!gasTankUI.activeSelf);
		}
	}
	#endregion

	#region Custom methods
	// Update the inventory UI by:
	//		- Adding items
	//		- Clearing empty slots
	// This is called using a delegate on the Inventory.
	void UpdateUI()
	{
		// Loop through all the slots
		for (int i = 0; i < slots.Length; i++)
		{
			if (i < inventory.items.Count)  // If there is an item to add
			{
				slots[i].AddItem(inventory.items[i]);   // Add it
			}
			else
			{
				// Otherwise clear the slot
				//slots[i].ClearSlot();
			}
		}
	}
	#endregion
}
