public class Swift : Spell, ICastable
{
    int dexModifier;

    public Swift()
    { 
        setSpellName("Swift");
        setManaCost(15);
        dexModifier = 15;
    }

    public override int cast(Character self, Character opponent)
    {
        int modifier = dexModifier + (self.intl.getValue() / 2);
        self.dex.addModifier(modifier);
        return modifier;
    }
}
