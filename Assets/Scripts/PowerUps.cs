using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject ball;
    float timeMin = 10f, timeMax = 15f, duration = 5f;
    public GameObject[] powerupPrefabs;
    GameObject powerupInstance, ballInstance, lastPowerup;
    Boolean isPlayer;
    float initialScale=2;
    float maximizeTimer = 5f, minimizeTimer = 5f;
    Boolean decreaseMaximizeTimer = false, decreaseMinimizeTimer = false;
    Boolean endMaximize = false, endMinimize = false;
    Boolean checkPlayer, checkEnemy;
    public string playerTouched;

    private void Start()
    {
        StartCoroutine(SpawnPowerupRoutine());
    }

    private void Update()
    {
        ballController ball = GameObject.Find("Pelota").GetComponent<ballController>();
        if (decreaseMaximizeTimer == true)
        {
            maximizeTimer -= Time.deltaTime;
            if (maximizeTimer <= 0)
            {
                endMaximize = true;
            }
        } 
        else if (decreaseMinimizeTimer == true)
        {
            minimizeTimer -= Time.deltaTime;
            if (minimizeTimer <= 0)
            {
                endMinimize = true;
            }
        }

        playerTouched = ball.lastPlayerTouched;
        if (ball != null)
        {
            if (checkPlayer == true)
            {

                if (playerTouched.Equals("Jugador"))
                {
                    ball.speed = ball.speed / 2;
                    checkPlayer = false;
                }
                
            }
            if (checkEnemy == true)
            {
                if (playerTouched.Equals("Contrincante"))
                {
                    checkEnemy = false;
                    ball.speed = ball.speed / 2;
                }
                
            }
        }
        
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(timeMin, timeMax));
            GameObject powerupPrefab = GetRandomPowerup();
            if (powerupPrefab != null)
            {
                Vector3 position = new Vector3(UnityEngine.Random.Range(-6.1f, 6.1f), UnityEngine.Random.Range(-4.1f, 3.1f), 0f);
                GameObject powerupInstance = Instantiate(powerupPrefab, position, Quaternion.identity);

                yield return new WaitForSeconds(duration);
                Destroy(powerupInstance);
            }
            if (endMaximize == true)
            {
                resetValuesMaximize();
            } 
            else if (endMinimize == true)
            {
                resetValuesMinimize();
            }
            
        }
    }

    GameObject GetRandomPowerup()
    {
        if (powerupPrefabs.Length == 1)
            return powerupPrefabs[0];

        List<GameObject> availablePowerups = new List<GameObject>(powerupPrefabs);
        availablePowerups.Remove(lastPowerup);
        GameObject randomPowerup = availablePowerups[UnityEngine.Random.Range(0, availablePowerups.Count)];
        lastPowerup = randomPowerup;
        return randomPowerup;
    }

    public void maximizeActions(Boolean playerTouched)
    {
        if (playerTouched == true) //maximize player
        {
            player.transform.localScale = new Vector3(0.5f, initialScale * 2, 0.5f);

        }
        else //maximize enemy
        {
            enemy.transform.localScale = new Vector3(0.5f, initialScale * 2, 0.5f);
        }
        decreaseMaximizeTimer = true;
    }

    public void minimizeActions(Boolean playerTouched)
    {
        if (playerTouched == true) //minimize enemy
        {
            enemy.transform.localScale = new Vector3(0.5f, initialScale / 2, 0.5f);
        }
        else //minimize player
        {
            player.transform.localScale = new Vector3(0.5f, initialScale / 2, 0.5f);
        }
        decreaseMinimizeTimer = true;
    }

    public void speedActions(Boolean playerTouched)
    {
        ballController ball = GameObject.Find("Pelota").GetComponent<ballController>();
        if (ball != null)
        {
            float initialSpeed = ball.speed;
            ball.speed = initialSpeed * 2;

            if (playerTouched == true) // player has touched
            {
                checkEnemy = true;
                
            } 
            else // enemy has touched
            {
                checkPlayer = true;
            }
        }
    }


    public void resetValuesMaximize()
    {
        maximizeTimer = 5f;
        decreaseMaximizeTimer = false;
        endMaximize = false;
        if(player.transform.localScale.y == initialScale*2)
        {
            player.transform.localScale = new Vector3(0.5f, initialScale, 0.5f);
        } 
        else if (enemy.transform.localScale.y == initialScale * 2)
        {
            enemy.transform.localScale = new Vector3(0.5f, initialScale, 0.5f);
        }
    }

    public void resetValuesMinimize()
    {
        minimizeTimer = 5f;
        decreaseMinimizeTimer = false;
        endMinimize = false;
        if (player.transform.localScale.y == initialScale / 2)
        {
            player.transform.localScale = new Vector3(0.5f, initialScale, 0.5f);
        }
        else if (enemy.transform.localScale.y == initialScale / 2)
        {
            enemy.transform.localScale = new Vector3(0.5f, initialScale, 0.5f);
        }
    }
}
