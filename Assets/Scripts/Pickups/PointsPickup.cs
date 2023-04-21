using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsPickup : MonoBehaviour
{
    public PointsPowerup powerup;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter(Collider other)
    {
        PowerupManager powerupManager = other.GetComponent<PowerupManager>();

        if (powerupManager != null)
        {
            powerupManager.Add(powerup);

            Destroy(gameObject);

            GameManager.instance.level.totalPowerups -= 1;
        }
    }
}