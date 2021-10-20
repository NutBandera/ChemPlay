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

    public static void createBase(int xElements, int yElements, byte[] image, int ajuste){
        GameObject photo = new GameObject();
        photo.AddComponent<RectTransform>();
        photo.GetComponent<RectTransform>().sizeDelta = new Vector2(ajuste*xElements, ajuste*yElements); 
        photo.AddComponent<Image>();
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(image);
        photo.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        photo.transform.parent = panel.transform;
        exerciseItems.Add(photo);
    }

    public static void createEmptyeBase(int xElements, int yElements, byte[] image, int ajuste, int x, int y){
        GameObject photo = new GameObject();
        photo.AddComponent<RectTransform>();
        photo.GetComponent<RectTransform>().sizeDelta = new Vector2(ajuste*xElements+200, ajuste*yElements); 
        photo.AddComponent<Image>();
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(image);
        photo.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        photo.transform.parent = panel.transform;
        photo.transform.position = new Vector3(x, y, 0f);
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


    public static void colocarExerciseItems(int itemsPerRow = -1, int rows = 2) { //TODO delete items per row and rows
        /*if (itemsPerRow < 0){
            itemsPerRow = exerciseItems.Count/2;
            if (exerciseItems.Count == 1){
                itemsPerRow = 1;
                rows = 1;
            } else {
                 if (exerciseItems.Count%2 == 0) {
                itemsPerRow = exerciseItems.Count/2;
                } else {
                    itemsPerRow = exerciseItems.Count/2 + 1;
                }
            }
        }
        var numeroFilas = 0;
        if (exerciseItems.Count < 3){
            numeroFilas = 1;
        } else {
            numeroFilas = 2;
        }
        // Si el número de elementos es par
        if (exerciseItems.Count%2 == 0) {
            itemsPerRow= exerciseItems.Count/numeroFilas;
        } else {
            itemsPerRow= exerciseItems.Count/numeroFilas + 1;
        }*/
        if (exerciseItems.Count.Equals(1)) {
            itemsPerRow =  1;
        } else {
            itemsPerRow = 2;
        }
        /*int counter = 1;
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
        }*/
        var x = Screen.width/(itemsPerRow+1)-100;
        var y = Screen.height/2;
        for (int i=0; i<exerciseItems.Count; i++){
            if (i == itemsPerRow){
                y -= y/2;
                x = Screen.width/(itemsPerRow+1)-100;
            }
            exerciseItems[i].transform.position = new Vector3(x, y, 0f);
            x *= 3;
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

    public static void createExerciseItem(int ancho, int alto, int ajuste, byte[] image){
        createBase(ancho, alto, image, ajuste);
        thisAjuste = ajuste;
    }

     public static void createEmptyExerciseItem(byte[] image, int xElements, int yElements, int x, int y, bool hide){
        // pass ancho y alto
        createEmptyeBase(xElements, yElements, image, 100, x, y);
        // put slots
        colocarSlotsCompleto(x, y, xElements, yElements, 700, 500, hide); // pass width and height
        // arreglar esto
        // x,y,xElements,yElements,width,height
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

    public static void colocarSlotsCompleto(int x, int y, int xElements, int yElements, float width, float height, bool hide) {
        GameObject baseSlot = new GameObject(); // move inside loop
        baseSlot.AddComponent<CanvasGroup>();
        baseSlot.AddComponent<ItemSlot>(); 
        baseSlot.AddComponent<Image>();
        baseSlot.GetComponent<Image>().sprite = Resources.Load<Sprite>("Slots/matrix");
        float sizeX = 0;
        float sizeY = 0;
        sizeX = width/xElements;
        sizeY = height/yElements; 
        var initialPosX = x-width/2 + sizeX/2;
        var initialPosY = y+height/2 - sizeY/2;
        baseSlot.transform.position = new Vector3(initialPosX, 
        initialPosY, 0f); 
        baseSlot.AddComponent<RectTransform>();
        baseSlot.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeX, sizeY);
        baseSlot.AddComponent<BoxCollider2D>();
        baseSlot.GetComponent<BoxCollider2D>().size = baseSlot.GetComponent<RectTransform>().sizeDelta;
        if (hide) {
            baseSlot.GetComponent<Image>().color = new Color(1f,1f,1f,0f);
        }
        baseSlot.transform.parent = panel.transform;
        int pos;
        for (int i=0; i<yElements; i++) {
            for (int j=0; j<xElements; j++) {
                // slot for coordinate
                // set position
                GameObject slot = (GameObject)Object.Instantiate(baseSlot);
                //slot.tag = "slot";
                slot.transform.position = new Vector3(baseSlot.transform.position.x + sizeX*j, 
                baseSlot.transform.position.y - sizeY*i, 0f);

                pos = i*xElements+j;
                
                slot.GetComponent<ItemSlot>().setPosition(pos);
                // slot.GetComponent<ItemSlot>().setCorrectItem(); set this when in pos -> create another script
                //slot.GetComponent<ItemSlot>().setFinalImage("Items-final/"+item.Value);
                slot.transform.parent = panel.transform;
            }
        }
        GameObject.Destroy(baseSlot); // another option: move inside loop
    }

        public static void clocarSlots(Dictionary <int, string> slots, int index){
        // BORRAR 
    }

    /*
    * Metodo para colocar los slots con las respuestas correctas
    * @param xElementsPixels
    */
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
        plane.AddComponent<BoxCollider2D>();
        plane.GetComponent<BoxCollider2D>().size = plane.GetComponent<RectTransform>().sizeDelta;
        if (xElementsPixels < 0) {
            plane.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeX, sizeY);
        } else {
            plane.GetComponent<RectTransform>().sizeDelta = new Vector2(width/xElementsPixels, height/yElementsPixels);
        }
        // plane.GetComponent<Image>().color = new Color(1f,1f,1f,0f);
        plane.transform.parent = panel.transform;
        int[] coordinates;
        foreach (var item in slots){
            coordinates = convertPosition(item.Key, xElements, yElements);
            // if correct item contains "double" -> size/2
            GameObject gridPlane = (GameObject)Object.Instantiate(plane);
            GameObject gridPlane2 = (GameObject)Object.Instantiate(gridPlane);


            if (item.Value.Contains("double")){ //TODO eliminar
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
                gridPlane.GetComponent<ItemSlot>().setFinalImage(CurrentExercise.findItemByName(finalCorrectItem).getBytes());

                gridPlane2.GetComponent<ItemSlot>().setCorrectItem(finalCorrectItem);
                gridPlane2.GetComponent<ItemSlot>().setFinalImage(CurrentExercise.findItemByName(finalCorrectItem).getBytes());

                gridPlane2.transform.parent = panel.transform;
            } else { 
                gridPlane.transform.position = new Vector3(plane.transform.position.x + sizeX*coordinates[1],
                plane.transform.position.y - sizeY*coordinates[0], 0f);
                gridPlane.GetComponent<ItemSlot>().setCorrectItem(item.Value);
                Debug.Log(CurrentExercise.findItemByName(item.Value));
                gridPlane.GetComponent<ItemSlot>().setFinalImage(CurrentExercise.findItemByName(item.Value).getBytes());
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

    public static void resetExerciseItems() {
        exerciseItems = new List<GameObject>();
    }

  public static void createSlotsFromNumberOfElements(Dictionary <int, string> slots, float x, 
                                 float y, float width, float height, int xElements, int yElements){
    int[] dimensions = calculateNumberSlots(xElements, yElements);
    createSlotsFromDimensions(slots, x, y, width, height, dimensions[0], dimensions[1]);
  }
}
