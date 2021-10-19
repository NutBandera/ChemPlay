using System.Collections.Generic;

public static class CurrentExercise
{
    private static int _index = 0;
    private static string _ID;
    private static string _nombre;
    private static byte[] _enunciado;
    private static List<Item> _items = new List<Item>();
    private static List<ParteContenido> contenido = new List<ParteContenido>();
    private static List<Exercise> exercises = new List<Exercise>();
    private static bool _editMode;
    
    public static byte[] getEnunciado() {
        return _enunciado;
    }
    public static void setEnunciado(byte[] enunciado) {
        _enunciado = enunciado;
    }
    public static List<Item> getItems() {
        return _items;
    }
    public static void addItem(Item item) {
        _items.Add(item);
    }

    public static void removeItem(string itemName) {
        Item item = findItemByName(itemName);
        if (item != null) {
            _items.Remove(item);
        }   
    }

    public static void addContenido(ParteContenido part) {
        contenido.Add(part);
    }
    public static int getIndex() {
        return _index;
    }
    public static void updateContenido(ParteContenido part) {
        foreach (ParteContenido parte in contenido) {
            if (parte.getBaseName().Equals(part.getBaseName())) {
                parte.setSolutions(part.getSolutions());
                break;
            }
        }; 
    }

    public static void removePart(string partName) {
        // search by name
        ParteContenido part = findPartByName(partName);
        if (part != null){
            contenido.Remove(part);
        }
    }
    public static ParteContenido findPartByName(string name) {
        foreach (ParteContenido part in contenido) {
            if (name.Equals(part.getBaseName())) return part;
        }
        return null;
    }
    private static Exercise findExerciseByID(int ID) {
        foreach (Exercise exercise in exercises) {
            if (ID.Equals(exercise.getID())) return exercise;
        }
        return null;
    }
    public static Item findItemByName(string itemName) {
        foreach (Item item in _items) {
            if (item.getNombre().Equals(itemName)) {
                return item;
            }
        }
        return null;
    }

    public static List<ParteContenido> getContenido() {
        return contenido;
    }
    public static void addExercise(Exercise exercise){
        exercises.Add(exercise);
        _index++;
    }
    public static void updateExercise(Exercise exercise){
        foreach (Exercise ex in exercises) {
            if (ex.getID().Equals(exercise.getID())) {
                ex.setNombre(exercise.getNombre());
                ex.setEnunciado(exercise.getEnunciado());
                ex.setItems(exercise.getItems());
                ex.setContenido(exercise.getContenido());
                break;
            }
        };
    }
    public static List<Exercise> getExercises() {
        return exercises;
    }
    public static void setExercises(List<Exercise> _exercises) {
        exercises = _exercises;
    }
    public static void setExercise(int ID) {
        // find exercise from list
        Exercise exercise = findExerciseByID(ID);
        if (exercise != null) {
            _nombre = exercise.getNombre();
            _enunciado = exercise.getEnunciado();
            _items = exercise.getItems();
            contenido = exercise.getContenido();
        }
        
    }
    public static void removeExercise(int ID) {
        Exercise exercise = findExerciseByID(ID);
        if (exercise != null){
            exercises.Remove(exercise);
        }
    }
    public static void setNombre(string nombre) {
        _nombre = nombre;
    }
    public static string getNombre() {
        return _nombre;
    }
    public static void setID(string ID) {
        _ID = ID;
    }
    public static string getID() {
        return _ID;
    }
    public static void reset() {
        _nombre = "";
        _enunciado = null;
        _items = new List<Item>();
        contenido = new List<ParteContenido>();
    }
    public static void resetExercises() {
        exercises = new List<Exercise>();
    }
    public static bool getEditMode() {
        return _editMode;
    }
    public static void setEditMode(bool editMode) {
        _editMode = editMode;
    }

}
