using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public void change_scene(string scene_name){
        SceneManager.LoadScene(scene_name);
    }

     public void showMessage(string message){
    }

    public void createScene(string name){

    }

}
