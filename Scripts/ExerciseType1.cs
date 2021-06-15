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
        SlotTemplate.createExerciseItem(5, 5, 100, CurrentExercise.getBase());   
        SlotTemplate.colocarExerciseItems(1, 1);
        // colocar slots
        SlotTemplate.clocarSlotsDimensions(CurrentExercise.getSolutions(),
         0, CurrentExercise.getWidth(), CurrentExercise.getHeight());
         // create exercise items
        BaseTemplate.createItems(CurrentExercise.getItems(), 1, 100); // number of lines !!!! -> create another method that does it alone?

    }
}
