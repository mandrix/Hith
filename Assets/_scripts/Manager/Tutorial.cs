using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private GameObject[] windows;
    private GameObject window;
    public Sprite[] sprites;
    private SpriteRenderer SprRender;
    private int reset = 0;
    public int state = 0;
    public GameObject[] items;
    public float stress; 
    public GameObject camera;
    private StressReceiver receiver;
    public GameObject coronelDialog;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 0) {
            dialogueStep();
        }
        if (state == 1) {
            FirstStep();
        }
        else if (state == 2) {
            SecondStep();
            state += 1;
        }

    }

    private void SecondStep() {
        camera.GetComponent<StressReceiver>().InduceStress(stress);
    }

    private void FirstStep() {
        //gameObject.SetActive(true);
        int indice = 0;
        int max = items.Length;
        for (;indice<max;) {
            items[indice++].gameObject.SetActive(true); };
    }

    private void dialogueStep() {
        coronelDialog.GetComponent<DialogHandler>().StartCoroutine("StartDialog");
    }
}
