using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using SFB; 

public class CreateExercise : MonoBehaviour
{
    [SerializeField] private GameObject selectedEnunciado;
    [SerializeField] private Button contenidoButton;
    [SerializeField] private GameObject panelItems;
    [SerializeField] private InputField inputNombre;
    [SerializeField] private InputField inputEnunciado;
    [SerializeField] private Text noItemsText;
    [SerializeField] private GameObject panel;
    private Button[] panelButtons;
    private InputField[] inputFields;
    [SerializeField] GameObject dialogMessage;
    [SerializeField] GameObject itemsLimitMessage;
    [SerializeField] GameObject itemAlreadyAddedMessage;
    private int xItem = 400;
    private int xCross = 730;
    private float y;
    private List<GameObject> items = new List<GameObject>();
    private List<GameObject> buttons = new List<GameObject>();

    void Start() {
        if (CurrentExercise.getEditMode()) {
            contenidoButton.GetComponentInChildren<Text>().text = "Editar contenido";
        }
        setFields();
        
        dialogMessage.SetActive(false);
        itemsLimitMessage.SetActive(false);
        itemAlreadyAddedMessage.SetActive(false);
        panelButtons = panel.GetComponentsInChildren<Button>();
        inputFields = panel.GetComponentsInChildren<InputField>();
    }
    private void setFields(){
        inputNombre.text = CurrentExercise.getNombre();
        inputEnunciado.text = CurrentExercise.getEnunciado();

        if (CurrentExercise.getEnunciado() != null) {
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(File.ReadAllBytes(string.Concat(CurrentExercise.getRoute(), CurrentExercise.getEnunciado())));
            selectedEnunciado.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        }
        contenidoButton.interactable = true;

        foreach (Item item in CurrentExercise.getItems()) {
            addItem(item.getBytes(), item.getNombre());
        }
    }
    public void aceptarClicked() {
       dialogMessage.SetActive(false);
       itemsLimitMessage.SetActive(false);
       itemAlreadyAddedMessage.SetActive(false);
       activateBasePanel();
   }
     public void deactivateBasePanel() {
        foreach (Button button in panelButtons) {
            button.interactable = false;
        }
        foreach (InputField inputField in inputFields) {
            inputField.interactable = false;
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
    public void selectItem() {
        if (items.Count < 12) {
            string[] fileTypes = new string[] { "image/*" };
            NativeFilePicker.PickFile( ( path ) =>
			{
				if( path == null )
					Debug.Log( "Operation cancelled" );
				else {
                    var pathSplitted = path.Split('/');
                    var elementName = pathSplitted[pathSplitted.Length - 1].ToString().Split('.')[0];

                    if (CurrentExercise.findItemByName(elementName) == null) {
                        byte[] bytes = File.ReadAllBytes(path);

                        Item item = new Item();
                        item.setNombre(elementName);
                        item.setBytes(bytes);
                        CurrentExercise.addItem(item);

                        addItem(bytes, elementName);

                    } else {
                        itemAlreadyAddedMessage.SetActive(true);
                        deactivateBasePanel();
                    }

                    if (CurrentExercise.getEnunciado() != null && !string.IsNullOrEmpty(inputNombre.text)) {
                        contenidoButton.interactable = true;
                    } 
                } 
		}, fileTypes);
        } else {
        // show message
        itemsLimitMessage.SetActive(true);
        deactivateBasePanel();
        }
            
    }

    private void addItem(byte[] bytes, string elementName){
        if (items.Count > 0){
            y = items[items.Count-1].transform.position.y - 100;
        } else {
            y = 600;
        }
        // Crear item
        GameObject item = new GameObject();
        item.AddComponent<Image>();

        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(bytes);

        item.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        item.transform.position = new Vector3(xItem, y, 0f);
        item.transform.parent = panelItems.transform;
        item.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80); 

        // Create delete button and associate it with the element
        GameObject button = new GameObject();
        button.transform.parent = panelItems.transform;
        button.AddComponent<RectTransform>();
        button.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80); 
        button.AddComponent<Button>();
        button.transform.position = new Vector3(xCross, y, 0f); 
        button.AddComponent<Image>();
        button.GetComponent<Image>().sprite = Resources.Load<Sprite>("cross");
        button.GetComponent<Button>().onClick.AddListener(delegate{deleteItem(elementName, item, button);});

        items.Add(item);
        buttons.Add(button);

        noItemsText.gameObject.SetActive(false);
    }

    private void deleteItem(string elementName, GameObject item, GameObject button) {
        // Delete from list
        CurrentExercise.removeItem(elementName);
        // Delete from scroll
        Destroy(button.gameObject);
        Destroy(item.gameObject);
        // reassign positions
        int index = items.IndexOf(item);
        int i;
        for (i=index+1; i<items.Count; i++){
            // add 100 to each
            items[i].transform.position = new Vector3(items[i].transform.position.x,
            items[i].transform.position.y + 100, 0f);
        }
        for (i=index+1; i<buttons.Count; i++){
            // add 100 to each
            buttons[i].transform.position = new Vector3(buttons[i].transform.position.x,
            buttons[i].transform.position.y + 100, 0f);
        }
        items.Remove(item);
        buttons.Remove(button);

        if (items.Count == 0){
            contenidoButton.interactable = false;
            noItemsText.gameObject.SetActive(true);
        }
    }
    public void crearContenido() {
        if (inputNombre.text.Length < 3) {
            dialogMessage.SetActive(true);
            deactivateBasePanel();
        } else if (inputEnunciado.text.Length < 20) {
            // TODO: crear otro dialogo
            dialogMessage.SetActive(true);
            deactivateBasePanel();
        } else {
            CurrentExercise.setNombre(inputNombre.text);
            CurrentExercise.setEnunciado(inputEnunciado.text);
            SceneManager.LoadScene("Scenes/Interface/CrearContenido");
        }
    }
    public void nameChanged() {
        if (string.IsNullOrEmpty(inputNombre.text)){
            contenidoButton.interactable = false;
        } else {
            if (items.Count > 0 && CurrentExercise.getEnunciado() != null) {
                contenidoButton.interactable = true;
            }
        }
    }
    public void enunciadoChanged() {
        if (string.IsNullOrEmpty(inputEnunciado.text)){
            contenidoButton.interactable = false;
        } else {
            if (items.Count > 0 && CurrentExercise.getEnunciado() != null) {
                contenidoButton.interactable = true;
            }
        }
    }
}
