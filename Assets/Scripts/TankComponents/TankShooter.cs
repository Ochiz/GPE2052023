using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TankShooter : Shooter
{
    
    public Transform firepointTransform;
    // Start is called before the first frame update
    public override void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }
    //function to shoot a shell
    public override void Shoot(GameObject shellPrefab, float fireForce, float damageDone, float lifespan)
    {
        GameObject newShell = Instantiate(shellPrefab, firepointTransform.position, firepointTransform.rotation) as GameObject;
        DamageOnHit doh = newShell.GetComponent<DamageOnHit>();
        if (doh != null)
        {
            doh.damageDone = damageDone;
            doh.owner = GetComponent<Pawn>();
        }
        Rigidbody rb = newShell.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(firepointTransform.forward * fireForce);
        }
        if (GetComponent<PlayerController>() != null)
        {
            GetComponent<AudioSources>().SFXSource.PlayOneShot(GetComponent<PlayerController>().ShootSFX);
        }
        Destroy(newShell, lifespan);
        
    }
}
