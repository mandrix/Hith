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
    public GameObject item;
    // Start is called before the first frame update
    void Start()
    {
        SearchWindows();
        RenderWindows();
    }

    // Update is called once per frame
    void Update()
    {
        if (reset++ == 25) {
            RenderWindows();
            reset = 0;
        }
        if (state == 1) { FirstStep(); }
        else if (state == 2) { }

    }

    private void SearchWindows() {
        windows = GameObject.FindGameObjectsWithTag("window");
    }

    private void RenderWindows() {
        int indice = 1;
        int maxWindows = windows.Length;
        int maxSprites = sprites.Length;
        int rnd;
        while (indice != maxWindows) {
            window = windows[indice++];
            rnd = Random.Range(0, maxSprites);
            SprRender = window.GetComponent<SpriteRenderer>();
            SprRender.sprite = sprites[rnd];
        }
        
    }

    private void FirstStep() {
        //gameObject.SetActive(true);
        item.gameObject.SetActive(true);
    }
}
