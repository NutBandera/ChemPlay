using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour
{
    public string correctItem;
    public byte[] finalImage;
    private int errors; 
    private int position;

    public void setCorrectItem(string name){
        correctItem = name;
    }
    public string getCorrectItem(){
        return correctItem;
    }
    public void removeCorrectItem() {
        correctItem = null;
    }
    public void setFinalImage(byte[] bytes){
        finalImage = bytes;
    }
    public byte[] getFinalImage() {
        return finalImage;
    }
    public void setPosition(int pos){
        position = pos;
    }
    public int getPosition(){
        return position;
    }
}
