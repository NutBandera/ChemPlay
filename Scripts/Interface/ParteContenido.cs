using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParteContenido
{
    private int width;
    private int height;
    private int pixelsX;
    private int pixelsY;
    private string baseName;
    private Dictionary<int, string> solutions;

    public int getWidth() {
        return width;
    }
    public void setWidth(int width) {
        this.width = width;
    }
    public int getHeight() {
        return height;
    }
    public void setHeight(int height) {
        this.height = height;
    }
    public int getPixelsX() {
        return pixelsX;
    }
    public void setPixelsX(int pixelsX) {
        this.pixelsX = pixelsX;
    }
    public int getPixelsY() {
        return pixelsY;
    }
    public void setPixelsY(int pixelsY) {
        this.pixelsY = pixelsY;
    }
    public string getBaseName() {
        return baseName;
    }
    public void setBaseName(string baseName) {
        this.baseName = baseName;
    }
    public Dictionary<int,string> getSolutions() {
        return solutions;
    }
    public void setSolutions(Dictionary<int,string> solutions) {
        this.solutions = solutions;
    }

}
