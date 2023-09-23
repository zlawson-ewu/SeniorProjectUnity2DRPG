using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class InventoryMenu : MonoBehaviour
{
    Player_Movement playerMovement;
    Character playerCharacter;

    //Game objects as backgrounds
    [SerializeField] GameObject inventoryCanvas;
    [SerializeField] GameObject inventoryBackground;
    [SerializeField] GameObject InventoryPanel;

    //Text boxes
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI itemA;
    [SerializeField] TextMeshProUGUI itemB;
    [SerializeField] TextMeshProUGUI itemC;
    [SerializeField] TextMeshProUGUI itemD;
    [SerializeField] TextMeshProUGUI itemE;
    [SerializeField] TextMeshProUGUI itemF;
    [SerializeField] TextMeshProUGUI itemG;
    [SerializeField] TextMeshProUGUI itemH;
    [SerializeField] TextMeshProUGUI itemI;
    [SerializeField] TextMeshProUGUI itemJ;
    [SerializeField] TextMeshProUGUI itemK;
    [SerializeField] TextMeshProUGUI itemL;
    [SerializeField] TextMeshProUGUI itemM;
    [SerializeField] TextMeshProUGUI Gold;
    [SerializeField] TextMeshProUGUI itemKAmount;
    [SerializeField] TextMeshProUGUI itemLAmount;
    [SerializeField] TextMeshProUGUI itemMAmount;
    [SerializeField] TextMeshProUGUI GoldAmount;

    //buttons
    [SerializeField] Button itemAButton;
    [SerializeField] Button itemBButton;
    [SerializeField] Button itemCButton;
    [SerializeField] Button itemDButton;
    [SerializeField] Button itemEButton;
    [SerializeField] Button itemFButton;
    [SerializeField] Button itemGButton;
    [SerializeField] Button itemHButton;
    [SerializeField] Button itemIButton;
    [SerializeField] Button itemJButton;

    private List<Consumable> consumables;
    private Equipment[] equipmentList;
    private Gold gold;

    private Dictionary<Equipment, Button> equipmentDictionary;

    void Start()
    {
        GameObject playerPrefab = GameObject.FindGameObjectWithTag("Player");
        playerMovement = playerPrefab.GetComponent<Player_Movement>();
        playerCharacter = playerPrefab.GetComponent<Character>();
        inventoryCanvas.gameObject.SetActive(false);
        equipmentList = FindObjectsOfType<Equipment>();
        consumables = playerCharacter.consumables;
        gold = FindObjectOfType<Gold>();
        equipmentDictionary = new()
        {
            [equipmentList[0]] = itemAButton,
            [equipmentList[1]] = itemBButton,
            [equipmentList[2]] = itemCButton,
            [equipmentList[3]] = itemDButton,
            [equipmentList[4]] = itemEButton,
            [equipmentList[5]] = itemFButton,
            [equipmentList[6]] = itemGButton,
            [equipmentList[7]] = itemHButton,
            [equipmentList[8]] = itemIButton,
            [equipmentList[9]] = itemJButton,
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setInventoryMenuAs(bool active)
    {
        if (active) SoundManager.Instance.PlaySFX("PauseSFX");
        playerMovement.setIsInteracting(active);
        inventoryCanvas.gameObject.SetActive(active);
        setText();
        SetEquipTextForEachButton();
    }

    public void onResumeButton()
    {
        SoundManager.Instance.PlaySFX("UnpauseSFX");
        inventoryCanvas.gameObject.SetActive(false);
        playerMovement.setIsInteracting(false);
    }


    private void setText()
    {
        if (equipmentList[0].getAmount() > 0)
        {
            itemA.text = equipmentList[0].getIdentifier();
            itemA.gameObject.SetActive(true);
            itemAButton.gameObject.SetActive(true);
        }
        else
        {
            itemA.gameObject.SetActive(false);
            itemAButton.gameObject.SetActive(false);
        }
        if (equipmentList[1].getAmount() > 0)
        {
            itemB.text = equipmentList[1].getIdentifier();
            itemB.gameObject.SetActive(true);
            itemBButton.gameObject.SetActive(true);
        }
        else
        {
            itemB.gameObject.SetActive(false);
            itemBButton.gameObject.SetActive(false);
        }
        if (equipmentList[2].getAmount() > 0)
        {
            itemC.text = equipmentList[2].getIdentifier();
            itemC.gameObject.SetActive(true);
            itemCButton.gameObject.SetActive(true);
        }
        else
        {
            itemC.gameObject.SetActive(false);
            itemCButton.gameObject.SetActive(false);
        }
        if (equipmentList[3].getAmount() > 0)
        {
            itemD.text = equipmentList[3].getIdentifier();
            itemD.gameObject.SetActive(true);
            itemDButton.gameObject.SetActive(true);
        }
        else
        {
            itemD.gameObject.SetActive(false);
            itemDButton.gameObject.SetActive(false);
        }
        if (equipmentList[4].getAmount() > 0)
        {
            itemE.text = equipmentList[4].getIdentifier();
            itemE.gameObject.SetActive(true);
            itemEButton.gameObject.SetActive(true);
        }
        else
        {
            itemE.gameObject.SetActive(false);
            itemEButton.gameObject.SetActive(false);
        }
        if (equipmentList[5].getAmount() > 0)
        {
            itemF.text = equipmentList[5].getIdentifier();
            itemF.gameObject.SetActive(true);
            itemFButton.gameObject.SetActive(true);
        }
        else
        {
            itemF.gameObject.SetActive(false);
            itemFButton.gameObject.SetActive(false);
        }
        if (equipmentList[6].getAmount() > 0)
        {
            itemG.text = equipmentList[6].getIdentifier();
            itemG.gameObject.SetActive(true);
            itemGButton.gameObject.SetActive(true);
        }
        else
        {
            itemG.gameObject.SetActive(false);
            itemGButton.gameObject.SetActive(false);
        }
        if (equipmentList[7].getAmount() > 0)
        {
            itemH.text = equipmentList[7].getIdentifier();
            itemH.gameObject.SetActive(true);
            itemHButton.gameObject.SetActive(true);
        }
        else
        {
            itemH.gameObject.SetActive(false);
            itemHButton.gameObject.SetActive(false);
        }
        if (equipmentList[8].getAmount() > 0)
        {
            itemI.text = equipmentList[8].getIdentifier();
            itemI.gameObject.SetActive(true);
            itemIButton.gameObject.SetActive(true);
        }
        else
        {
            itemI.gameObject.SetActive(false);
            itemIButton.gameObject.SetActive(false);
        }
        if (equipmentList[9].getAmount() > 0)
        {
            itemJ.text = equipmentList[9].getIdentifier();
            itemJ.gameObject.SetActive(true);
            itemJButton.gameObject.SetActive(true);
        }
        else
        {
            itemJ.gameObject.SetActive(false);
            itemJButton.gameObject.SetActive(false);
        }

        itemK.text = consumables.Find(x => x.getIdentifier().Equals("Health Potion")).getIdentifier();
        itemL.text = consumables.Find(x => x.getIdentifier().Equals("Mana Potion")).getIdentifier();
        itemM.text = consumables.Find(x => x.getIdentifier().Equals("Smoke Bomb")).getIdentifier();
        Gold.text = "Gold";
        itemKAmount.text = "x" + consumables.Find(x => x.getIdentifier().Equals("Health Potion")).getAmount();
        itemLAmount.text = "x" + consumables.Find(x => x.getIdentifier().Equals("Mana Potion")).getAmount();
        itemMAmount.text = "x" + consumables.Find(x => x.getIdentifier().Equals("Smoke Bomb")).getAmount();
        GoldAmount.text = "x" + gold.getAmount();
    }

    private void SetEquipTextForEachButton()
    {
        foreach ((Equipment equip, Button button) in equipmentDictionary)
        {
            if (equip.equipped)
            {
                button.GetComponentInChildren<TextMeshProUGUI>().SetText("Equipped");
            }
            else
            {
                button.GetComponentInChildren<TextMeshProUGUI>().SetText("Equip");
            }
        }
    }

    public void onEquipAButton()
    {
        SoundManager.Instance.PlaySFX("ConfirmSFX");
        equipmentList[0].equip(playerCharacter);
        SetEquipTextForEachButton();
    }
    public void onEquipBButton()
    {
        SoundManager.Instance.PlaySFX("ConfirmSFX");
        equipmentList[1].equip(playerCharacter);
        SetEquipTextForEachButton();
    }
    public void onEquipCButton()
    {
        SoundManager.Instance.PlaySFX("ConfirmSFX");
        equipmentList[2].equip(playerCharacter);
        SetEquipTextForEachButton();
    }
    public void onEquipDButton()
    {
        SoundManager.Instance.PlaySFX("ConfirmSFX");
        equipmentList[3].equip(playerCharacter);
        SetEquipTextForEachButton();
    }
    public void onEquipEButton()
    {
        SoundManager.Instance.PlaySFX("ConfirmSFX");
        equipmentList[4].equip(playerCharacter);
        SetEquipTextForEachButton();
    }
    public void onEquipFButton()
    {
        SoundManager.Instance.PlaySFX("ConfirmSFX");
        equipmentList[5].equip(playerCharacter);
        SetEquipTextForEachButton();
    }
    public void onEquipGButton()
    {
        SoundManager.Instance.PlaySFX("ConfirmSFX");
        equipmentList[6].equip(playerCharacter);
        SetEquipTextForEachButton();
    }
    public void onEquipHButton()
    {
        SoundManager.Instance.PlaySFX("ConfirmSFX");
        equipmentList[7].equip(playerCharacter);
        SetEquipTextForEachButton();
    }
    public void onEquipIButton()
    {
        SoundManager.Instance.PlaySFX("ConfirmSFX");
        equipmentList[8].equip(playerCharacter);
        SetEquipTextForEachButton();
    }
    public void onEquipJButton()
    {
        SoundManager.Instance.PlaySFX("ConfirmSFX");
        equipmentList[9].equip(playerCharacter);
        SetEquipTextForEachButton();
    }
}
