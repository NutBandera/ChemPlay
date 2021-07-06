using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void nuevoEjercicio() {
        SceneManager.LoadScene("Scenes/Interface/NuevoEjercicio");
    }

    public void verEjercicios() {
        SceneManager.LoadScene("Scenes/Interface/VerEjercicios");
    }

    public void probarEjercicios() {
        SceneManager.LoadScene("Scenes/Interface/ProbarEjercicios");
    }
    public void guardarEjercicios() {
        SaveManager.SaveToJson();
        //SaveManager.LoadFromJson();
    }
}
