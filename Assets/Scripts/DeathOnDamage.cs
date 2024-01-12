using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeathOnDamage : MonoBehaviour, IDamegeable
{
    public int damage { get ; private set; }

    public event Action<int, string> DeathEvent;

    public void TakeDamage(int damage, string debuff = null)
    {
        DeathEvent?.Invoke(damage, debuff);

    }

}
