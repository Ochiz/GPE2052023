using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class PlayerController : Controller
{
    public KeyCode moveForwardKey;
    public KeyCode moveBackwardKey;
    public KeyCode rotateClockwiseKey;
    public KeyCode rotateCounterClockwiseKey;
    public KeyCode sprintKey;
    public KeyCode shootKey;
    public Text scoreHud;
    public Text lifeHud;
    private float progressToExtraLife;
    public float playerScore;
    public int playerLives;
    public float scoreToExtraLife;
    // Start is called before the first frame update
    public override void Start()
    {
        //null check
        if(GameManager.instance != null)
        {
            if(GameManager.instance.players != null)
            {
                GameManager.instance.players.Add(this);
            }
        }
        //scoreHud = pawn.GetComponent<ScoreText>();
       // lifeHud = pawn.Canvas.LifeText;
       // scoreHud.text = playerScore.ToString();
        //lifeHud.text = playerLives.ToString();
        progressToExtraLife = 0;
        // Run the Start() function from the parent (base) class
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        // Process our Keyboard Inputs
        ProcessInputs();

        // Run the Update() function from the parent (base) class
        base.Update();
    }
    //function called when destroyed
    public void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.players != null)
            {
                GameManager.instance.players.Remove(this);
            }
        }
    }
    //function to check player inputs
    public override void ProcessInputs()
    {
        if (Input.GetKey(moveForwardKey))
        {
            if (Input.GetKey(sprintKey))
            {
                pawn.MoveForward(true);
            }
            else
            {
                pawn.MoveForward(false);
            }
        }

        if (Input.GetKey(moveBackwardKey))
        {
            pawn.MoveBackward();
        }

        if (Input.GetKey(rotateClockwiseKey))
        {
            pawn.RotateClockwise();
        }

        if (Input.GetKey(rotateCounterClockwiseKey))
        {
            pawn.RotateCounterClockwise();
        }
        if (Input.GetKeyDown(shootKey))
        {
            pawn.Shoot();
        }
    }
    public override void AddToScore(float scoreToAdd)
    {
        playerScore = playerScore + scoreToAdd;
        scoreHud.text = playerScore.ToString();
        progressToExtraLife = progressToExtraLife + scoreToAdd;
        if(progressToExtraLife >= scoreToExtraLife)
        {
            playerLives += 1;
            lifeHud.text = playerLives.ToString();
            progressToExtraLife = 0;
        }
    }
}