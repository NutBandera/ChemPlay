using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CurrentExercise
{
    private static string _nombre;
    private static string _enunciado;
    private static List<string> _items;
    private static string _base;
    
    public static string getEnunciado() {
        return _enunciado;
    }
    public static void setEnunciado(string enunciado) {
        _enunciado = enunciado;
    }
    public static List<string> getItems() {
        return _items;
    }

    public static List<string> getMockItems() {
        List<string> items = new List<string>{"sp", "sp2", "sp3"};
        return items;
    }

    public static void setItems(List<string> items) {
        _items = items;
    }

    public static string getBase() {
        return _base;
    }
    public static void setBase(string bases) {
        _base = bases;
    }
}
