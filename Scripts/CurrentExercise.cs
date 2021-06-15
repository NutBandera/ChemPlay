using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CurrentExercise
{
    private static string _nombre;
    private static string _enunciado;
    private static List<string> _items = new List<string>();
    private static string _base;
    private static Dictionary<int, string> _solutions;
    private static int _width;
    private static int _height;

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

    public static void addItem(string item) {
        _items.Add(item);
    }

    public static string getBase() {
        return _base;
    }
    public static void setBase(string bases) {
        _base = bases;
    }

    public static void setSolutions(Dictionary<int, string> solutions) {
        _solutions = solutions;
    }

     public static Dictionary<int, string> getSolutions() {
        return _solutions;
    }

    public static int getWidth() {
        return _width;
    }
    public static void setWidth(int width) {
        _width = width;
    }

    public static int getHeight() {
        return _height;
    }
    public static void setHeight(int height) {
        _height = height;
    }

}
