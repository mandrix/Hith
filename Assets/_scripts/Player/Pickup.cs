using UnityEngine;
// Creado por JZ1999

public class Pickup : MonoBehaviour
{
	#region Variables
	public bool canPickup = false;
    #endregion

    #region Unity Methods
    #endregion

    #region Custom methods
	void togglePickup()
	{
		// This function is used in the animator. It is called after the pickup animation is done playing
		canPickup = !canPickup;
	}
    #endregion
}
