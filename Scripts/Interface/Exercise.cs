using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Exercise
{
    public string nombre;
    public string enunciado;
    public List<string> items = new List<string>();
    public Dictionary<int, string> solutions;
    public List<ParteContenido> contenido = new List<ParteContenido>();

    public Exercise(string nombre, string enunciado, List<string> items, Dictionary<int, string> solutions,
    List<ParteContenido> contenido) {
        this.nombre = nombre;
        this.enunciado = enunciado;
        this.items = items;
        this.solutions = solutions;
        this.contenido = contenido;
    }

    public Exercise() {}

    public string getNombre() {
        return nombre;
    }
    public void setNombre(string nombre) {
        this.nombre = nombre;
    }
    public string getEnunciado() {
        return enunciado;
    }
    public List<ParteContenido> getContenido() {
        return contenido;
    }
    public List<string> getItems() {
        return items;
    }
}
