using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InterfaceItemSlot : MonoBehaviour, IDropHandler
{
    private string correctItem = null;
    private string finalImage = "";
    private int position;

  public void OnDrop(PointerEventData eventData){
      if (eventData.pointerDrag != null){
          eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
      }
      if (!string.IsNullOrEmpty(this.correctItem)) {
        eventData.pointerDrag.GetComponent<InterfaceDragAndDrop>().setDroppedOnSlot(false);
      } else {
        eventData.pointerDrag.GetComponent<InterfaceDragAndDrop>().setDroppedOnSlot(true);
        this.setCorrectItem(eventData.pointerDrag.GetComponent<InterfaceDragAndDrop>().getName());
        eventData.pointerDrag.GetComponent<InterfaceDragAndDrop>().setInInitialPos(false);
      // cuando se quite el item quitarle el nombre
        eventData.pointerDrag.GetComponent<InterfaceDragAndDrop>().setImage(finalImage); 
        eventData.pointerDrag.GetComponent<InterfaceDragAndDrop>().setSlotSize(gameObject.GetComponent<RectTransform>().sizeDelta);
        eventData.pointerDrag.GetComponent<InterfaceDragAndDrop>().setSlot(gameObject);
      }
  }

  public void setCorrectItem(string name){
      correctItem = name;
  }

  public void removeCorrectItem() {
    correctItem = null;
  }

  public string getCorrectItem(){
      return correctItem;
  }

    public void setFinalImage(string name){
      finalImage = name;
  }

  public void setPosition(int pos){
      position = pos;
  }
  public int getPosition(){
      return position;
  }
}
