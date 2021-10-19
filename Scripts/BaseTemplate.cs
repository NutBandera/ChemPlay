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

 public static void colocarEnunciado(byte[] enunciadoBytes){
        GameObject enunciado = new GameObject();
        enunciado.AddComponent<RectTransform>();
        enunciado.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height/3);
        enunciado.AddComponent<Image>();
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(enunciadoBytes);
        enunciado.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        
        enunciado.transform.position = new Vector3(Screen.width/2, Screen.height-Screen.height/6, 0f);
        enunciado.transform.parent = panel.transform;
    }

    public static void addSlot(float x, float y, string correctItem) {
        var slot = new GameObject(); 
        slot.AddComponent<CanvasGroup>();
        slot.AddComponent<ItemSlot>(); 
        slot.GetComponent<ItemSlot>().setCorrectItem(correctItem); 
        slot.GetComponent<ItemSlot>().setFinalImage(CurrentExercise.findItemByName(correctItem).getBytes());
        slot.AddComponent<Image>();
        slot.GetComponent<Image>().sprite = Resources.Load<Sprite>("Slots/cuadrado"); // change picture
        slot.transform.position = new Vector3(x, y, 0f); 
        slot.AddComponent<RectTransform>();
        slot.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
        slot.GetComponent<Image>().color = new Color(1f,1f,1f,0f);
        slot.transform.parent = panel.transform;
    }

      public static void createItems(List<Item> items, int size, int y = 0, bool isInterface = false){ 
          var numeroFilas = 0;
        if (items.Count < 6){
            numeroFilas = 1;
        } else {
            numeroFilas = 2;
        }
        var nItemsPerRow = 0;
        // Si el número de elementos es par
        if (items.Count%2 == 0) {
            nItemsPerRow= items.Count/numeroFilas;
        } else {
            nItemsPerRow= items.Count/numeroFilas + 1;
        }

        var pos = Screen.width/(nItemsPerRow+1); 
        if (y == 0){
            y = Screen.height-Screen.height/3-100;
        }
        var index = 0;
        for (int i=0; i<items.Count; i++){
            if (i == nItemsPerRow){
                y -= 150;
                index = 0;
            }
            GameObject item = new GameObject();
            item.AddComponent<Image>();
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(items[i].getBytes());
            item.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);

            item.transform.position = new Vector3(pos+(index+1)*150, y, 0f);
            item.transform.parent = panel.transform;
            item.GetComponent<RectTransform>().sizeDelta = new Vector2(size, size); 
            item.AddComponent<CanvasGroup>();
            if (isInterface){
                item.AddComponent<SolutionsDragAndDrop>(); 
                item.GetComponent<SolutionsDragAndDrop>().setName(items[i].getNombre()); 
                item.GetComponent<SolutionsDragAndDrop>().setInInitialPos(true); 
            } else {
                item.AddComponent<DragAndDrop>(); 
                item.GetComponent<DragAndDrop>().setName(items[i].getNombre()); 
            }
            item.AddComponent<BoxCollider2D>();
            item.GetComponent<BoxCollider2D>().isTrigger = true;
            item.AddComponent<Rigidbody2D>();
            item.GetComponent<Rigidbody2D>().gravityScale = 0;
            item.GetComponent<Rigidbody2D>().freezeRotation = true;
        
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
