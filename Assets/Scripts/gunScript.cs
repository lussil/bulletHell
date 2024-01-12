using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class gunScript : MonoBehaviour
{

    GameObject bullet;
    GameObject audioShot;

    private int damage;
    private float bulletSpeed = 30f;

    float timerBulletDestroy = 1.5f;

    public void Shoot()
    {

        // No gameController Pegar nivel da bala
        // No gameController Dado da arma


        damage = DamageGunLevel();
        bullet = TypeOfBulletForLevel();

        audioShot = Resources.Load<GameObject>("AudioManager");

        GameObject bulletFired = Instantiate(  // Spawna uma bala saindo da "frente" do cano e na rotação do cano em direção a frente (direção do mouse)
            bullet,
            transform.position,
            transform.rotation);

        IAttributes bulletAttributes = bulletFired.GetComponent<IAttributes>();
        if (bulletAttributes != null)
        {
            bulletAttributes.Damage = damage;

        }

        GameObject _ = Instantiate(audioShot, transform.position, transform.rotation);

        Rigidbody2D bulletRb = bulletFired.GetComponent<Rigidbody2D>();
        bulletRb.velocity = transform.up * bulletSpeed;

        Destroy(bulletFired, timerBulletDestroy);
        Destroy(_, timerBulletDestroy);

    }

    private int DamageGunLevel()
    {
        int damagePerNivel;
        switch (GameController.NivelGun)
        {
            case 1:
                damagePerNivel = new System.Random().Next(9, 15);
                break;

            case 2:
                damagePerNivel = new System.Random().Next(12, 22);
                break;

            case 3:
                damagePerNivel = new System.Random().Next(28, 36);
                break;

            default:
                damagePerNivel = 10;
                break;
        }

        return damagePerNivel;
    }

    private GameObject TypeOfBulletForLevel()
    {
        GameObject damagePerNivel;
        switch (GameController.NivelBullet)
        {
            case 1:
                damagePerNivel = Resources.Load<GameObject>("bulletBlack");
                break;

            case 2:
                damagePerNivel = Resources.Load<GameObject>("bulletRed");
                break;

            case 3:
                damagePerNivel = Resources.Load<GameObject>("bulletYellow");
                break;

            default:
                damagePerNivel = Resources.Load<GameObject>("bulletBlack");
                break;
        }

        return damagePerNivel;
    }
}
