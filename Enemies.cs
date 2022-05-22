//Put all enemy/monsters here.

public class Skeleton : Character
{
    public override string Name => "SKELETON";
    public override CharacterType _characterType => CharacterType.SKELETON;
    public override int MaxHealthPoints => 5;
    public override IAttack BasicAttack => new BoneCrunch();
    public override AttackModifierDefine _attackModifier => AttackModifierDefine.STONE_ARMOR;
}
public class BoneCrunch : IAttack
{
    public string Name => "BONE CRUNCH";
    public Random _random = new Random();
    public AttackData Generate() => new AttackData(_random.Next(1, 3));
}

public class StoneAmarok : Character
{
    public override string Name => "Stone Amarok";
    public override CharacterType _characterType => CharacterType.STONE_AMAROK;
    public override int MaxHealthPoints => 10;
    public override IAttack BasicAttack => new Smash();
    public override IAttackModifiers DefensiveMods => new StoneArmor();

}

public class Smash : IAttack
{
    public string Name => "SMASH";
    public Random _random { get; } = new Random();
    public AttackData Generate() => new AttackData(_random.Next(1, 4));
}

public class StoneArmor : IAttackModifiers
{
    public string Name => "STONE ARMOR";
    public int DamageReduction => 1;
    public int IncreaseDamage { get; }
    public AttackData RunModifier(AttackData data, Character user, Character target)
    {
        var damage = data.Damage;
        var damageReduction = DamageReduction;

        //ensures they can't heal from the damage if it's too low.
        damage = Math.Max(damage - damageReduction, 0);
        return new AttackData(damage);
    }
}

public class UncodedOne : Character
{
    public override string Name => "The Uncoded One";
    public override CharacterType _characterType => CharacterType.UNCODED_ONE;
    public override int MaxHealthPoints => 15;
    public override IAttack BasicAttack => new Unraveling();
}

public class Unraveling : IAttack
{
    public string Name => "UNRAVELING";
    public Random _random { get; } = new Random();
    public AttackData Generate() => new AttackData(_random.Next(1, 5), damageType: DamageType.DECODING);
}
