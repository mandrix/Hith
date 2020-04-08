using UnityEngine;
// Creado por JZ1999

public class HighlightLimiter : MonoBehaviour
{
	#region Variables
	private Transform player;
	[SerializeField]
	[Range(1, 100)]
	private float minDistanceForHighlight;
	#endregion

	#region Unity Methods
	private void Start()
	{
		player = GameObject.Find("_astronaut").transform;
	}

	void Update()
    {
		float distance = Vector3.Distance(player.position, transform.position);

		if (distance <= minDistanceForHighlight)
		{
			foreach (Transform child in transform)
			{
				child.gameObject.SetActive(true);
			}
		}
		else
		{
			foreach (Transform child in transform)
			{
				child.gameObject.SetActive(false);
			}
		}
	}
    #endregion

    #region Custom methods

    #endregion
}
