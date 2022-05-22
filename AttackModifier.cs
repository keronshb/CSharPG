public interface IAttackModifiers
{
    //What is this modifier called
    public string Name { get; }
    //Does it reduce damage?
    public int DamageReduction { get; }
    //Does it increase damage?
    public int IncreaseDamage { get; }
    //What does this modifier do.
    public AttackData RunModifier(AttackData data, Character user, Character target);
}
