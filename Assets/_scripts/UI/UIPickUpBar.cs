using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPickUpBar : MonoBehaviour
{
    public GameObject player;

    public Vector3 positionInitial;
    public Vector3 positionUpdate;
    public Transform camara;
    public Image pickUpBar;
    // Start is called before the first frame update
    void Start()
    {
        pickUpBar = transform.Find("PickUpBarBG").Find("PickUpBar").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<pause>().Paused == false)
        {
            pickUpBar.fillAmount -= 0.02f;
            positionUpdate = player.transform.position;
            gameObject.transform.LookAt(camara);
            if (positionUpdate.x != positionInitial.x || positionUpdate.z != positionInitial.z)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void SetPositionPlayer(Vector3 position) {
        positionInitial = position;
        pickUpBar.fillAmount = 100;
    }
}
