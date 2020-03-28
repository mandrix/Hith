using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]
public class sePuedeRecojer : MonoBehaviour
{
	// Este script ayuda en que si el jugador está en el rango del collider de este objeto
	// le permite presionar espacio y poder recolectar la pieza de la nave espacial
	#region Variables
	public enum nombreDePiezas // your custom enumeration
    {
        puerta1,
        pistola4,
        pistola3,
        pistola2,
        pistola1,
        ala1,
        ala2,
        motor4,
        motor3,
        flaps2,
        motor2,
        puerta2,
        motor1,
        flaps1,
        rampa,
        vidrio,
        vidrio2,
        naveatras,
        navedelantera,
        lore
    };

    [SerializeField]
    private nombreDePiezas nombreDePieza;
    [SerializeField]
    private GameObject jugador;
    [SerializeField]
    [Range(0, 6)]
    private float tiempoParaRecojer;
    [SerializeField]
    private GameObject nave;
    [SerializeField]
    private float minDistanceForHighlight;

    private bool flag = false;
	public bool PickedUp
	{
		get { return flag; }
	}

    // AGREGADO X JUAN
    public AudioSource winTheme;
    private AudioSource[] allAudioSources;
    private bool winState = false;
    private int esperaBaile = 20;
    private int tiempoVentaWin = 5;
    public GameObject winStateUI;
	#endregion

	#region Unity Methods
	private void Update()
    {
		if (jugador.GetComponent<Pickup>().canPickup && flag)
		{
			AgregarInverntario();
		}

        // minDistanceForHighlight = 20f;
        float distance = Vector3.Distance(jugador.transform.position, transform.position);

        if (distance <= minDistanceForHighlight)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(jugador.tag))
        {
            if (Input.GetButtonDown("Fire1") && !flag)
            {
                flag = true;
            }
        }
    }

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag(jugador.tag))
		{
			flag = false;
		}
	}
	#endregion

	#region Custom methods
	void AgregarInverntario()
    {

        if (nombreDePieza != nombreDePiezas.lore)
		{ 
            nave.GetComponent<armarNave>().armar(nombreDePieza);
        }
        else
        {
			jugador.GetComponent<Inventario>().agregarAInventarioLore(gameObject.GetComponent<lore>().getLoreText());
			jugador.GetComponent<Inventario>().saveLore();
			GetComponent<lore>().showLore();
        }

        // AGREGADO POR JUAN
        try
        {
            if (nombreDePieza != nombreDePiezas.lore)
            {
                if (nave.GetComponent<armarNave>().estaCompleta())
                {
                    winState = true;
                    stopAllAudio();
                    jugador.GetComponent<movement>().enabled = false;
                    jugador.GetComponent<Animator>().SetBool("dance", true);
                    winTheme.Play();
                    winTheme.volume = 1.0f;

                    StartCoroutine(esperaAnimacionBaile());

                }
            }
        }
        catch (UnityException e)
        {
            Debug.LogError(e.Message);
        }



        Destroy(gameObject, 0.10f);
    }

    void stopAllAudio()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Stop();
        }
    }

    public nombreDePiezas GetNombreDePieza()
    {
        return nombreDePieza;
    }

    IEnumerator esperaAnimacionBaile()
    {
        yield return new WaitForSeconds(esperaBaile);
        winStateUI.SetActive(true);
        StartCoroutine(cambiarEscena());
    }

    IEnumerator cambiarEscena()
    {
        yield return new WaitForSeconds(tiempoVentaWin);
        SceneManager.LoadScene("Inicio");
    }
	#endregion
}
