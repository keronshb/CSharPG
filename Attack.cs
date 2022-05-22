/// <summary>
/// Controls and runs the attacks.
/// </summary>
public class AttackController
{
    //The character we're targeting with the attack
    private Character Target { get; }
    //Attack interface, handles how the attack act and access the data
    public IAttack Attack { get; private set; }
    //Attack record to store data
    public AttackData Data { get; private set; }
    //AttackModifier interface, modifies the AttackData of an attack based on conditions
    public IAttackModifiers AttackModifiers { get; private set; }

    //When the attack is made, who owns the attack and who is the target for the attack
    public AttackController(Character character, Character target)
    {
        Target = target;
        RunAttack(character);
    }
    public void DealDamage(Character target)
    {
        var damage = Data.Damage;
        //Makes sure HP can't go below 0.
        target.CurrentHealthPoints = Math.Max(target.CurrentHealthPoints - damage, 0);
    }
    //run the attack
    public void RunAttack(Character user)
    {
        //Displays the attack menu to the user
        AttackMenu(user);
        //Generates the attack data
        Data = Attack.Generate();
        var attackSuccess = AttackSuccessful(this, user);
        if (Target.DefensiveMods != null)
        {
            AttackModifiers = Target.DefensiveMods;
            Data = AttackModifiers.RunModifier(Data, user, Target);
        }
        
        if (attackSuccess)
        {
            ColoredText.TextWriteLine($"{user.Name} used {Attack.Name} on {Target.Name}", ConsoleColor.Yellow);
            DealDamage(Target);
            AttackDisplay(user);
            HPDisplay(Target);
        }
        else
        {
            ColoredText.TextWriteLine($"{user.Name} attack MISSED", ConsoleColor.Red);
        }

    }
    //Lets the character pick an attack
    private void AttackMenu(Character user)
    {
        var hasWeapon = user.Weapon != null;
        var computerPlayer = user._playerType == PlayerType.CPU;
        int input = 0;
        //If the computer has a weapon, do a weapon attack, otherwise do a basic attack
        if (computerPlayer)
        {
            input = hasWeapon ?  2 : 1;
        }
        else
        {
            while (true)
            {
                Console.WriteLine("Enter what attack type you wish to do! 1 for a basic attack! 2 for a weapon attack!");
                if (Int32.TryParse(Console.ReadLine(), out input))
                {
                    Console.Clear();
                    break;
                }
                else Console.WriteLine("An invalid key was entered. Please try again.\n");
            }
        }
        //checks to see if they have a weapon, does a basic attack if not.
        if (!hasWeapon && input == 2)
        {
            Console.WriteLine("But there was no weapon to use!  You decide to use your basic attack instead.");
            Attack = user.BasicAttack;
        }
        else
        {
            //stores the attack interface to _attack.
            Attack = input switch
            {
                1 => user.BasicAttack,
                2 => user.Weapon.WeaponAttack,
                _ => user.BasicAttack
            };
        }
    }
    //shows how much damage the attack was
    private void AttackDisplay(Character user)
    {
        var damage = Data.Damage;

        ColoredText.TextWriteLine($"{Attack.Name} did {damage} points of damage to {Target.Name}", ConsoleColor.Red);
    }
    //shows how much health the character has left from the attack
    private void HPDisplay(Character character)
    {
        ColoredText.TextWriteLine($"{Target.Name}'s HP: {character.CurrentHealthPoints}/{character.MaxHealthPoints}", ConsoleColor.Cyan);
    }
    //Checks to see if an attack was successful or not
    private bool AttackSuccessful(AttackController attack, Character target )
    {
        Random random = new Random();
        var attackProbability = Data.SuccessProbability;
        if (attackProbability == 1) return true;
        else if (attackProbability > 0 && attackProbability < 1)
        {
            var coinFlip = random.Next(0, 2);
            if (coinFlip == 1) return true;
            else return false;
        }
        else return false;
    }
}

//Data for the attack
public record AttackData
{
    //How much damage the attack will do
    public int Damage { get; }
    //The success rate of the attack.  1 being guaranteed hit (default), .5 being 50/50, and 0 being guaranteed miss.
    public float SuccessProbability { get; }
    //What kind of damage does this attack do, default is normal.
    public DamageType _damageType { get; }

    public AttackData(int damage, float successProbability = 1, DamageType damageType = DamageType.NORMAL)
    {
        Damage = damage;
        SuccessProbability = successProbability;
        _damageType = damageType;
    }
}

public enum AttackModifierDefine { NONE, STONE_ARMOR, OBJECT_SIGHT } //Modify attack damage

public enum DamageType { NORMAL, DECODING } //Check the damage type, used for strength/weaknesses
