using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Ejer114 : MonoBehaviour
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

        Dictionary<int, string> slots1 = new Dictionary<int, string> {};
         Dictionary<int, string> slots2 = new Dictionary<int, string> {
            [23] = "menos",
            [38] = "mas",
        };
         Dictionary<int, string> slots3 = new Dictionary<int, string> {
            [3] = "mas",
        };
        Dictionary<int, string> slots4 = new Dictionary<int, string> {
            [21] = "mas"
        };
        Dictionary<int, string> slots5 = new Dictionary<int, string> {
            [11] = "mas",
            [15] = "menos",
            [25] = "menos",
        };
        Dictionary<int, string> slots6 = new Dictionary<int, string> {
            [1] = "menos",
            [3] = "mas",
        };

        List<string> images = new List<string>{"mas", "menos"};

        SlotTemplate.createExerciseItem(2, 2, ajuste, "Tema1/Parte1/Ejercicio4/slot1", slots1); 
        SlotTemplate.createExerciseItem(4, 3, ajuste, "Tema1/Parte1/Ejercicio4/slot2", slots2); // pass size
        SlotTemplate.createExerciseItem(3, 1, ajuste, "Tema1/Parte1/Ejercicio4/slot3", slots3);
        SlotTemplate.createExerciseItem(4, 4, ajuste, "Tema1/Parte1/Ejercicio4/slot4", slots4);
        SlotTemplate.createExerciseItem(3, 3, ajuste, "Tema1/Parte1/Ejercicio4/slot5", slots5);
        SlotTemplate.createExerciseItem(2, 2, ajuste, "Tema1/Parte1/Ejercicio4/slot6", slots6);
        SlotTemplate.colocarExerciseItems();
        SlotTemplate.clocarSlots(slots1, 0);
        SlotTemplate.clocarSlots(slots2, 1);
        SlotTemplate.clocarSlots(slots3, 2);
        SlotTemplate.clocarSlots(slots4, 3);
        SlotTemplate.clocarSlots(slots5, 4);
        SlotTemplate.clocarSlots(slots6, 5);
        BaseTemplate.createItems(images, 1, 50);
    }
}
