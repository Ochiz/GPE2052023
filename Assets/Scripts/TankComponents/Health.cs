using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Health : MonoBehaviour
{
    //variable declaration
    public float currentHealth;
    public float maxHealth;
    public Image healthImage;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float amount, Pawn source)
    {
        currentHealth = currentHealth - amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthImage.fillAmount = currentHealth / maxHealth;
        if (GetComponent<PlayerController>() != null)
        {
            GetComponent<AudioSources>().SFXSource.PlayOneShot(GetComponent<PlayerController>().DmgSFX);
        }
        Debug.Log(source.name + " did" + amount + " damage to " + gameObject.name);
        if (currentHealth <= 0)
        {
            if (GetComponent<PlayerController>() != null)
            {
                if (GetComponent<PlayerController>().playerLives > 0)
                {
                    currentHealth = maxHealth;
                    GetComponent<PlayerController>().playerLives -= 1;
                    GetComponent<PlayerController>().lifeHud.text = GetComponent<PlayerController>().playerLives.ToString();
                }
                else
                {                     
                    Die(source);
                }
            }
            else
            {
                Die(source);
            }
        }
    }
    public void Heal(float amount, Pawn source)
    {
        currentHealth = currentHealth + amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthImage.fillAmount = currentHealth / maxHealth;
        Debug.Log(source.name + " did" + amount + " healing to " + gameObject.name);
    }
    public void Die(Pawn source)
    {
        Destroy(gameObject);
        source.controller.AddToScore(source.controller.scoreForKill);
    }
}
