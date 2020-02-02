using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class switchScenes : MonoBehaviour
{
    public void switchScene(string name)
	{
		SceneManager.LoadScene(name);
	}

	public void switchSceneAndRemoveSave(string name)
	{
		ES3.Save<Vector3>("user-v3", Vector3.zero);
		ES3.Save<float>("user-oxygen", 100f);
		ES3.Save<List<sePuedeRecojer.nombreDePiezas>>("user-ship-parts", new List<sePuedeRecojer.nombreDePiezas>());
		SceneManager.LoadScene(name);
	}
}
