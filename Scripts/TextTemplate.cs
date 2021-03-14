using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public static class TextTemplate
{
    private static GameObject panel;

    public static void setup(GameObject p){
        panel = p;
    }

    public static void createExerciseItem(string exercisePath) {
        var rect = panel.GetComponent<RectTransform>();
        var pos = Screen.width/2;

        string[] lines = File.ReadAllLines(exercisePath);  
        var space = Screen.height/(lines.Length+1);
        var increment = 0;
        int width = 0;
        int height = 0;
        foreach (string line in lines)  {
            switch (line.Substring(0,1)){
                case "/":
                    var comment = line.Split(',');
                    width = int.Parse(comment[0].Substring(comment[0].Length-1, 1));
                    height = int.Parse(comment[1]);
                    break;
                case ":":
                    var question = line.Split(':')[3].Split('{')[0];
                    GameObject textObject = new GameObject();
                    textObject.transform.parent = panel.transform;
                    textObject.transform.position = new Vector3(pos, Screen.height/3+100-increment, 0f); 
                    Text text = textObject.AddComponent<Text>();
                    text.text = question;
                    text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                    text.fontSize = 60;
                    text.color = Color.black;
                    text.GetComponent<RectTransform>().sizeDelta = new Vector3(1000, 100);
                    
                    var slot = new GameObject();
                    slot.AddComponent<Image>();

                    switch (width){ 

                        case 3:
                            slot.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 100); 
                            slot.GetComponent<Image>().sprite = Resources.Load<Sprite>("Slots/bigSlot");
                            break;
                        case 1: 
                            slot.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100); 
                            slot.GetComponent<Image>().sprite = Resources.Load<Sprite>("Slots/smallSlot");
                            break;
                        default:
                            break;
                    }

                    var corner = textObject.transform.position.x + slot.GetComponent<RectTransform>().rect.width;
                    slot.transform.position = new Vector3(corner, Screen.height/3+100-increment, 0f); 
                    slot.transform.parent = panel.transform;

                    Dictionary<int, string> slots = new Dictionary<int, string>(); 
                    var answers = line.Split(':')[3].Split('{')[1];
                  
                    var key = -1;
                    var value = "";

                    int startIndexKey = -1;
                    int endIndexKey = -1;
                    int startIndexValue = -1;
                    int endIndexValue = -1;

                    int index = 0;

                    foreach (char c in answers){
                        if (c == '='){
                           startIndexKey = index+1;
                        } 
                        else if (c == '-'){
                            endIndexKey = index-1;
                        } else if (c == '>'){
                            startIndexValue = index+2;
                        } else if (c == '.') {
                            endIndexValue = index;
                        }
                        
                        if (startIndexKey >= 0 && endIndexKey >= 0){
                            key = int.Parse(BaseTemplate.Slice(answers, startIndexKey, endIndexKey));
                            startIndexKey = -1;
                            endIndexKey = -1;
                        }

                        if (startIndexValue >= 0 && endIndexValue >= 0){
                            value = BaseTemplate.Slice(answers, startIndexValue, endIndexValue);
                            startIndexValue = -1;
                            endIndexValue = -1;
                        }

                        if (key >= 0 && !String.IsNullOrEmpty(value)){
                            slots.Add(key, value);
                            key = -1;
                            value = "";
                        }
                        index++;
                    }

                    SlotTemplate.createSlotsFromDimensions(slots, slot.transform.position.x, slot.transform.position.y, 
                    slot.GetComponent<RectTransform>().rect.width, slot.GetComponent<RectTransform>().rect.height,
                    width, height);

                    increment+=100;
                    break;
            }
        }
    }

    private static void createQuestion() {

    }

    private static void createAnswerSlot(){
         
    }
    
}

