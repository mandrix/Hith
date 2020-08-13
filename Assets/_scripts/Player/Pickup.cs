using UnityEngine;
// Creado por JZ1999

public class Pickup : MonoBehaviour
{
	#region Variables
	public bool canPickup = false;
    private Interactable itemScript;
    #endregion

    #region Unity Methods
    #endregion

    #region Custom methods

    public void SetItem(Interactable newScript) {
        itemScript = newScript;
    }

	public void togglePickup()
	{
		// This function is used in the animator. It is called after the pickup animation is done playing
		canPickup = !canPickup;
        if (canPickup) {
            if (itemScript) {
                itemScript.Interact();
            }
        }
	}
    #endregion
}
