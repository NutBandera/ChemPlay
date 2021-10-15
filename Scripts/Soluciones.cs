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
    private ItemSlot[] slots;
    private Dictionary<int, string> dic;
    [SerializeField] private InputField xInput;
    [SerializeField] private InputField yInput;
    private ParteContenido part;
    private static string selectedBase;

    void Start() {
        dialogMessage.SetActive(false);
        panelButtons = panel.GetComponentsInChildren<Button>();
        inputFields = panel.GetComponentsInChildren<InputField>();

        part = new ParteContenido();
        part.setBaseName(selectedBase);
        dic = new Dictionary<int, string>();
        // permitir quitar elemento arrastrado
        BaseTemplate.setup(panel);
        SlotTemplate.setup(panel);

        part.setWidth(5);
        part.setHeight(5);
        part.setPixelsX(-1);
        part.setPixelsY(-1);

        // posiciones relativas
        SlotTemplate.createEmptyExerciseItem(part.getBaseName(), 5, 5, 450, 1000, false);// which dimensions??
        // maybe separate in two
        BaseTemplate.createItems(CurrentExercise.getItems(), 1, 100, 1500, true); // + pos
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
        CurrentExercise.addContenido(part);
        SceneManager.LoadScene("Scenes/Interface/CrearContenido");
        // Set part in scroll
   }

   private void clear() {
       slots = FindObjectsOfType<ItemSlot>();
       foreach (ItemSlot slot in slots) {
            Destroy(slot.gameObject);
        }
   }

   public void yesClearClicked() {
      var items = FindObjectsOfType<InterfaceDragAndDrop>();
       foreach (InterfaceDragAndDrop item in items) {
           if (!item.getInInitialPos())
            Destroy(item.gameObject);
        }
        slots = FindObjectsOfType<ItemSlot>();
        foreach (ItemSlot slot in slots) {
            slot.setCorrectItem(null);
        }
        dialogMessage.SetActive(false);
        activateBasePanel();
   }
   public void noClearClicked() {
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
    }
    public void activateBasePanel() {
         foreach (Button button in panelButtons) {
            button.interactable = true;
        }
        foreach (InputField inputField in inputFields) {
            inputField.interactable = true;
        }
    }

   public void changeMatrixSize() {
        // limite 25
        // check not blank or negative
       // look for better way
       // eliminar lo anterior
       this.clear();
       SlotTemplate.colocarSlotsCompleto(450, 1000, int.Parse(xInput.text), int.Parse(yInput.text), 700, 500, false);
    
       part.setPixelsX(-1);
       part.setPixelsY(-1);
       part.setWidth(int.Parse(xInput.text));
       part.setHeight(int.Parse(yInput.text));
       this.clearItems();
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
       // go back to "crear contenido" page
       SceneManager.LoadScene("Scenes/Interface/CrearContenido");
   }

   public void clearItems() {
       dialogMessage.SetActive(true);
       deactivateBasePanel();
   }

   public static void setSelectedBase(string name) {
       selectedBase = name;
   }
   
}
