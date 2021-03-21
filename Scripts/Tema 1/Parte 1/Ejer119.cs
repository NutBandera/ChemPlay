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
            [3] = "h",
            [14] = "una",
            [23] = "h",
            [24] = "una",
            [26] = "una",
            [28] = "una",
            [36] = "una",
            [47] = "h"
        };
        Dictionary<int, string> slots2 = new Dictionary<int, string> {
            [5] = "h",
            [16] = "una",
            [23] = "h",
            [24] = "una",
            [26] = "dos",
            [28] = "una",
            [30] = "una",
            [31] = "h"
        };
        Dictionary<int, string> slots3 = new Dictionary<int, string> {
            [5] = "h",
            [16] = "una",
            [23] = "h",
            [24] = "una",
            [26] = "dos",
            [28] = "una",
            [30] = "una",
            [31] = "h"
        };
        Dictionary<int, string> slots4 = new Dictionary<int, string> {
            [23] = "h",
            [24] = "una",
            [26] = "una",
            [28] = "dos",
            [36] = "una",
            [38] = "una",
            [47] = "h",
            [49] = "h"
        };
        Dictionary<int, string> slots5 = new Dictionary<int, string> {
            [5] = "h",
            [16] = "una",
            [23] = "h",
            [24] = "una",
            [26] = "dos",
            [28] = "una",
            [30] = "una",
            [31] = "h"
        };
        Dictionary<int, string> slots6 = new Dictionary<int, string> {
            [23] = "h",
            [24] = "una",
            [26] = "una",
            [28] = "dos",
            [36] = "una",
            [38] = "una",
            [47] = "h",
            [49] = "h"
        };

        List<string> images = new List<string>{"una", "dos", "tres", "menos", "mas", "horizontal", "c", "o", "n", "h", "f"};

        SlotTemplate.createExerciseItem(4, 4, ajuste, "Tema1/Parte1/Ejercicio9/c-left", slots1); 
        SlotTemplate.createExerciseItem(4, 4, ajuste, "Tema1/Parte1/Ejercicio9/o", slots2); 
        SlotTemplate.createExerciseItem(4, 4, ajuste, "Tema1/Parte1/Ejercicio9/c-left", slots3);
        SlotTemplate.createExerciseItem(4, 4, ajuste, "Tema1/Parte1/Ejercicio9/b", slots4);
        SlotTemplate.createExerciseItem(4, 4, ajuste, "Tema1/Parte1/Ejercicio9/n", slots5);
        SlotTemplate.createExerciseItem(4, 4, ajuste, "Tema1/Parte1/Ejercicio9/c", slots6);
        SlotTemplate.colocarExerciseItems();
        SlotTemplate.clocarSlotsDimensions(slots1, 0, 11, 5);
        SlotTemplate.colocarTitulo("Tema1/Parte1/Ejercicio9/title1", 0, 200, 70);
        SlotTemplate.clocarSlotsDimensions(slots2, 1, 11, 5);
        SlotTemplate.colocarTitulo("Tema1/Parte1/Ejercicio9/title2", 1, 130, 80);
        SlotTemplate.clocarSlotsDimensions(slots3, 2, 11, 5);
        SlotTemplate.colocarTitulo("Tema1/Parte1/Ejercicio9/title3", 2, 250, 80);
        SlotTemplate.clocarSlotsDimensions(slots4, 3, 11, 5);
        SlotTemplate.colocarTitulo("Tema1/Parte1/Ejercicio9/title4", 3, 130, 70);
        SlotTemplate.clocarSlotsDimensions(slots5, 4, 11, 5);
        SlotTemplate.colocarTitulo("Tema1/Parte1/Ejercicio9/title5", 4, 130, 70);
        SlotTemplate.clocarSlotsDimensions(slots6, 5, 11, 5);
        SlotTemplate.colocarTitulo("Tema1/Parte1/Ejercicio9/title6", 5, 150, 80);
        BaseTemplate.createItems(images, 2, 70);
    }
}
