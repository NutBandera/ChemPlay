using System.Collections.Generic;

[System.Serializable]
public class Exercises
{
    public List<Exercise> exercises;
    
    public List<Exercise> getExercises() {
        return exercises;
    }
}
