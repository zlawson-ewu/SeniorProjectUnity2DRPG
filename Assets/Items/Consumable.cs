using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Consumable : Item
{
    [SerializeField] int strength;
    [SerializeField] stat statAffected;

    public enum stat
    {
        Strength,
        Constitution,
        Dexterity,
        Intelligence,
        CurrentHP,
        Mana
    }
   
    public int getStrength()
    {
        return strength;
    }
    
    public void use(Character player)
    {
        switch (statAffected)
        {
            case stat.Strength:
                player.str.addModifier(strength);
                break;
            case stat.Constitution:
                player.con.addModifier(strength);
                break;
            case stat.Dexterity:
                break;
            case stat.Intelligence:
                player.intl.addModifier(strength);
                break;
            case stat.CurrentHP:
                Debug.Log("HP potion used");
                player.heal(strength);
                break;
            case stat.Mana:
                player.restore(strength);
                break;
        }
        amount--;

    }
}
