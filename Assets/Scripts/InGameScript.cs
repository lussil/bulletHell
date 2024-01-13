using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class InGameScript : MonoBehaviour
{
    public Text scoreText;
    public Text liveText;
    public Text waveText;
    public GameObject vida1;
    public GameObject vida2;
    public GameObject vida3;

    public Text upBulletText;
    public Text upGunText;
    public Text upPlayerText;

    public AudioSource backgroundMusic;

    Player player;

    public static int enemiesDead = 0;
    public static int numberOfEnemies = 0;
    public static bool isEndGame = false;


    private void Start()
    {
        GameObject ps = GameObject.Find("player");
        player = ps.GetComponent<Player>();

    }
    // Update is called once per frame
    void Update()
    {
        scoreText.text = ScoreManagerScript.score.ToString();

        waveText.text = SpawnerEnemyScript.wave;
        ;

        if (player.Life <= (30 / 3))
        {
            vida3.SetActive(false);
            vida2.SetActive(false);

        }
        else if (player.Life <= (player.initialLife * 2 / 3))
        {
            vida3.SetActive(false);
        }

        liveText.text = player.Life.ToString();

        upBulletText.text = "NV. " + GameController.NivelBullet; ;
        upGunText.text = "NV. " + GameController.NivelGun; ;
        upPlayerText.text = "NV. " + GameController.NivelPlayer;

        if (isEndGame)
        {
            if (enemiesDead >= numberOfEnemies)
            {
                StartEndGame();
            }

        }
 

    }
    //AtualizarCoracao();
    void StartEndGame()
    {
        // Menu de parabens
        // tocar musica
        // menu inicial
       
        StartDelayedMenu();
    }

    public void StartDelayedMenu()
    {
        enemiesDead = 0;
        numberOfEnemies = 0;
        isEndGame = false;
        StartCoroutine(PlayMusicAndLoadScene());
    }

    IEnumerator PlayMusicAndLoadScene()
    {
        GameController.IsPaused = true;
        backgroundMusic.Play();
        yield return new WaitForSeconds(2.5f);

        // Carrega a cena do menu inicial
        SceneManager.LoadScene("MenuInicial");
    }
}

