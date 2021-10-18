using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Item
{
    public string nombre;
    public byte[] bytes;

    public Item (string nombre, byte[] bytes) {
        this.nombre = nombre;
        this.bytes = bytes;
    }
    public string getNombre() {
        return nombre;
    }
    public void setNombre(string nombre) {
        this.nombre = nombre;
    }
    public byte[] getBytes() {
        return bytes;
    }
    public void setBytes(byte[] bytes) {
        this.bytes = bytes;
    }

}