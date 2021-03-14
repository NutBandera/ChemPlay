using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Ejer115 : MonoBehaviour
{
   [SerializeField] private GameObject panel;
    [SerializeField] private string enunciadoPath;
    [SerializeField] private int ajuste;

    void Start()
    {
        panel.GetComponent<Image>().sprite = null;
        panel.GetComponent<Image>().color = new Color(1f,1f,1f,1f);

        SlotTemplate.setup(panel);
        SlotTemplate.colocarEnunciado(enunciadoPath, panel);

        Dictionary<int, string> slotsA = new Dictionary<int, string> {
            [0] = "sp",
            [12] = "sp2",
        };
         Dictionary<int, string> slotsB = new Dictionary<int, string> {
            [3] = "sp",
            [1] = "sp2",
        };
         Dictionary<int, string> slotsC = new Dictionary<int, string> {
            [3] = "sp",
            [1] = "sp2",
        };
        Dictionary<int, string> slotsD = new Dictionary<int, string> {
            [3] = "sp",
            [1] = "sp2",
        };

        List<string> images = new List<string>{"c"};

        // alternativa -> lista con elementos correctos (null, sp, sp3, null null, ...) 

        SlotTemplate.createExerciseItem(4, 3, ajuste, "Tema1/Parte1/Ejercicio2/1", slotsA); 
        SlotTemplate.createExerciseItem(4, 3, ajuste, "Tema1/Parte1/Ejercicio2/2", slotsB);
        SlotTemplate.createExerciseItem(2, 3, ajuste, "Tema1/Parte1/Ejercicio2/3", slotsC);
        SlotTemplate.createExerciseItem(4, 3, ajuste, "Tema1/Parte1/Ejercicio2/4", slotsD);
        SlotTemplate.colocarExerciseItems();
        SlotTemplate.clocarSlots(slotsA, 0);
        SlotTemplate.clocarSlots(slotsB, 1);
        SlotTemplate.clocarSlots(slotsC, 2);
        SlotTemplate.clocarSlots(slotsD, 3);
        SlotTemplate.createItems(images);
    }
}
