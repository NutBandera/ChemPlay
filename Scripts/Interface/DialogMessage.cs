using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DialogMessage : MonoBehaviour
{
    [SerializeField] GameObject dialogMessage;
    [SerializeField] GameObject basePanel;
    private Button[] panelButtons;
    private InputField[] inputFields;
    [SerializeField] private InputField inputPass;
    private static string password = "pass"; // --> config file

    public void Start() {
        dialogMessage.SetActive(false);
        panelButtons = basePanel.GetComponentsInChildren<Button>();
        inputFields = basePanel.GetComponentsInChildren<InputField>();
    }
    public void login() { //TODO -> mover a su script
        if (inputPass.text.Equals(password)) {
            SceneManager.LoadScene("Scenes/Interface/TeacherMenu");
        } else {
            dialogMessage.SetActive(true);
            deactivateBasePanel();
        }
    }
    public void logout() {
        CurrentExercise.reset();
        dialogMessage.SetActive(true);
        deactivateBasePanel();
    }
    public void yesClicked() {
        SceneManager.LoadScene("Scenes/Interface/RolMenu");
    }
    public void close() {
        dialogMessage.SetActive(false);
        activateBasePanel();
    }

    public void deactivateBasePanel() {
        foreach (Button button in panelButtons) {
            button.interactable = false;
        }
        foreach (InputField inputField in inputFields) {
            inputField.interactable = false;
        }
        foreach (SolutionsDragAndDrop item in basePanel.GetComponentsInChildren<SolutionsDragAndDrop>()){
            // item.SetActive();
        }
    }
    public void activateBasePanel() {
         foreach (Button button in panelButtons) {
            button.interactable = true;
        }
        foreach (InputField inputField in inputFields) {
            inputField.interactable = true;
        }
    }
}