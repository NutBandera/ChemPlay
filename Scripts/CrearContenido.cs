using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CrearContenido : MonoBehaviour
{
    public void addSolutions() {
        // Go to "Soluciones" page
        SceneManager.LoadScene("Scenes/Interface/Soluciones");
    }

    public void createExercise() {
        SceneManager.LoadScene("Scenes/ExerciseType1");
    }
}
