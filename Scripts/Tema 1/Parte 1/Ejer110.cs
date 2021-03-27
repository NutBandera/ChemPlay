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
            [4] = "cl",
            [6] = "enlace1",
            [8] = "enlace2",
            [16] = "enlace2",
            [17] = "enlace3",
            [18] = "enlace1",
            [20] = "cl",
            [22] = "cl",
            [24] = "cl"
        };
        Dictionary<int, string> slots2 = new Dictionary<int, string> {
            [0] = "f",
            [2] = "f",
            [4] = "f",
            [6] = "enlace1",
            [7] = "enlace3",
            [8] = "enlace2",
            [16] = "enlace2",
            [17] = "enlace3",
            [18] = "enlace1",
            [20] = "f",
            [22] = "f",
            [24] = "f"
        };

        List<string> images = new List<string>{"enlace", "dos", "menos", "mas", "horizontal", "cl", "f", "8", "10", "12"};

        SlotTemplate.createExerciseItem(4, 4, ajuste, "Tema1/Parte1/Ejercicio10/p", slots1); 
        SlotTemplate.createExerciseItem(4, 4, ajuste, "Tema1/Parte1/Ejercicio10/s", slots2); 
        SlotTemplate.colocarExerciseItems(2, 1);
        SlotTemplate.clocarSlotsDimensions(slots1, 0, 5, 5);
        SlotTemplate.clocarSlotsDimensions(slots2, 1, 5, 5);
        BaseTemplate.createItems(images, 2, 70);
    }

}
