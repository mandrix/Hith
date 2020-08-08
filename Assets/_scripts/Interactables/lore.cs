using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lore : Interactable
{
    [Header("Custom Variables")]
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


    protected override void Start()
    {
        Debug.Log(minimapUI.activeSelf);
        minimapUI.SetActive(false);
        Debug.Log(minimapUI.activeSelf);
        base.Start();
    }

    override protected void Interact()
	{
		minimapUI.SetActive(false);
		gasTankUI.SetActive(false);
		loreUI.SetActive(true);
		loreUI.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = loreText;
		loreUI.GetComponentInChildren<TMPro.TextMeshProUGUI>().font = font;
        //base.Interact();
	}

	public string getLoreText()
	{
		return loreText;
	}
}
