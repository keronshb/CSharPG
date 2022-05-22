public abstract class Equipment
{
    //The name of our equipment
    public abstract string Name { get; }
    //What does this equipment do?
    public abstract string Description { get; }
    //what the weapon attack is
    public abstract IAttack WeaponAttack { get; }
    /// <summary>
    /// Equip a weapon to a character, checking to see if it's in the party's inventory or not.
    /// </summary>
    /// <param name="battle"></param>
    /// <param name="character"></param>
    public void EquipWeapon(BattleSystem battle, Character character)
    {
        //check the active party's equipment
        var activePartyEquipment = battle.ActiveParty.PartyEquipment;
        //if the character doesn't have a weapon, equip this weapon to them and remove it from the party equipment list
        if (character.Weapon == null)
        {
            character.Weapon = this;
            activePartyEquipment.Remove(this);
            Console.WriteLine($"{character} equipped {this}");
        }
        //if the character has a weapon, add that weapon to the party equipment list and equip the new weapon.
        if (character.Weapon != null)
        {
            activePartyEquipment.Add(character.Weapon);
            character.Weapon = this;
            activePartyEquipment.Remove(this);
            Console.WriteLine($"{character} equipped {this}");
        }
    }
}

public class Dagger : Equipment
{
    public override string Name => "Dagger";
    public override string Description => "Stabby!";
    public override IAttack WeaponAttack => new Stab();
}
public class Stab : IAttack
{
    public string Name => "STAB";
    public AttackData Generate() => new AttackData(1);
}

public class Axe : Equipment
{
    public override string Name => "Axe";
    public override string Description => "Slashy!";
    public override IAttack WeaponAttack => new Swing();
}
public class Swing : IAttack
{
    public string Name => "AXE SLASH";
    public AttackData Generate() => new AttackData(3);
}

public class Bow : Equipment
{
    public override string Name => "Bow";
    public override string Description => "Property of Vin Fletcher";
    public override IAttack WeaponAttack => new Shoot();
}

public class Shoot : IAttack
{
    public string Name => "BASIC SHOT";
    public AttackData Generate() => new AttackData(1, successProbability: 0.5f);
}
