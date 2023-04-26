using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePickup : Pickup
{
    public DamagePowerup powerup;
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
    public override void OnTriggerEnter(Collider other)
    {
        PowerupManager powerupManager = other.GetComponent<PowerupManager>();
        sfxSource.PlayOneShot(pickup);
        if (powerupManager != null)
        {
            powerupManager.Add(powerup);

            Destroy(gameObject);
            GameManager.instance.level.totalPowerups -= 1;
        }
    }
    public override void RotateClockwise()
    {
        mover.Rotate(turnSpeed);
    }
}
