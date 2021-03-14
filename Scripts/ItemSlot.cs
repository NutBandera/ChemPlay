using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public string correctItem;
    private int errors; 

  public void OnDrop(PointerEventData eventData){
      if (eventData.pointerDrag != null){
          eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
      }
      if (eventData.pointerDrag.GetComponent<DragAndDrop>().name == correctItem) {
          eventData.pointerDrag.GetComponent<DragAndDrop>().droppedOnSlot = true;
      } else {
          errors++; // no contar si es posición inicial
      }
  }

  public void setCorrectItem(string name){
      correctItem = name;
  }
}
