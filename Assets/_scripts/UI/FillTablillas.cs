using UnityEngine;
using System.Collections.Generic;
using TMPro;
// Creado por JZ1999

public class FillTablillas : MonoBehaviour
{
	#region Variables
	public HashSet<string> lores;
	[SerializeField]
	private GameObject lorePrefab;

	private ES3File es3File;
	#endregion

	#region Unity Methods
	void Start()
    {
		es3File = new ES3File("save.es3");

		lores = es3File.Load("lores", new HashSet<string>());

		int counter = 1;
		foreach (string lore in lores)
		{
			createTablillaEntry(lore, counter);
			counter++;
		}
    }
    #endregion

    #region Custom methods
	void createTablillaEntry(string loreText, int id)
	{
		GameObject instance = Instantiate(lorePrefab, transform);

		TextMeshProUGUI description = instance.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
		TextMeshProUGUI title = instance.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

		title.text = "#" + id.ToString();
		description.text = loreText;
	}
    #endregion
}
