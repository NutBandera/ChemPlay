using UnityEngine.SceneManagement;
using UnityEngine;

public class StudentMenu : MonoBehaviour
{
    [SerializeField] GameObject logoutConfirmation;
    void Start() {
        logoutConfirmation.SetActive(false);
    }
    public void play() {
        SceneManager.LoadScene("Scenes/Interface/StudentResources");
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
