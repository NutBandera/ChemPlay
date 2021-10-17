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
            [40] = "sp3",
            [140] = "sp3",
            [176] = "sp2" 
        };
        Dictionary<int, string> slots2 = new Dictionary<int, string> {
            [34] = "sp3",
            [101] = "sp2",
            [110] = "sp2"
        };
         Dictionary<int, string> slots3 = new Dictionary<int, string> {
            [101] = "sp",
            [165] = "sp3",
            [347] = "sp2"
        };
        Dictionary<int, string> slots4 = new Dictionary<int, string> {
            [61] = "sp3",
            [131] = "sp2",
            [165] = "sp2"
        };
        Dictionary<int, string> slots5 = new Dictionary<int, string> {
            [74] = "sp2",
            [149] = "sp3",
            [207] = "sp3",
            [281] = "sp3",
        };
        Dictionary<int, string> slots6 = new Dictionary<int, string> {
            [77] = "sp3",
            [188] = "sp3",
            [201] = "sp"
        };

        List<string> images = new List<string>{"sp", "sp2", "sp3"};

        SlotTemplate.createExerciseItem(7, 3, ajuste, "Tema1/Parte1/Ejercicio1/base1"); 
        SlotTemplate.createExerciseItem(6, 4, ajuste, "Tema1/Parte1/Ejercicio1/base2"); 
        SlotTemplate.createExerciseItem(7, 5, ajuste, "Tema1/Parte1/Ejercicio1/base3");
        SlotTemplate.createExerciseItem(7, 4, ajuste, "Tema1/Parte1/Ejercicio1/base4");
        SlotTemplate.createExerciseItem(7, 5, ajuste, "Tema1/Parte1/Ejercicio1/base5");
        SlotTemplate.createExerciseItem(7, 4, ajuste, "Tema1/Parte1/Ejercicio1/base6");
        SlotTemplate.colocarExerciseItems(3, 2);
        SlotTemplate.clocarSlotsDimensions(slots1, 0, 23, 10, 11, 5); // pass size directly ?
        SlotTemplate.clocarSlotsDimensions(slots2, 1, 18, 10, 11, 5);
        SlotTemplate.clocarSlotsDimensions(slots3, 2, 26, 15, 11, 5);
        SlotTemplate.clocarSlotsDimensions(slots4, 3, 20, 11, 11, 5);
        SlotTemplate.clocarSlotsDimensions(slots5, 4, 21, 15, 11, 5);
        SlotTemplate.clocarSlotsDimensions(slots6, 5, 22, 11, 11, 5);
        BaseTemplate.createItems(images, 100, 70);
    }
}
