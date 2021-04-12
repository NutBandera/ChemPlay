using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Ejer119 : MonoBehaviour
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

        Dictionary<int, string> slots1 = new Dictionary<int, string> {
            [2] = "h",
            [6] = "h",
            [9] = "enlace3",
            [11] = "horizontal",
            [12] = "enlace2",
            [14] = "h",
            [15] = "enlace",
            [17] = "enlace",
            [18] = "n",
            [23] = "enlace3",
            [26] = "enlace1",
            [30] = "h",
            [34] = "h"
        };
        Dictionary<int, string> slots2 = new Dictionary<int, string> {
            [3] = "horizontal",
            [10] = "n",
            [16] = "dos",
            [18] = "enlace1",
            [19] = "horizontal",
            [20] = "menos",
            [21] = "vertical",
            [22] = "o",
            [27] = "vertical",
            [29] = "horizontal",
            [33] = "horizontal"
        };
        Dictionary<int, string> slots3 = new Dictionary<int, string> {
            [1] = "h",
            [6] = "una",
            [7] = "h",
            [21] = "una",
            [28] = "dos",
            [14] = "una",
            [35] = "una"
        };
        Dictionary<int, string> slots4 = new Dictionary<int, string> {
            [10] = "f",
            [17] = "enlace3",
            [30] = "enlace2",
            [32] = "enlace1",
            [36] = "f",
            [40] = "f"
        };
        Dictionary<int, string> slots5 = new Dictionary<int, string> {
            [8] = "h",
            [17] = "enlace1",
            [20] = "mas",
            [22] = "horizontal",
            [26] = "c",
            [27] = "dos",
            [29] = "dos",
            [30] = "n",
            [31] = "menos",
            [33] = "enlace2",
            [38] = "horizontal",
            [40] = "h"
        };
        Dictionary<int, string> slots6 = new Dictionary<int, string> {
            [6] = "menos",
            [12] = "vertical",
            [13] = "o",
            [14] = "vertical",
            [20] = "enlace2",
            [25] = "o",
            [26] = "dos",
            [36] = "enlace1",
            [38] = "menos",
            [44] = "vertical",
            [45] = "o",
            [46] = "vertical",
            [53] = "horizontal"
        };

        List<string> images = new List<string>{"enlace", "dos", "tres", "menos", "mas", "horizontal", "c", "o", "n", "h", "f"};
        SlotTemplate.setNumberRowsItems(2);
        
        SlotTemplate.createExerciseItem(4, 4, ajuste, "Tema1/Parte1/Ejercicio9/c-left", slots1); 
        SlotTemplate.createExerciseItem(4, 4, ajuste, "Tema1/Parte1/Ejercicio9/o", slots2); 
        SlotTemplate.createExerciseItem(4, 4, ajuste, "Tema1/Parte1/Ejercicio9/c-left", slots3);
        SlotTemplate.createExerciseItem(4, 4, ajuste, "Tema1/Parte1/Ejercicio9/b", slots4);
        SlotTemplate.createExerciseItem(4, 4, ajuste, "Tema1/Parte1/Ejercicio9/n", slots5);
        SlotTemplate.createExerciseItem(4, 4, ajuste, "Tema1/Parte1/Ejercicio9/c", slots6);
        SlotTemplate.colocarExerciseItems(3, 2);
        SlotTemplate.clocarSlotsDimensions(slots1, 0, 7, 6);
        SlotTemplate.colocarTitulo("Tema1/Parte1/Ejercicio9/title1", 0, 200, 70);
        SlotTemplate.clocarSlotsDimensions(slots2, 1, 7, 8);
        SlotTemplate.colocarTitulo("Tema1/Parte1/Ejercicio9/title2", 1, 130, 80);
        SlotTemplate.clocarSlotsDimensions(slots3, 2, 7, 6);
        SlotTemplate.colocarTitulo("Tema1/Parte1/Ejercicio9/title3", 2, 250, 80);
        SlotTemplate.clocarSlotsDimensions(slots4, 3, 7, 7);
        SlotTemplate.colocarTitulo("Tema1/Parte1/Ejercicio9/title4", 3, 130, 70);
        SlotTemplate.clocarSlotsDimensions(slots5, 4, 8, 7);
        SlotTemplate.colocarTitulo("Tema1/Parte1/Ejercicio9/title5", 4, 130, 70);
        SlotTemplate.clocarSlotsDimensions(slots6, 5, 8, 7);
        SlotTemplate.colocarTitulo("Tema1/Parte1/Ejercicio9/title6", 5, 150, 80);
        BaseTemplate.createItems(images, 2, 50);
    }
}
