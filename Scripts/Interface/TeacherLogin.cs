using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class TeacherLogin : MonoBehaviour
{
    [SerializeField] private InputField inputPass;
    private static string password = "pass"; // --> config file

    public void login() {
        if (inputPass.text.Equals(password)) {
            SceneManager.LoadScene("Scenes/Interface/TeacherMenu");
        } else {
            // dialog error
            Debug.Log("error");
        }
    }
    public void back() {
        SceneManager.LoadScene("Scenes/Interface/RolMenu");
    }
}
