using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeCollider : MonoBehaviour
{
    [SerializeField]
    public GameObject player;

    private float distanceToPlayerInitial;
    private float distanceToPlayer;

    private treeMusic musicSettings;
    private settingsToSaved saved;
    private airTankLevel tank;

    [SerializeField]
    private bool getTree = true;


    [SerializeField]
    [Range(0, 100)]
    private int oxygenToAdd;
    //unity functions


    private void Start()
    {
        musicSettings = GetComponent<treeMusic>();
        saved = player.GetComponent<settingsToSaved>();
        tank = player.GetComponent<airTankLevel>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(player.tag))
        {
            distanceToPlayerInitial = Vector3.Distance(player.transform.position, transform.position);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(player.tag))
        {
            distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            float[] volumes = volume(distanceToPlayer, distanceToPlayerInitial);
            musicSettings.manejarMusica(volumes[0], volumes[1]);
            if (getTree && distanceToPlayer <= distanceToPlayerInitial / 2)
            {
                getTree = false;
                tank.addAir(oxygenToAdd);
                saved.Saving();
            }
        }
    }

    //custom functions
    static float[] volume(float initialDistance, float currentDistance)
    {
        float[] result = {0f,0f};
        result[0]= (currentDistance / initialDistance);
        result[1] = (1- result[0]);
        return result;
    }




}
