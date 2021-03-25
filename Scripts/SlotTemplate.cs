using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public static class SlotTemplate 
{
    private static GameObject panel;
    private static List<GameObject> exerciseItems;
    private static int width;
    private static int height;
    private static GameObject plane;
    private static int thisAjuste;

    public static void setup(GameObject p){
        panel = p;
        exerciseItems = new List<GameObject>();
    }

     public static void colocarEnunciado(string enunciadoPath, GameObject p ){
        // BORRAR
    }

    public static void createItems(List<string> images){
       // BORRAR 
    }

    public static void createBase(int xElements, int yElements, string photoName, int ajuste){
        GameObject photo = new GameObject();
        photo.AddComponent<RectTransform>();
        photo.GetComponent<RectTransform>().sizeDelta = new Vector2(ajuste*xElements, ajuste*yElements); 
        photo.AddComponent<Image>();
        photo.GetComponent<Image>().sprite = Resources.Load<Sprite>(photoName);
        photo.transform.parent = panel.transform;
        exerciseItems.Add(photo);
    }

      public static void createBase2(int width, int height, string photoName){
        GameObject photo = new GameObject();
        photo.AddComponent<RectTransform>();
        photo.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height); 
        photo.AddComponent<Image>();
        photo.GetComponent<Image>().sprite = Resources.Load<Sprite>(photoName);
        photo.transform.parent = panel.transform;
    }

    public static void colocarExerciseItems(int itemsPerRow, int rows){
      //  int itemsPerRow = exerciseItems.Count/2; // pass as param
        int counter = 1;
        int index = 0;
        int ajuste = 0;
        int n = 300;
        int m = 100;
        var x = Screen.width/(itemsPerRow+1)-100;
        var y = Screen.height/2-m;
        if (itemsPerRow == 3){
            n = 200;
            m = 50;
        } 
        for (int i=1; i<=itemsPerRow; i++) {
            exerciseItems[index].transform.position = new Vector3(x*i+n*ajuste, y, 0f);
            index++;
            ajuste++;
            if (counter<rows && i==itemsPerRow){
                i=0;
                counter++;
                y = y/2;
                ajuste = 0;
            }
        }
    }

    public static void colocarTitulo(string path, int index, int sizeX, int sizeY) {
        var title = new GameObject(); 
        title.AddComponent<CanvasGroup>();
        title.AddComponent<Image>();
        title.GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
        title.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeX, sizeY);
        title.transform.parent = panel.transform;
        title.transform.position = new Vector3(exerciseItems[index].transform.position.x, 
        exerciseItems[index].transform.position.y +
        exerciseItems[index].GetComponent<RectTransform>().rect.height/2 + 60, 0f); 
    }

    public static void calculateNumberOfSlots(int xElements, int yElements){
        width = 1 + 2 * xElements;
        height = 1 + 2 * yElements;
    }

     public static int[] calculateNumberSlots(int xElements, int yElements){
        width = 1 + 2 * xElements;
        height = 1 + 2 * yElements;
        int[] res = {width, height};
        return res;
    }

    public static int[] convertPosition(int position, float width, float height){
        int x = (int)(position/width);
        int y = (int)(position-width*x);
        int[] res = {x, y};
        return res;
    }

    public static void createExerciseItem(int ancho, int alto, int ajuste, string photoName, Dictionary <int, string> slots){
        createBase(ancho, alto, photoName, ajuste);
        thisAjuste = ajuste;
    }

    public static void clocarSlotsNumberOfElements(Dictionary <int, string> slots,
        int index, int xElements, int yElements){
        createSlotsFromNumberOfElements(slots, exerciseItems[index].transform.position.x,
        exerciseItems[index].transform.position.y,
        exerciseItems[index].GetComponent<RectTransform>().rect.width,
        exerciseItems[index].GetComponent<RectTransform>().rect.height, xElements, yElements);
    }

        public static void clocarSlotsDimensions(Dictionary <int, string> slots,
        int index, int xElements, int yElements){
        createSlotsFromDimensions(slots, exerciseItems[index].transform.position.x,
        exerciseItems[index].transform.position.y,
        exerciseItems[index].GetComponent<RectTransform>().rect.width,
        exerciseItems[index].GetComponent<RectTransform>().rect.height, xElements, yElements);
    }

        public static void clocarSlots(Dictionary <int, string> slots, int index){
        // BORRAR 
    }


    public static void createSlotsFromDimensions(Dictionary <int, string> slots, float x, float y, 
    float width, float height, int xElements, int yElements){
        plane = new GameObject(); // move inside loop
        plane.AddComponent<CanvasGroup>();
        plane.AddComponent<ItemSlot>(); 
        plane.GetComponent<ItemSlot>().setCorrectItem("sp"); 
        plane.AddComponent<Image>();
        plane.GetComponent<Image>().sprite = Resources.Load<Sprite>("Slots/cuadrado");
        float sizeX = width/xElements;
        float sizeY = height/yElements; 
        var initialPosX = x-width/2 + sizeX/2;
        var initialPosY = y+height/2 - sizeY/2;
        plane.transform.position = new Vector3(initialPosX, 
        initialPosY, 0f); 
        plane.AddComponent<RectTransform>();
        plane.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeX, sizeY);
       // plane.GetComponent<Image>().color = new Color(1f,1f,1f,0f);
        plane.transform.parent = panel.transform;
        int[] coordinates;
        foreach (var item in slots){
            coordinates = convertPosition(item.Key, xElements, yElements);
            GameObject gridPlane = (GameObject)Object.Instantiate(plane);
            gridPlane.transform.position = new Vector3(plane.transform.position.x + sizeX*coordinates[1], 
            plane.transform.position.y - sizeY*coordinates[0], 0f);
            gridPlane.GetComponent<ItemSlot>().setCorrectItem(item.Value);
            gridPlane.GetComponent<ItemSlot>().setFinalImage("Items-final/"+item.Value);
            gridPlane.transform.parent = panel.transform;
        }
    }

  public static void createSlotsFromNumberOfElements(Dictionary <int, string> slots, float x, 
                                 float y, float width, float height, int xElements, int yElements){
    int[] dimensions = calculateNumberSlots(xElements, yElements);
    createSlotsFromDimensions(slots, x, y, width, height, dimensions[0], dimensions[1]);
  }
}
