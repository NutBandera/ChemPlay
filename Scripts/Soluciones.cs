using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Soluciones : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    private Button[] panelButtons;
    private InputField[] inputFields;
    [SerializeField] GameObject dialogMessage;
    [SerializeField] GameObject incorrectFormatMessage;
    [SerializeField] GameObject changeMatrixSizeMessage;
    private ItemSlot[] slots;
    private Dictionary<int, string> dic;
    private static Dictionary<int, string> _solutions;
    private static ParteContenido _part;
    [SerializeField] private InputField xInput;
    [SerializeField] private InputField yInput;
    private ParteContenido part;
    private static string selectedBase;
    private static bool _isEditing;

    void Start() {
        dialogMessage.SetActive(false);
        incorrectFormatMessage.SetActive(false);
        changeMatrixSizeMessage.SetActive(false);
        panelButtons = panel.GetComponentsInChildren<Button>();
        inputFields = panel.GetComponentsInChildren<InputField>();

        BaseTemplate.setup(panel);
        SlotTemplate.setup(panel);

        if (!_isEditing) {
            dic = new Dictionary<int, string>();

            part = new ParteContenido();
            part.setBaseName(selectedBase);
            
            part.setWidth(5);
            part.setHeight(5);
            part.setPixelsX(-1);
            part.setPixelsY(-1);

            // posiciones relativas
            SlotTemplate.createEmptyExerciseItem(part.getBaseName(), 5, 5, 450, 1000, false); // which dimensions??
        } else {
            // edit mode
            dic = _solutions;
            SlotTemplate.createEmptyExerciseItem(_part.getBaseName(), _part.getWidth(),
             _part.getHeight(), 450, 1000, false); // which dimensions??
            colocarItems();
            part = _part;
        }

        BaseTemplate.createItems(CurrentExercise.getItems(), 100, 1500, true);
    }
    private void colocarItems() {
        foreach (var solution in _solutions) {
            if (CurrentExercise.findItemByName(solution.Value) != null) {
                GameObject item = new GameObject();
                item.AddComponent<Image>();
                
                Item foundItem = CurrentExercise.findItemByName(solution.Value);
                Texture2D tex = new Texture2D(2, 2);
                tex.LoadImage(foundItem.getBytes());

                item.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
                var coordinates = SlotTemplate.convertPosition(solution.Key, _part.getWidth(), _part.getHeight());
                var sizeX = 700/_part.getWidth();
                var sizeY = 500/_part.getHeight();
                var initialPosX = 450 - 700/2 + sizeX/2;
                var initialPosY = 1000 + 500/2 - sizeY/2;   
                item.transform.position = new Vector3(initialPosX+sizeX*coordinates[1],
                initialPosY-sizeY*coordinates[0], 0f);
                item.transform.parent = panel.transform;
                item.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100); 
                item.AddComponent<CanvasGroup>();
                item.AddComponent<SolutionsDragAndDrop>(); 
                item.GetComponent<SolutionsDragAndDrop>().setName(solution.Value); 
                item.GetComponent<SolutionsDragAndDrop>().setInInitialPos(false); 
                item.AddComponent<BoxCollider2D>();
                item.GetComponent<BoxCollider2D>().isTrigger = true;
                item.AddComponent<Rigidbody2D>();
                item.GetComponent<Rigidbody2D>().gravityScale = 0;
                item.GetComponent<Rigidbody2D>().freezeRotation = true;
            }
        }
    }
    
   public void aceptar() {
       // iterar por slots, crear diccionario
        slots = FindObjectsOfType<ItemSlot>();
        foreach (ItemSlot slot in slots)
        {
           if (!string.IsNullOrEmpty(slot.getCorrectItem())){
               dic.Add(slot.getPosition(), slot.getCorrectItem());
           }
        }
        part.setSolutions(dic);
        setSolutions(null);
        if (_isEditing) {
            CurrentExercise.updateContenido(part);
        } else {
            CurrentExercise.addContenido(part);
        }
        setIsEditing(false);
        SceneManager.LoadScene("Scenes/Interface/CrearContenido");
   }

   private void clear() {
       slots = FindObjectsOfType<ItemSlot>();
       foreach (ItemSlot slot in slots) {
            Destroy(slot.gameObject);
        }
   }
   public static void setSolutions(Dictionary<int, string> solutions) {
       // colocar las soluciones que ya tiene esa parte
        _solutions = solutions;
   }
   public static void setPart(ParteContenido part) {
       _part = part;
   }

   public void yesClearClicked() {
        clearItems();
        dialogMessage.SetActive(false);
        activateBasePanel();
   }
   private void clearItems() {
       var items = FindObjectsOfType<SolutionsDragAndDrop>();
       foreach (SolutionsDragAndDrop item in items) {
           if (!item.getInInitialPos())
            Destroy(item.gameObject);
        }
        slots = FindObjectsOfType<ItemSlot>();
        foreach (ItemSlot slot in slots) {
            slot.setCorrectItem(null);
        }
   }
   public void noClearClicked() {
        dialogMessage.SetActive(false);
        activateBasePanel();
   }
   public void aceptarClicked() {
        incorrectFormatMessage.SetActive(false);
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
    public void changeMatrixSizeClicked() {
        changeMatrixSizeMessage.SetActive(true);
    }
    public void changeSizeYesClicked() {
        changeMatrixSize();
        changeMatrixSizeMessage.SetActive(false);
        activateBasePanel();
    }
    public void changeSizeNoClicked() {
        changeMatrixSizeMessage.SetActive(false);
        activateBasePanel();
    }
   private void changeMatrixSize() {
       if (string.IsNullOrEmpty(xInput.text) || string.IsNullOrEmpty(yInput.text)){
           incorrectFormatMessage.SetActive(true);
           deactivateBasePanel();
       } else {
            var x = int.Parse(xInput.text);
            var y = int.Parse(yInput.text);
            if (x > 25 || y > 25 || x < 1 || y < 1) {
                incorrectFormatMessage.SetActive(true);
                deactivateBasePanel();
            } else {
                this.clear();
                    SlotTemplate.colocarSlotsCompleto(450, 1000, int.Parse(xInput.text), int.Parse(yInput.text), 700, 500, false);
                    
                    part.setPixelsX(-1);
                    part.setPixelsY(-1);
                    part.setWidth(int.Parse(xInput.text));
                    part.setHeight(int.Parse(yInput.text));
                    this.clearItems();
            }
       }
   }

   public void changeToPixels() {
       this.clear();
       SlotTemplate.colocarSlotsCompleto(450, 1000, 20, 20, 700, 500, true); // pas true to hide the pixels
       part.setPixelsX(5);
       part.setPixelsY(5);
       part.setWidth(20);
       part.setHeight(20);
   }
 
   public void cancelar() {
       SceneManager.LoadScene("Scenes/Interface/CrearContenido");
   }

   public void clearItemsClicked() {
       dialogMessage.SetActive(true);
       deactivateBasePanel();
   }

   public static void setSelectedBase(string name) {
       selectedBase = name;
   }
   public static void setIsEditing(bool isEditing) {
       _isEditing = isEditing;
   }
   
}
