using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;

public class DialogHandler : NPCBehaviour
{
    [Range(1, 10)]
    public int waitSeconds = 10;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("StartDialog");
    }


    IEnumerator StartDialog() {
        yield return new WaitForSecondsRealtime(waitSeconds);
        OpenDialog(transform);
        player.GetComponent<pause>().Paused = true;
        cameraOptions.cursorUnlock();
        cameraOptions.cursorSetVisible(true);

    }


}
