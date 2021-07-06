using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using SFB;

public static class SaveManager
{
   public static void SaveToJson()
    {
        if (CurrentExercise.getExercises().Count > 0) {
            Exercises exercises = new Exercises();
            List<Exercise> exercisesList = CurrentExercise.getExercises();
            exercises.exercises = exercisesList;
    
            string json = JsonUtility.ToJson(exercises, true);

            var extensionList = new [] {
                new ExtensionFilter("JSON", "json")
            };
            var path = StandaloneFileBrowser.SaveFilePanel("Save File", "", "exercises", extensionList);

            File.WriteAllText(path, json);
        } else {
            // show message
        }
        
    }
 
    public static List<Exercise> LoadFromJson()
    {
        var path = StandaloneFileBrowser.OpenFilePanel("Open File", "", "json", false);
        string json = File.ReadAllText(path[0]);
        Exercises data = JsonUtility.FromJson<Exercises>(json);
        return data.getExercises();
    }
}
