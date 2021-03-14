using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Ejer116 : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private string enunciadoPath;

    void Start()
    {
        panel.GetComponent<Image>().sprite = null;
        panel.GetComponent<Image>().color = new Color(1f,1f,1f,1f);

        BaseTemplate.setup(panel);
        TextTemplate.setup(panel);
        BaseTemplate.colocarEnunciado(enunciadoPath);

        List<string> images = new List<string>{"una", "o", "n", "horizontal", 
        "vertical", "si", "no", "14", "17", "18", "menos", "mas"};
        TextTemplate.createExerciseItem("Assets/Resources/Tema1/Parte1/ejercicio1/ejercicio1.txt");
        BaseTemplate.createItems(images, 2, 80);
    }
}
