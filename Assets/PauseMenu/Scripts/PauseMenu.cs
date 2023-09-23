using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;

    [SerializeField] GameObject pauseCanvas;
    [SerializeField] GameObject pauseMenuPannel;
    [SerializeField] GameObject savePannel;
    [SerializeField] GameObject characterPannel;
    [SerializeField] GameObject quitPannel;

    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI expText;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI mpText;
    [SerializeField] TextMeshProUGUI strText;
    [SerializeField] TextMeshProUGUI conText;
    [SerializeField] TextMeshProUGUI dexText;
    [SerializeField] TextMeshProUGUI intText;
    [SerializeField] TextMeshProUGUI atkText;
    [SerializeField] TextMeshProUGUI defText;
    [SerializeField] TextMeshProUGUI accText;
    [SerializeField] TextMeshProUGUI evaText;
    [SerializeField] TextMeshProUGUI crtText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Destroying duplicate PauseMenu");
            Destroy(this);
        }
        else
        {
            Debug.Log("Creating PauseMenu");
            Instance = this;
        }
    }

    void Start()
    {
        pauseCanvas.gameObject.SetActive(false);
    }

    public void setPauseMenuAs(bool active)
    {
        if (active) SoundManager.Instance.PlaySFX("PauseSFX");
        Player_Movement.Instance.setIsInteracting(active);
        pauseCanvas.gameObject.SetActive(active);
    }

    public void onResumeButton()
    {
        SoundManager.Instance.PlaySFX("UnpauseSFX");
        pauseCanvas.gameObject.SetActive(false);
        Player_Movement.Instance.setIsInteracting(false);
    }

    public void onSaveButton()
    {
        SoundManager.Instance.PlaySFX("ConfirmSFX");
        pauseMenuPannel.gameObject.SetActive(false);
        savePannel.gameObject.SetActive(true);
    }

    public void onSaveYes()
    {
        Debug.Log("Saving Game");
        SoundManager.Instance.PlaySFX("ConfirmSFX");
        SaveManager.Instance.SaveGame();
        pauseMenuPannel.gameObject.SetActive(true);
        savePannel.gameObject.SetActive(false);
    }

    public void onSaveNo()
    {
        SoundManager.Instance.PlaySFX("DeclineSFX");
        pauseMenuPannel.gameObject.SetActive(true);
        savePannel.gameObject.SetActive(false);
    }

    public void onCharacterButton()
    {
        SoundManager.Instance.PlaySFX("ConfirmSFX");
        pauseMenuPannel.gameObject.SetActive(false);
        fillInCharacterSheet();
        characterPannel.gameObject.SetActive(true);
    }

    public void onCharacterBack()
    {
        SoundManager.Instance.PlaySFX("DeclineSFX");
        characterPannel.gameObject.SetActive(false);
        pauseMenuPannel.gameObject.SetActive(true);
    }

    public void onQuitButton()
    {
        SoundManager.Instance.PlaySFX("ConfirmSFX");
        pauseMenuPannel.gameObject.SetActive(false);
        quitPannel.gameObject.SetActive(true);
    }

    public void onQuitYes()
    {
        Debug.Log("Quitting Game");
        SoundManager.Instance.PlaySFX("ConfirmSFX");
        GameManager.Instance.ReturnToMainMenu();
    }

    public void onQuitNo()
    {
        SoundManager.Instance.PlaySFX("DeclineSFX");
        pauseMenuPannel.gameObject.SetActive(true);
        quitPannel.gameObject.SetActive(false);
    }

    void fillInCharacterSheet()
    {
        Character playerCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();

        nameText.text = playerCharacter.characterName;
        levelText.text = "Level: " + playerCharacter.lvl.getValue();
        expText.text = "Exp: " + playerCharacter.exp.getValue();
        hpText.text = "HP: " + playerCharacter.currentHp + " / " + playerCharacter.maxhp.getValue();
        mpText.text = "MP: " + playerCharacter.currentMp + " / " + playerCharacter.maxmp.getValue();
        strText.text = "STR: " + playerCharacter.str.getValue();
        dexText.text = "DEX: " + playerCharacter.dex.getValue();
        conText.text = "CON: " + playerCharacter.con.getValue();
        intText.text = "INT: " + playerCharacter.intl.getValue();
        atkText.text = "ATK: " + playerCharacter.atk.getValue();
        defText.text = "DEF: " + playerCharacter.def.getValue();
        accText.text = "ACC: " + playerCharacter.acc.getValue();
        evaText.text = "EVA: " + playerCharacter.eva.getValue();
        crtText.text = "CRT: " + playerCharacter.crit.getValue();
    }

}
