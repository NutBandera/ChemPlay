using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using SFB; 

public class CrearContenido : MonoBehaviour
{
    private int xPart = 400;
    private int xCross = 950;
    private float y;
    private List<GameObject> parts = new List<GameObject>();
    private List<GameObject> buttons = new List<GameObject>();
    [SerializeField] private GameObject panelParts;
    [SerializeField] private Text noContentText;

    public void Start() {
        foreach (ParteContenido part in CurrentExercise.getContenido()) {
            this.addPart(part.getBaseName());
        }
    }
    public void addSolutions() {
        // Examinar
        var selectedBase = this.selectBase();
        // Si el path no es null -> go to "Soluciones" page
        if (!string.IsNullOrEmpty(selectedBase)){
            Soluciones.setSelectedBase(selectedBase);
            
            SceneManager.LoadScene("Scenes/Interface/Soluciones");
        }
    }
    public void createExercise() {
        Exercise exercise = new Exercise(CurrentExercise.getNombre(), CurrentExercise.getEnunciado(), CurrentExercise.getItems(),
        CurrentExercise.getSolutions(), CurrentExercise.getContenido());
        CurrentExercise.addExercise(exercise);
        SceneManager.LoadScene("Scenes/ExerciseType1");
    }
    public string selectBase() {
        var path = StandaloneFileBrowser.OpenFilePanel("Open File", "", "", false); // accepted extions
        var pathSplitted = Regex.Split(path[0], "Resources/");
        var elementName = pathSplitted[1].ToString();
        // si ya existe -> mensaje + return null
        return elementName.Split('.')[0];
    }

    public void addPart(string partName) {
        if (parts.Count > 0){
            y = parts[parts.Count-1].transform.position.y - 350;
        } else {
            y = 1200;
        }
        // Crear item
        GameObject part = new GameObject();
        part.AddComponent<Image>();
        part.GetComponent<Image>().sprite = Resources.Load<Sprite>(partName);
        part.transform.position = new Vector3(xPart, y, 0f);
        part.transform.parent = panelParts.transform;
        part.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 300); 

        // Create delete button and associate it with the element
        GameObject button = new GameObject();
        button.transform.parent = panelParts.transform;
        button.AddComponent<RectTransform>();
        button.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80); 
        button.AddComponent<Button>();
        button.transform.position = new Vector3(xCross, y, 0f); 
        button.AddComponent<Image>();
        button.GetComponent<Image>().sprite = Resources.Load<Sprite>("cross");
        button.GetComponent<Button>().onClick.AddListener(delegate{deletePart(partName, part, button);});

        parts.Add(part);
        buttons.Add(button);

        noContentText.gameObject.SetActive(false);

        // panelParts.transform.localCorners[0].Set(0.5f, 0.5f, 1f);
        // change bottom
    }
     private void deletePart(string partName, GameObject part, GameObject button) {
        // Delete from list
        CurrentExercise.removePart(partName);
        // Delete from scroll
        Destroy(button.gameObject);
        Destroy(part.gameObject);
        // reassign positions
        int index = parts.IndexOf(part);
        int i;
        for (i=index+1; i<parts.Count; i++){
            parts[i].transform.position = new Vector3(parts[i].transform.position.x,
            parts[i].transform.position.y + 350, 0f);
        }
        for (i=index+1; i<buttons.Count; i++){
            buttons[i].transform.position = new Vector3(buttons[i].transform.position.x,
            buttons[i].transform.position.y + 350, 0f);
        }
        parts.Remove(part);
        buttons.Remove(button);

        if (parts.Count == 0) {
            noContentText.gameObject.SetActive(true);
        }
    }

}
