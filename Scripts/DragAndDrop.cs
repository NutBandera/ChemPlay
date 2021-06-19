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
    public Vector3 defaultPos;
    public bool droppedOnSlot;
    public string name;
    public string image;
    public Vector2 slotSize;
    public bool inInitialPos;
    public GameObject slot;
    public GameObject slotAnterior;

    private void Awake(){
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = true;
        defaultPos = rectTransform.localPosition;
        inInitialPos = true;
    }

     public void OnBeginDrag(PointerEventData eventData){
        canvasGroup.blocksRaycasts = false;
        droppedOnSlot = false;
        slotAnterior = slot;
        // Create clon if in initial position only
        if (inInitialPos) {
            clone = Instantiate(gameObject);
            clone.transform.parent = gameObject.transform.parent;
            clone.transform.position = gameObject.transform.position;
        } 

    }
    public void OnPointerDown(PointerEventData eventData){
    }

      public void OnDrag(PointerEventData eventData){
        rectTransform.anchoredPosition += eventData.delta;
    }

     public void OnEndDrag(PointerEventData eventData){
        canvasGroup.blocksRaycasts = true;
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
            if (inInitialPos) {
                Destroy(clone);
            }
        }
    }
    public void setName(string n){
        name = n;
    }

    public void removeCorrectItem() {
        slotAnterior.GetComponent<InterfaceItemSlot>().removeCorrectItem();
    }
}
