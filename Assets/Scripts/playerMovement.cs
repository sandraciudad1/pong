using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject ball;
    float increase = 0.03f;
    public Boolean multiplayer;
    public float minYPos = -3.7f, maxYPos = 3.7f;

    // Start is called before the first frame update
    void Start()
    {
        player.transform.position = new Vector3(-8, 0, 0);
        enemy.transform.position = new Vector3(8, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        playerMov();
        if (multiplayer == true)
        {
            enemyMov();
        } else
        {
            IAMov();
        }
        
    }

    void playerMov()
    {
        if (Input.GetKey(KeyCode.W)) //up
        {
            Vector3 newPosUp = new Vector3(player.transform.position.x, (player.transform.position.y + increase), player.transform.position.z);
            player.transform.position = newPosUp;
            checkLimits(player);
        }
        else if (Input.GetKey(KeyCode.S)) //down
        {
            Vector3 newPosDown = new Vector3(player.transform.position.x, (player.transform.position.y - increase), player.transform.position.z);
            player.transform.position = newPosDown;
            checkLimits(player);
        }
    }

    void enemyMov()
    {
        if (Input.GetKey(KeyCode.UpArrow)) //up
        {
            Vector3 newPosUp = new Vector3(enemy.transform.position.x, (enemy.transform.position.y + increase), enemy.transform.position.z);
            enemy.transform.position = newPosUp;
            checkLimits(enemy);
        }
        else if (Input.GetKey(KeyCode.DownArrow)) //down
        {
            Vector3 newPosDown = new Vector3(enemy.transform.position.x, (enemy.transform.position.y - increase), enemy.transform.position.z);
            enemy.transform.position = newPosDown;
            checkLimits(enemy);
        }
    }

    void IAMov()
    {
        checkLimits(enemy);
        float ypos = Mathf.Clamp(ball.transform.position.y, minYPos, maxYPos);
        enemy.transform.position = new Vector3(enemy.transform.position.x, ypos, enemy.transform.position.z);
    }

    void checkLimits(GameObject jugador)
    {
        if(jugador.transform.localScale.y == 2f)
        {
            maxYPos = 3.7f;
            minYPos = -3.7f;
        } 
        else if (jugador.transform.localScale.y == 4f)
        {
            maxYPos = 2.7f;
            minYPos = -2.7f;
        } 
        else if (jugador.transform.localScale.y == 1f)
        {
            maxYPos = 4.2f;
            minYPos = -4.2f;
        }

        if (jugador.transform.position.y > maxYPos)
        {
            jugador.transform.position = new Vector3(jugador.transform.position.x, maxYPos, jugador.transform.position.z);
        } 
        else if (jugador.transform.position.y < minYPos)
        {
            jugador.transform.position = new Vector3(jugador.transform.position.x, minYPos, jugador.transform.position.z);
        }
    }

}
