using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using SFB; 

public class CrearContenido : MonoBehaviour
{
    private int xPart = 400;
    private int xCross = 950;
    private int xEdit = 850;
    private float y;
    private List<GameObject> parts = new List<GameObject>();
    private List<GameObject> deleteButtons = new List<GameObject>();
    private List<GameObject> editButtons = new List<GameObject>();
    [SerializeField] private GameObject panelParts;
    [SerializeField] private GameObject panel;
    [SerializeField] private Button buttonCreateExercise;
    private InputField[] inputFields;
    [SerializeField] GameObject dialogMessage;
    [SerializeField] GameObject limitPartsMessage;
    [SerializeField] private Text noContentText;
    [SerializeField] GameObject partAlreadyAddedMessage;
    private string partName;
    private GameObject part;
    private GameObject deleteButton;
    private GameObject editButton;

    public void Start() {
        foreach (ParteContenido part in CurrentExercise.getContenido()) {
            this.addPart(part.getImage(), part.getBaseName());
        }
        dialogMessage.SetActive(false);
        limitPartsMessage.SetActive(false);
        partAlreadyAddedMessage.SetActive(false);
        inputFields = panel.GetComponentsInChildren<InputField>();
        if (CurrentExercise.getEditMode()) {
            buttonCreateExercise.GetComponentInChildren<Text>().text = "Guardar cambios";
        }
    }
    public void addSolutions() {
         if (parts.Count < 4) {
            string[] fileTypes = new string[] { "image/*" };
            NativeFilePicker.PickFile( ( path ) =>
			{
				if( path == null )
					Debug.Log( "Operation cancelled" );
				else {
                    var pathSplitted = path.Split('/');
                    var elementName = pathSplitted[pathSplitted.Length - 1].ToString().Split('.')[0];
                    // Si el path no es null -> go to "Soluciones" page
                    if (!string.IsNullOrEmpty(elementName) && CurrentExercise.findPartByName(elementName) == null){
                        Soluciones.setSelectedBaseName(elementName);
                        Soluciones.setSelectedBaseImage(File.ReadAllBytes(path));
                        Soluciones.setIsEditing(false);
                        SceneManager.LoadScene("Scenes/Interface/Soluciones");
                    } else {
                        // show message
                        partAlreadyAddedMessage.SetActive(true);
                        deactivateBasePanel();
            }
                }
			}, fileTypes );
            
        } else  {
            limitPartsMessage.SetActive(true);
            deactivateBasePanel();
        }
    }
    public void yesDeleteClicked() {
        // Delete from list
        CurrentExercise.removePart(partName);
        // Delete from scroll
        Destroy(deleteButton.gameObject);
        Destroy(editButton.gameObject);
        Destroy(part.gameObject);
        // reassign positions
        int index = parts.IndexOf(part);
        int i;
        for (i=index+1; i<parts.Count; i++){
            parts[i].transform.position = new Vector3(parts[i].transform.position.x,
            parts[i].transform.position.y + 350, 0f);
        }
        for (i=index+1; i<deleteButtons.Count; i++){
            deleteButtons[i].transform.position = new Vector3(deleteButtons[i].transform.position.x,
            deleteButtons[i].transform.position.y + 350, 0f);
        }
        for (i=index+1; i<editButtons.Count; i++){
            editButtons[i].transform.position = new Vector3(editButtons[i].transform.position.x,
            editButtons[i].transform.position.y + 350, 0f);
        }
        parts.Remove(part);
        deleteButtons.Remove(deleteButton);
        editButtons.Remove(editButton);

        dialogMessage.SetActive(false);
        activateBasePanel();

        if (parts.Count == 0) {
            noContentText.gameObject.SetActive(true);
            buttonCreateExercise.interactable = false;
        }
    }
    public void noDeleteClicked() {
        dialogMessage.SetActive(false);
        activateBasePanel();
    }
    public void aceptarClicked() {
        limitPartsMessage.SetActive(false);
        partAlreadyAddedMessage.SetActive(false);
        activateBasePanel();
    }
    public void deactivateBasePanel() {
        foreach (Button button in panel.GetComponentsInChildren<Button>()) {
            button.interactable = false;
        }
        foreach (InputField inputField in inputFields) {
            inputField.interactable = false;
        }
    }
    public void activateBasePanel() {
         foreach (Button button in panel.GetComponentsInChildren<Button>()) {
            button.interactable = true;
        }
        foreach (InputField inputField in inputFields) {
            inputField.interactable = true;
        }
    }
    public void back() {
        SceneManager.LoadScene("Scenes/Interface/NuevoEjercicio");
    }
    public void createExercise() {
        Exercise exercise = new Exercise(CurrentExercise.getIndex(), CurrentExercise.getNombre(),
        CurrentExercise.getEnunciado(), CurrentExercise.getItems(), CurrentExercise.getContenido());
        if (CurrentExercise.getEditMode()) {
            CurrentExercise.updateExercise(exercise);
        } else {
            CurrentExercise.addExercise(exercise);
        }
        CurrentExercise.setEditMode(false);
        SceneManager.LoadScene("Scenes/ExerciseType1"); //TODO change scene name
    }
    public void addPart(byte[] image, string partName) {
        if (parts.Count > 0){
            y = parts[parts.Count-1].transform.position.y - 350;
        } else {
            y = 1200;
        }
        // Crear item
        GameObject part = new GameObject();
        part.AddComponent<Image>();
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(image);
        part.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        part.transform.position = new Vector3(xPart, y, 0f);
        part.transform.parent = panelParts.transform;
        part.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 300); 

        // Create edit button and associate it with the element
        GameObject editButton = new GameObject();
        editButton.transform.parent = panelParts.transform;
        editButton.AddComponent<RectTransform>();
        editButton.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80); 
        editButton.AddComponent<Button>();
        editButton.transform.position = new Vector3(xEdit, y, 0f); 
        editButton.AddComponent<Image>();
        editButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("edit");
        editButton.GetComponent<Button>().onClick.AddListener(delegate{editPart(partName, part, editButton);});

        // Create delete button and associate it with the element
        GameObject deleteButton = new GameObject();
        deleteButton.transform.parent = panelParts.transform;
        deleteButton.AddComponent<RectTransform>();
        deleteButton.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80); 
        deleteButton.AddComponent<Button>();
        deleteButton.transform.position = new Vector3(xCross, y, 0f); 
        deleteButton.AddComponent<Image>();
        deleteButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("cross");
        deleteButton.GetComponent<Button>().onClick.AddListener(delegate{deletePart(partName, part, deleteButton, editButton);});

        parts.Add(part);
        deleteButtons.Add(deleteButton);
        editButtons.Add(editButton);

        noContentText.gameObject.SetActive(false);

        // panelParts.transform.localCorners[0].Set(0.5f, 0.5f, 1f);
        buttonCreateExercise.interactable = true;
    }
     private void deletePart(string partName, GameObject part, GameObject deleteButton, GameObject editButton) {
        this.partName = partName;
        this.part = part;
        this.deleteButton = deleteButton;
        this.editButton = editButton;

        dialogMessage.SetActive(true);
        deactivateBasePanel();
    }

    private void editPart(string partName, GameObject part, GameObject button) {
        var editedPart = CurrentExercise.findPartByName(partName);
        Soluciones.setSolutions(editedPart.getSolutions());
        Soluciones.setPart(editedPart);
        Soluciones.setIsEditing(true);
        SceneManager.LoadScene("Scenes/Interface/Soluciones");
    }

}
