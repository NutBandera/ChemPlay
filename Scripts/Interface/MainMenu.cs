using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
 public void teacherRole() {
        SceneManager.LoadScene("Scenes/Interface/TeacherLogin");
    }

    public void studentRole() {
        SceneManager.LoadScene("Scenes/Interface/StudentMenu");
    }
}
