using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class StudentResources : MonoBehaviour
{
    [SerializeField] private GameObject selectedFile;
    [SerializeField] private Button playButton;
    [SerializeField] private GameObject incorrectFormatMessage;
    [SerializeField] private GameObject emptyFileMessage;
    [SerializeField] private GameObject panel;
     public void Start() {
        incorrectFormatMessage.SetActive(false);
        emptyFileMessage.SetActive(false);
    }
    public void goBack() {
        SceneManager.LoadScene("Scenes/Interface/StudentMenu");
    }
    public void loadExercises() {
        List<Exercise> exercises = null;
        try {
            exercises = SaveManager.LoadFromJson();
            if (exercises != null && exercises.Count > 0) {
                CurrentExercise.setExercises(exercises);
                playButton.interactable = true;
            } else if (exercises.Count.Equals(0)) {
                emptyFileMessage.SetActive(true);
                deactivateBasePanel();
            }
        } catch (Exception e) {
            incorrectFormatMessage.SetActive(true);
            deactivateBasePanel();
        }
    }
    public void play() {
        SceneManager.LoadScene("Scenes/Interface/ProbarEjercicios");
    }
    public void aceptarClicked() {
        incorrectFormatMessage.SetActive(false);
        emptyFileMessage.SetActive(false);
        activateBasePanel();
        playButton.interactable = false;
    }
    public void deactivateBasePanel() {
        foreach (Button button in panel.GetComponentsInChildren<Button>()) {
            button.interactable = false;
        }
    }
    public void activateBasePanel() {
         foreach (Button button in panel.GetComponentsInChildren<Button>()) {
            button.interactable = true;
        }
    }
}
