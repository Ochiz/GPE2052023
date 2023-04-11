using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Powerup> powerups;
    private List<Powerup> removedPowerupQueue;
    public virtual void Start()
    {
        powerups = new List<Powerup>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void LateUpdate()
    {
        ApllyRemovePowerupsQueue();
    }

    public void Add(Powerup powerupToAdd)
    {
        powerupToAdd.Apply(this);
        powerups.Add(powerupToAdd);
    }

    public void Remove(Powerup powerupToRemove)
    {
        powerupToRemove.Remove(this);
        removedPowerupQueue.Add(powerupToRemove);
    }
    public void DecrementPowerupTimers()
    {
        foreach (Powerup powerup in powerups)
        {
            powerup.duration -= Time.deltaTime;
            if (powerup.duration <= 0)
            {
                Remove(powerup);
            }
        }

    }
    private void ApllyRemovePowerupsQueue()
    {
        if (removedPowerupQueue != null)
        {
            foreach (Powerup powerup in removedPowerupQueue)
            {
                powerups.Remove(powerup);
            }
            removedPowerupQueue.Clear();
        }
    }


}
