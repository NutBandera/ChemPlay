using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public static class BaseTemplate
{
    private static GameObject panel;

     public static void setup(GameObject p){
        panel = p;
    }

 public static void colocarEnunciado(string enunciadoPath){
        GameObject enunciado = new GameObject();
        enunciado.AddComponent<RectTransform>();
        enunciado.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height/3);
        enunciado.AddComponent<Image>();
        enunciado.GetComponent<Image>().sprite = Resources.Load<Sprite>(enunciadoPath);
        enunciado.transform.position = new Vector3(Screen.width/2, Screen.height-Screen.height/6, 0f);
        enunciado.transform.parent = panel.transform;
    }

    public static void addSlot(float x, float y, string correctItem) {
        var slot = new GameObject(); 
        slot.AddComponent<CanvasGroup>();
        slot.AddComponent<ItemSlot>(); 
        slot.GetComponent<ItemSlot>().setCorrectItem(correctItem); 
        slot.GetComponent<ItemSlot>().setFinalImage("Items-final/"+correctItem);
        slot.AddComponent<Image>();
        slot.GetComponent<Image>().sprite = Resources.Load<Sprite>("Slots/cuadrado"); // change picture
        slot.transform.position = new Vector3(x, y, 0f); 
        slot.AddComponent<RectTransform>();
        slot.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
        slot.GetComponent<Image>().color = new Color(1f,1f,1f,0f);
        slot.transform.parent = panel.transform;
    }

      public static void createItems(List<string> images, int numeroFilas, int size, int y = 0, bool isInterface = false){ 
        var nItemsPerRow = images.Count/numeroFilas;
        var pos = Screen.width/(nItemsPerRow+1)+50; 
        if (y == 0){
            y = Screen.height-Screen.height/3-100;
        }
        var index = 0;
        for (int i=0; i<images.Count; i++){
            if (i == nItemsPerRow){
                y -= 100;
                index = 0;
            }
            GameObject item = new GameObject();
            item.AddComponent<Image>();
            item.GetComponent<Image>().sprite = Resources.Load<Sprite>("Items/"+images[i]);
            item.transform.position = new Vector3(pos+(index+1)*100, y, 0f);
            item.transform.parent = panel.transform;
            item.GetComponent<RectTransform>().sizeDelta = new Vector2(size, size); 
            item.AddComponent<CanvasGroup>();
            if (isInterface){
                item.AddComponent<InterfaceDragAndDrop>(); 
                item.GetComponent<InterfaceDragAndDrop>().setName(images[i]); 
            } else {
                item.AddComponent<DragAndDrop>(); 
                item.GetComponent<DragAndDrop>().setName(images[i]); 
            }
            
            
            index++;
        }
    }

    public static string Slice(string source, int start, int end) {
        if (end < 0) 
        {
            end = source.Length + end;
        }
        int len = end - start;
        return source.Substring(start, len);
    }
}
