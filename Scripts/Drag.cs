using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    private bool isDragging = false;
    private bool inUse = false;
    private Vector2 pos;

    // Update is called once per frame
    void Update()
    {
        if (isDragging) {
            transform.position = Input.mousePosition;
        }
    }
    public void OnMouseDown() {
        
        isDragging = true;
    }
    public void OnMouseUp() {
        isDragging = false;
        if (inUse){
            Debug.Log("WORKS");
            transform.position = pos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        inUse = true;
        pos = collision.transform.position;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        inUse = false;
    }
}
