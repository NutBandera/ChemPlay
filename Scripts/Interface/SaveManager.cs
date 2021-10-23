using System.Collections.Generic;
using UnityEngine;
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
            string filePath = Path.Combine( Application.temporaryCachePath, "exercises.json" );
            File.WriteAllText(filePath, json);
            NativeFilePicker.ExportFile( filePath, ( success ) => Debug.Log( "File exported: " + success ) );
            
            /*foreach (Exercise ex in CurrentExercise.getExercises()) {
                Debug.Log("Nombre: " + ex.getNombre());
                Debug.Log("Enunciado " + ex.getEnunciado());
                Debug.Log("ID: " + ex.getID());
                foreach (Item item in ex.getItems()) {
                    Debug.Log("Nombre item: " + item.getNombre());
                    Debug.Log("Bytes item: " + item.getBytes());
                }
                foreach (ParteContenido parte in ex.getContenido()) {
                    Debug.Log("Parte name: " + parte.getBaseName());
                    Debug.Log("Parte width: " + parte.getWidth());
                    Debug.Log("Parte height: " + parte.getHeight());
                    Debug.Log("Parte pixelsx: " + parte.getPixelsX());
                    Debug.Log("Parte pixelsy: " + parte.getPixelsY());
                    Debug.Log("Parte image: " + parte.getImage());
                    foreach (KeyValuePair<int, string> entry in parte.getSolutions()) {
                        Debug.Log("Solution: " + entry.Key + ", " + entry.Value);
                    }
                }
            }*/

        } else {
            /*Exercises exercises = new Exercises();
            List<Exercise> exercisesList = new List<Exercise>();
            // (int ID, string nombre, byte[] enunciado, List<Item> items, List<ParteContenido> contenido
            List<Item> items = new List<Item>();
            Item item = new Item();
            item.setNombre("Item1");
            item.setBytes(new byte[]{23});
            items.Add(item);
            List<ParteContenido> contenido = new List<ParteContenido>();
            ParteContenido parte = new ParteContenido();
            parte.setWidth(5);
            parte.setHeight(5);
            parte.setPixelsX(-1);
            parte.setPixelsY(-1);
            parte.setBaseName("Parte");
            parte.setImage(new byte[]{35});
            var solutions = new Dictionary<int, string>()
            {
            { 1, "solution" }};
            parte.setSolutions(solutions);
            contenido.Add(parte);
            Exercise exercise = new Exercise(0, "Ejercicio 1", new byte[]{56}, items, contenido);
            exercisesList.Add(exercise);
            exercises.exercises = exercisesList;

            string json = JsonConvert.SerializeObject(exercises);

            var extensionList = new [] {
                new ExtensionFilter("JSON", "json")
            };
            string filePath = Path.Combine( Application.temporaryCachePath, "exercises.json" );
            File.WriteAllText(filePath, json);
            NativeFilePicker.ExportFile( filePath, ( success ) => Debug.Log( "File exported: " + success ) );*/
        }
        
    }
 
    public static List<Exercise> LoadFromJson()
    {
        string[] fileTypes = new string[] { "json", "JSON" };
        List<Exercise> res = null;
            NativeFilePicker.PickFile( ( path ) =>
			{
				if( path == null )
					Debug.Log( "Operation cancelled" );
				else {
                    string json = File.ReadAllText(path);
                    Exercises data = JsonConvert.DeserializeObject<Exercises>(json);
                    res = data.getExercises();
                    CurrentExercise.setRoute(splitPath(path));
                    Debug.Log("ruta"+CurrentExercise.getRoute());
                }
			}, fileTypes );
        return res;
    }
    private static string splitPath(string path) {
        var aux = path.Split('/');
        var res = "";
        for (int i=0; i<aux.Length-1; i++) {
            string.Concat(res, aux[i]);
        }
        return res;
    }
}