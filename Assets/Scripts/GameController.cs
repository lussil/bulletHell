using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
public class GameController : MonoBehaviour
{

    Player player;
    AudioSource audioSource;
    public AudioSource backgroundMusic;

    SpawnerEnemyScript SpawnerScript;


    static int nivelBulletMax = 3;
    public static int NivelBullet { get; private set; } = 1;

    static int nivelGunMax = 3;
    public static int NivelGun { get; private set; } = 1;

    static int nivelPlayerMax = 3;
    public static int NivelPlayer { get; private set; } = 1;


    static bool isPaused = false;
    static bool isGameOver = false;

    public static bool IsPaused
    {
        get { return isPaused; }
        set { isPaused = value; }
    }


    public static bool IsGameOver
    {
        get { return isGameOver; }
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        SpawnerScript = GameObject
          .FindGameObjectWithTag("Spawner")
          .GetComponent<SpawnerEnemyScript>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();

        }
        if (SpawnerScript == null)
        {
            SpawnerScript = GameObject
              .FindGameObjectWithTag("Spawner")
              .GetComponent<SpawnerEnemyScript>();

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;
        }

        if (isPaused | isGameOver)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.UnPause();
        }
    }

    static public async Task GameOver()
    {
        isGameOver = true;
        await Task.Delay(2000);

        SceneManager.LoadScene("MenuInicial");
    }

    public static void updateNivelBullet()
    {
        if (NivelBullet < nivelBulletMax)
        {
            NivelBullet++;
        }
    }

    public static void updateNivelGun()
    {
        if (NivelGun < nivelGunMax)
        {
            NivelGun++;
        }

    }

    public static void updateNivelPlayer()
    {
        if (NivelPlayer < nivelPlayerMax)
        {
            NivelPlayer++;
        }
    }


    public static void quitGame()
    {
        Application.Quit();
    }

    public void startGame()
    {
        SceneManager.LoadScene("Level_1");

        NivelBullet = 1;

        NivelGun = 1;

        nivelPlayerMax = 3;
        NivelPlayer = 1;

        isPaused = false;
        isGameOver = false;

        ScoreManagerScript.score = 0;
        SpawnerEnemyScript.wave = "Onda 1/3";

        _ = StartCoroutine(SpawnerScript.StartSpawn());
       

    }
    public static void StartMenu()
    {
        SceneManager.LoadScene("MenuInicial");
    }









}
