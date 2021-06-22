using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using UnityEngine.UI;

public class InterfaceDragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private GameObject clone;
    private Vector3 defaultPos;
    private string name;
    private string image;
    private Vector2 slotSize;
    private bool inInitialPos;
    private GameObject slot;
    private GameObject slotAnterior;
    private bool droppedOnSlot;
    private Vector2 pos;
    private Vector2 startPos;
    private bool isInPapelera;

    /*private void Awake(){
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = true;
        defaultPos = rectTransform.localPosition;
        inInitialPos = true;
    }*/

    void Start() {
        startPos = transform.position;
        inInitialPos = true;
        droppedOnSlot = false;
        isInPapelera = false;
    }

     public void OnBeginDrag(PointerEventData eventData){
        /*canvasGroup.blocksRaycasts = false;
        droppedOnSlot = false;
        slotAnterior = slot;*/
        slotAnterior = slot;
        // Create clon if in initial position only
        if (inInitialPos) {
            clone = Instantiate(gameObject);
            clone.transform.parent = gameObject.transform.parent;
            clone.transform.position = gameObject.transform.position;
            clone.GetComponent<InterfaceDragAndDrop>().setName(name);
        } else {
            slot.GetComponent<ItemSlot>().removeCorrectItem();
        }
    }
    public void OnPointerDown(PointerEventData eventData){
    }

      public void OnDrag(PointerEventData eventData){
        // rectTransform.anchoredPosition += eventData.delta;
        transform.position = Input.mousePosition;
    }

     public void OnEndDrag(PointerEventData eventData){
        if (droppedOnSlot){
            transform.position = pos;
            inInitialPos = false;
            startPos = transform.position;
            slot.GetComponent<ItemSlot>().setCorrectItem(name);
        } else if (isInPapelera){
            Destroy(gameObject);
        } else {
            transform.position = startPos;
            if (inInitialPos) {
                Destroy(clone);
            }
        }
        /*canvasGroup.blocksRaycasts = true;
        if (droppedOnSlot){ // if droppedInCorrect slot
            defaultPos = this.rectTransform.localPosition; 
            // si se ha cambiado de slot
            if (slotAnterior != null && slot != slotAnterior){
                // eliminar correct item del slot anterior
                this.removeCorrectItem();
            }
            if (!string.IsNullOrEmpty(image)){
                // change image 
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(image);
                gameObject.GetComponent<Image>().rectTransform.sizeDelta = slotSize;
            }
        } else {
            this.rectTransform.localPosition = defaultPos;   
            // Delete clon if coming from initial position
            
        }*/
    }

     private void OnTriggerEnter2D(Collider2D collision) {
         if (collision.CompareTag("Papelera")){
            isInPapelera = true;
        } else {
            droppedOnSlot = true;
            pos = collision.transform.position;
            slot = collision.GetComponent<Collider2D>().gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        droppedOnSlot = false;
        isInPapelera = false;
    }

    public void removeCorrectItem() {
        slotAnterior.GetComponent<ItemSlot>().removeCorrectItem();
    }
    public string getName(){
        return name;
    }
    public void setName(string name){
        this.name = name;
    }
    public bool getDroppedOnSlot(){
        return droppedOnSlot;
    }
    public void setDroppedOnSlot(bool droppedOnSlot){
        this.droppedOnSlot = droppedOnSlot;
    }
    public string getImage(){
        return image;
    }
    public void setImage(string image){
        this.image = image;
    }
    public Vector2 getSlotSize(){
        return slotSize;
    }
    public void setSlotSize(Vector2 slotSize){
        this.slotSize = slotSize;
    }
    public bool getInInitialPos(){
        return inInitialPos;
    }
    public void setInInitialPos(bool inInitialPos){
        this.inInitialPos = inInitialPos;
    } 
    public GameObject getSlot() {
        return slot;
    }
    public void setSlot(GameObject slot) {
        this.slot = slot;
    }
}
