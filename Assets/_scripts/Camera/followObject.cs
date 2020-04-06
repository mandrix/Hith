using UnityEngine;
// Creado por JZ1999

public class followObject : MonoBehaviour
{
	#region Variables
	[SerializeField]
	[Tooltip("The transform to follow")]
	private Transform objTransform;
	[Space]
	[SerializeField]
	private bool freezeX = false;
	[Space]
	[SerializeField]
	private bool freezeY = false;
	[Space]
	[SerializeField]
	private bool freezeZ = false;
	#endregion

	#region Unity Methods
    void Update()
    {
		float x = freezeX ? transform.position.x : objTransform.position.x;
		float y = freezeY ? transform.position.y : objTransform.position.y;
		float z = freezeX ? transform.position.z : objTransform.position.z;

		transform.position = new Vector3(x, y, z);
    }
    #endregion

    #region Custom methods

    #endregion
}
