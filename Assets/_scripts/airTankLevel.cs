using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class airTankLevel : MonoBehaviour
{
    public GameObject player;
    [SerializeField]
    private float airLevel = 100;
    public float coef = 0.5f;

    private Animator playerAnimator;
    private float initialYScale;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = player.GetComponent<Animator>();
        initialYScale = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        // DEATH STATE
        if (airLevel <= 0)
        {
            playerAnimator.SetTrigger("die");
            player.GetComponent<movement>().enabled = false;
            Debug.Log("jugador muerto");
            Destroy(gameObject, 1);

        }
        else
        {
            try
            {
                // IS ALIVE
                airLevel -= coef * Time.deltaTime;
                transform.localScale = new Vector3(transform.localScale.x, (airLevel/100) * initialYScale, transform.localScale.z);
            }
            catch (UnityException e)
            {
                Debug.LogError(e.Message);
            }
            
        }
    }

    public void addAir(float health)
    {
        airLevel += health;
    }

    public void reduceAir(float damage)
    {
        airLevel -= damage;
    }

}
