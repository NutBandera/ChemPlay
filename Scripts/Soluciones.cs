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
    private GameObject baseImage;

    void Start() {
        this.selectBase(); // what if user doesnt select
        dic = new Dictionary<int, string>();
        // permitir quitar elemento arrastrado
        BaseTemplate.setup(panel);
        SlotTemplate.setup(panel);

        // x, y -> depende de la matriz seleccionada
        // posiciones relativas
        int x = (Screen.width / 2) - 500/4; // width, not 500
        SlotTemplate.createEmptyExerciseItem(CurrentExercise.getBase(), 5, 5, 558, 1000);// which dimensions??
        // maybe separate in two
        BaseTemplate.createItems(CurrentExercise.getMockItems(), 1, 100, 1500); // + pos
    }

     public void selectBase() {
        var path = StandaloneFileBrowser.OpenFilePanel("Open File", "", "", false); // accepted extions
        var pathSplitted = Regex.Split(path[0], "Resources/");
        var elementName = pathSplitted[1].ToString();

        var baseName = elementName.Split('.')[0];

        CurrentExercise.setBase(baseName);
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
        CurrentExercise.setSolutions(dic);
        CurrentExercise.setWidth(int.Parse(xInput.text));
        CurrentExercise.setHeight(int.Parse(yInput.text));
       // go back to "crear contenido" page
       SceneManager.LoadScene("Scenes/Interface/interface");
   }

   public void changeMatrixSize() {
       // look for better way
       // eliminar lo anterior
       slots = FindObjectsOfType<InterfaceItemSlot>();
       foreach (InterfaceItemSlot slot in slots) {
            Destroy(slot.gameObject);
        }
       SlotTemplate.colocarSlotsCompleto(558, 1000, int.Parse(xInput.text), int.Parse(yInput.text), 500, 500);
       // no dejar dar a aceptar sin numeros
   }
 
   public void cancelar() {
       // go back to "crear contenido" page
       SceneManager.LoadScene("Scenes/Interface/CrearContenido");
   }
}
