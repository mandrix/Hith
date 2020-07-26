using UnityEngine;
// Creado por JZ1999

public class OpenInventory : MonoBehaviour
{
	#region Variables
	[SerializeField]
	private GameObject inventoryUI;

	[SerializeField]
	private GameObject gasTankUI;

	[SerializeField]
	private GameObject minimapUI;

	[SerializeField]
	private GameObject player;
	#endregion

	#region Unity Methods
	void Start()
    {
	}

    void Update()
    {
		if (Input.GetButtonDown("Inventory"))
		{
			if (!inventoryUI.activeSelf)
			{
				player.GetComponent<pause>().pauseGame(inventoryUI);
				minimapUI.SetActive(false);
				gasTankUI.SetActive(false);
			}
			else
			{
				player.GetComponent<pause>().unpauseGame(inventoryUI);
				minimapUI.SetActive(true);
				gasTankUI.SetActive(true);
			}
		}
	}
    #endregion

    #region Custom methods

    #endregion
}
