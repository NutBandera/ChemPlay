using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StudentResources : MonoBehaviour
{
    [SerializeField] private GameObject logoutConfirmation;

    void Start() {
        logoutConfirmation.SetActive(false);
    }
    public void goBack() {
        SceneManager.LoadScene("Scenes/Interface/StudentMenu");
    }

      public void logout() {
        logoutConfirmation.SetActive(true);
    }
    public void yesClicked() {
        SceneManager.LoadScene("Scenes/Interface/RolMenu");
    }
    public void noClicked() {
        logoutConfirmation.SetActive(false);
    }
}
