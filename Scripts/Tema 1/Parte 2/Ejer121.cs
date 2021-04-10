using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Ejer121 : MonoBehaviour
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

        string [] correctItems = new string[] {"sp", "sp", "sp"};
        SlotTemplate.createHierarchy(3, correctItems, 200);
    }

}
