using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProbarEjercicios : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject buttonNex;
    [SerializeField] private GameObject buttonBack;
    [SerializeField] private GameObject buttonHome;
    [SerializeField] private Text noExercisesText;
    private int index;
    void Start()
    {
        SlotTemplate.setup(panel);
        BaseTemplate.setup(panel);

        if (CurrentExercise.getExercises().Count > 0) {
            noExercisesText.gameObject.SetActive(false);
            this.index = 0;
            createExercise();
        } else {
            buttonNex.SetActive(false); 
            buttonHome.SetActive(false);
        }
    }

    private void createExercise() {
        SlotTemplate.resetExerciseItems();
        Exercise ex = CurrentExercise.getExercises()[index];
        
        BaseTemplate.colocarEnunciado(ex.getEnunciado());
        // colocar bases SIZE
        foreach (ParteContenido part in ex.getContenido()) {
            SlotTemplate.createExerciseItem(500, 300, 1, part.getImage());   
        }
        SlotTemplate.colocarExerciseItems();
        // colocar slots
        int i = 0;
        foreach (ParteContenido part in ex.getContenido()) {
            SlotTemplate.clocarSlotsDimensions(part.getSolutions(),
            i, part.getWidth(), part.getHeight(), part.getPixelsX(), part.getPixelsY());
            i++;
        }
         // create exercise items
        BaseTemplate.createItems(ex.getItems(), 100);
        //plane.GetComponent<Image>().color = new Color(1f,1f,1f,0f);

        // last exercise
        if (CurrentExercise.getExercises().Count - 1 == index) {
            buttonNex.SetActive(false);
        }
    }

    public void next() {
        index++;
        reset();
        createExercise();
    }

    public void back() {
        if (index == 0) {
            SceneManager.LoadScene("Scenes/Interface/TeacherMenu");
        } else {
            index--;
            reset();
            createExercise();
            buttonNex.SetActive(true);
        }
    }

    public void home() {
        SceneManager.LoadScene("Scenes/Interface/TeacherMenu");
    }

    private void reset() {
        foreach(Transform child in panel.transform) {
            Destroy(child.gameObject);
        }
    }
    // Idea: un panel para cada ejercicio. Si ya está creado -> ocultar al pasar de ejercicio
}
