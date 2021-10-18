using System.Collections.Generic;
using System.IO;
using SFB;
using Newtonsoft.Json;

public static class SaveManager
{
   public static void SaveToJson()
    {
        if (CurrentExercise.getExercises().Count > 0) {
            Exercises exercises = new Exercises();
            List<Exercise> exercisesList = CurrentExercise.getExercises();
            exercises.exercises = exercisesList;

            string json = JsonConvert.SerializeObject(exercises);

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
        var path = StandaloneFileBrowser.OpenFilePanel("Open File", "", "json", false); // JSON too

        string json = File.ReadAllText(path[0]);
        Exercises data = JsonConvert.DeserializeObject<Exercises>(json);
        return data.getExercises();
    }
}
