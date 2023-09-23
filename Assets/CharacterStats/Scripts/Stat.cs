using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    private string name;
    private int value;

    private List<int> modifiers = new List<int>();

    public Stat(string name)
    {
        this.name = name;
        this.value = 0;
    }

    public string getName()
    {
        return this.name;
    }

    public void setName(string name)
    {
        this.name = name;
    }

    public int getRawValue()
    {
        return this.value;
    }

    public int getValue()
    {
        int modifiedValue = value;
        foreach (int mod in modifiers)
        {
            modifiedValue += mod;
        }
        return modifiedValue;
    }

    public void setValue(int value)
    {
        this.value = value;
    }

    public void addValue(int value)
    {
        this.value += value;
    }

    public  void addModifier (int mod)
    {
        if(mod != 0)
        {
            modifiers.Add(mod);
        }
    }

    public void removeModifier(int mod)
    {
        if (mod != 0)
        {
            modifiers.Remove(mod);
        }
    }
}
