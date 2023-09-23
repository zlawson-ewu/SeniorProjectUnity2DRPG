using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Test purposes only, do not use this in the final implementation

public class Pickup : MonoBehaviour
{
    public PickupType pickupType;
    public int amount;

    public StatTestUIManager statTestUIManager;

    public enum PickupType
    {
        Health, Armor, Poison
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Character character = collision.GetComponent<Character>();

            switch (pickupType)
            {
                case PickupType.Health:
                    character.heal(amount);
                    statTestUIManager.updateUi();
                    break;
                case PickupType.Armor:
                    character.def.addModifier(amount);
                    Debug.Log(character.characterName + " has gained " + amount + " " + character.def.getName());
                    statTestUIManager.updateUi();
                    break;
                case PickupType.Poison:
                    character.takeDamage(amount);
                    statTestUIManager.updateUi();
                    break;
            }
            Destroy(gameObject);
        }
    }
}
