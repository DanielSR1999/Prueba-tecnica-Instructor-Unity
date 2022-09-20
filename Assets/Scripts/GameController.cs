using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public enum movementSelected {ninguno, movimientoVertical, movimientoHorizontal, movimientoPorCasillas}
    public movementSelected _movementSelected;
    public static GameController instance;
    public GameObject balaPrefab;
    public float velocidadDeBala = 50;
    public float velocidadJugador = 50;
    private void Awake()
    {
        //Implementación del patrón Singleton
        if(instance==null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void RegresarAlMenuPrincipal()
    {
        Debug.Log(nameof(RegresarAlMenuPrincipal));
        SceneManager.LoadScene(0);
    }

}
