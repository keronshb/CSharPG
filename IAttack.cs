
public interface IAttack
{
    public string Name { get; }
    //generates the attack data for this attack
    public AttackData Generate();
}
