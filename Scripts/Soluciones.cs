using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using SFB;

public class Soluciones : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    private InterfaceItemSlot[] slots;
    private Dictionary<int, string> dic;
    [SerializeField] private InputField xInput;
    [SerializeField] private InputField yInput;
    private ParteContenido part;

    void Start() {
        part = new ParteContenido();
        this.selectBase(); // what if user doesnt select
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

     public void selectBase() {
        var path = StandaloneFileBrowser.OpenFilePanel("Open File", "", "", false); // accepted extions
        var pathSplitted = Regex.Split(path[0], "Resources/");
        var elementName = pathSplitted[1].ToString();
        var baseName = elementName.Split('.')[0];
        part.setBaseName(baseName);
    }
    
   public void aceptar() {
       // iterar por slots, crear diccionario
        slots = FindObjectsOfType<InterfaceItemSlot>();
        foreach (InterfaceItemSlot slot in slots)
        {
           if (!string.IsNullOrEmpty(slot.getCorrectItem())){
               dic.Add(slot.getPosition(), slot.getCorrectItem());
           }
        }
        part.setSolutions(dic);
        CurrentExercise.addContenido(part);
       // create exercise
       SceneManager.LoadScene("Scenes/Interface/CrearContenido");
   }

   private void clear() {
       slots = FindObjectsOfType<InterfaceItemSlot>();
       foreach (InterfaceItemSlot slot in slots) {
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
       var items = FindObjectsOfType<DragAndDrop>();
       foreach (DragAndDrop item in items) {
           if (!item.inInitialPos)
            Destroy(item.gameObject);
        }
        slots = FindObjectsOfType<InterfaceItemSlot>();
        foreach (InterfaceItemSlot slot in slots) {
            slot.setCorrectItem(null);
        }
   }
}
