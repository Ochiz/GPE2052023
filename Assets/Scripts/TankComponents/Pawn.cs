using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public abstract class Pawn : MonoBehaviour
{
    //variable declaration
    public Controller controller;
    public float moveSpeed;
    public float sprintSpeed;
    public float turnSpeed;
    public Mover mover;
    public Shooter shooter;
    public Health health;
    public GameObject shellPrefab;
    public float fireForce;
    public float damageDone;
    public float shellLifespan;
    public float fireRate;
    protected float secondsPerShot;
    // Start is called before the first frame update
    public virtual void Start()
    {
        secondsPerShot = 1 / fireRate;
        //get statements
        mover = GetComponent<Mover>();
        shooter = GetComponent<Shooter>();
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }
    //functions to be overwritten
    public abstract void MoveForward(bool sprint);
    public abstract void MoveBackward();
    public abstract void RotateClockwise();
    public abstract void RotateCounterClockwise();
    public abstract void Shoot();
    public abstract void RotateTowards(Vector3 targetPosition);
}
