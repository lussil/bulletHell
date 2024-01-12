using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.PointerEventData;

[RequireComponent(typeof(IDamegeable))]
public class Player : MonoBehaviour, IAttributes
{
    public Rigidbody2D rb;
    public GameObject gunPreFabs;
    public AudioSource audioSource;

    IDamegeable damageable;

    public int score;

    public float direction;
    public float velocity = 8;

    private int damage;
    private int life;
    public readonly int initialLife = 30;

    public bool isALive = true;

    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public int Life
    {
        get => life;
        set => Life = value;
    }

    private string debuff;
    public string Debuff
    {
        get => debuff;
        set => debuff = value;
    }
    // Start is called before the first frame update
    void Start()
    {
        Damage = new System.Random().Next(5, 16);

        life = initialLife;

        damageable = GetComponent<IDamegeable>();
        damageable.DeathEvent += onTakeDamage;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.IsPaused & !GameController.IsGameOver)
        {
            // Mira
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

            transform.up = direction;

            // Movimentação
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            GetVelocityOfNivel();
            rb.velocity = new Vector2(horizontal, vertical).normalized * velocity;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Fire();
            }

        }
        else
        {
            rb.velocity = new Vector2(0, 0).normalized * 0;
        }
    }

    public void GetVelocityOfNivel()
    {

        switch (GameController.NivelPlayer)
        {
            case 1:
                velocity = 8;
                break;

            case 2:
                velocity = 12;
                break;

            case 3:
                velocity = 16;
                break;

            default:
                velocity = 8;
                break;
        }

    }
    private void Fire()
    {
        gunScript gun = gunPreFabs.GetComponent<gunScript>();
        gun.Shoot();
    }
    private void onDeath(int dano)
    {

    }

    private void onTakeDamage(int dano, string debuffTake)
    {

        if (isALive)
        {
            life -= dano;
        }


        if (life <= 0)
        {
            isALive = false;
            audioSource.Play();
            if (damageable != null)
            {
                damageable.DeathEvent -= onTakeDamage;
            }
            _ = GameController.GameOver();
        }
    }



}
