using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ItemSlotPapelera : MonoBehaviour, IDropHandler
{
     public void OnDrop(PointerEventData eventData){
      if (eventData.pointerDrag != null){
          eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
      }
      eventData.pointerDrag.GetComponent<InterfaceDragAndDrop>().removeCorrectItem();
      Destroy(eventData.pointerDrag.gameObject);
  }
}
