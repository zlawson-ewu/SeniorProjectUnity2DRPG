enum Target
{
    Self,
    Opponent
}

public abstract class Spell : ICastable
{
    string spellName;
    int manaCost;
    Target target;

    virtual public int cast(Character self, Character opponent)
    {
        throw new System.NotImplementedException();
    }

    public string getSpellName()
    {
        return spellName;
    }

    public void setSpellName(string name)
    {
        spellName = name;
    }

    public int getManaCost()
    {
        return manaCost;
    }

    public void setManaCost(int cost)
    {
        manaCost = cost;
    }
}
