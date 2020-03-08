using UnityEngine;
// Creado por JZ1999

public class followObject : MonoBehaviour
{
	#region Variables
	[SerializeField]
	[Tooltip("The transform to follow")]
	private Transform objTransform;
    #endregion

    #region Unity Methods
    void Start()
    {
        
    }

    void Update()
    {
		transform.position = new Vector3(objTransform.position.x, transform.position.y, objTransform.position.z);
    }
    #endregion

    #region Custom methods

    #endregion
}
