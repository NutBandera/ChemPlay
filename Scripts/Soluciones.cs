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

    void Start() {
        this.selectBase(); // what if user doesnt select
        dic = new Dictionary<int, string>();
        // permitir quitar elemento arrastrado
        BaseTemplate.setup(panel);
        SlotTemplate.setup(panel);

        // x, y -> depende de la matriz seleccionada
        // posiciones relativas
        SlotTemplate.createEmptyExerciseItem(CurrentExercise.getBase(), 5, 5, Screen.width/3, 1000);// which dimensions??

        BaseTemplate.createItems(CurrentExercise.getMockItems(), 1, 100, 1500); // + pos
    }

     public void selectBase() {
        var path = StandaloneFileBrowser.OpenFilePanel("Open File", "", "", false); // accepted extions
        var pathSplitted = Regex.Split(path[0], "Resources/");
        var elementName = pathSplitted[1].ToString();

        var baseName = elementName.Split('.')[0];

        CurrentExercise.setBase(baseName);
    }

    public void selectMatrixSize(){
        // CurrentExercise.setWidth(x);
        // CurrentExercise.setHeight(y);
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
       // go back to "crear contenido" page
       SceneManager.LoadScene("Scenes/Interface/interface");
   }
 
   public void cancelar() {
       // go back to "crear contenido" page
       SceneManager.LoadScene("Scenes/Interface/CrearContenido");
   }
}
