using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character : MonoBehaviour, ISaveManager
{
    public string characterName;

    public int level;
    public int experiencePoints;

    public int strength;
    public int dexterity;
    public int constitution;
    public int intelligence;

    public Stat lvl = new Stat("LVL");
    public Stat exp = new Stat("EXP");

    public Stat str = new Stat("STR");
    public Stat dex = new Stat("DEX");
    public Stat con = new Stat("CON");
    public Stat intl = new Stat("INT");

    public Stat maxhp = new Stat("HP");
    public Stat maxmp = new Stat("MP");
    public Stat atk = new Stat("ATK");
    public Stat def = new Stat("DEF");
    public Stat acc = new Stat("ACC");
    public Stat eva = new Stat("EVA");
    public Stat crit = new Stat("CRIT");

    public int currentHp;
    public int currentMp;
    public List<Consumable> consumables;
    public Equipment[] equipment;

    public Dictionary<string, bool> equipList;

    public bool isDead = false;
    private void Awake()
    {
        equipment = FindObjectsOfType<Equipment>();
        recalculateStatsWithFullHeal();
    }

    public void LoadData(GameData data)
    {
        if (gameObject.GetComponent<Player_Movement>() != null)
        {
            characterName = data.characterName;
            level = data.level;
            experiencePoints = data.experiencePoints;
            strength = data.strength;
            dexterity = data.dexterity;
            constitution = data.constitution;
            intelligence = data.intelligence;
            recalculateStats();
            currentHp = data.currentHp;
            currentMp = data.currentMp;
        }

        if (gameObject.GetComponent<EnemyController>() != null)
        {
            if (data.enemyIsDead.ContainsKey(gameObject.transform.parent.name))
            {
                isDead = data.enemyIsDead[gameObject.transform.parent.name];
                gameObject.transform.parent.gameObject.SetActive(!isDead);
            }
            if (data.enemyPos.ContainsKey(gameObject.transform.parent.name))
            {
                transform.position = data.enemyPos[gameObject.transform.parent.name];
            }
        }
    }

    public void SaveData(GameData data)
    {
        if (gameObject.GetComponent<Player_Movement>() != null)
        {
            data.characterName = characterName;
            data.level = lvl.getValue();
            data.experiencePoints = exp.getValue();
            data.strength = str.getRawValue();
            data.dexterity = dex.getRawValue();
            data.constitution = con.getRawValue();
            data.intelligence = intl.getRawValue();
            data.currentHp = currentHp;
            data.currentMp = currentMp;
        }

        if (gameObject.GetComponent<EnemyController>() != null)
        {
            if (!data.enemyIsDead.TryAdd(gameObject.transform.parent.name, isDead))
            {
                data.enemyIsDead[gameObject.transform.parent.name] = isDead;
            }
            if (!data.enemyPos.TryAdd(gameObject.transform.parent.name, transform.position))
            {
                data.enemyPos[gameObject.transform.parent.name] = transform.position;
            }
        }
    }

    private void setCoreStats()
    {
        str.setValue(strength);
        dex.setValue(dexterity);
        con.setValue(constitution);
        intl.setValue(intelligence);

        lvl.setValue(level);
        exp.setValue(experiencePoints);
    }

    private void calculateDerivedStats()
    {
        maxhp.setValue(4 * con.getValue());
        maxmp.setValue(2 * intl.getValue());
        atk.setValue(str.getValue());
        eva.setValue(dex.getValue());
    }

    public void recalculateStats()
    {
        setCoreStats();
        calculateDerivedStats();
        adjustHealthAndManaIfOverMax();
    }

    public void recalculateStatsWithFullHeal()
    {
        setCoreStats();
        calculateDerivedStats();
        fullHeal();
    }

    public void fullHeal()
    {
        currentHp = maxhp.getValue();
        currentMp = maxmp.getValue();
    }

    public void adjustHealthAndManaIfOverMax()
    {
        if (currentHp > maxhp.getValue())
        {
            currentHp = maxhp.getValue();
        }
        if (currentMp > maxmp.getValue())
        {
            currentMp = maxmp.getValue();
        }
    }

    public int takeDamage(int damage) 
    {
        damage = Mathf.Clamp(damage - def.getValue(), 1, int.MaxValue);
        currentHp = Mathf.Clamp(currentHp - damage, 0, int.MaxValue);
        Debug.Log(characterName + " has taken " + damage + " damage");
        Debug.Log(characterName + " has " + currentHp + " remaining");

        if (currentHp <= 0)
        {
            Debug.Log(characterName + " has died");
            die();
        }

        return damage;
    }

    public void heal(int amount)
    {
        currentHp = Mathf.Clamp(currentHp + amount, 1, maxhp.getValue());
        Debug.Log(characterName + " has been healed for " + amount);
    }

    public void restore(int amount)
    {
        currentMp = Mathf.Clamp(currentMp + amount, 1, maxmp.getValue());
        Debug.Log(characterName + " has restored " + amount + " mana");
    }

    public virtual void die() 
    {
        isDead = true;
    }
}
