using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    GameController gameController;
    Rigidbody2D playerRigidbody;
    public TextMeshProUGUI instruccionesText;
    [Header("Movimiento por casillas")]
    public GameObject casillasPadre;
    public int actualCasillaJugador;
    public int actualCasillaEnPeligro;
    public Image[] casillas;
    [Header("Movimiento vertical y horizontal")]
    public GameObject imagenFondo;
    public float fuerzaDeSalto = 100;
    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        playerRigidbody=GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        if(gameController._movementSelected==GameController.movementSelected.movimientoPorCasillas)
        {
            ActivarMovimientoPorCasillas();
        }
        else
        {
            ActivarMovimientoVertical_Horizontal();   
        }
    }
    private void Update()
    {
        LeerInputs();
    }
    void ActivarMovimientoVertical_Horizontal()
    {
        Debug.Log(nameof(ActivarMovimientoVertical_Horizontal));

        //Adecuamos lo necesario para este modo de juego
        imagenFondo.SetActive(true);
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, -165, 0);

        if (gameController._movementSelected == GameController.movementSelected.movimientoVertical)
        {
            instruccionesText.text = "Presiona 'ESPACIO' para saltar y esquivar los obstáculos";
        }
        else
        {
            instruccionesText.text = "Esquiva los obstáculos que caen";
        }
        InvokeRepeating(nameof(InstanciarBala), 2f, Random.Range(3, 6));
    }
    void InstanciarBala()
    {
        Debug.Log(nameof(InstanciarBala));
        GameObject nuevaBala = Instantiate(gameController.balaPrefab, Vector3.zero,Quaternion.identity);
        nuevaBala.transform.SetParent(GameObject.Find("Canvas").transform);
        nuevaBala.GetComponent<RectTransform>().anchoredPosition3D = direccionBala();
        if(gameController._movementSelected == GameController.movementSelected.movimientoVertical)
        {
            nuevaBala.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 500 * gameController.velocidadDeBala, ForceMode2D.Force);
        }
        else
        {
            nuevaBala.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            nuevaBala.GetComponent<Rigidbody2D>().gravityScale = 15;
            nuevaBala.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        Destroy(nuevaBala, 5);
    }
    Vector3 direccionBala()
    {
        if (gameController._movementSelected == GameController.movementSelected.movimientoVertical)
        {
            return new Vector3(-1000, -150, 0);
        }
        else
        {
            return new Vector3(Random.Range(-900,900), 900, 0);
        }

    }
    void LeerInputs()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(gameController._movementSelected==GameController.movementSelected.movimientoPorCasillas)
            {
                if (casillas[actualCasillaJugador].GetComponent<CasillaController>().casillaIzquierda != null)
                {
                    actualCasillaJugador = casillas[actualCasillaJugador].GetComponent<CasillaController>().casillaIzquierda.transform.GetSiblingIndex();
                    transform.position = casillas[actualCasillaJugador].transform.position;
                }
            }   
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (gameController._movementSelected == GameController.movementSelected.movimientoPorCasillas)
            {
                if (casillas[actualCasillaJugador].GetComponent<CasillaController>().casillaDerecha != null)
                {
                    actualCasillaJugador = casillas[actualCasillaJugador].GetComponent<CasillaController>().casillaDerecha.transform.GetSiblingIndex();
                    transform.position = casillas[actualCasillaJugador].transform.position;
                }
            }      
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (gameController._movementSelected == GameController.movementSelected.movimientoPorCasillas)
            {
                if (casillas[actualCasillaJugador].GetComponent<CasillaController>().casillaAbajo != null)
                {
                    actualCasillaJugador = casillas[actualCasillaJugador].GetComponent<CasillaController>().casillaAbajo.transform.GetSiblingIndex();
                    transform.position = casillas[actualCasillaJugador].transform.position;
                }
            }     
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (gameController._movementSelected == GameController.movementSelected.movimientoPorCasillas)
            {
                if (casillas[actualCasillaJugador].GetComponent<CasillaController>().casillaArriba != null)
                {
                    actualCasillaJugador = casillas[actualCasillaJugador].GetComponent<CasillaController>().casillaArriba.transform.GetSiblingIndex();
                    transform.position = casillas[actualCasillaJugador].transform.position;
                }
            }        
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            if(gameController._movementSelected == GameController.movementSelected.movimientoVertical)
            {
                playerRigidbody.AddForce(Vector2.up * 100 * fuerzaDeSalto, ForceMode2D.Impulse);
            }
        }
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        Debug.Log(movimientoHorizontal);
        if(gameController._movementSelected == GameController.movementSelected.movimientoHorizontal)
        {
            playerRigidbody.position += new Vector2(movimientoHorizontal * 100 * gameController.velocidadJugador * Time.deltaTime, 0);
        }

    }
    #region movimiento por casillas
    void ActivarMovimientoPorCasillas()
    {
        Debug.Log(nameof(ActivarMovimientoPorCasillas));

        //Activamos los componentes necesarios para este modo de juego
        casillasPadre.SetActive(true);
        instruccionesText.text = "Muévete por las casillas para esquivar los obstáculos";
        casillas = casillasPadre.GetComponentsInChildren<Image>();
        GetComponent<Rigidbody2D>().isKinematic = true;

        //Movemos al jugador a una casilla aleatoria
        int numeroAleatorio = Random.Range(0, casillas.Length);
        transform.position = casillas[numeroAleatorio].transform.position;
        actualCasillaJugador = numeroAleatorio;

        InvokeRepeating(nameof(ActivarObstaculos),1.5f,2);
    }
    void ActivarObstaculos()
    {
        int casillaEnPeligro = Random.Range(0, casillas.Length);
        casillas[casillaEnPeligro].color = Color.red;
        actualCasillaEnPeligro = casillaEnPeligro;
        casillas[casillaEnPeligro].GetComponent<BoxCollider2D>().enabled = true;
        
        if(actualCasillaJugador==casillaEnPeligro)
        {
            PerderJuego();
        }

        Invoke(nameof(ReactivarCasilla), 0.75f);
    }
    void ReactivarCasilla()
    {
        casillas[actualCasillaEnPeligro].color = new Color(0, 0, 0.4f, 1);
        casillas[actualCasillaEnPeligro].GetComponent<BoxCollider2D>().enabled = false;
    }
    #endregion
    public void PerderJuego()
    {
        instruccionesText.text = "Has perdido";
        enabled = false;
        CancelInvoke();
        gameController.Invoke(nameof(gameController.RegresarAlMenuPrincipal), 1.25f);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bala")
        {
            PerderJuego();
            Destroy(collision.gameObject);
        }
    }
}
