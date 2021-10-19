using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class TeacherMenu : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    private Button[] panelButtons;
    [SerializeField] GameObject dialogMessage;
    public void Start() {
        dialogMessage.SetActive(false);
        panelButtons = panel.GetComponentsInChildren<Button>();
    }
    public void nuevoEjercicio() {
        CurrentExercise.reset();
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
    public void nuevoSet() {
        dialogMessage.SetActive(true);
        deactivateBasePanel();
    }
    public void yesContinueClicked() {
        CurrentExercise.resetExercises();
        dialogMessage.SetActive(false);
        activateBasePanel();
    }
    public void noContinueClicked() {
        dialogMessage.SetActive(false);
        activateBasePanel();
    }
    public void deactivateBasePanel() {
        foreach (Button button in panelButtons) {
            button.interactable = false;
        }
    }
    public void activateBasePanel() {
         foreach (Button button in panelButtons) {
            button.interactable = true;
        }
    }
}
