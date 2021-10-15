using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CurrentExercise
{
    private static string _nombre;
    private static string _enunciado;
    private static List<string> _items = new List<string>();
    private static List<ParteContenido> contenido = new List<ParteContenido>();
    private static List<Exercise> exercises = new List<Exercise>();
    
    public static string getEnunciado() {
        return _enunciado;
    }
    public static void setEnunciado(string enunciado) {
        _enunciado = enunciado;
    }
    public static List<string> getItems() {
        return _items;
    }
    public static void addItem(string item) {
        _items.Add(item);
    }

    public static void removeItem(string item) {
        _items.Remove(item);
    }

    public static void addContenido(ParteContenido parte) {
        contenido.Add(parte);
    }

    public static void removePart(string partName) {
        // search by name
        ParteContenido part = findPartByName(partName);
        if (part != null){
            contenido.Remove(part);
        }
    }
    private static ParteContenido findPartByName(string name) {
        foreach (ParteContenido part in contenido) {
            if (name.Equals(part.getBaseName())) return part;
        }
        return null;
    }
    private static Exercise findExerciseByName(string name) {
        foreach (Exercise exercise in exercises) {
            if (name.Equals(exercise.getNombre())) return exercise;
        }
        return null;
    }

    public static List<ParteContenido> getContenido() {
        return contenido;
    }
    public static void addExercise(Exercise exercise){
        exercises.Add(exercise);
    }
    public static List<Exercise> getExercises() {
        return exercises;
    }
    public static void setExercises(List<Exercise> _exercises) {
        exercises = _exercises;
    }
    public static void removeExercise(string name) {
        Exercise exercise = findExerciseByName(name);
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
    public static void reset() {
        _nombre = "";
        _enunciado = "";
        _items = new List<string>();
        contenido = new List<ParteContenido>();
    }
    public static void resetExercises() {
        exercises = new List<Exercise>();
    }

}
