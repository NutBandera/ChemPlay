using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Interface
{
    private static string _enunciado;
    
    public static string getEnunciado() {
        return _enunciado;
    }
    public static void setEnunciado(string enunciado) {
        _enunciado = enunciado;
    }
}
