using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeathOnDamage : MonoBehaviour, IDamegeable
{
    public int damage { get ; private set; }

    public event Action<int> DeathEvent;

    public void takeDamage(int damage)
    {
        DeathEvent?.Invoke(damage);

    }

}
