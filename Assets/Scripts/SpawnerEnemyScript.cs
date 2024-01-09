using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.Threading.Tasks;

public class SpawnerEnemyScript : MonoBehaviour
{
    [SerializeField]
    GameObject enemy;
    public static string wave;


    // Start is called before the first frame update
    IEnumerator Start()
    {

        wave = "Onda 1/3";
        yield return StartCoroutine(SpawnEnemiesWithDelay());   // Onda 1

        yield return StartCoroutine(CronometroForNextRound());
        wave = "Onda 2/3";
        yield return StartCoroutine(SpawnEnemiesWithDelay(10)); // Onda 2

        yield return StartCoroutine(CronometroForNextRound());
        wave = "Onda 3/3";
        yield return StartCoroutine(SpawnEnemiesWithDelay(15)); // Onda 3




    }

    IEnumerator SpawnEnemiesWithDelay(int NunberOfEnemy = 5)
    {
        GameObject enemy = null;


        for (int i = 0; i < NunberOfEnemy; i++)
        {
            yield return new WaitForSeconds(2f);

            while (GameController.IsPaused | GameController.IsGameOver)
            {
                yield return null;
            }
           
            enemy = instantiateEnemy();
        }

        while (enemy != null)
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


    GameObject instantiateEnemy()
    {
        float RandomX = UnityEngine.Random.Range(-13f, 13f);
        float RandomY = UnityEngine.Random.Range(-13f, 13.0f);
        GameObject vims = Instantiate(enemy, new Vector2(RandomY, RandomX), transform.rotation);
        return vims;
    }

}
