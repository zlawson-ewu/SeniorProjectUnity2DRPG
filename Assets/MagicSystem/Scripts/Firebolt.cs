public class Firebolt : Spell, ICastable
{
    int baseDamage;

    public Firebolt()
    {
        setSpellName("Firebolt");
        setManaCost(10);
        baseDamage = 20;
    }

    public override int cast(Character self, Character opponent)
    {
        int damage = baseDamage + (self.intl.getValue() / 4);
        opponent.takeDamage(damage);
        return damage;
    }
}
