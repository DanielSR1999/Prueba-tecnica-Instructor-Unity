using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CasillaController : MonoBehaviour
{
    public Image casillaIzquierda;
    public Image casillaDerecha;
    public Image casillaAbajo;
    public Image casillaArriba;
    GameController gameController;
    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player")
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();
            playerController.PerderJuego();
            
        }
    }
}
