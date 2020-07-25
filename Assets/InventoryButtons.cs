using UnityEngine;
// Creado por JZ1999


public class InventoryButtons : MonoBehaviour
{
    #region Variables
	public enum pages
	{
		inventory,
		fullMap
	};

	[SerializeField]
	private GameObject inventoryUI;
	[SerializeField]
	private GameObject fullMapUI;

	public static pages currentPage = pages.inventory;
    #endregion

    #region Unity Methods
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    #endregion

    #region Custom methods
	public void inventory()
	{
		if(!(currentPage == pages.inventory))
		{
			inventoryUI.SetActive(true);
			fullMapUI.SetActive(false);
		}
	}

	public void fullMap()
	{
		if (!(currentPage == pages.fullMap))
		{
			fullMapUI.SetActive(true);
			inventoryUI.SetActive(false);
		}
	}
	#endregion
}
