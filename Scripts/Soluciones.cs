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
    void Start() {
        this.selectBase();
        BaseTemplate.setup(panel);
        SlotTemplate.setup(panel);
                // set items
        // add slots to base -> everywhere (visible?) and with no correct answer (new method)

        
        // x, y -> depende de la matriz seleccionada
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
   public void aceptar() {
       // save solutions
        // CurrentExercise.setSolutions();
       // go back to "crear contenido" page
       SceneManager.LoadScene("Scenes/Interface/CrearContenido");
   }

   public void cancelar() {
       // go back to "crear contenido" page
       SceneManager.LoadScene("Scenes/Interface/CrearContenido");
   }
}
