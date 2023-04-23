using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Health : MonoBehaviour
{
    //variable declaration
    private AudioClip dmg;
    private AudioClip death;
    public float currentHealth;
    public float maxHealth;
    public Image healthImage;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        dmg = GetComponent<Pawn>().controller.DmgSFX;
        death = GetComponent<Pawn>().controller.DeathSFX;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float amount, Pawn source)
    {
        //first take damage
        currentHealth = currentHealth - amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthImage.fillAmount = currentHealth / maxHealth;
        //play damage sound
        GetComponent<AudioSources>().SFXSource.PlayOneShot(dmg);
        
        Debug.Log(source.name + " did" + amount + " damage to " + gameObject.name);
        //check if health is less than 0
        if (currentHealth <= 0) 
        {
            //check if pawn is player
            if (GetComponent<Pawn>().controller is PlayerController)
            {
                //check if player has another life
                if (GetComponent<Pawn>().controller.playerLives > 0)
                {
                    currentHealth = maxHealth;
                    GetComponent<Pawn>().controller.playerLives -= 1;
                    GetComponent<Pawn>().GetComponent<HudValues>().lifeHud.text = "Lives: " + GetComponent<Pawn>().controller.playerLives.ToString();
                    
                }
                else
                {   
                    //null check
                    if(GameManager.instance != null)
                    {
                        //check if all players have 0 lives
                        int count = 0;
                        foreach (PlayerController player in GameManager.instance.players)
                        {
                            if(player.playerLives <= 0)
                            {
                                count++;
                            }
                        }
                        //if everyone is dead end game
                        if(count == GameManager.instance.players.Count)
                        {
                            GameManager.instance.DoGameOverState();
                        }
                        else
                        {
                            count = 0;
                        }

                    }
                    Die(source);
                }
            }
            else
            {
                Die(source);
            }
        }
    }
    //heal function
    public void Heal(float amount, Pawn source)
    {
        currentHealth = currentHealth + amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthImage.fillAmount = currentHealth / maxHealth;
        Debug.Log(source.name + " did" + amount + " healing to " + gameObject.name);
    }
    //die function
    public void Die(Pawn source)
    {
        GetComponent<AudioSources>().SFXSource.PlayOneShot(death);
        Destroy(gameObject);
        source.controller.AddToScore(source.controller.scoreForKill);
    }
}
