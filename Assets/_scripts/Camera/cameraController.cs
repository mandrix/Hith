using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    // VARS
    public Transform target;
    public Vector3 offset;
    public bool useOffsetValues;

    // ROTAR LA CAMARA
    public float rotateSpeed;
    public Transform pivot;

    // PARA LIMITAR LA CAMARA
    public float maxViewAngle = 45;
    public float minViewAngle = -30;
    public bool invertY;



    // Start is called before the first frame update
    void Start()
    {
        if (!useOffsetValues)
        {
            offset = target.position - transform.position;
        }

        pivot.transform.position = target.transform.position;
        // pivot.transform.parent = target.transform;
        pivot.transform.parent = null;

        cameraOptions.cursorLock();
    }

    void LateUpdate()
    {
		if (!target.GetComponent<pause>().Paused)
		{
			pivot.transform.position = target.transform.position;


			// GET THE X POSITION OF THE MOUSE AND ROTATE THE CAMERA
			float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
			// target.Rotate(0.0f, horizontal, 0.0f);
			pivot.Rotate(0.0f, horizontal, 0.0f);

			// GET THE Y POSITION OF THE MOUSE AND ROTATE THE PIVOT
			float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;

			if (invertY)
			{
				pivot.Rotate(vertical, 0.0f, 0.0f);
			}
			else
			{
				pivot.Rotate(-vertical, 0.0f, 0.0f);
			}

			// LIMIT UP/DOWN CAMERA ROTATION
			if (pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
			{
				// pivot.rotation = Quaternion.Euler(maxViewAngle, 0.0f, 0.0f);
			}
			if (pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + minViewAngle)
			{
				// pivot.rotation = Quaternion.Euler(360f + minViewAngle, 0.0f, 0.0f);
			}

			// MOVE THE CAMERA BASED ON THE CURRENT ROTATION OF THE TARGET AND THE ORIGINAL OFFSET
			float desiredYAngle = pivot.eulerAngles.y;
			float desiredXAngle = pivot.eulerAngles.x;
			Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0.0f);
			transform.position = target.position - (rotation * offset);

			if (transform.position.y < target.position.y)
			{
				transform.position = new Vector3(transform.position.x, target.position.y - 0.5f, target.position.z);
			}

			transform.LookAt(target); // MIRAR AL TARGET ESPECIFICO
		}
	}
}
