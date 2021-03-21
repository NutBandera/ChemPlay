using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public string correctItem;
    public string finalImage = "";
    private int errors; 

  public void OnDrop(PointerEventData eventData){
      if (eventData.pointerDrag != null){
          eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
      }
      if (eventData.pointerDrag.GetComponent<DragAndDrop>().name == correctItem) {
          eventData.pointerDrag.GetComponent<DragAndDrop>().droppedOnSlot = true;
          eventData.pointerDrag.GetComponent<DragAndDrop>().image = finalImage;
          eventData.pointerDrag.GetComponent<DragAndDrop>().slotSize = gameObject.GetComponent<RectTransform>().sizeDelta;
      } else {
          errors++; // no contar si es posición inicial
      }
  }

  public void setCorrectItem(string name){
      correctItem = name;
  }

    public void setFinalImage(string name){
      finalImage = name;
  }
}
