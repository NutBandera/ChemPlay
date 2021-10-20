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
        List<Exercise> exercises = new List<Exercise>();
        var path = StandaloneFileBrowser.OpenFilePanel("Open File", "", "json", false); // JSON too
        if (path.Length > 0) {
            string json = File.ReadAllText(path[0]);
            if (!string.IsNullOrEmpty(json)) {
                Exercises data = JsonConvert.DeserializeObject<Exercises>(json);
                exercises = data.getExercises();
            }
            return exercises;
        }
        return null;
    }
}
