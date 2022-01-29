using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    //Cambio de escena
    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    //Salir del juego
    public void Quit()
    {
        Application.Quit();
    }
}
