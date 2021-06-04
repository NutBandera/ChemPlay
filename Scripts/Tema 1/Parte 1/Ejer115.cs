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
        BaseTemplate.setup(panel);
        BaseTemplate.colocarEnunciado(enunciadoPath);

        Dictionary<int, string> slots1 = new Dictionary<int, string> {
            [1] = "pos"
        };
        Dictionary<int, string> slots2 = new Dictionary<int, string> {
            [6] = "pos",
            [14] = "neg",
        };
         Dictionary<int, string> slots3 = new Dictionary<int, string> {
            [0] = "pos",
            [2] = "neg"
        };
        Dictionary<int, string> slots4 = new Dictionary<int, string> {
            [2] = "neg",
            [10] = "pos"
        };
        Dictionary<int, string> slots5 = new Dictionary<int, string> {
            [1] = "pos",
            [3] = "neg"
        };
        Dictionary<int, string> slots6 = new Dictionary<int, string> {
            [10] = "pos",
            [12] = "neg",
        };

        List<string> images = new List<string>{"pos", "neg"};

        SlotTemplate.createExerciseItem(4, 4, ajuste, "Tema1/Parte1/Ejercicio5/slot1"); 
        SlotTemplate.createExerciseItem(5, 4, ajuste, "Tema1/Parte1/Ejercicio5/slot2"); 
        SlotTemplate.createExerciseItem(4, 3, ajuste, "Tema1/Parte1/Ejercicio5/slot3");
        SlotTemplate.createExerciseItem(4, 4, ajuste, "Tema1/Parte1/Ejercicio5/slot4");
        SlotTemplate.createExerciseItem(4, 3, ajuste, "Tema1/Parte1/Ejercicio5/slot5");
        SlotTemplate.createExerciseItem(5, 4, ajuste, "Tema1/Parte1/Ejercicio5/slot6");
        SlotTemplate.colocarExerciseItems(3, 2);
        SlotTemplate.clocarSlotsDimensions(slots1, 0, 3, 5);
        SlotTemplate.clocarSlotsDimensions(slots2, 1, 5, 5);
        SlotTemplate.clocarSlotsDimensions(slots3, 2, 3, 2);
        SlotTemplate.clocarSlotsDimensions(slots4, 3, 3, 4);
        SlotTemplate.clocarSlotsDimensions(slots5, 4, 4, 2);
        SlotTemplate.clocarSlotsDimensions(slots6, 5, 9, 5);
        BaseTemplate.createItems(images, 1, 70);
    }
}
