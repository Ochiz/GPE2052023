using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PointsPickup : MonoBehaviour
{
    public PointsPowerup powerup;
    public AudioClip pickup;
    public AudioSource sfxSource;
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
        sfxSource.PlayOneShot(pickup);
        PowerupManager powerupManager = other.GetComponent<PowerupManager>();

        if (powerupManager != null)
        {
            powerupManager.Add(powerup);

            Destroy(gameObject);

            GameManager.instance.level.totalPowerups -= 1;
        }
    }
}
