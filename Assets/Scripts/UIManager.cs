using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject winnerScreen;
    playerMovement playermovement;
    ballController ballcontroller;
    PowerUps powerup;

    // Start is called before the first frame update
    void Start()
    {
        playermovement = GameObject.Find("Jugador").GetComponent<playerMovement>();
        ballcontroller = GameObject.Find("Pelota").GetComponent<ballController>();
        powerup = GameObject.Find("PowerUps").GetComponent<PowerUps>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void onClickMultijugador()
    {
        menu.SetActive(false);
        playermovement.multiplayer = true;
        ballcontroller.canStart = true;
    }

    public void onClickJugador()
    {
        menu.SetActive(false);
        playermovement.multiplayer = false;
        ballcontroller.canStart = true;
    }

    public void onClickHome()
    {
        winnerScreen.SetActive(false);
        menu.SetActive(true);
        ballcontroller.resetValues();
        powerup.resetValuesMaximize();
        powerup.resetValuesMinimize();
    }
}
