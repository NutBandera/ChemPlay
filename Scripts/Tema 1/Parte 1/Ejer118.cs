using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Ejer118 : MonoBehaviour
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
            [14] = "enlace3",
            [23] = "h",
            [24] = "enlace",
            [26] = "enlace",
            [28] = "dos",
            [36] = "enlace3",
            [47] = "h"
        };
        Dictionary<int, string> slots2 = new Dictionary<int, string> {
            [5] = "h",
            [16] = "enlace3",
            [23] = "h",
            [24] = "enlace",
            [26] = "dos",
            [28] = "enlace",
            [30] = "enlace",
            [31] = "h"
        };
         Dictionary<int, string> slots3 = new Dictionary<int, string> {
            [12] = "h",
            [24] = "enlace1",
            [37] = "dos",
            [39] = "enlace",
            [41] = "enlace",
            [42] = "h",
            [46] = "enlace2",
            [56] = "h"
        };
        Dictionary<int, string> slots4 = new Dictionary<int, string> {
            [23] = "h",
            [24] = "enlace",
            [26] = "enlace",
            [28] = "dos",
            [36] = "enlace3",
            [38] = "enlace3",
            [47] = "h",
            [49] = "h"
        };

        List<string> images = new List<string>{"h", "enlace", "dos", "tres", "vertical", "horizontal"};

        SlotTemplate.createExerciseItem(7, 4, ajuste, "Tema1/Parte1/Ejercicio8/cno", slots1); 
        SlotTemplate.createExerciseItem(7, 4, ajuste, "Tema1/Parte1/Ejercicio8/nco", slots2); 
        SlotTemplate.createExerciseItem(7, 4, ajuste, "Tema1/Parte1/Ejercicio8/cno", slots3);
        SlotTemplate.createExerciseItem(7, 4, ajuste, "Tema1/Parte1/Ejercicio8/nco", slots4);
        SlotTemplate.colocarExerciseItems(2, 2);
        SlotTemplate.clocarSlotsDimensions(slots1, 0, 11, 5);
        SlotTemplate.clocarSlotsDimensions(slots2, 1, 11, 5);
        SlotTemplate.clocarSlotsDimensions(slots3, 2, 11, 7);
        SlotTemplate.clocarSlotsDimensions(slots4, 3, 11, 5);
        BaseTemplate.createItems(images, 1, 50);
    }
}
