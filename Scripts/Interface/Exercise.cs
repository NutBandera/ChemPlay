using System.Collections.Generic;

[System.Serializable]
public class Exercise
{
    public int ID;
    public string nombre;
    public string enunciado;
    public List<Item> items = new List<Item>();
    public List<ParteContenido> contenido = new List<ParteContenido>();

    public Exercise(int ID, string nombre, string enunciado, List<Item> items, List<ParteContenido> contenido) {
        this.nombre = nombre;
        this.enunciado = enunciado;
        this.items = items;
        this.contenido = contenido;
    }

    public Exercise() {}

    public string getNombre() {
        return nombre;
    }
    public void setNombre(string nombre) {
        this.nombre = nombre;
    }
    public int getID() {
        return ID;
    }
    public void setID(int ID) {
        this.ID = ID;
    }
    public string getEnunciado() {
        return enunciado;
    }
    public void setEnunciado(string enunciado) {
        this.enunciado = enunciado;
    }
    public List<ParteContenido> getContenido() {
        return contenido;
    }
    public void setContenido(List<ParteContenido> contenido) {
        this.contenido = contenido;
    }
    public List<Item> getItems() {
        return items;
    }
    public void setItems(List<Item> items) {
        this.items = items;
    }
}
