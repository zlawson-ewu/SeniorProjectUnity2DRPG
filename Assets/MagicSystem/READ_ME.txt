Spell.cs:     This is the super class that every spell should inherit from. Every spell has a name, a target, and a manaCost. 
              These values are all able to be set inside the Unity inspector. Each Spell also should override the Cast(self, opponent)
              method. 

Cast():       This is the method that will run whenever  a Spell effect is activated. If a spell is a buff type spell, MAKE SURE
              TO RETURN THE BUFF MODIFIER VALUE. Otherwise, make sure to return 0. If you forget to return the buff modifier value
              then any buffs the player casts during combat will become permanent. 

MendWound.cs: A basic recovery spell. The caster will heal themselves for the baseRecovery + 25% of thier Intelligence stat.

Firebolt.cs:  A basic damage spell that will deal damage to the target equal to the baseDamage + 25% of the casters Intelligence stat.

Swift.cs:     A basic buff spell that buffs the caster for the dexModifier amount + 50% of the casters Dexterity stat. 