using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class VerEjercicios : MonoBehaviour
{
    private List<GameObject> exercises = new List<GameObject>();
    private List<GameObject> buttons = new List<GameObject>();
    [SerializeField] private GameObject panelParts;
    [SerializeField] private Text noExercisesText;
    private int xPart = 400;
    private int xCross = 950;
    private float y;
    void Start()
    {
        if (exercises.Count > 0) {
            noExercisesText.gameObject.SetActive(false);
        } else {
            noExercisesText.gameObject.SetActive(true);
        }
        foreach (Exercise exercise in CurrentExercise.getExercises()){
            this.addExercise(exercise.getNombre());
        }
    }
    private void addExercise(string name) {
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

        // Create delete button and associate it with the element
        GameObject button = new GameObject();
        button.transform.parent = panelParts.transform;
        button.AddComponent<RectTransform>();
        button.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80); 
        button.AddComponent<Button>();
        button.transform.position = new Vector3(xCross, y+100, 0f); 
        button.AddComponent<Image>();
        button.GetComponent<Image>().sprite = Resources.Load<Sprite>("cross");
        button.GetComponent<Button>().onClick.AddListener(delegate{deletePart(name, exercise, button);});

        exercises.Add(exercise);
        buttons.Add(button);

         noExercisesText.gameObject.SetActive(false);
    }

    private void deletePart(string name, GameObject part, GameObject button) {
        // Delete from list
        CurrentExercise.removeExercise(name);
        // Delete from scroll
        Destroy(button.gameObject);
        Destroy(part.gameObject);
        // reassign positions
        int index = exercises.IndexOf(part);
        int i;
        for (i=index+1; i<exercises.Count; i++){
            exercises[i].transform.position = new Vector3(exercises[i].transform.position.x,
            exercises[i].transform.position.y + 350, 0f);
        }
        for (i=index+1; i<buttons.Count; i++){
            buttons[i].transform.position = new Vector3(buttons[i].transform.position.x,
            buttons[i].transform.position.y + 350, 0f);
        }
        exercises.Remove(part);
        buttons.Remove(button);

        if (exercises.Count == 0) {
            noExercisesText.gameObject.SetActive(true);
        } 
    }
}
