using System;

[Serializable]
public class Item
{   
    public string nombre;
    public byte[] bytes;

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