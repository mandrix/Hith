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
    private GameObject player;
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

	[SerializeField]
	private Item itemInfo;

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
		if (player.GetComponent<Pickup>().canPickup && flag)
		{
			AddToInventory();
		}

        // minDistanceForHighlight = 20f;
        float distance = Vector3.Distance(player.transform.position, transform.position);

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
        if (other.CompareTag(player.tag))
        {
            if (Input.GetButtonDown("Fire1") && !flag)
            {
				player.GetComponent<movement>().setCanPick(true);
				flag = true;
            }
        }
    }

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag(player.tag))
		{
			player.GetComponent<movement>().setCanPick(false);
			flag = false;
		}
	}
	#endregion

	#region Custom methods
	void AddToInventory()
    {

        if (nombreDePieza != nombreDePiezas.lore)
		{
			bool wasPickedUp = Inventory.instance.Add(itemInfo);    // Add to inventory
			nave.GetComponent<armarNave>().armar(nombreDePieza);	
        }
        else
        {
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
                    player.GetComponent<movement>().enabled = false;
                    player.GetComponent<Animator>().SetBool("dance", true);
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


		flag = false;
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
