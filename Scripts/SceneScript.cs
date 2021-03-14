using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SceneScript : MonoBehaviour
{
     [SerializeField] private GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        // Get images from "Items" folder and create an object for each one
        Sprite[] items = Resources.LoadAll<Sprite>("Items"); 
        var rect = panel.GetComponent<RectTransform>();
        var pos = Screen.width/(items.Length+1);
        var itemPos = pos;

        // Ejercicios de texto (uno por fila)
        // Opción 1: pasar fotos
        // Opción 2: pasar archivos de texto / un archivo de texto con diferentes filas
        // Leer fila de archivo, en cada fila indicar (hasSlot), size y respuesta(?)
        /* Ejemplo:
            1. ¿Electrones de valencia?.1.15
            2. Dibuja el esqueleto.3.ID(?)
            3. Determina la carga formal de cada uno.
        */

        string[] lines = File.ReadAllLines("Assets/Resources/Ejercicio1/ejercicio1.txt");  
        var space = Screen.height/(lines.Length+1);
        var increment = 0;
        foreach (string line in lines)  {
            // Specify size of answer slot and adjust size
            var splitted = line.Split('/');
            GameObject textObject = new GameObject();
            textObject.transform.parent = panel.transform;
            textObject.transform.position = new Vector3(itemPos*3-pos, Screen.height/3-increment, -167.0398f); 
            Text text = textObject.AddComponent<Text>();
            text.text = splitted[0];
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.fontSize = 60;
            text.color = Color.black;
            text.GetComponent<RectTransform>().sizeDelta = new Vector3(1000, 100);
            
            GameObject slot = new GameObject();
            slot.AddComponent<Image>();
            slot.GetComponent<Image>().sprite = Resources.Load<Sprite>("Slots/cuadrado");
            slot.transform.position = new Vector3(Screen.width-100, Screen.height/3-increment, -167.0398f); 
            slot.transform.parent = panel.transform;
            slot.AddComponent<CanvasGroup>();
            slot.AddComponent<ItemSlot>(); 
            slot.GetComponent<ItemSlot>().setCorrectItem(splitted[1]); 
            increment+=100;
        }
        

        for (int i=0; i<items.Length; i++){
            GameObject item = new GameObject();
            itemPos = pos*(i+1);
            item.AddComponent<Image>();
            item.GetComponent<Image>().sprite = items[i];
            item.transform.position = new Vector3(itemPos, Screen.height/2, -167.0398f); // change, poner fondo ...
            item.transform.parent = panel.transform;
            item.AddComponent<CanvasGroup>();
            item.AddComponent<DragAndDrop>(); 
            item.GetComponent<DragAndDrop>().setName(items[i].name); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
