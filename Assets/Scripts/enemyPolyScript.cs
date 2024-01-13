using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.GraphicsBuffer;


[RequireComponent(typeof(IDamegeable))]

public class enemyPolyScript : MonoBehaviour, IAttributes
{
    public Rigidbody2D rb;
    public float speedForce = 5;
    public GameObject player;
    private int point_enemy = 1;

    IDamegeable damageable;

    private int damage;
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    private int life;
    public int Life
    {
        get => life;
        set => life = value;
    }

    private string debuff;
    public string Debuff
    {
        get => debuff;
    }

    private float debuffCountown;
    private float timer;

    void Start()
    {
        Damage = 10;
        life = 20;
        rb = this.GetComponent<Rigidbody2D>();

        damageable = GetComponent<IDamegeable>();
        player = GameObject.Find("player");

        if (damageable == null)
        {
            Debug.Break();
        }

        damageable.DeathEvent += onTakeDamage;
    }

    // Update is called once per frame
    void Update()
    {

        if (!GameController.IsPaused & !GameController.IsGameOver)
        {


            Vector2 directionPlayer = player.transform.position;
            Vector2 directionPoly = transform.position;

            Vector2 direction = (directionPlayer - directionPoly).normalized;
            //direcao.norm


            //rb.AddForce(direcao.normalized * speedForce);
            rb.velocity = direction.normalized * speedForce;


            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Aplica a rotação suavizada
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10 * Time.deltaTime);
            TakeDebuff();

        }
    }

    private void FixedUpdate()
    {

    }
    public void onTakeDamage(int damage, string debuffTake)
    {
        if (life > 0)
        {
            life -= damage;
        }
        AddDebuff(debuffTake);

        ExecuteOnDeath();
    }

    public void ExecuteOnDeath()
    {
        if (life <= 0)
        {
           
            onDeath();
            Destroy(this.gameObject);
        }
    }

    private void TakeDebuff()
    {
        debuffCountown -= Time.deltaTime;

        if (debuffCountown > 0)
        {

            timer += Time.deltaTime;

            if (timer >= 1)
            {
                ApplyDebuff();
                timer = 0;
            }

        }
        else
        {
            debuff = null;
            speedForce = 5f;
        }

    }

    private void ApplyDebuff()
    {
        if (debuff == "burn")
        {
            life -= 5;
        }

        else if (debuff == "shock")

        {
            speedForce = 1.5f;
        }

        ExecuteOnDeath();

    }

    private void AddDebuff(string debuffTake)
    {
        debuff = debuffTake;

        debuffCountown = 3f;
        ApplyDebuff();

    }
    private void onDeath()
    {
        InGameScript.enemiesDead++;
        ScoreManagerScript.score += point_enemy;
        damageable.DeathEvent -= onTakeDamage;
    }
}
