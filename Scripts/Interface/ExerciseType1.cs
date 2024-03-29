using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ExerciseType1 : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    void Start()
    {
        panel.GetComponent<Image>().sprite = null;
        panel.GetComponent<Image>().color = new Color(1f,1f,1f,1f);

        SlotTemplate.setup(panel);
        BaseTemplate.setup(panel);
        BaseTemplate.colocarEnunciado(CurrentExercise.getEnunciado());
        // colocar bases SIZE
        foreach (ParteContenido part in CurrentExercise.getContenido()) {
            SlotTemplate.createExerciseItem(500, 300, 1, part.getImage());   
        }
        SlotTemplate.colocarExerciseItems();
        // colocar slots
        int index = 0;
        foreach (ParteContenido part in CurrentExercise.getContenido()) {
            SlotTemplate.clocarSlotsDimensions(part.getSolutions(),
            index, part.getWidth(), part.getHeight(), part.getPixelsX(), part.getPixelsY());
            index++;
        }

        BaseTemplate.createItems(CurrentExercise.getItems(), 100);

    }
}
