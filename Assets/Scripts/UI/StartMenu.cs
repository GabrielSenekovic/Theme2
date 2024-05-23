using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] CanvasGroup activeCanvas;
    [SerializeField] SaveFileSelect saveFileSelect;

    private void Awake()
    {
        activeCanvas.alpha = 1;
        activeCanvas.blocksRaycasts = true;
        activeCanvas.interactable = true;
    }
    private void Start()
    {
        saveFileSelect.SetStartMenu(this);
    }
    public void SwitchCanvasGroup(CanvasGroup newCanvas)
    {
        activeCanvas.alpha = 0;
        activeCanvas.blocksRaycasts = false;
        activeCanvas.interactable = false;
        newCanvas.alpha = 1;
        newCanvas.blocksRaycasts = true;
        newCanvas.interactable = true;
        activeCanvas = newCanvas;
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadSave(SaveFile file)
    {
        SceneLoader.Instance.Load("Overworld");
    }
}
