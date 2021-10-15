using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StudentResources : MonoBehaviour
{
    public void goBack() {
        SceneManager.LoadScene("Scenes/Interface/StudentMenu");
    }
}
