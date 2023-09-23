using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEquipment : MonoBehaviour
{
    [SerializeField] Equipment headGear;
    [SerializeField] Equipment chestGear;
    [SerializeField] Equipment rHandGear;
    [SerializeField] Equipment lHandGear;
    [SerializeField] Equipment LegGear;


    public enum EquipmentSlot
    {
        Head,
        Body,
        RHand,
        LHand,
        Legs
    }

    public void equip(Equipment gear, Character player)
    {
        EquipmentSlot slot = (EquipmentSlot)gear.getSlot();
        switch (slot)
        {
            case EquipmentSlot.Head:
                if (headGear != null)
                {
                    headGear.unequip(player);
                }
                headGear = gear;
                break;
            case EquipmentSlot.Body:
                if (chestGear != null)
                {
                    chestGear.unequip(player);
                }
                chestGear = gear;
                break;
            case EquipmentSlot.RHand:
                if (rHandGear != null)
                {
                    rHandGear.unequip(player);
                }
                rHandGear = gear;
                break;
            case EquipmentSlot.LHand:
                if (lHandGear != null)
                {
                    lHandGear.unequip(player);
                }
                lHandGear = gear;
                break;
            case EquipmentSlot.Legs:
                if (LegGear != null)
                {
                    LegGear.unequip(player);
                }
                LegGear = gear;
                break;
        }
    }

}