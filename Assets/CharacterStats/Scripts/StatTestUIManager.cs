using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatTestUIManager : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI defText;

    Character playerStats;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = player.GetComponent<Character>();
        nameText.text = playerStats.characterName;
        updateUi();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void updateUi()
    {
        hpText.text = "HP:" + playerStats.currentHp + "/" + playerStats.maxhp.getValue();
        defText.text = "DEF: +" + playerStats.def.getValue();
    }
}
