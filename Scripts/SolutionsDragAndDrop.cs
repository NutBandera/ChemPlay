using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using UnityEngine.UI;

public class SolutionsDragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private GameObject clone;
    private Vector3 defaultPos;
    private string name;
    private GameObject slot;
    private GameObject slotAnterior;
    private string image;
    private Vector2 slotSize;
    private bool droppedOnSlot;
    private Vector2 pos;
    private Vector2 startPos;
    private bool isInPapelera;

    void Start() {
        startPos = transform.position;
        droppedOnSlot = false;
        isInPapelera = false;
    }

     public void OnBeginDrag(PointerEventData eventData){
        slotAnterior = slot;
        if (!isInMatrix()) {
            clone = Instantiate(gameObject, gameObject.transform.parent, true);
            clone.transform.parent = gameObject.transform.parent;
            clone.transform.position = gameObject.transform.position;
            clone.GetComponent<SolutionsDragAndDrop>().setName(name);
        } else {
            slot.GetComponent<ItemSlot>().removeCorrectItem();
        }
    }
    public void OnPointerDown(PointerEventData eventData){
    }

      public void OnDrag(PointerEventData eventData){
        transform.position = Input.mousePosition;
    }
    public bool isInMatrix() {
        return gameObject.transform.position.y >= 800 && gameObject.transform.position.y <= 1200;
    }

     public void OnEndDrag(PointerEventData eventData){
        if (droppedOnSlot){
            transform.position = pos;
            startPos = transform.position;
            slot.GetComponent<ItemSlot>().setCorrectItem(name);
        } else if (isInPapelera){
            Destroy(gameObject);
        } else {
            transform.position = startPos;
            if (!isInMatrix()) {
                Destroy(clone);
            }
        }
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


}
