using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tanqueUI : MonoBehaviour
{
	[SerializeField]
	private GameObject player;
    // Update is called once per frame
    void LateUpdate()
    {
		RectTransform rt = GetComponent<RectTransform>();
		float airLevel = player.GetComponent<airTankLevel>().getAirLevel();
		if(airLevel >= 0)
		{
			float height = -(90 - ((airLevel / 100) * 90));
			rt.sizeDelta = new Vector2(100, height);
		}
		
    }

}
