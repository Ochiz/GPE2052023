using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public abstract class Controller : MonoBehaviour
{
    //variable declaration
    public Pawn pawn;
    public float playerScore;
    public int playerLives;
    public int scoreForKill;
    public float scoreToExtraLife;
    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }
    //functions to be overwritten
    public abstract void ProcessInputs();
    public abstract void AddToScore(float scoreToAdd);
}
