using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class airTankLevel : MonoBehaviour
{
    public GameObject airTank;
    [SerializeField]
    private float airLevel = 100;
    public float coef = 0.5f;
    public float coef_copy;

    private Animator playerAnimator;
    private float initialYScale;

    public AudioSource deathTheme;
    private AudioSource[] allAudioSources;
    private bool isPlayerDeath = false;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        initialYScale = airTank.transform.localScale.y;
        coef_copy = coef;
    }

    // Update is called once per frame
    void Update()
    {

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
                playerAnimator.SetTrigger("die");

                // DETENER TODOS LOS AUDIOS Y REPRODUCIR EL DE MUERTE
                // GetComponentsInChildren
                stopAllAudio();
                deathTheme.volume = 1.0f;
                deathTheme.Play();

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

    void stopAllAudio()
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

}
