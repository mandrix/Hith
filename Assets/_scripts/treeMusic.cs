using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class treeMusic : MonoBehaviour
{
	[SerializeField]
	private AudioSource treeAudio;
	[SerializeField]
	private AudioSource ambientAudio;
	[SerializeField]
	private GameObject player;

	private float distanceToPlayerInitial;
	private float distanceToPlayer;
	private bool inRadius = false;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag(player.tag) && !inRadius)
		{
			distanceToPlayerInitial = Vector3.Distance(player.transform.position, transform.position);
			inRadius = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag(player.tag))
		{
			inRadius = false;
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag(player.tag))
		{
			distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

			float percentageAmbient = treeMusic.percentageVolAmbient(distanceToPlayerInitial, distanceToPlayer);
			ambientAudio.volume = percentageAmbient/100;
			//Debug.Log(string.Format("Ambient {0}%", percentageAmbient));

			float percentageTree = treeMusic.percentageVolTree(distanceToPlayerInitial, distanceToPlayer);
			treeAudio.volume = percentageTree / 100;
			Debug.Log(string.Format("Tree {0}%", percentageTree));

		}
	}

	static float percentageVolAmbient(float initialDistance, float currentDistance)
	{
		float result = (currentDistance / initialDistance) * 100;
		return result;
	}

	static float percentageVolTree(float initialDistance, float currentDistance)
	{
		float result = 100 - ((currentDistance / initialDistance) * 100);
		return result;
	}
}
