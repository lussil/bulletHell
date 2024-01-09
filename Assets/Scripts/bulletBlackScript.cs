using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBlackScript : MonoBehaviour, IBullet, IAttributes
{

    public int bulletDamage { get; set; }

    public int BulletDamage
    {
        get { return bulletDamage; }
        set { bulletDamage = value; }
    }
    

    private int damage;
    private int life;
    public bool isALive = true;

    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public int Life
    {
        get { return life; }
        set { Life = value; }
    }
    private string debuff;
    public string Debuff
    {
        get => debuff;
        set => debuff = value;
    }

    public bulletBlackScript()
    {
        BulletDamage = 10;
        life = 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
