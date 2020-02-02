using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lore : MonoBehaviour
{
	[SerializeField]
	[TextArea]
	private string loreText;
	[SerializeField]
	private GameObject loreUI;
	[Space]
	[Header("Fonts")]
	[SerializeField]
	private TMPro.TMP_FontAsset font;
	public void showLore()
	{
		loreUI.SetActive(true);
		loreUI.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = loreText;
		loreUI.GetComponentInChildren<TMPro.TextMeshProUGUI>().font = font;
	}

	public string getLoreText()
	{
		return loreText;
	}
}
