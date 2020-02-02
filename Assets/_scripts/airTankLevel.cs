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
        // DEATH STATE
        if (airLevel <= 0)
        {
            playerAnimator.SetTrigger("die");
            GetComponent<movement>().enabled = false;
            Destroy(airTank, 1);

        }
        else
        {
            try
            {
                // IS ALIVE
                airLevel -= coef * Time.deltaTime;
                airTank.transform.localScale = new Vector3(airTank.transform.localScale.x, (airLevel/100) * initialYScale, airTank.transform.localScale.z);
            }
            catch (UnityException e)
            {
                Debug.LogError(e.Message);
                airLevel = 0;
            }
            
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

        if(airLevel > 100)
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
