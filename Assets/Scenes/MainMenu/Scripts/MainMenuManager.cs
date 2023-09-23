using System.Collections;
using System.Collections.Generic;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public void NewGame()
    {
        SoundManager.Instance.PlaySFX("ConfirmSFX");
        GameManager.Instance.StartNewGame();
    }
    public void LoadGame()
    {
        if (!SaveManager.Instance.CheckIfGameDataExists())
        {
            SoundManager.Instance.PlaySFX("DeniedSFX");
            Button loadButton = GameObject.Find("Load Game").GetComponent<Button>();
            loadButton.interactable = false;
            loadButton.GetComponentInChildren<TextMeshProUGUI>().SetText("No Data Found");
            loadButton.GetComponentInChildren<TextMeshProUGUI>().color = loadButton.colors.disabledColor;
        }
        else
        {
            SoundManager.Instance.PlaySFX("ConfirmSFX");
            GameManager.Instance.LoadExistingGame();
        }
    }
    public void QuitGame()
    {
        SoundManager.Instance.PlaySFX("ConfirmSFX");
        Application.Quit();
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #endif
    }

}
