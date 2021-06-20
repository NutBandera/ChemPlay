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
    [SerializeField] private Button contenidoButton;
    [SerializeField] private GameObject panelItems;
    [SerializeField] private InputField inputNombre;
    private int xItem = 400;
    private int xCross = 730;
    private float y;
    private List<GameObject> items = new List<GameObject>();
    private List<GameObject> buttons = new List<GameObject>();

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
            if (items.Count > 0){
                y = items[items.Count-1].transform.position.y - 100;
            } else {
                y = 600;
            }
            // Crear item
            GameObject item = new GameObject();
            item.AddComponent<Image>();
            item.GetComponent<Image>().sprite = Resources.Load<Sprite>("Items/"+elementName);
            item.transform.position = new Vector3(xItem, y, 0f);
            item.transform.parent = panelItems.transform;
            item.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80); 

            // Create delete button and associate it with the element
            GameObject button = new GameObject();
            button.transform.parent = panelItems.transform;
            button.AddComponent<RectTransform>();
            button.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80); 
            button.AddComponent<Button>();
            button.transform.position = new Vector3(xCross, y, 0f); 
            button.AddComponent<Image>();
            button.GetComponent<Image>().sprite = Resources.Load<Sprite>("cross");
	        button.GetComponent<Button>().onClick.AddListener(delegate{deleteItem(elementName, item, button);});

            items.Add(item);
            buttons.Add(button);

        } else {
            // show message
            Debug.Log("Ya has seleccionado ese elemento");
        }

        if (!string.IsNullOrEmpty(CurrentExercise.getEnunciado())) {
            contenidoButton.interactable = true;
        } 
    }

    private void deleteItem(string elementName, GameObject item, GameObject button) {
        // Delete from list
        CurrentExercise.removeItem(elementName);
        // Delete from scroll
        Destroy(button.gameObject);
        Destroy(item.gameObject);
        // reassign positions
        int index = items.IndexOf(item);
        int i;
        for (i=index+1; i<items.Count; i++){
            // add 100 to each
            items[i].transform.position = new Vector3(items[i].transform.position.x,
            items[i].transform.position.y + 100, 0f);
        }
        for (i=index+1; i<buttons.Count; i++){
            // add 100 to each
            buttons[i].transform.position = new Vector3(buttons[i].transform.position.x,
            buttons[i].transform.position.y + 100, 0f);
        }
        items.Remove(item);
        buttons.Remove(button);
    }

    public void crearContenido() {
        // Go to "Crear contenido" page
        CurrentExercise.setNombre(inputNombre.text);
        SceneManager.LoadScene("Scenes/Interface/CrearContenido");
    }
}
