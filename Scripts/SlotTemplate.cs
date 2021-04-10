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
    private static int numberRowsItems = 1;

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

    public static void setNumberRowsItems(int rows){
        numberRowsItems = rows;
    }

    public static void colocarExerciseItems(int itemsPerRow, int rows){
      //  int itemsPerRow = exerciseItems.Count/2; // pass as param
        int counter = 1;
        int index = 0;
        int ajuste = 0;
        int n = 300;
        int m = 100;
        var x = Screen.width/(itemsPerRow+1)-100;
        var y = Screen.height/2-m - 100*(numberRowsItems-1);
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

    public static void colocarTitulo(string path, int index, int sizeX, int sizeY) { // move to base template
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
        int index, int xElements, int yElements, int xElementsPixels = -1, int yElementsPixels = -1){
        createSlotsFromDimensions(slots, exerciseItems[index].transform.position.x,
        exerciseItems[index].transform.position.y,
        exerciseItems[index].GetComponent<RectTransform>().rect.width,
        exerciseItems[index].GetComponent<RectTransform>().rect.height, xElements, yElements,
        xElementsPixels, yElementsPixels);
    }

        public static void clocarSlots(Dictionary <int, string> slots, int index){
        // BORRAR 
    }


    public static void createSlotsFromDimensions(Dictionary <int, string> slots, float x, float y, 
    float width, float height, int xElements, int yElements, int xElementsPixels = -1, int yElementsPixels = -1){
        plane = new GameObject(); // move inside loop
        plane.AddComponent<CanvasGroup>();
        plane.AddComponent<ItemSlot>(); 
        plane.GetComponent<ItemSlot>().setCorrectItem("sp"); 
        plane.AddComponent<Image>();
        plane.GetComponent<Image>().sprite = Resources.Load<Sprite>("Slots/cuadrado");
        float sizeX = 0;
        float sizeY = 0;
        sizeX = width/xElements;
        sizeY = height/yElements; 
        var initialPosX = x-width/2 + sizeX/2;
        var initialPosY = y+height/2 - sizeY/2;
        plane.transform.position = new Vector3(initialPosX, 
        initialPosY, 0f); 
        plane.AddComponent<RectTransform>();
        if (xElementsPixels < 0) {
            plane.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeX, sizeY);
        } else {
            plane.GetComponent<RectTransform>().sizeDelta = new Vector2(width/xElementsPixels, height/yElementsPixels);
        }
      //  plane.GetComponent<Image>().color = new Color(1f,1f,1f,0f);
        plane.transform.parent = panel.transform;
        int[] coordinates;
        foreach (var item in slots){
            coordinates = convertPosition(item.Key, xElements, yElements);
            // if correct item contains "double" -> size/2
            GameObject gridPlane = (GameObject)Object.Instantiate(plane);
            GameObject gridPlane2 = (GameObject)Object.Instantiate(gridPlane);

            if (item.Value.Contains("double")){
                // clonar
                if (item.Value.Contains("vertical")){
                    gridPlane.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeX/2, sizeY);
                    gridPlane2.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeX/2, sizeY);

                    gridPlane.transform.position = new Vector3(plane.transform.position.x + sizeX*coordinates[1] - sizeX/3, 
                    plane.transform.position.y - sizeY*coordinates[0], 0f);

                    gridPlane2.transform.position = new Vector3(plane.transform.position.x + sizeX*coordinates[1] + sizeX/3, 
                    plane.transform.position.y - sizeY*coordinates[0], 0f);
                } else {
                    gridPlane.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeX, sizeY/2);
                    gridPlane2.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeX, sizeY/2);

                    gridPlane.transform.position = new Vector3(plane.transform.position.x + sizeX*coordinates[1], 
                    plane.transform.position.y - sizeY*coordinates[0] - sizeY/3, 0f);

                    gridPlane2.transform.position = new Vector3(plane.transform.position.x + sizeX*coordinates[1], 
                    plane.transform.position.y - sizeY*coordinates[0] + sizeY/3, 0f);
                }

                var finalCorrectItem = item.Value.Split('-')[0];

                gridPlane.GetComponent<ItemSlot>().setCorrectItem(finalCorrectItem);
                gridPlane.GetComponent<ItemSlot>().setFinalImage("Items-final/"+finalCorrectItem);

                gridPlane2.GetComponent<ItemSlot>().setCorrectItem(finalCorrectItem);
                gridPlane2.GetComponent<ItemSlot>().setFinalImage("Items-final/"+finalCorrectItem);

                gridPlane2.transform.parent = panel.transform;
            } else { 
                gridPlane.transform.position = new Vector3(plane.transform.position.x + sizeX*coordinates[1], 
                plane.transform.position.y - sizeY*coordinates[0], 0f);
                gridPlane.GetComponent<ItemSlot>().setCorrectItem(item.Value);
                gridPlane.GetComponent<ItemSlot>().setFinalImage("Items-final/"+item.Value);
            }
            gridPlane.transform.parent = panel.transform;
        }
    }

    public static void createHierarchy(int numberOfElements, string[] correctItems, int y){ // > by default
        var pos = Screen.width/(numberOfElements*2-1); 
        var index = 0;

        for (int i=0; i<numberOfElements; i++){
            var slot = new GameObject(); 
            slot.AddComponent<CanvasGroup>();
            slot.AddComponent<ItemSlot>(); 
            slot.GetComponent<ItemSlot>().setCorrectItem(correctItems[i]); 
            slot.AddComponent<Image>();
            slot.GetComponent<Image>().sprite = Resources.Load<Sprite>("Slots/slot");
            slot.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
            slot.transform.position = new Vector3(pos+(index+1)*100, y, 0f); 
            slot.transform.parent = panel.transform;
            index++;
            if (i<numberOfElements-1){
                // put > sign
                var sign = new GameObject(); 
                sign.AddComponent<CanvasGroup>();
                sign.AddComponent<Image>();
                sign.GetComponent<Image>().sprite = Resources.Load<Sprite>("Slots/mayor");
                sign.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
                sign.transform.position = new Vector3(pos+(index+1)*100, y, 0f); 
                sign.transform.parent = panel.transform;
                index++;
            }
        }
    }

  public static void createSlotsFromNumberOfElements(Dictionary <int, string> slots, float x, 
                                 float y, float width, float height, int xElements, int yElements){
    int[] dimensions = calculateNumberSlots(xElements, yElements);
    createSlotsFromDimensions(slots, x, y, width, height, dimensions[0], dimensions[1]);
  }
}
