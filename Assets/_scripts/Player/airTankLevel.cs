using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class airTankLevel : MonoBehaviour
{
    public GameObject airTank;
    [SerializeField]
    private float airLevel = 100;
    public float coef = 0.1f;
    public float coef_copy;

    private Animator playerAnimator;
    private float initialYScale;

    public AudioSource deathTheme;
    private AudioSource[] allAudioSources;
    private bool isPlayerDeath = false;
	public bool IsPlayerDeath
	{
		get { return isPlayerDeath; }
	}

    public GameObject camera;
    public GameObject gameOverUi;
    private int tiempoAnimacionMuerte = 6;
    private int tiempoVentanaMuerte = 5;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        initialYScale = airTank.transform.localScale.y;
        coef_copy = coef;
    }
    
    void LateUpdate()
    {
        if (!isPlayerDeath)
        {
            // DEATH STATE
            if (airLevel <= 0)
            {
                isPlayerDeath = true;

                GetComponent<movement>().enabled = false;
                camera.GetComponent<cameraController>().enabled = false;

                // DETENER TODOS LOS AUDIOS Y REPRODUCIR EL DE MUERTE
                // GetComponentsInChildren
                stopAllAudio();
                deathTheme.volume = 1.0f;

                playerAnimator.SetTrigger("die");
                deathTheme.Play();

                StartCoroutine(mostrarGameOverUI());

                Destroy(airTank, 1);
            }
            else
            {
                try
                {
                    // IS ALIVE
                    airLevel -= coef * Time.deltaTime;
                    airTank.transform.localScale = new Vector3(airTank.transform.localScale.x, (airLevel / 100) * initialYScale, airTank.transform.localScale.z);
                }
                catch (UnityException e)
                {
                    Debug.LogError(e.Message);
                    airLevel = 0;
                }

            }
        }
    }

    private void stopAllAudio()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Stop();
        }
    }

    public float getAirLevel()
    {
        return airLevel;
    }

    public void setAirLevel(float _airlevel)
    {
        airLevel = _airlevel;
    }

    public void addAir(float health)
    {
        airLevel += health;

        if (airLevel > 100)
        {
            airLevel = 100;
        }
    }

    public void reduceAir(float damage)
    {
        airLevel -= damage;

        if (airLevel < 0)
        {
            airLevel = 0;
        }
    }


    IEnumerator mostrarGameOverUI()
    {
        yield return new WaitForSeconds(tiempoAnimacionMuerte);
        gameOverUi.SetActive(true);
        StartCoroutine(cambiarEscena());
    }

    IEnumerator cambiarEscena()
    {
        yield return new WaitForSeconds(tiempoVentanaMuerte);
        SceneManager.LoadScene("Inicio");
    }
}
