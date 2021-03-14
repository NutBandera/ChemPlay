using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneTemplate;

public class SwitchScene : MonoBehaviour
{
    [SerializeField] private SceneTemplateAsset sceneTemplate;

    public void change_scene(string scene_name){
        SceneManager.LoadScene(scene_name);
    }

     public void showMessage(string message){
        Debug.Log(message);
        InstantiationResult createdScene = SceneTemplateService.Instantiate(sceneTemplate, false); // which name takes?
        // contains scene (scene.name) and sceneAsset
        SceneManager.LoadScene(createdScene.scene.name);
    }

    public void createScene(string name){

    }

}
