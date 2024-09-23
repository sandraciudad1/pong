using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ballController : MonoBehaviour
{
    [SerializeField] GameObject ball;
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;
    [SerializeField] TextMeshPro playerMarker;
    [SerializeField] TextMeshPro enemyMarker;
    [SerializeField] GameObject winnerScreen;
    [SerializeField] GameObject winnerJugador;
    [SerializeField] GameObject winnerContrincante;
    [SerializeField] GameObject infoCanvas;
    int playerMarkerCounter = 0, enemyMarkerCounter = 0;
    public float speed; 
    float xmov, ymov;
    Vector3 direction;
    int ballCollisions = 0;
    public Boolean canStart;
    Boolean initRound=false;
    public AudioClip collisionSound;
    public AudioClip winSound;
    public string lastPlayerTouched;
    public Boolean isplayer;

    // Start is called before the first frame update
    void Start()
    {
        ball.transform.position = new Vector3(0, 0, 0);
        initPositionMovement();
    }

    // Update is called once per frame
    void Update()
    {
        if (canStart == true)
        {
            if(playerMarkerCounter==0 && enemyMarkerCounter == 0)
            {
                ball.transform.position += direction * speed * Time.deltaTime;

                if (ball.transform.position.y >= 4.4 || ball.transform.position.y <= -4.4)
                {
                    direction.y = direction.y * -1;
                }

                checkPoints();
                speedControl();
            } else
            {
                
                if (Input.GetKey(KeyCode.Space))
                {
                    initRound = true;
                    infoCanvas.SetActive(false);
                }

                if (initRound == true)
                {
                    ball.transform.position += direction * speed * Time.deltaTime;

                    if (ball.transform.position.y >= 4.4 || ball.transform.position.y <= -4.4)
                    {
                        direction.y = direction.y * -1;
                    }

                    checkPoints();
                    speedControl();
                }

            }
            
        }
    }

    void initPositionMovement()
    {
        do
        {
            xmov = UnityEngine.Random.Range(-0.15f, 0.15f);
        } while (xmov > -0.09f && xmov < 0.09f); 
        ymov = UnityEngine.Random.Range(-0.15f, 0.15f);
        direction = new Vector3(xmov, ymov, 0);
        speed = 20f;
        initRound = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Jugador") && ball.transform.position.x > -8f)
        {
            AudioSource.PlayClipAtPoint(collisionSound, transform.position);
            direction.x = direction.x * -1;
            ballCollisions++;
            lastPlayerTouched = "Jugador";
        } 
        else if (collision.gameObject.CompareTag("Contrincante") && ball.transform.position.x < 8f)
        {
            AudioSource.PlayClipAtPoint(collisionSound, transform.position);
            direction.x = direction.x * -1;
            ballCollisions++;
            lastPlayerTouched = "Contrincante";
        } 
    }

    void OnTriggerEnter(Collider other)
    {
        PowerUps powerups = GameObject.Find("PowerUps").GetComponent<PowerUps>();
        if(lastPlayerTouched== "Jugador")
        {
            isplayer = true;
        } else
        {
            isplayer = false;
        }

        if (other.CompareTag("powerupMaximize"))
        {
            powerups.maximizeActions(isplayer);
        } 
        else if (other.CompareTag("powerupMinimize"))
        {
            powerups.minimizeActions(isplayer);
        } 
        else if (other.CompareTag("powerupSpeed"))
        {
            powerups.speedActions(isplayer);
        }
        Destroy(other.gameObject);
    }

    void checkPoints()
    {
        if (ball.transform.position.x <= -9) //enemy point
        {
            AudioSource.PlayClipAtPoint(winSound, transform.position);
            enemyMarkerCounter++;
            enemyMarker.text = enemyMarkerCounter.ToString();
            infoCanvas.SetActive(true);
            ball.transform.position = new Vector3(-7.2f, player.transform.position.y, 0f);
            initPositionMovement();
        } 
        else if (ball.transform.position.x >= 9) //player point
        {
            AudioSource.PlayClipAtPoint(winSound, transform.position);
            playerMarkerCounter++;
            playerMarker.text = playerMarkerCounter.ToString();
            infoCanvas.SetActive(true);
            ball.transform.position = new Vector3(7.2f, enemy.transform.position.y, 0f);
            initPositionMovement();
        }

        if (enemyMarkerCounter == 10)
        {
            string option = "winnerContrincante";
            StartCoroutine(waitBeforeWin(option));
        } 
        else if (playerMarkerCounter == 10)
        {
            string option = "winnerJugador";
            StartCoroutine(waitBeforeWin(option)); 
        }
    }

    IEnumerator waitBeforeWin(string option)
    {
        canStart = false;
        yield return new WaitForSeconds(1.2f);
        winnerScreen.SetActive(true);
        if (option.Equals("winnerContrincante"))
        {
            winnerContrincante.SetActive(true);
        } 
        else if (option.Equals("winnerJugador"))
        {
            winnerJugador.SetActive(true);
        }
        
    }

    void speedControl()
    {
        if (ballCollisions==5)
        {
            speed += 7f;
            ballCollisions = 0;
        }
    }

    public void resetValues()
    {
        playerMarkerCounter = 0;
        enemyMarkerCounter = 0;
        playerMarker.text = playerMarkerCounter.ToString();
        enemyMarker.text = enemyMarkerCounter.ToString();
        ballCollisions = 0;
        infoCanvas.SetActive(false);
    }

}
