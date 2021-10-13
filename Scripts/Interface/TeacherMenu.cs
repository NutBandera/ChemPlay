using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TeacherMenu : MonoBehaviour
{
    [SerializeField] GameObject logoutConfirmation;
    
    void Start() {
        logoutConfirmation.SetActive(false);
    }
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
        // check here if there exist exercises
        SaveManager.SaveToJson();
    }
    public void cargarEjercicios() {
        // Cargar
        List<Exercise> exercises = SaveManager.LoadFromJson();
        // Set
        CurrentExercise.setExercises(exercises);
        // Probar
        SceneManager.LoadScene("Scenes/Interface/ProbarEjercicios");
    }
    public void logout() {
        logoutConfirmation.SetActive(true);
    }
    public void yesClicked() {
        SceneManager.LoadScene("Scenes/Interface/RolMenu");
    }
    public void noClicked() {
        logoutConfirmation.SetActive(false);
    }
}
