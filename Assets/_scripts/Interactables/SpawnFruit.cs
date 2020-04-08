using System;
using System.Collections.Generic;
using UnityEngine;
// Creado por JZ1999

public class SpawnFruit : MonoBehaviour
{
	#region Variables
	[SerializeField]
	[Tooltip("GameObject to spawn")]
	private GameObject fruit;
	[SerializeField]
	[Tooltip("Player Transform to track when we should spawn the fruits")]
	private Transform player;
	[SerializeField]
	[Tooltip("Distance to indicate when we can spawn the fruit")]
	[Range(1, 300)]
	private int distance;

	private List<Vector3> spawnPoints = new List<Vector3>();
	private System.Random rng = new System.Random(Guid.NewGuid().GetHashCode());
	private bool spawned = false;
	#endregion

	#region Unity Methods
	void Start()
	{
		obtainVectors();		
		Shuffle(spawnPoints);
	}

	private void Update()
	{
		if (Vector3.Distance(player.position, transform.position) < distance && !spawned)
		{
			spawned = true;
			for (int i = 0; i < 4; i++)
			{
				Instantiate(fruit, spawnPoints[i], Quaternion.identity);
			}
		}
	}

	private void obtainVectors()
	{
		int counter = 0;
		while (transform.childCount > counter)
		{
			spawnPoints.Add(transform.GetChild(counter).position);

			counter++;
		}
	}
	#endregion

	#region Custom methods

	public void Shuffle<T>(IList<T> list)
	{
		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = rng.Next(n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}
	#endregion
}
