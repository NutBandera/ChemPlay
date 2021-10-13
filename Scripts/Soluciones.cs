using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Soluciones : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    private ItemSlot[] slots;
    private Dictionary<int, string> dic;
    [SerializeField] private InputField xInput;
    [SerializeField] private InputField yInput;
    [SerializeField] private GameObject logoutConfirmation;
    private ParteContenido part;
    private static string selectedBase;

    void Start() {
        part = new ParteContenido();
        part.setBaseName(selectedBase);
        dic = new Dictionary<int, string>();
        // permitir quitar elemento arrastrado
        BaseTemplate.setup(panel);
        SlotTemplate.setup(panel);

        logoutConfirmation.SetActive(false);

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

   public void changeMatrixSize() {
       // look for better way
       // eliminar lo anterior
       this.clear();
       SlotTemplate.colocarSlotsCompleto(450, 1000, int.Parse(xInput.text), int.Parse(yInput.text), 700, 500, false);
       // no dejar dar a aceptar sin numeros
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
       var items = FindObjectsOfType<InterfaceDragAndDrop>();
       foreach (InterfaceDragAndDrop item in items) {
           if (!item.getInInitialPos())
            Destroy(item.gameObject);
        }
        slots = FindObjectsOfType<ItemSlot>();
        foreach (ItemSlot slot in slots) {
            slot.setCorrectItem(null);
        }
   }

   public static void setSelectedBase(string name) {
       selectedBase = name;
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
