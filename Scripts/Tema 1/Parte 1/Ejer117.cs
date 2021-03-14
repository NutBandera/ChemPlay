using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Ejer117 : MonoBehaviour
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

          List<string> images = new List<string>{"una", "o", "n", "c",
           "horizontal", "vertical", "si", "no", "15", "18"};
        BaseTemplate.createItems(images, 2, 80);
    }
}
