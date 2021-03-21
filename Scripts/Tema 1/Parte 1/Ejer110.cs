using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Ejer110 : MonoBehaviour
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
            [0] = "cl",
            [2] = "cl",
            [3] = "cl",
            [5] = "cl",
            [7] = "cl"
        };
        Dictionary<int, string> slots2 = new Dictionary<int, string> {
            [1] = "f",
            [3] = "f",
            [5] = "f",
            [6] = "f",
            [8] = "f",
            [10] = "f"
        };

        List<string> images = new List<string>{"una", "dos", "menos", "mas", "horizontal", "cl", "f", "8", "10", "12"};

        SlotTemplate.createExerciseItem(4, 4, ajuste, "Tema1/Parte1/Ejercicio9/c-left", slots1); 
        SlotTemplate.createExerciseItem(4, 4, ajuste, "Tema1/Parte1/Ejercicio9/o", slots2); 
        SlotTemplate.colocarExerciseItems();
        SlotTemplate.clocarSlotsDimensions(slots1, 0, 3, 3);
        SlotTemplate.clocarSlotsDimensions(slots2, 1, 3, 4);
        BaseTemplate.createItems(images, 2, 70);
    }

}
