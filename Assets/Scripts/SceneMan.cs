using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMan : MonoBehaviour
{    
    public GameObject mapEditorWindow;
    public Slider mapSlider;
    public Text mapWidthLabel;
    public Text mapHeightLabel;
    int startSize = 32;

    public void Campaign()
    {
        SceneManager.LoadScene(1);
    }
    public void QuickStart()
    {
        SceneManager.LoadScene(1);
    }
    public void Load()
    {
        SceneManager.LoadScene(1);
    }
    public void MapEditorWindow()
    {
        mapEditorWindow.SetActive(true);
    }
    public void MapEditorCancel()
    {
        mapEditorWindow.SetActive(false);
    }
    public void MapSlider()
    {
        mapWidthLabel.text = (32 + (mapSlider.value * 8)).ToString();
        mapHeightLabel.text = (32 + (mapSlider.value * 8)).ToString();
        startSize = (int)(32 + (mapSlider.value * 8));
    }

    public void MapEditor()
    {
        mapEditorWindow.SetActive(false);
        PlayerPrefs.SetInt("EditorNewSize", startSize);
        SceneManager.LoadScene(2);
    }
    public void GameExit()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
