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
    [SerializeField] private Text selectedItems;
    [SerializeField] private Button contenidoButton;
    [SerializeField] private GameObject panel;
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

         if (CurrentExercise.getItems().Count > 0) {
            contenidoButton.interactable = true;
        } 
    }

    public void selectItems() {
        var path = StandaloneFileBrowser.OpenFilePanel("Open File", "", "", true);
        var pathSplitted = path[0].Split('/');
        var elementName = pathSplitted[pathSplitted.Length - 1].ToString().Split('.')[0];

        if (!CurrentExercise.getItems().Contains(elementName)) {
            CurrentExercise.addItem(elementName);
            // Create delete button and associate it with the element
            GameObject button = new GameObject();
            button.transform.parent = panel.transform;
            button.AddComponent<RectTransform>();
            button.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50); 
            button.AddComponent<Button>();
            button.transform.position = new Vector3(200, 200, 0f); 
            button.AddComponent<Image>();
            button.GetComponent<Image>().sprite = Resources.Load<Sprite>("cross");
	        button.GetComponent<Button>().onClick.AddListener(delegate{deleteItem(elementName);});
            // crear cuadro de texto y botón -> al clickar destruir los dos (dentro de un mismo panel?)
            // auto destruir
             // show items selected
            selectedItems.text += " " + elementName;
        } else {
            // show message
            Debug.Log("Ya has seleccionado ese elemento");
        }

        if (!string.IsNullOrEmpty(CurrentExercise.getEnunciado())) {
            contenidoButton.interactable = true;
        } 
    }

    private void deleteItem(string elementName) {
        // delete from list
        Debug.Log("works");
        CurrentExercise.removeItem(elementName);
    }

    public void crearContenido() {
        // Go to "Crear contenido" page
        SceneManager.LoadScene("Scenes/Interface/CrearContenido");
    }
}
