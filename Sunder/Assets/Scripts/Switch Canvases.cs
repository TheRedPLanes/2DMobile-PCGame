using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryCanvasManager : MonoBehaviour
{
    public GameObject newCanvasGameObject; // Drag your new Canvas GameObject here in the Inspector
    public GameObject mainCanvasGameObject; //this is our current canvas, aka, whatever canvas we put this code on
    public string levelToLoad;
    public void OpenNewCanvas()
    {
        if (newCanvasGameObject != null) //if the new canvas prefab isn't nothing...
        {
            newCanvasGameObject.SetActive(true); //activate a new canvas (whatever you select)
        }
        if (mainCanvasGameObject != null)
        {
            mainCanvasGameObject.SetActive(false); // Hide the main canvas
        }
    }
    public void SwitchScenes()
    {
        SceneManager.LoadScene(levelToLoad);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
