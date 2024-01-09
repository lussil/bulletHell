using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateNivelBullet()
    {
        GameController.updateNivelBullet();
    }

    public void updateNivelGun()
    {
        GameController.updateNivelGun();
    }

    public void updateNivelPlayer()
    {
        GameController.updateNivelPlayer();
    }
}
