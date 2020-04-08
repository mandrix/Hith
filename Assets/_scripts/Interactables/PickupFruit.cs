using UnityEngine;
// Creado por JZ1999

public class PickupFruit : MonoBehaviour
{
	#region Variables
	[SerializeField]
	private string fruitTag;
	[SerializeField]
	[Range(1, 100)]
	private int oxygenAmount = 50;
	[SerializeField]
	private GameObject oxygenEffect;

	private ParticleSystem particles;
	#endregion

	#region Unity Methods
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag(fruitTag))
		{
			GetComponent<airTankLevel>().addAir(oxygenAmount);
			Destroy(other.gameObject);
			particles = Instantiate(oxygenEffect, transform).GetComponent<ParticleSystem>();
		}
	}

	private void Update()
	{
		if (particles)
		{
			if (!particles.IsAlive() || particles.isStopped)
			{
				Destroy(particles);
			}
		}
	}
	#endregion

	#region Custom methods

	#endregion
}
