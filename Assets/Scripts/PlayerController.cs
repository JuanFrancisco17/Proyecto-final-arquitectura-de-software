using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("SINGLETON")]
    //----SINGLETON----
    private static PlayerController instance;

    public static PlayerController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PlayerController>();
            }
            return instance;
        }
    }

    [Header("VARIABLES MOVIMIENTO")]

    [Tooltip("Determina la velocidad a la que va el player")]
    [SerializeField] float speed;
    //Rigidbody
    private Rigidbody rb2d;
    //Valor del movimiento horizontal
    private float hor;
    //Valor del movimiento Vertical
    private float ver;
    //Dirección del movimiento del jugador
    private Vector3 direction;

    [Header("VARIABLES MODELO")]

    //Modelo del jugador
    private GameObject model;

    [Header("VARIABLES DISPARO")]

    [Tooltip("posicion donde spawnea los disparos del jugador")]
    public GameObject shootSpawnPos;

    [Header("VARIABLES UI")]

    [Tooltip("Numero de enemigos matados por el jugador")]
    public int enemyKill = 0;
    [Tooltip("Pantalla de GameOver")]
    public GameObject deadScreen;
    [Tooltip("Pantalla de victoria")]
    public GameObject winScreen;

    void Start()
    {
        Time.timeScale = 1f;
        rb2d = GetComponent<Rigidbody>();
        model = GameObject.Find("Model");
        deadScreen.SetActive(false);
        winScreen.SetActive(false);
    }

    void FixedUpdate()
    {

        //Si el jugador se mueve
        if (hor != 0f || ver != 0f)
        {
            //Añadimos la fuerza del movimiento
            rb2d.velocity = direction * speed * Time.deltaTime;
        }
        else
        {   //Si no se mueve el modelo se queda parado
            rb2d.velocity = Vector3.zero;
        }
    }

    void Update()
    {
        //Recogemos los inputs pulsados del jugador
        hor = Input.GetAxisRaw("Horizontal");
        ver = Input.GetAxisRaw("Vertical");

        //Añadimos la información de los inputs del jugador en un Vector3
        direction = new Vector3(hor, 0f, ver).normalized;

        //Hacemos que el modelo mire hacia la posición del ratón
        model.transform.LookAt(new Vector3(MousePosition.Instance.transform.position.x, 0, MousePosition.Instance.transform.position.z));
        //Debug.Log(MousePosition.instance.transform.position);

        if (Input.GetButtonDown("Fire1"))
        {
            BulletPoolManager.Instance.GetObjectFromPool();
        }

        //Comprobar si el jugador ha ganado
        if (enemyKill >= 6)
        {
            winScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    //Función para matar al jugador
    public void Dead()
    {
        deadScreen.SetActive(true);
        Time.timeScale = 0f;
        Destroy(this.gameObject);
    }
}
