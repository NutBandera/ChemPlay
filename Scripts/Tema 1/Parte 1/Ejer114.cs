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
            [9] = "menos",
            [19] = "mas",
        };
         Dictionary<int, string> slots3 = new Dictionary<int, string> {
            [3] = "mas",
        };
        Dictionary<int, string> slots4 = new Dictionary<int, string> {
            [35] = "mas"
        };
        Dictionary<int, string> slots5 = new Dictionary<int, string> {
            [16] = "mas",
            [19] = "menos",
            [29] = "menos",
        };
        Dictionary<int, string> slots6 = new Dictionary<int, string> {
            [0] = "menos",
            [2] = "mas",
        };

        List<string> images = new List<string>{"mas", "menos"};

        SlotTemplate.createExerciseItem(3, 3, ajuste, "Tema1/Parte1/Ejercicio4/slot1", slots1); 
        SlotTemplate.createExerciseItem(5, 4, ajuste, "Tema1/Parte1/Ejercicio4/slot2", slots2); 
        SlotTemplate.createExerciseItem(4, 2, ajuste, "Tema1/Parte1/Ejercicio4/slot3", slots3);
        SlotTemplate.createExerciseItem(6, 5, ajuste, "Tema1/Parte1/Ejercicio4/slot4", slots4);
        SlotTemplate.createExerciseItem(4, 4, ajuste, "Tema1/Parte1/Ejercicio4/slot5", slots5);
        SlotTemplate.createExerciseItem(3, 2, ajuste, "Tema1/Parte1/Ejercicio4/slot6", slots6);
        SlotTemplate.colocarExerciseItems(3, 2);
        SlotTemplate.clocarSlotsDimensions(slots1, 0, 0, 0);
        SlotTemplate.clocarSlotsDimensions(slots2, 1, 6, 5);
        SlotTemplate.clocarSlotsDimensions(slots3, 2, 6, 2);
        SlotTemplate.clocarSlotsDimensions(slots4, 3, 8, 5);
        SlotTemplate.clocarSlotsDimensions(slots5, 4, 6, 5);
        SlotTemplate.clocarSlotsDimensions(slots6, 5, 3, 2);
        BaseTemplate.createItems(images, 1, 70);
    }
}
