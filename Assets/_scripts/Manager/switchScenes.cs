using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class switchScenes : MonoBehaviour
{
	private ES3File es3File;

	private void Start()
    {
		es3File = new ES3File("save.es3");

		cameraOptions.cursorUnlock();
    }

    public void switchScene(string name)
	{
		SceneManager.LoadScene(name);
	}

	public void switchSceneAndRemoveSave(string name)
	{
		es3File.Save<Vector3>("user-v3", Vector3.zero);
		es3File.Save<float>("user-oxygen", 100f);
		es3File.Save<List<sePuedeRecojer.nombreDePiezas>>("user-ship-parts", new List<sePuedeRecojer.nombreDePiezas>());
		SceneManager.LoadScene(name);
	}
}
