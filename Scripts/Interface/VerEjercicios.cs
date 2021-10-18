using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class VerEjercicios : MonoBehaviour
{
    private List<GameObject> exercises = new List<GameObject>();
    private List<GameObject> deleteButtons = new List<GameObject>();
    private List<GameObject> editButtons = new List<GameObject>();
    [SerializeField] private GameObject panelParts;
    [SerializeField] private GameObject panel;
    [SerializeField] GameObject dialogMessage;
    [SerializeField] private Text noExercisesText;
    private int xPart = 400;
    private int xCross = 950;
    private int xEdit = 850;
    private float y;
    private int exID;
    private GameObject exercise;
    private GameObject deleteButton;
    private GameObject editButton;

    void Start()
    {
        if (exercises.Count > 0) {
            noExercisesText.gameObject.SetActive(false);
        } else {
            noExercisesText.gameObject.SetActive(true);
        }
        foreach (Exercise exercise in CurrentExercise.getExercises()){
            this.addExercise(exercise.getID(), exercise.getNombre());
        }
        dialogMessage.SetActive(false);
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
    public void yesDeleteClicked() {
        // Delete from list
        CurrentExercise.removeExercise(exID);
        // Delete from scroll
        Destroy(deleteButton.gameObject);
        Destroy(editButton.gameObject);
        Destroy(exercise.gameObject);
        // reassign positions
        int index = exercises.IndexOf(exercise);
        int i;
        for (i=index+1; i<exercises.Count; i++){
            exercises[i].transform.position = new Vector3(exercises[i].transform.position.x,
            exercises[i].transform.position.y + 350, 0f);
        }
        for (i=index+1; i<deleteButtons.Count; i++){
            deleteButtons[i].transform.position = new Vector3(deleteButtons[i].transform.position.x,
            deleteButtons[i].transform.position.y + 350, 0f);
        }
        for (i=index+1; i<editButtons.Count; i++){
            editButtons[i].transform.position = new Vector3(editButtons[i].transform.position.x,
            editButtons[i].transform.position.y + 350, 0f);
        }
        exercises.Remove(exercise);
        deleteButtons.Remove(deleteButton);
        editButtons.Remove(editButton);

        if (exercises.Count == 0) {
            noExercisesText.gameObject.SetActive(true);
        } 
        dialogMessage.SetActive(false);
        activateBasePanel();
    }
    public void noDeleteClicked() {
        dialogMessage.SetActive(false);
        activateBasePanel();
    }
    private void addExercise(int ID, string name) {
        if (exercises.Count > 0){
            y = exercises[exercises.Count-1].transform.position.y - 350;
        } else {
            y = 1200;
        }
        // Crear item
        GameObject exercise = new GameObject();
        exercise.AddComponent<Text>();
        exercise.GetComponent<Text>().text = name;
        exercise.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        exercise.GetComponent<Text>().color = Color.black;
        exercise.GetComponent<Text>().fontSize = 48;

        exercise.transform.position = new Vector3(xPart, y, 0f);
        exercise.transform.parent = panelParts.transform;
        exercise.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 300); 

        // Create edit button and associate it with the element
        GameObject editButton = new GameObject();
        editButton.transform.parent = panelParts.transform;
        editButton.AddComponent<RectTransform>();
        editButton.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80); 
        editButton.AddComponent<Button>();
        editButton.transform.position = new Vector3(xEdit, y+100, 0f); 
        editButton.AddComponent<Image>();
        editButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("edit");
        editButton.GetComponent<Button>().onClick.AddListener(delegate{editPart(ID, exercise, editButton);});

        // Create delete button and associate it with the element
        GameObject deleteButton = new GameObject();
        deleteButton.transform.parent = panelParts.transform;
        deleteButton.AddComponent<RectTransform>();
        deleteButton.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80); 

        deleteButton.AddComponent<Button>();
        deleteButton.transform.position = new Vector3(xCross, y+100, 0f); 
        deleteButton.AddComponent<Image>();
        deleteButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("cross");
        deleteButton.GetComponent<Button>().onClick.AddListener(delegate{deletePart(ID, exercise, deleteButton, editButton);});

        exercises.Add(exercise);
        deleteButtons.Add(deleteButton);
        editButtons.Add(editButton);

        noExercisesText.gameObject.SetActive(false);
    }

    private void deletePart(int ID, GameObject exercise, GameObject deleteButton, GameObject editButton) {
        this.exID = ID;
        this.exercise = exercise;
        this.deleteButton = deleteButton;
        this.editButton = editButton;

        dialogMessage.SetActive(true);
        deactivateBasePanel();
    }
    private void editPart(int ID, GameObject exercise, GameObject button) {
        CurrentExercise.setEditMode(true);
        CurrentExercise.setExercise(ID);
        SceneManager.LoadScene("Scenes/Interface/NuevoEjercicio");
    }
}
