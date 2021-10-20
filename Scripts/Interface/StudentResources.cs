using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class StudentResources : MonoBehaviour
{
    [SerializeField] private GameObject selectedImage;
    public void goBack() {
        SceneManager.LoadScene("Scenes/Interface/StudentMenu");
    }
    public void loadExercises() {
        List<Exercise> exercises = SaveManager.LoadFromJson();
        CurrentExercise.setExercises(exercises);
    }
    public void play() {
        // check everything ...
        SceneManager.LoadScene("Scenes/Interface/ProbarEjercicios");
    }
}
