using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{

    [Header("VARIABLES UI")]
    //Texto
    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }
    void Update()
    {
        //Actualizamos el texto con el numero de enemigos que hemos matado
        text.text = "Enemigos derrotados:" + PlayerController.Instance.enemyKill + "/6";
    }
}
