using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class treeMusic : MonoBehaviour
{
	[SerializeField]
	public AudioSource treeAudio;
	[SerializeField]
	public AudioSource ambientAudio;

    public void manejarMusica(float volumeAmbient, float volumeTree)
    {
        ambientAudio.volume = volumeAmbient;
        treeAudio.volume = volumeTree;
    }

}
