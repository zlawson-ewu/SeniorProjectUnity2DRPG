using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleUI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI mpText;

    public void populateUI(Character character)
    {
        nameText.text = character.characterName;
        levelText.text = "Lvl: " + character.lvl.getValue();
        hpText.text = "HP: " + character.currentHp + "/" + character.maxhp.getValue();
        mpText.text = "MP: " + character.currentMp + "/" + character.maxmp.getValue();
    }

    public void updateHp(Character character)
    {
        hpText.text = hpText.text = "HP: " + character.currentHp + "/" + character.maxhp.getValue();
    }

    public void updateMp(Character character)
    {
        mpText.text = mpText.text = "MP: " + character.currentMp + "/" + character.maxmp.getValue();
    }

    public void setBattleUIAs(bool enabled)
    {
        nameText.gameObject.SetActive(enabled);
        levelText.gameObject.SetActive(enabled);
        hpText.gameObject.SetActive(enabled);
        mpText.gameObject.SetActive(enabled);
    }
}
