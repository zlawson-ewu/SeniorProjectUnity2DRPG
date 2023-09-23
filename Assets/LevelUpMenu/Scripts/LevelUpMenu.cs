using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUpMenu : MonoBehaviour
{
    public static LevelUpMenu Instance;

    [SerializeField] GameObject levelUpCanvas;
    [SerializeField] TextMeshProUGUI levelText;

    [SerializeField] GameObject decrementStrengthButton;
    [SerializeField] TextMeshProUGUI strengthPoints;
    [SerializeField] GameObject incrementStrengthButton;

    [SerializeField] GameObject decrementDexterityButton;
    [SerializeField] TextMeshProUGUI dexterityPoints;
    [SerializeField] GameObject incrementDexterityButton;

    [SerializeField] GameObject decrementConstitutionButton;
    [SerializeField] TextMeshProUGUI constitutionPoints;
    [SerializeField] GameObject incrementConstitutionButton;

    [SerializeField] GameObject decrementIntelligenceButton;
    [SerializeField] TextMeshProUGUI intelligencePoints;
    [SerializeField] GameObject incrementIntelligenceButton;

    [SerializeField] TextMeshProUGUI pointsRemainingText;
    [SerializeField] TextMeshProUGUI pointsRemainingPoints;

    [SerializeField] GameObject backButton;
    [SerializeField] GameObject acceptButton;

    int baseStrength;
    int currentStrength;
    int baseDexterity;
    int currentDexterity;
    int baseConstitution;
    int currentConstitution;
    int baseIntelligence;
    int currentIntelligence;
    int statPoints;

    Character playerCharacter;

    public Dictionary<string, bool> equipList;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Destroying duplicate LevelUpMenu");
            Destroy(this);
        }
        else
        {
            Debug.Log("Creating LevelUpMenu");
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        equipList = new();
        levelUpCanvas.gameObject.SetActive(false);
        setDecrementButtonsAs(false);
    }

    public bool hasLeveled()
    {
        playerCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        if ((playerCharacter.exp.getValue() / 100) + 1 > playerCharacter.lvl.getValue())
        {
            return true;
        }
        return false;
    }

    public void levelUp()
    {
        SoundManager.Instance.PlaySFX("IceFreezeSFX");
        Player_Movement.Instance.setIsInteracting(true);
        playerCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        
        baseStrength = playerCharacter.str.getRawValue();
        baseDexterity = playerCharacter.dex.getRawValue();
        baseConstitution = playerCharacter.con.getRawValue();
        baseIntelligence = playerCharacter.intl.getRawValue();
        currentStrength = baseStrength;
        currentDexterity = baseDexterity;
        currentConstitution = baseConstitution;
        currentIntelligence = baseIntelligence;
        statPoints = 3;
        fillInLevelUpMenu();
        levelUpCanvas.gameObject.SetActive(true);
    }

    public void incrementStrength()
    {
        if(statPoints > 0)
        {
            SoundManager.Instance.PlaySFX("EquipSFX");
            currentStrength++;
            statPoints--;
            fillInLevelUpMenu();
            decrementStrengthButton.gameObject.SetActive(true);
        }
    }

    public void decrementStrength()
    {
        if (statPoints < 3)
        {
            SoundManager.Instance.PlaySFX("DeniedSFX");
            currentStrength--;
            statPoints++;
            fillInLevelUpMenu();
            incrementStrengthButton.gameObject.SetActive(true);
        }
    }

    public void incrementDexterity()
    {
        if (statPoints > 0)
        {
            SoundManager.Instance.PlaySFX("EquipSFX");
            currentDexterity++;
            statPoints--;
            fillInLevelUpMenu();
            decrementDexterityButton.gameObject.SetActive(true);
        }
    }

    public void decrementDexterity()
    {
        if (statPoints < 3)
        {
            SoundManager.Instance.PlaySFX("DeniedSFX");
            currentDexterity--;
            statPoints++;
            fillInLevelUpMenu();
            incrementDexterityButton.gameObject.SetActive(true);
        }
    }

    public void incrementConstitution()
    {
        if (statPoints > 0)
        {
            SoundManager.Instance.PlaySFX("EquipSFX");
            currentConstitution++;
            statPoints--;
            fillInLevelUpMenu();
            decrementConstitutionButton.gameObject.SetActive(true);
        }
    }

    public void decrementConstitution()
    {
        if (statPoints < 3)
        {
            SoundManager.Instance.PlaySFX("DeniedSFX");
            currentConstitution--;
            statPoints++;
            fillInLevelUpMenu();
            incrementConstitutionButton.gameObject.SetActive(true);
        }
    }

    public void incrementIntelligence()
    {
        if (statPoints > 0)
        {
            SoundManager.Instance.PlaySFX("EquipSFX");
            currentIntelligence++;
            statPoints--;
            fillInLevelUpMenu();
            decrementIntelligenceButton.gameObject.SetActive(true);
        }
    }

    public void decrementIntelligence()
    {
        if (statPoints < 3)
        {
            SoundManager.Instance.PlaySFX("DeniedSFX");
            currentIntelligence--;
            statPoints++;
            fillInLevelUpMenu();
            incrementIntelligenceButton.gameObject.SetActive(true);
        }
    }

    public void onAcceptButton()
    {
        SoundManager.Instance.PlaySFX("ConfirmSFX");
        playerCharacter.level++;
        playerCharacter.strength = currentStrength;
        playerCharacter.dexterity = currentDexterity;
        playerCharacter.constitution = currentConstitution;
        playerCharacter.intelligence = currentIntelligence;
        playerCharacter.recalculateStatsWithFullHeal();
        playerCharacter.fullHeal();

        levelUpCanvas.gameObject.SetActive(false);
        Player_Movement.Instance.setIsInteracting(false);
    }

    public void onBackButton()
    {
        SoundManager.Instance.PlaySFX("DeclineSFX");
        baseStrength = playerCharacter.str.getValue();
        baseDexterity = playerCharacter.dex.getValue();
        baseConstitution = playerCharacter.con.getValue();
        baseIntelligence = playerCharacter.intl.getValue();
        currentStrength = baseStrength;
        currentDexterity = baseDexterity;
        currentConstitution = baseConstitution;
        currentIntelligence = baseIntelligence;
        statPoints = 3;
        fillInLevelUpMenu();
    }

    void setDecrementButtonsAs(bool enabled)
    {
        decrementStrengthButton.gameObject.SetActive(enabled);
        decrementDexterityButton.gameObject.SetActive(enabled);
        decrementConstitutionButton.gameObject.SetActive(enabled);
        decrementIntelligenceButton.gameObject.SetActive(enabled);
    }

    void setIncrementButtonsAs(bool enabled)
    {
        incrementStrengthButton.gameObject.SetActive(enabled);
        incrementDexterityButton.gameObject.SetActive(enabled);
        incrementConstitutionButton.gameObject.SetActive(enabled);
        incrementIntelligenceButton.gameObject.SetActive(enabled);
    }

    void fillInLevelUpMenu()
    {
        levelText.text = "Level: " + (playerCharacter.lvl.getValue() + 1);
        strengthPoints.text = currentStrength.ToString();
        dexterityPoints.text = currentDexterity.ToString();
        constitutionPoints.text = currentConstitution.ToString();
        intelligencePoints.text = currentIntelligence.ToString();
        pointsRemainingPoints.text = statPoints.ToString();

        if(statPoints == 0)
        {
            setIncrementButtonsAs(false);
            pointsRemainingText.gameObject.SetActive(false);
            pointsRemainingPoints.gameObject.SetActive(false);
            backButton.gameObject.SetActive(true);
            acceptButton.gameObject.SetActive(true);
        }
        else
        {
            setIncrementButtonsAs(true);
            pointsRemainingText.gameObject.SetActive(true);
            pointsRemainingPoints.gameObject.SetActive(true);
            backButton.gameObject.SetActive(false);
            acceptButton.gameObject.SetActive(false);
        }

        if(currentStrength == baseStrength)
        {
            decrementStrengthButton.gameObject.SetActive(false);
        }
        if (currentDexterity == baseDexterity)
        {
            decrementDexterityButton.gameObject.SetActive(false);
        }
        if (currentConstitution == baseConstitution)
        {
            decrementConstitutionButton.gameObject.SetActive(false);
        }
        if (currentIntelligence == baseIntelligence)
        {
            decrementIntelligenceButton.gameObject.SetActive(false);
        }
    }
}
