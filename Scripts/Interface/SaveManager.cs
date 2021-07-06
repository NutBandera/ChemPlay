using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public static class SaveManager
{
   public static void SaveToJson()
    {
        Exercises exercises = new Exercises();
        List<Exercise> exercisesList = CurrentExercise.getExercises();
        exercises.exercises = exercisesList;
 
        string json = JsonUtility.ToJson(exercises, true);
        File.WriteAllText("Assets/Resources/Ejercicios.json", json); // use SFB
    }
 
    public static void LoadFromJson()
    {
        string json = File.ReadAllText("Assets/Resources/Ejercicios.json");
        Exercises data = JsonUtility.FromJson<Exercises>(json);
    }
}
