using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    [Header("SINGLETON")]

    private static BulletPoolManager instance;

    public static BulletPoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<BulletPoolManager>();
            }
            return instance;
        }
    }

    [Header("VARIABLES DE LA COLA DE LA POOL")]

    [Tooltip("tamaño de la pool")]
    [SerializeField]
    float poolSize;
    //Cola de la pool
    private Queue<GameObject> bulletPool;

    [Header("VARIABLES DISPARO")]

    [Tooltip("Prefab de la bala")]
    [SerializeField]
    GameObject bulletPrefab;


    void Start()
    {
        //Referenciamos la cola y añadimos las balas desactivadas a la cola al empezar el juego
        bulletPool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newObj = Instantiate(bulletPrefab);
            bulletPool.Enqueue(newObj);
            newObj.SetActive(false);
        }
    }

    //Función para sacar la bala de la cola y activarla
    public void GetObjectFromPool()
    {
        GameObject newObj = bulletPool.Dequeue();
        newObj.SetActive(true);
        newObj.transform.SetPositionAndRotation(PlayerController.Instance.shootSpawnPos.transform.position, PlayerController.Instance.shootSpawnPos.transform.rotation);

    }
    
    //Función para meter la bala desactivada al final de la cola cuando termine
    public void ReturnOjectToPool(GameObject go)
    {
        go.SetActive(false);
        bulletPool.Enqueue(go);
    }
}
