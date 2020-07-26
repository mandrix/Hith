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
    public GameObject minimap;
    public GameObject minimapUI;


    public void RoutuineDialogStartnivel1() {
        minimap.SetActive(false);
        minimapUI.SetActive(false);
        StartCoroutine("StartDialog", 3);
    }

    public void SetMinimap() {
        PlayCtrl();
        minimap.SetActive(true);
        minimapUI.SetActive(true);
    }
    public void RoutineDialogStart() {
        firstDialog();
    }

    IEnumerator StartDialog(int dialogue) {
        yield return new WaitForSecondsRealtime(2);
        dialogs[dialogue].HasCloseButton = false;
        OpenDialog(transform, 0, dialogs[dialogue]);
        StopCtrl();
    }

    public void firstDialog() {
        StopCoroutine("StartDialog");
        StartCoroutine("StartDialog", 0);
    }

    public void SecondDialog()
    {
        PlayCtrl();
        StopAllCoroutines();
        StartCoroutine("StartDialog", 1);
    }

    public void ThirdDialog() {
        PlayCtrl();
        StopAllCoroutines();
        StartCoroutine("StartDialog", 2);
    }

    public void NextFirst(int next) {
        StopCoroutine("StartDialog");
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

    public void SetCloseBottom(int indice) {
        dialogs[indice].HasCloseButton = true;
    }
    public void SetState() {
        tutorial.GetComponent<Tutorial>().SetState(1);
    }

    public void obtainUIWASD() {
        tutorial.GetComponent<Tutorial>().createuiwasd();
    }

    public void nextQ() {
        SetCloseBottom(1);
        OpenDialog(transform, 1, dialogs[1]);
    }
}
