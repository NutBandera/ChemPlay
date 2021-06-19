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
      var auxName = correctItem;
      if (eventData.pointerDrag != null){
          eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
      }
      if (correctItem.Contains("-")){
          auxName = correctItem.Split('-')[0].ToString();
      }
      if (eventData.pointerDrag.GetComponent<DragAndDrop>().getName() == auxName) {
          eventData.pointerDrag.GetComponent<DragAndDrop>().setDroppedOnSlot(true);
          eventData.pointerDrag.GetComponent<DragAndDrop>().setImage(finalImage); 
          eventData.pointerDrag.GetComponent<DragAndDrop>().setSlotSize(gameObject.GetComponent<RectTransform>().sizeDelta);
          eventData.pointerDrag.GetComponent<DragAndDrop>().setInInitialPos(false);
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
