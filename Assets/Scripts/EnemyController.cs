using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static EnemyState;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [Header("VARIABLES NAVMESH")]

    //Variable del target (el jugador)
    private GameObject target;
    //Variable para el agente del navmesh
    private NavMeshAgent agent;

    [Header("VARIABLES RUTINA DE MOVIMIENTO")]
    //Numero de la rutina (si sale 0 se queda quieto y si sale 1 de forma aleatoria (segun lo que salga en el angulo y grado) se movera
    private int routine;
    //contador
    private float counter;
    //Angulo del movimiento
    private Quaternion angle;
    //Grado del movimiento
    private float grade;

    [Header("VARIABLES DE ESTADO")]

    //Estado actual
    EnemyState currentState;

    [Header("VARIABLES DE VIDA")]

    //Vida del enemigo
    private float life = 4;



    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player");
        currentState = new Patrol(target,agent,routine,counter,angle,grade);
    }

    void Update()
    {
        currentState = currentState.Process();

        if (life <= 0)
        {
            Die();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Bullet")
        {
            life--;

        }
    }

    private void Die()
    {
        PlayerController.Instance.enemyKill++;
        Destroy(this.gameObject);

    }


}
