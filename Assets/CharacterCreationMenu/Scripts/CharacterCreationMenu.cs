using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterCreationMenu : MonoBehaviour
{
    public static CharacterCreationMenu Instance = null;

    [SerializeField] GameObject characterCreationCanvas;

    [SerializeField] GameObject characterNameScreen;
    string characterName;

    [SerializeField] GameObject pointAllocationScreen;

    [SerializeField] GameObject decrementStrengthButton;
    [SerializeField] TextMeshProUGUI allocatedStrengthPointsText;
    [SerializeField] GameObject incrementStrengthButton;

    [SerializeField] GameObject decrementDexterityButton;
    [SerializeField] TextMeshProUGUI allocatedDexterityPointsText;
    [SerializeField] GameObject incrementDexterityButton;

    [SerializeField] GameObject decrementConstitutionButton;
    [SerializeField] TextMeshProUGUI allocatedConstitutionPointsText;
    [SerializeField] GameObject incrementConstitutionButton;

    [SerializeField] GameObject decrementIntelligenceButton;
    [SerializeField] TextMeshProUGUI allocatedIntelligencePointsText;
    [SerializeField] GameObject incrementIntelligenceButton;

    [SerializeField] TextMeshProUGUI pointsRemainingText;
    [SerializeField] TextMeshProUGUI pointsText;
    [SerializeField] GameObject nextButton;

    [SerializeField] int statPoints = 10;
    int strength = 1;
    int dexterity = 1;
    int constitution = 1;
    int intelligence = 1;

    [SerializeField] GameObject confirmationPanel;
    [SerializeField] TextMeshProUGUI confirmationPanelNameText;
    [SerializeField] TextMeshProUGUI confirmationPanelStrengthText;
    [SerializeField] TextMeshProUGUI confirmationPanelDexterityText;
    [SerializeField] TextMeshProUGUI confirmationPanelConstitutionText;
    [SerializeField] TextMeshProUGUI confirmationPanelIntelligenceText;
    [SerializeField] GameObject backButton;
    [SerializeField] GameObject startGameButton;

    private void Awake()
    {
        if (Instance == null)
        {
            Debug.Log("Creating CharacterCreationMenu");
            Instance = this;
        }
        else
        {
            Debug.Log("Destroying duplicate CharacterCreationMenu");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        characterCreationCanvas.gameObject.SetActive(true);
        Player_Movement.Instance.setIsInteracting(true);
    }

    public void closeCharacterCreationMenu()
    {
        characterCreationCanvas.gameObject.SetActive(false);
        Player_Movement.Instance.setIsInteracting(false);
    }

    public void getCharacterName(string inputName)
    {
        SoundManager.Instance.PlaySFX("ConfirmSFX");
        characterName = inputName;
        characterNameScreen.gameObject.SetActive(false);
        updatePoints();
        enableDecrementButtonsAs(false);
        enableIncrementButtonsAs(true);
        pointAllocationScreen.gameObject.SetActive(true);
    }

    public void incrementStrength()
    {
        if (statPoints > 0)
        {
            SoundManager.Instance.PlaySFX("EquipSFX");
            strength++;
            statPoints--;
            updatePoints();
            decrementStrengthButton.gameObject.SetActive(true);
        }
    }

    public void decrementStrength()
    {
        if (strength > 1)
        {
            SoundManager.Instance.PlaySFX("DeniedSFX");
            strength--;
            statPoints++;
            updatePoints();

            if(strength == 1)
            {
                decrementStrengthButton.gameObject.SetActive(false);
            }
        }
    }

    public void incrementDexterity()
    {
        if (statPoints > 0)
        {
            SoundManager.Instance.PlaySFX("EquipSFX");
            dexterity++;
            statPoints--;
            updatePoints();
            decrementDexterityButton.gameObject.SetActive(true);
        }
    }

    public void decrementDexterity()
    {
        if (dexterity > 1)
        {
            SoundManager.Instance.PlaySFX("DeniedSFX");
            dexterity--;
            statPoints++;
            updatePoints();

            if (dexterity == 1)
            {
                decrementDexterityButton.gameObject.SetActive(false);
            }
        }
    }

    public void incrementConstitution()
    {
        if (statPoints > 0)
        {
            SoundManager.Instance.PlaySFX("EquipSFX");
            constitution++;
            statPoints--;
            updatePoints();
            decrementConstitutionButton.gameObject.SetActive(true);
        }
    }

    public void decrementConstitution()
    {
        if (constitution > 1)
        {
            SoundManager.Instance.PlaySFX("DeniedSFX");
            constitution--;
            statPoints++;
            updatePoints();

            if (constitution == 1)
            {
                decrementConstitutionButton.gameObject.SetActive(false);
            }
        }
    }

    public void incrementIntelligence()
    {
        if (statPoints > 0)
        {
            SoundManager.Instance.PlaySFX("EquipSFX");
            intelligence++;
            statPoints--;
            updatePoints();
            decrementIntelligenceButton.gameObject.SetActive(true);
        }
    }

    public void decrementIntelligence()
    {
        if (intelligence > 1)
        {
            SoundManager.Instance.PlaySFX("DeniedSFX");
            intelligence--;
            statPoints++;
            updatePoints();

            if (intelligence == 1)
            {
                decrementIntelligenceButton.gameObject.SetActive(false);
            }
        }
    }

    public void onNextButton()
    {
        SoundManager.Instance.PlaySFX("ConfirmSFX");
        fillInConfirmationPanel();
        pointAllocationScreen.gameObject.SetActive(false);
        confirmationPanel.gameObject.SetActive(true);
    }

    public void onBackButton()
    {
        SoundManager.Instance.PlaySFX("DeclineSFX");
        strength = 1;
        dexterity = 1;
        constitution = 1;
        intelligence = 1;
        statPoints = 10;
        confirmationPanel.gameObject.SetActive(false);
        characterNameScreen.gameObject.SetActive(true);
    }

    public void onStartGameButton()
    {
        
        Character playerCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();

        playerCharacter.characterName = characterName;
        playerCharacter.level = 1;
        playerCharacter.experiencePoints = 0;
        playerCharacter.strength = strength;
        playerCharacter.dexterity = dexterity;
        playerCharacter.constitution = constitution;
        playerCharacter.intelligence = intelligence;
        playerCharacter.recalculateStatsWithFullHeal();

        characterCreationCanvas.gameObject.SetActive(false);
        Player_Movement.Instance.setIsInteracting(false);
        SoundManager.Instance.PlaySFX("ConfirmSFX");
    }

    void updatePoints()
    {
        allocatedStrengthPointsText.text = strength.ToString();
        allocatedDexterityPointsText.text = dexterity.ToString();
        allocatedConstitutionPointsText.text = constitution.ToString();
        allocatedIntelligencePointsText.text = intelligence.ToString();
        pointsText.text = statPoints.ToString();

        if(statPoints == 0)
        {
            enableIncrementButtonsAs(false);
            pointsRemainingText.gameObject.SetActive(false);
            pointsText.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(true);
        }
        else
        {
            enableIncrementButtonsAs(true);
            pointsRemainingText.gameObject.SetActive(true);
            pointsText.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(false);
        }
    }

    void enableIncrementButtonsAs(bool enabled)
    {
        incrementStrengthButton.gameObject.SetActive(enabled);
        incrementDexterityButton.gameObject.SetActive(enabled);
        incrementConstitutionButton.gameObject.SetActive(enabled);
        incrementIntelligenceButton.gameObject.SetActive(enabled);
    }

    void enableDecrementButtonsAs(bool enabled)
    {
        decrementStrengthButton.gameObject.SetActive(enabled);
        decrementDexterityButton.gameObject.SetActive(enabled);
        decrementConstitutionButton.gameObject.SetActive(enabled);
        decrementIntelligenceButton.gameObject.SetActive(enabled);
    }

    void fillInConfirmationPanel()
    {
        confirmationPanelNameText.text = characterName;
        confirmationPanelStrengthText.text = strength.ToString();
        confirmationPanelDexterityText.text = dexterity.ToString();
        confirmationPanelConstitutionText.text = constitution.ToString();
        confirmationPanelIntelligenceText.text = intelligence.ToString();
    }
}
