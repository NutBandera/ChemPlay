using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Ejer113 : MonoBehaviour
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
            [13] = "item3",
            [19] = "pi",
            [63] = "item1"
        };
         Dictionary<int, string> slotsB = new Dictionary<int, string> {
            [35] = "alpha",
            [105] = "pi",
        };
         Dictionary<int, string> slotsC = new Dictionary<int, string> {
            [22] = "item2",
            [42] = "item2",
            [77] = "item2"
        };
        Dictionary<int, string> slotsD = new Dictionary<int, string> {
            [22] = "alpha",
            [47] = "pi",
            [64] = "item2"
        };

        List<string> images = new List<string>{"item1", "pi", "item2", "alpha", "item3"};

        SlotTemplate.createExerciseItem(6, 4, ajuste, "Tema1/Parte1/Ejercicio3/base1", slotsA); 
        SlotTemplate.createExerciseItem(7, 5, ajuste, "Tema1/Parte1/Ejercicio3/base2", slotsB);
        SlotTemplate.createExerciseItem(7, 5, ajuste, "Tema1/Parte1/Ejercicio3/base3", slotsC);
        SlotTemplate.createExerciseItem(6, 5, ajuste, "Tema1/Parte1/Ejercicio3/base4", slotsD);
        SlotTemplate.colocarExerciseItems(2, 2);
        SlotTemplate.clocarSlotsDimensions(slotsA, 0, 11, 7, 5, 4);
        SlotTemplate.clocarSlotsDimensions(slotsB, 1, 14, 9, 5, 4);
        SlotTemplate.clocarSlotsDimensions(slotsC, 2, 15, 9, 5, 4);
        SlotTemplate.clocarSlotsDimensions(slotsD, 3, 10, 8, 5, 4);
        BaseTemplate.createItems(images, 1, 90); 
    }
}
