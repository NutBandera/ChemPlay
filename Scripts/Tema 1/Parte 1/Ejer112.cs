using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Ejer112 : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private string enunciadoPath;
    [SerializeField] private int ajuste;

    void Start()
    {
        panel.GetComponent<Image>().sprite = null;
        panel.GetComponent<Image>().color = new Color(1f,1f,1f,1f);

        SlotTemplate.setup(panel);
        BaseTemplate.setup(panel);
        BaseTemplate.colocarEnunciado(enunciadoPath);

        Dictionary<int, string> slotsA = new Dictionary<int, string> {
            [13] = "vertical",
            /*[15] = "vertical",
            [21] = "horizontal",
            [23] = "horizontal-double",
            [29] = "vertical",
            [31] = "vertical",
            [33] = "vertical",
            [39] = "horizontal",*/
        };

         Dictionary<int, string> slotsB = new Dictionary<int, string> {
            [13] = "vertical",
            [15] = "vertical",
            [21] = "horizontal",
            [23] = "horizontal-double",
            [25] = "horizontal",
            [26] = "menos",
            [29] = "vertical",
            [31] = "vertical",
            [33] = "vertical",
            [39] = "horizontal",
            [41] = "horizontal-double",
            [43] = "horizontal",
            [49] = "vertical",
            [51] = "vertical",
        };
         Dictionary<int, string> slotsC = new Dictionary<int, string> {
            [1] = "horizontal",
            [5] = "vertical",
            [7] = "vertical",
            [11] = "horizontal",
            [17] = "vertical-double",
            [21] = "horizontal",
            [25] = "vertical",
            [27] = "vertical",
            [31] = "horizontal"
        };
        Dictionary<int, string> slotsD = new Dictionary<int, string> {
            [21] = "horizontal",
            [23] = "horizontal",
            [24] = "mas",
            [29] = "vertical",
            [31] = "vertical",
            [33] = "vertical",
            [39] = "horizontal",
            [41] = "horizontal"
        };

        List<string> images = new List<string>{"horizontal", "vertical", "mas", "menos"};
        
        SlotTemplate.createExerciseItem(4, 3, ajuste, "Tema1/Parte1/Ejercicio2/1");       
        SlotTemplate.createExerciseItem(4, 3, ajuste, "Tema1/Parte1/Ejercicio2/2");       
       // SlotTemplate.createExerciseItem(3, 3, ajuste, "Tema1/Parte1/Ejercicio2/3", slotsC);       
        //SlotTemplate.createExerciseItem(4, 3, ajuste, "Tema1/Parte1/Ejercicio2/4", slotsD);
        SlotTemplate.colocarExerciseItems(2, 1);
        SlotTemplate.clocarSlotsNumberOfElements(slotsA, 0, 4, 3);
        SlotTemplate.colocarTitulo("Tema1/Parte1/Ejercicio2/title1", 0, 280, 80);
        SlotTemplate.clocarSlotsNumberOfElements(slotsB, 1, 4, 3);
        SlotTemplate.colocarTitulo("Tema1/Parte1/Ejercicio2/title2", 1, 280, 80);
        /*SlotTemplate.clocarSlotsNumberOfElements(slotsC, 2, 2, 3);
        SlotTemplate.colocarTitulo("Tema1/Parte1/Ejercicio2/title3", 2, 150, 80);
        SlotTemplate.clocarSlotsNumberOfElements(slotsD, 3, 4, 3);
        SlotTemplate.colocarTitulo("Tema1/Parte1/Ejercicio2/title4", 3, 280, 80);*/
        BaseTemplate.createItems(images, 1, 50);
    }

}
