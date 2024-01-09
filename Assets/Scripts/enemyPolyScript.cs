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
        set => debuff = value;
    }

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
        }
    }

    public void onTakeDamage(int damage)
    {
        if (life > 0)
        {
            life -= damage;
        }

        // debuff 


        if (life <= 0)
        {
            onDeath();
            Destroy(this.gameObject);
        }
    }

    private void onDeath()
    {
        ScoreManagerScript.score += point_enemy;
        damageable.DeathEvent -= onTakeDamage;
    }
}
