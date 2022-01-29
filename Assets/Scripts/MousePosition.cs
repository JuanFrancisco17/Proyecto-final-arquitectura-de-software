using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{

    [Header("SINGLETON")]
    private static MousePosition instance;

    public static MousePosition Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<MousePosition>();
            }
            return instance;
        }
    }


    [Header("VARIABLES CAMARA")]
    
    //Camara
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        //lanzamos un rayo que nos dice cual es la posición del ratón
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            transform.position = hit.point;
        }
    }
}
