using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    private GameObject[] windows;
    private GameObject window;
    public Sprite[] sprites;
    private SpriteRenderer SprRender;
    private int reset = 0;
    public int state = 0;
    public GameObject[] items;
    public GameObject UIwasd;
    public float stress; 
    public GameObject camera;
    private StressReceiver receiver;
    public GameObject coronelDialog;
    public GameObject player;
    public switchScenes switchScene;
    public int switchfinal = 0;
    // Update is called once per frame
    void Update()
    {

        if (state == 0)
        {
            coronelDialog.GetComponent<DialogHandler>().RoutineDialogStart();
            state = -1;
        }
        else if (state == 1)
        {
            FirstStep(true);
            state = -1;
        }
        else if (state == 2)
        {
            FirstStep(false);
            SecondStep();

        }
        else if (state == 3)
        {
            ThirdStep();
            state = -1;
        }
        
    }

    private void FirstStep(bool active)
    {
        //gameObject.SetActive(true);
        int indice = 0;
        int max = items.Length;
        for (; indice < max;)
        {
            items[indice++].gameObject.SetActive(active);
        };
    }

    private void SecondStep()
    {
        switchfinal += 1;
        if (switchfinal >= 360)
            state = 3;
        camera.GetComponent<StressReceiver>().InduceStress(stress);
    }

    private void ThirdStep() {
        camera.GetComponent<StressReceiver>().InduceStress(0);
        StartGame();
    }

    public void SetState(int newState) {
        state = newState;
    }

    public void createuiwasd() {
        UIwasd.SetActive(true);
    }

    private void StartGame() {
        SceneManager.LoadScene("nivel1");
    }
}
