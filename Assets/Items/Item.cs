using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public int amount;
    public string identifier;
    public int cost;
    
    public int getAmount()
    {
        return amount;
    }
    public string getIdentifier()
    {
        return identifier;
    }
    public void addAmount(int gain)
    {
        amount += gain;
    }
}
