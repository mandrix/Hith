using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNivel1 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject coronelDialog;

    void Start()
    {
        coronelDialog.GetComponent<DialogHandler>().RoutuineDialogStartnivel1();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
