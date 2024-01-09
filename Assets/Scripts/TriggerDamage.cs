using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class TriggerDamage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject gameObjectAtingido = collision.gameObject;
        IDamegeable damageable = gameObjectAtingido.GetComponent<IDamegeable>();

        IAttributes attributesAttacker = GetComponent<IAttributes>();

        if (gameObjectAtingido.CompareTag("Player") & damageable != null)
        {
            damageable.takeDamage(attributesAttacker.Damage);
        }

        if (gameObjectAtingido.CompareTag("Enemy") & GetComponent<IBullet>() != null)
        { // enemy só toma dano se for da interface bala 
            //Debug.Log(attributesAttacker.Damage);
            damageable.takeDamage(attributesAttacker.Damage);
        }

    }
}
