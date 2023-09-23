using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : Item
{
    [SerializeField] int modifier;
    [SerializeField] stat statBoosted;
    [SerializeField] EquipmentSlot slot;
    public bool equipped;
    static CharacterEquipment inventory;

    public enum EquipmentSlot
    {
        Head,
        Body,
        RHand,
        LHand,
        Legs
    }

    public enum stat
    {
        Strength,
        Constitution,
        Dexterity,
        Intelligence
}

    // Start is called before the first frame update
    void Start()
    {
        inventory = FindObjectOfType<CharacterEquipment>();
        
    }

    public stat getStat()
    {
        return statBoosted;
    }

    public int getModifier()
    {
        return modifier;
    }

    public void equip(Character player)
    {
        if (player.GetComponent<Player_Movement>() != null)
        {
            if (equipped == false)
            {
                inventory.equip(this, player);
                equipped = true;
                switch (statBoosted)
                {
                    case stat.Strength:
                        player.str.addModifier(modifier);
                        break;
                    case stat.Intelligence:
                        player.intl.addModifier(modifier);
                        break;
                    case stat.Constitution:
                        player.con.addModifier(modifier);
                        break;
                    case stat.Dexterity:
                        player.dex.addModifier(modifier);
                        break;
                }
                player.recalculateStats();
            }
        }
    }
    public void unequip(Character player)
    {
        if (player.GetComponent<Player_Movement>() != null)
        {
            equipped = false;
            switch (statBoosted)
            {
                case stat.Strength:
                    player.str.removeModifier(modifier);
                    break;
                case stat.Intelligence:
                    player.intl.removeModifier(modifier);
                    break;
                case stat.Constitution:
                    player.con.removeModifier(modifier);
                    break;
                case stat.Dexterity:
                    player.dex.removeModifier(modifier);
                    break;
            }
            player.recalculateStats();
        }
    }

    public EquipmentSlot getSlot()
    {
        return slot;
    }

     
}
