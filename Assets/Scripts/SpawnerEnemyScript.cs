using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.Threading.Tasks;
using static UnityEngine.EventSystems.EventTrigger;

public class SpawnerEnemyScript : MonoBehaviour
{
    [SerializeField]
    GameObject enemy;

    [SerializeField]
    GameObject hexanemy;

    public static string wave;

    private int countEnemies = 0;


    // Start is called before the first frame update
    void Start()
    {
        _ = StartCoroutine(StartSpawn());

    }

    public IEnumerator StartSpawn()
    {


        wave = "Onda 1/3";
        yield return StartCoroutine(SpawnEnemiesWithDelay());   // Onda 1

        yield return StartCoroutine(CronometroForNextRound());
        wave = "Onda 2/3";
        yield return StartCoroutine(SpawnEnemiesWithDelay(8, 3)); // Onda 2

        yield return StartCoroutine(CronometroForNextRound());
        wave = "Onda 3/3";
        yield return StartCoroutine(SpawnEnemiesWithDelay(12, 7)); // Onda 3

        //countEnemies
        InGameScript.numberOfEnemies = countEnemies;
        InGameScript.isEndGame = true;



    }

    IEnumerator SpawnEnemiesWithDelay(int NunberOfEnemy = 5, int numberOfSecondEnemy = 0)
    {
        GameObject enemy = null;
        GameObject enemy2 = null;
        countEnemies += NunberOfEnemy + numberOfSecondEnemy;

        for (int i = 0; i < NunberOfEnemy; i++)
        {
            yield return new WaitForSeconds(2f);

            while (GameController.IsPaused | GameController.IsGameOver)
            {
                yield return null;
            }

            enemy = instantiateEnemy();
        }

        for (int i = 0; i < numberOfSecondEnemy; i++)
        {
            yield return new WaitForSeconds(2f);

            while (GameController.IsPaused | GameController.IsGameOver)
            {
                yield return null;
            }

            enemy2 = instantiateEnemy(true);
        }



        while (enemy != null)
        {
            yield return null;
        }

        while (enemy2 != null)
        {
            yield return null;
        }

    }


    IEnumerator CronometroForNextRound()
    {
        for (int i = 5; i > 0; i--)
        {

            yield return new WaitForSeconds(1f);

            while (GameController.IsPaused | GameController.IsGameOver)
            {
                yield return null;
            }
            wave = i.ToString();
        }
    }


    GameObject instantiateEnemy(bool instatiateHexanamy = false)
    {
        GameObject inimigoToIntantiete = enemy;

        if (instatiateHexanamy)
        {
            inimigoToIntantiete = hexanemy;
        }

        float RandomX = UnityEngine.Random.Range(-13f, 13f);
        float RandomY = UnityEngine.Random.Range(-13f, 13.0f);
        GameObject vims = Instantiate(inimigoToIntantiete, new Vector2(RandomY, RandomX), transform.rotation);
        return vims;
    }

}
