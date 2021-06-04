using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using SFB; 

public class CreateExercise : MonoBehaviour
{
    [SerializeField] private Text selectedEnunciado;
    [SerializeField] private GameObject baseImage;
    
    public void createExercise() {
        SceneManager.LoadScene("Scenes/ExerciseType1"); // depende del tipo de ejericio
    }

    public void selectEnunciado() {
        var paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", "", false); // accepted extions
        // Comprobar que es una ruta válida
        // if selected 
        // let choose one only
        var pathSplitted = paths[0].Split('/');
        var elementName = pathSplitted[pathSplitted.Length - 1].ToString();
        CurrentExercise.setEnunciado("Enunciados/" + elementName.Split('.')[0]); // removes extension
        // show file selected
        selectedEnunciado.text = elementName; // set image ?
    }

    public void selectItems() {
        var paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", "", true);
        List<string> items = new List<string>();
        var elementName = "";
        for (int i=0; i<paths.Length; i++){
            var pathSplitted = paths[i].Split('/');
            elementName = pathSplitted[pathSplitted.Length - 1].ToString().Split('.')[0];
            items.Add(elementName);
        }
        CurrentExercise.setItems(items);

        // show items selected

    }

    public void crearContenido() {
        // Go to "Crear contenido" page
        SceneManager.LoadScene("Scenes/Interface/CrearContenido");

    }
}
