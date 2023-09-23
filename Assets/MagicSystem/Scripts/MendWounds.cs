public class MendWounds : Spell
{
    int baseRecovery;

    public MendWounds()
    {
        setSpellName("Mend Wounds");
        setManaCost(10);
        baseRecovery = 25;
    }

    public override int cast(Character self, Character opponent)
    {
        int healAmount = baseRecovery + (self.intl.getValue() / 4);
        self.heal(healAmount);
        return healAmount;
    }
}
