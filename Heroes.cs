//Put all heros here.

public class TrueProgrammer : Character
{
    public override string Name { get; }
    public override CharacterType _characterType => CharacterType.TRUE_PROGRAMMER;
    public override PlayerType _playerType => PlayerType.HUMAN;
    public TrueProgrammer(string name) { Name = name; }
    public override int MaxHealthPoints => 25;
    public override IAttack BasicAttack => new Punch();
    public override IAttackModifiers DefensiveMods => new ObjectSight();

}
public class Punch : IAttack
{
    public string Name => "PUNCH";
    public AttackData Generate() => new AttackData(1);
}

public class ObjectSight : IAttackModifiers
{
    public string Name => "OBJECT SIGHT";
    public int DamageReduction => 1;
    public int IncreaseDamage { get; }
    public AttackData RunModifier(AttackData data, Character user, Character target)
    {
        var damage = data.Damage;
        var damageReduction = DamageReduction;
        var isDecoding = data._damageType == DamageType.DECODING;

        if (isDecoding)
        {
            //ensures they can't heal from the damage if it's too low.
            damage = Math.Max(damage - damageReduction, 0);
            return new AttackData(damage);
        }
        //otherwise returns the original data
        else return data;
    }
}
public class Vin : Character
{
    public override string Name => "Vin Fletcher";
    public override CharacterType _characterType => CharacterType.HUMAN;
    public override int MaxHealthPoints => 15;
    public override IAttack BasicAttack => new Punch();
    public override Equipment Weapon => new Bow();

}
