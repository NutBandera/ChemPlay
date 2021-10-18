using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using SFB;

public class StudentResources : MonoBehaviour
{
    [SerializeField] private GameObject selectedImage;
    public void goBack() {
        SceneManager.LoadScene("Scenes/Interface/StudentMenu");
    }
    public void loadImage() {
        var path = StandaloneFileBrowser.OpenFilePanel("Open File", "", "", false); // JSON too

        byte[] bytes = File.ReadAllBytes(path[0]);

        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(bytes);

        selectedImage.AddComponent<Image>();
        selectedImage.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
    }
}
