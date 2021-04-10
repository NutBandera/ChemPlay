using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Ejer111 : MonoBehaviour
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
            [162] = "sp"
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

        List<string> images = new List<string>{"sp", "sp2", "sp3"};

        SlotTemplate.createExerciseItem(7, 4, ajuste, "Tema1/Parte1/Ejercicio1/base1", slots1); 
      /*  SlotTemplate.createExerciseItem(7, 4, ajuste, "Tema1/Parte1/Ejercicio1/base2", slots2); 
        SlotTemplate.createExerciseItem(7, 4, ajuste, "Tema1/Parte1/Ejercicio1/base3", slots3);
        SlotTemplate.createExerciseItem(7, 4, ajuste, "Tema1/Parte1/Ejercicio1/base4", slots4);
        SlotTemplate.createExerciseItem(7, 4, ajuste, "Tema1/Parte1/Ejercicio1/base5", slots5);
        SlotTemplate.createExerciseItem(7, 4, ajuste, "Tema1/Parte1/Ejercicio1/base6", slots6);*/
       // SlotTemplate.colocarExerciseItems(3, 2);
        SlotTemplate.colocarExerciseItems(1, 1);
        SlotTemplate.clocarSlotsDimensions(slots1, 0, 43, 19, 11, 5);
       /* SlotTemplate.clocarSlotsDimensions(slots2, 1, 11, 5);
        SlotTemplate.clocarSlotsDimensions(slots3, 2, 11, 5);
        SlotTemplate.clocarSlotsDimensions(slots4, 3, 11, 5);
        SlotTemplate.clocarSlotsDimensions(slots3, 4, 11, 5);
        SlotTemplate.clocarSlotsDimensions(slots4, 5, 11, 5);*/
        BaseTemplate.createItems(images, 1, 70);
    }
}
