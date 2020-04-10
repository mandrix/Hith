using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;

public class DialogHandler : NPCBehaviour
{
    [Range(1, 10)]
    public int waitSeconds = 10;
    public GameObject player;
    public GameObject tutorial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RoutineDialogStart() {
        StopCoroutine("StartDialog");
        StartCoroutine("StartDialog", 0);
    }

    IEnumerator StartDialog(int dialogue) {
        yield return new WaitForSecondsRealtime(2);
        OpenDialog(transform, 0, dialogs[dialogue]);
        StopCtrl();
    }

    public void SecondDialog()
    {
        PlayCtrl();
        StopAllCoroutines();
        StartCoroutine("StartDialog", 1);
    }

    public void StopCtrl()
    {
        player.GetComponent<pause>().Paused = true;
        cameraOptions.cursorUnlock();
    }

    public void PlayCtrl()
    {
        cameraOptions.cursorLock();
        player.GetComponent<pause>().Paused = false;
    }
    public void SetState() {
        tutorial.GetComponent<Tutorial>().SetState(1);
    }
}
