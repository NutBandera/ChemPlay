using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exercise
{
    private string nombre;
    private string enunciado;
    private List<string> items = new List<string>();
    private Dictionary<int, string> solutions;
    private List<ParteContenido> contenido = new List<ParteContenido>();

    public Exercise(string nombre, string enunciado, List<string> items, Dictionary<int, string> solutions,
    List<ParteContenido> contenido) {
        this.nombre = nombre;
        this.enunciado = enunciado;
        this.items = items;
        this.solutions = solutions;
        this.contenido = contenido;
    }

    public string getNombre() {
        return nombre;
    }
}
