using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.SceneTemplate;

public class CreateExercise : MonoBehaviour
{
   [SerializeField] private InputField iField;

    public void createExercise() {
        Interface.setEnunciado(iField.text);
        SceneManager.LoadScene("Scenes/ExerciseType1"); // depende del tipo de ejericio
    }
}
