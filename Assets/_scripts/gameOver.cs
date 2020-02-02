using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOver : MonoBehaviour
{
    public GameObject player;
    public GameObject camera;
    public int tiempoDeEspera = 5;

    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<movement>().enabled = false;
        camera.GetComponent<cameraController>().enabled = false;
        StartCoroutine(changeScene());
        Debug.Log("Cerrando escena en 5 segundos");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator changeScene() {
        yield return new WaitForSeconds(tiempoDeEspera);
        SceneManager.LoadScene("Inicio");
    }
}
