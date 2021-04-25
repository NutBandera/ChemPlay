using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.SceneTemplate;
using SFB;

public class CreateExercise : MonoBehaviour
{
    [SerializeField] private Text selectedEnunciado;
    public void createExercise() {
        SceneManager.LoadScene("Scenes/ExerciseType1"); // depende del tipo de ejericio
    }

    public void selectFile() {
        var paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", "", false);
        // Comprobar que es una ruta válida
        // if selected 
        // let choose one only
        var pathSplitted = paths[0].Split('/');
        var elementName = pathSplitted[pathSplitted.Length - 1].ToString();
        Interface.setEnunciado("Enunciados/" + elementName.Split('.')[0]); // removes extension
        // show file selected
        selectedEnunciado.text = elementName;
    }
}
