using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsController : MonoBehaviour
{
    GameController gameController;

    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    public void BotonMovimientoVertical()
    {
        Debug.Log(nameof(BotonMovimientoVertical));
        gameController._movementSelected = GameController.movementSelected.movimientoVertical;
    }
    public void BotonMovimientoHorizontal()
    {
        Debug.Log(nameof(BotonMovimientoHorizontal));
        gameController._movementSelected = GameController.movementSelected.movimientoHorizontal;
    }
    public void BotonMovimientoPorCasillas()
    {
        Debug.Log(nameof(BotonMovimientoPorCasillas));
        gameController._movementSelected = GameController.movementSelected.movimientoPorCasillas;
    }
    public void BotonJugar()
    {
        Debug.Log(nameof(BotonJugar));
        if(gameController._movementSelected!=GameController.movementSelected.ninguno)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.Log("Debe seleccionar primero un tipo de movimiento");
        }
    }
}
