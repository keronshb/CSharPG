public abstract class Character
{
    public virtual string Name { get; } = "";
    //What type of character
    public virtual CharacterType _characterType { get; private set; } = CharacterType.HUMAN;
    //If the character is controlled by a player or a computer
    public virtual PlayerType _playerType { get; } = PlayerType.CPU;
    public ICharacterActions Actions { get; private set; }
    //Max health the character will have
    public virtual int MaxHealthPoints { get; private set; } = 0;
    //Current health the character will have
    public virtual int CurrentHealthPoints { get; set; } = 0;
    //Basic Attack
    public virtual IAttack BasicAttack { get; private set; }
    public bool IsAlive { get; private set; } = true;
    public virtual Equipment Weapon { get; set; }
    public virtual AttackModifierDefine _attackModifier { get; private set; }
    public virtual IAttackModifiers DefensiveMods { get; }

    public Character() { SetCurrentHealth(); }
    //Sets current health to max health at the start of a series of battle.
    private void SetCurrentHealth() { CurrentHealthPoints = MaxHealthPoints; }
    public void DeathCheck()
    {
        if (CurrentHealthPoints == 0)
            IsAlive = false;
    }
}

public enum PlayerType { HUMAN, CPU } //human control vs computer control
public enum CharacterType { HUMAN, SKELETON, TRUE_PROGRAMMER, UNCODED_ONE, STONE_AMAROK } //represent race of character
