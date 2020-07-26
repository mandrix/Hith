using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lore : MonoBehaviour
{
	[SerializeField]
	private GameObject minimapUI;
	[SerializeField]
	private GameObject gasTankUI;
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
		minimapUI.SetActive(false);
		gasTankUI.SetActive(false);
		loreUI.SetActive(true);
		loreUI.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = loreText;
		loreUI.GetComponentInChildren<TMPro.TextMeshProUGUI>().font = font;
	}

	public string getLoreText()
	{
		return loreText;
	}
}
