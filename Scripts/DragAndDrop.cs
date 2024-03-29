using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private GameObject clone;
    private Vector3 defaultPos;
    private string name;
    private GameObject slot;
    private string image;
    private Vector2 slotSize;
    private bool inInitialPos;
    private bool droppedOnSlot;
    private Vector2 pos;
    private Vector2 startPos;

    void Start() {
        startPos = transform.position;
        inInitialPos = true;
        droppedOnSlot = false;
    }

     public void OnBeginDrag(PointerEventData eventData){
        if (inInitialPos) {
            clone = Instantiate(gameObject);
            clone.transform.parent = gameObject.transform.parent;
            clone.transform.position = gameObject.transform.position;
            clone.GetComponent<DragAndDrop>().setName(name);
        }
    }
    public void OnPointerDown(PointerEventData eventData){
    }

      public void OnDrag(PointerEventData eventData){
        transform.position = Input.mousePosition;
    }

     public void OnEndDrag(PointerEventData eventData){
        if (droppedOnSlot){
            transform.position = pos;
            inInitialPos = false;
            startPos = transform.position;
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(slot.GetComponent<ItemSlot>().getFinalImage());
            slot.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
            slot.GetComponent<Image>().color = new Color(1f,1f,1f,1f);
            Destroy(gameObject);
        } else {
            transform.position = startPos;
            if (inInitialPos) {
                Destroy(clone);
            }
        }
    }

     private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<Collider2D>().gameObject.GetComponent<ItemSlot>().getCorrectItem()
        .Equals(this.getName())) {
            droppedOnSlot = true;
            pos = collision.transform.position;
            slot = collision.GetComponent<Collider2D>().gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        droppedOnSlot = false;
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

}
