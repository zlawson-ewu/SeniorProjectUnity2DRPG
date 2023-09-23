using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattlePhase
{
    BATTLE_START, PLAYER_TURN, ENEMY_TURN, WIN, LOSE, ESCAPED
}

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    GameObject enemyPrefab;
    [SerializeField] Image enemySprite;


    GameObject playerPrefab;

    Character playerCharacter;
    Character enemyCharacter;

    List<int> modifiers = new List<int>();

    Firebolt firebolt = new Firebolt();
    Swift swift = new Swift();
    MendWounds mendWounds = new MendWounds();

    

    [SerializeField] GameObject battleScreen;
    [SerializeField] BattleUI playerBattleUI;
    [SerializeField] BattleUI enemyBattleUI;
    [SerializeField] TextMeshProUGUI battleText;
    [SerializeField] Button attackButton;
    [SerializeField] Button runButton;
    [SerializeField] Button magicButton;
    [SerializeField] Button itemButton;

    
    [SerializeField] Button fireboltButton;
    [SerializeField] Button swiftButton;
    [SerializeField] Button mendWoundsButton;

    [SerializeField] Button backButton;

    [SerializeField] Button manaPotionButton;
    [SerializeField] Button healthPotionButton;
    [SerializeField] Button smokebombButton;
    
    [SerializeField] GameObject battleTextPannel;
    [SerializeField] GameObject playerInfoPannel;
    [SerializeField] GameObject enemyInfoPannel;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] Button backToTitleButton;

    public BattlePhase battlePhase;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Destroying duplicate BattleManager");
            Destroy(this);
        }
        else
        {
            Debug.Log("Creating BattleManager");
            Instance = this;
        }
    }

    void Start()
    {
        playerPrefab = GameObject.FindGameObjectWithTag("Player");
        playerCharacter = playerPrefab.GetComponent<Character>();
    }

    public void beginCombat(GameObject enemy)
    {
        enemyPrefab = enemy;
        enemyPrefab.transform.position = new Vector3(0f,-35f,0f); //out of bounds for safety, position change probably isn't necessary if rb simulation disabled
        battlePhase = BattlePhase.BATTLE_START;
        StartCoroutine(startBattle());
    }

    IEnumerator startBattle()
    {
        setPlayerInCombat(true);
        playerBattleUI.populateUI(playerCharacter);

        enemySprite.sprite = enemyPrefab.GetComponent<SpriteRenderer>().sprite;
        enemySprite.gameObject.SetActive(true);
        enemyCharacter = enemyPrefab.GetComponent<Character>();
        enemyBattleUI.populateUI(enemyCharacter);
        
        battleText.text = enemyCharacter.characterName + " is looking for a fight!";

        yield return new WaitForSeconds(2f);

        if(playerCharacter.dex.getValue() >= enemyCharacter.dex.getValue())
        {
            battlePhase = BattlePhase.PLAYER_TURN;
            playerTurn();
        }
        else
        {
            battlePhase = BattlePhase.ENEMY_TURN;
            StartCoroutine(enemyTurn());
            enemyTurn();
        }
    }

    void setPlayerInCombat(bool inCombat)
    {
        playerPrefab.GetComponent<Player_Movement>().setIsInteracting(inCombat);
        playerPrefab.GetComponent<SpriteRenderer>().gameObject.SetActive(!inCombat);
        battleScreen.SetActive(inCombat);
    }

    private void setCombatMenuButtonsAs(bool enabled)
    {
        battleText.gameObject.SetActive(!enabled);
        attackButton.gameObject.SetActive(enabled);
        runButton.gameObject.SetActive(enabled);
        magicButton.gameObject.SetActive(enabled);
        itemButton.gameObject.SetActive(enabled);
    }
    
    private void setMagicButtonsAs(bool enabled)
    {
        battleText.text = "";
        fireboltButton.gameObject.SetActive(enabled);
        swiftButton.gameObject.SetActive(enabled);
        mendWoundsButton.gameObject.SetActive(enabled);
        backButton.gameObject.SetActive(enabled);
    }

    private void setItemButtonsAs(bool enabled)
    {
        battleText.text = "";
        healthPotionButton.gameObject.SetActive(enabled);
        healthPotionButton.GetComponentInChildren<TextMeshProUGUI>().SetText("Health Potion: x" + playerCharacter.consumables.Find(x => x.getIdentifier().Equals("Health Potion")).getAmount());

        manaPotionButton.gameObject.SetActive(enabled);
        manaPotionButton.GetComponentInChildren<TextMeshProUGUI>().SetText("Mana Potion: x" + playerCharacter.consumables.Find(x => x.getIdentifier().Equals("Mana Potion")).getAmount());

        smokebombButton.gameObject.SetActive(enabled);
        smokebombButton.GetComponentInChildren<TextMeshProUGUI>().SetText("Smoke Bomb: x" + playerCharacter.consumables.Find(x => x.getIdentifier().Equals("Smoke Bomb")).getAmount());

        backButton.gameObject.SetActive(enabled);
    }

    void playerTurn()
    {
        setCombatMenuButtonsAs(true);
    }

    IEnumerator enemyTurn()
    {
        battleText.text = enemyCharacter.characterName + " attacks!";

        yield return new WaitForSeconds(1f);

        int dodgeRoll = Random.Range(1, 101);
        if (playerCharacter.eva.getValue() >= dodgeRoll)
        {
            SoundManager.Instance.PlaySFX("MissEvadeSFX");
            battleText.text = playerCharacter.characterName + " dodged the attack!";
            Debug.Log("Player Evasion: " + playerCharacter.eva.getValue());
            Debug.Log("Dodge Roll: " + dodgeRoll);
        }
        else
        {
            SoundManager.Instance.PlaySFX("ImpactFleshSFX");
            int damageTaken = playerCharacter.takeDamage(enemyCharacter.atk.getValue());
            playerBattleUI.updateHp(playerCharacter);
            battleText.text = playerCharacter.characterName + " took " + damageTaken + " damage.";
        }

        yield return new WaitForSeconds(2f);

        if (playerCharacter.isDead)
        {
            SoundManager.Instance.PlaySFX("DeathSFX");
            Debug.Log("Player Died");
            battlePhase = BattlePhase.LOSE;
            Debug.Log("BattlePhase = LOSE");
            StartCoroutine(endBattle());
        }
        else
        {
            Debug.Log("Player did not die.");
            battlePhase = BattlePhase.PLAYER_TURN;
            playerTurn();
        }
    }

    IEnumerator playerAttack()
    {
        setCombatMenuButtonsAs(false);

        int dodgeRoll = Random.Range(1, 101);
        if (enemyCharacter.eva.getValue() >= dodgeRoll)
        {
            SoundManager.Instance.PlaySFX("MissEvadeSFX");
            battleText.text = enemyCharacter.characterName + " dodged the attack!";
            Debug.Log("Enemy Evasion: " + enemyCharacter.eva.getValue());
            Debug.Log("Dodge Roll: " + dodgeRoll);
        }
        else
        {
            SoundManager.Instance.PlaySFX("SlashSFX");
            int damageTaken = enemyCharacter.takeDamage(playerCharacter.atk.getValue());
            enemyBattleUI.updateHp(enemyCharacter);
            battleText.text = enemyCharacter.characterName + " took " + damageTaken + " damage.";
        }

        yield return new WaitForSeconds(2f);

        if (enemyCharacter.isDead)
        {
            battlePhase = BattlePhase.WIN;
            StartCoroutine(endBattle());
        }
        else
        {
            battlePhase = BattlePhase.ENEMY_TURN;
            StartCoroutine(enemyTurn());
        }
    }

    public void onAttackButton()
    {
        if(battlePhase != BattlePhase.PLAYER_TURN)
        {
            return;
        }
        SoundManager.Instance.PlaySFX("ConfirmSFX");
        StartCoroutine(playerAttack());
    }



    IEnumerator playerRun()
    {
        setCombatMenuButtonsAs(false);

        int runChance = 50 + (playerCharacter.eva.getValue() - enemyCharacter.eva.getValue());
        int runRoll = Random.Range(1, 101);
        if (runChance < runRoll)
        {
            SoundManager.Instance.PlaySFX("DeniedSFX");
            battleText.text = playerCharacter.characterName + " can't find an opening to escape!";
            Debug.Log("Run Chance: " + runChance);
            Debug.Log("Run Roll: " + runRoll);
            yield return new WaitForSeconds(2f);
            battlePhase = BattlePhase.ENEMY_TURN;
            StartCoroutine(enemyTurn());
        }
        else
        {
            SoundManager.Instance.PlaySFX("FleeSFX");
            battlePhase = BattlePhase.ESCAPED;
            StartCoroutine(endBattle());
        }
    }

    public void onRunButton()
    {
        if (battlePhase != BattlePhase.PLAYER_TURN)
        {
            return;
        }
        SoundManager.Instance.PlaySFX("ConfirmSFX");
        StartCoroutine(playerRun());
    }

    public void onMagicButton()
    {
        if (battlePhase != BattlePhase.PLAYER_TURN)
        {
            return;
        }
        SoundManager.Instance.PlaySFX("ConfirmSFX");
        setCombatMenuButtonsAs(false);
        setMagicButtonsAs(true);
    }

    public void onItemButton()
    {
        if (battlePhase != BattlePhase.PLAYER_TURN)
        {
            return;
        }
        SoundManager.Instance.PlaySFX("ConfirmSFX");
        setCombatMenuButtonsAs(false);
        setItemButtonsAs(true);
    }

    public void onFireboltButton()
    {
        if (battlePhase != BattlePhase.PLAYER_TURN)
        {
            return;
        }
        SoundManager.Instance.PlaySFX("ConfirmSFX");
        StartCoroutine(playerFirebolt());
    }

    IEnumerator playerFirebolt()
    {
        setMagicButtonsAs(false);

        if (playerCharacter.currentMp >= firebolt.getManaCost())
        {
            SoundManager.Instance.PlaySFX("FireFlareSFX");
            battleText.text = playerCharacter.characterName + " cast " + firebolt.getSpellName() + "!";
            yield return new WaitForSeconds(2f);
            int damageTaken = firebolt.cast(playerCharacter, enemyCharacter);
            playerCharacter.currentMp -= firebolt.getManaCost();
            playerBattleUI.updateMp(playerCharacter);
            enemyBattleUI.updateHp(enemyCharacter);
            battleText.text = enemyCharacter.characterName + " took " + damageTaken + " damage.";
            yield return new WaitForSeconds(2f);

            if (enemyCharacter.isDead)
            {
                battlePhase = BattlePhase.WIN;
                StartCoroutine(endBattle());
            }
            else
            {
                battlePhase = BattlePhase.ENEMY_TURN;
                StartCoroutine(enemyTurn());
            }
        }
        else
        {
            SoundManager.Instance.PlaySFX("DeniedSFX");
            battleText.text = playerCharacter.characterName + " doesn't have enough mana!";
            yield return new WaitForSeconds(2f);
            setMagicButtonsAs(true);
        }
    }

    public void onSwiftButton()
    {
        if (battlePhase != BattlePhase.PLAYER_TURN)
        {
            return;
        }
        SoundManager.Instance.PlaySFX("ConfirmSFX");
        StartCoroutine(playerSwift());
    }

    IEnumerator playerSwift()
    {
        setMagicButtonsAs(false);

        if (playerCharacter.currentMp >= swift.getManaCost())
        {
            SoundManager.Instance.PlaySFX("SpeedUpSFX");
            battleText.text = playerCharacter.characterName + " cast " + swift.getSpellName() + "!";
            Debug.Log("CURRENT DEX: " + playerCharacter.dex.getValue());
            yield return new WaitForSeconds(2f);
            modifiers.Add(swift.cast(playerCharacter, enemyCharacter));
            playerCharacter.currentMp -= swift.getManaCost();
            playerBattleUI.updateMp(playerCharacter);
            Debug.Log("POST BUFF DEX: " + playerCharacter.dex.getValue());
            battleText.text = playerCharacter.characterName + "'s dexterity has increased!";
            yield return new WaitForSeconds(2f);

            battlePhase = BattlePhase.ENEMY_TURN;
            StartCoroutine(enemyTurn());
        }
        else
        {
            SoundManager.Instance.PlaySFX("DeniedSFX");
            battleText.text = playerCharacter.characterName + " doesn't have enough mana!";
            yield return new WaitForSeconds(2f);
            setMagicButtonsAs(true);
        }
    }

    public void onMendWoundsButton()
    {
        if (battlePhase != BattlePhase.PLAYER_TURN)
        {
            return;
        }
        SoundManager.Instance.PlaySFX("ConfirmSFX");
        StartCoroutine(playerMendWounds());
    }

    IEnumerator playerMendWounds()
    {
        setMagicButtonsAs(false);

        if (playerCharacter.currentMp >= mendWounds.getManaCost())
        {
            SoundManager.Instance.PlaySFX("HealSFX");
            battleText.text = playerCharacter.characterName + " cast " + mendWounds.getSpellName() + "!";
            yield return new WaitForSeconds(2f);
            int amountHealed = mendWounds.cast(playerCharacter, enemyCharacter);
            playerCharacter.currentMp -= mendWounds.getManaCost();
            playerBattleUI.updateMp(playerCharacter);
            playerBattleUI.updateHp(playerCharacter);
            battleText.text = playerCharacter.characterName + " healed " + amountHealed + " hit points!";
            yield return new WaitForSeconds(2f);

            battlePhase = BattlePhase.ENEMY_TURN;
            StartCoroutine(enemyTurn());
        }
        else
        {
            SoundManager.Instance.PlaySFX("DeniedSFX");
            battleText.text = playerCharacter.characterName + " doesn't have enough mana!";
            yield return new WaitForSeconds(2f);
            setMagicButtonsAs(true);
        }
    }

    public void onBackButton()
    {
        SoundManager.Instance.PlaySFX("DeclineSFX");
        setMagicButtonsAs(false);
        setItemButtonsAs(false);
        setCombatMenuButtonsAs(true);
    }
    public void onHealthPotionButton()
    {
        if (battlePhase != BattlePhase.PLAYER_TURN)
        {
            return;
        }
        SoundManager.Instance.PlaySFX("ConfirmSFX");
        StartCoroutine(playerHealthPotion());
    }

    IEnumerator playerHealthPotion()
    {
        setItemButtonsAs(false);

        //Boolean for testing purposes
        bool hasHealthPotion = false;
        if (playerCharacter.consumables.Find(x => x.getIdentifier().Equals("Health Potion")).getAmount() > 0)
        {
            hasHealthPotion = true;
        }
        //Be sure to update this boolean to check the players inventory
        if (hasHealthPotion)
        {
            SoundManager.Instance.PlaySFX("UseItemSFX");
            battleText.text = playerCharacter.characterName + " used a Health Potion!";
            yield return new WaitForSeconds(2f);


            //insert useManaPotion() method here. Make sure it return the amount of Hp healed and then uncomment the code below.

            playerCharacter.consumables.Find(x => x.getIdentifier().Equals("Health Potion")).use(playerCharacter);
            Debug.Log(playerCharacter.characterName + " restored " + playerCharacter.consumables.Find(x => x.getIdentifier().Equals("Health Potion")).getStrength() + " hit points!");
            playerBattleUI.updateHp(playerCharacter);
            battleText.text = playerCharacter.characterName + " restored " + playerCharacter.consumables.Find(x => x.getIdentifier().Equals("Health Potion")).getStrength() + " hit points!";
            yield return new WaitForSeconds(2f);
            

            battlePhase = BattlePhase.ENEMY_TURN;
            StartCoroutine(enemyTurn());
        
        }
        else
        {
            SoundManager.Instance.PlaySFX("DeniedSFX");
            battleText.text = playerCharacter.characterName + " doesn't have any Health Potions!";
            yield return new WaitForSeconds(2f);
            setItemButtonsAs(true);
        }
    }

    public void onManaPotionButton()
    {
        if (battlePhase != BattlePhase.PLAYER_TURN)
        {
            return;
        }
        SoundManager.Instance.PlaySFX("ConfirmSFX");
        StartCoroutine(playerManaPotion());
    }

    IEnumerator playerManaPotion()
    {
        setItemButtonsAs(false);

        //Boolean for testing purposes
        bool hasManaPotion = false;
        if (playerCharacter.consumables.Find(x => x.getIdentifier().Equals("Mana Potion")).getAmount() > 0)
        {
            hasManaPotion = true;
        }
        //Be sure to update this boolean to check the players inventory
        if (hasManaPotion)
        {
            SoundManager.Instance.PlaySFX("UseItemSFX");
            battleText.text = playerCharacter.characterName + " used a Mana Potion!";
            yield return new WaitForSeconds(2f);


            //insert useManaPotion() method here. Make sure it return the amount of MP healed and then uncomment the code below.

            playerCharacter.consumables.Find(x => x.getIdentifier().Equals("Mana Potion")).use(playerCharacter);
            playerBattleUI.updateMp(playerCharacter);
            battleText.text = playerCharacter.characterName + " restored " + playerCharacter.consumables.Find(x => x.getIdentifier().Equals("Mana Potion")).getStrength() + " mana points!";
            yield return new WaitForSeconds(2f);
     

            battlePhase = BattlePhase.ENEMY_TURN;
            StartCoroutine(enemyTurn());

        }
        else
        {
            SoundManager.Instance.PlaySFX("DeniedSFX");
            battleText.text = playerCharacter.characterName + " doesn't have any Mana Potions!";
            yield return new WaitForSeconds(2f);
            setItemButtonsAs(true);
        }
    }

    public void onSmokeBombButton()
    {
        if (battlePhase != BattlePhase.PLAYER_TURN)
        {
            return;
        }
        SoundManager.Instance.PlaySFX("ConfirmSFX");
        StartCoroutine(playerSmokeBomb());
    }

    IEnumerator playerSmokeBomb()
    {
        setItemButtonsAs(false);

        //Boolean for testing purposes
        bool hasSmokeBomb = false;

        if (playerCharacter.consumables.Find(x => x.getIdentifier().Equals("Smoke Bomb")).getAmount() > 0)
        {
            hasSmokeBomb = true;
        }
        
        //Be sure to update this boolean to check the players inventory
        if (hasSmokeBomb)
        {
            SoundManager.Instance.PlaySFX("UseItemSFX");
            battleText.text = playerCharacter.characterName + " used a Smoke Bomb!";
            yield return new WaitForSeconds(2f);

            //insert useSmoke() method here.
            playerCharacter.consumables.Find(x => x.getIdentifier().Equals("Smoke Bomb")).use(playerCharacter);
            battlePhase = BattlePhase.ESCAPED;
            StartCoroutine(endBattle());
        }
        else
        {
            SoundManager.Instance.PlaySFX("DeniedSFX");
            battleText.text = playerCharacter.characterName + " doesn't have any Smoke Bombs!";
            yield return new WaitForSeconds(2f);
            setItemButtonsAs(true);
        }
    }

    IEnumerator endBattle()
    {
        Debug.Log("Ending Battle");
        clearModifiers();
        if (battlePhase == BattlePhase.WIN)
        {
            battleText.text = $"{enemyCharacter.characterName} was defeated!";
            yield return new WaitForSeconds(2f);

            playerCharacter.experiencePoints += enemyCharacter.exp.getValue();
            playerCharacter.exp.addValue(enemyCharacter.exp.getValue());
            battleText.text = $"You gained {enemyCharacter.exp.getValue()} experience points.";
            yield return new WaitForSeconds(2f);

            int goldGained = (enemyCharacter.level * 10) + (int)Random.Range(1, 10);
            FindObjectOfType<Gold>().addAmount(goldGained);
            battleText.text = $"You gained {goldGained} gold.";

         
            yield return new WaitForSeconds(3f);
            setPlayerInCombat(false);
            GameManager.Instance.EndCombat();

            //Check if enemy was quest target
            if (enemyPrefab.GetComponent<QuestTarget>() != null)
            {
                QuestTarget questTarget = enemyPrefab.GetComponent<QuestTarget>();
                questTarget.QuestTargetCompleted();
            }
        } 
        else if(battlePhase == BattlePhase.LOSE)
        {
            battleText.text = playerCharacter.characterName + " was defeated.";
            yield return new WaitForSeconds(3f);
            GameManager.Instance.PlayerHasDied();
            StartCoroutine(gameOver());
        } 
        else if(battlePhase == BattlePhase.ESCAPED)
        {
            battleText.text = playerCharacter.characterName + " managed to get away.";
            yield return new WaitForSeconds(3f);
            setPlayerInCombat(false);
            GameManager.Instance.EndCombat();
        }
    }

    IEnumerator gameOver()
    {
        enemySprite.gameObject.SetActive(false);
        battleTextPannel.gameObject.SetActive(false);
        playerInfoPannel.gameObject.SetActive(false);
        enemyInfoPannel.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);

        while (gameOverText.color.a < 1.0f)
        {
            gameOverText.color = new Color(gameOverText.color.r, gameOverText.color.g, gameOverText.color.b, gameOverText.color.a + (Time.deltaTime / 3f));
            yield return null;
        }
        backToTitleButton.gameObject.SetActive(true);
    }

    private void clearModifiers()
    {
        foreach(int mod in modifiers)
        {
            playerCharacter.dex.removeModifier(mod);
        }
    }
}
