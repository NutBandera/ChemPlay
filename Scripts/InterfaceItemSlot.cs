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
    eventData.pointerDrag.GetComponent<DragAndDrop>().droppedOnSlot = true;
    this.setCorrectItem(eventData.pointerDrag.GetComponent<DragAndDrop>().name);
    // cuando s quite el item quitarle el nombre
    eventData.pointerDrag.GetComponent<DragAndDrop>().image = finalImage; 
    eventData.pointerDrag.GetComponent<DragAndDrop>().slotSize = gameObject.GetComponent<RectTransform>().sizeDelta;
  }

  public void setCorrectItem(string name){
      correctItem = name;
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
