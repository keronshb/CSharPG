public abstract class Item
{
    //What is this item called
    public abstract string Name { get; }
    //What does this item do?
    public abstract string Description { get; }
    //Quantity of the item
    public int Quantity { get; set; }
    //How items are used, define how they're used in subclasses.
    public abstract void UseItem(BattleSystem battle, Character character);
    //Removes item from the party list of items
    public void removeItem(BattleSystem battle)
    {
        battle.ActiveParty.PartyItems.Remove(this);
    }
}

public class Potion : Item
{
    public override string Name => "Potion";
    public override string Description => $"Use this to heal yourself for {HealStrength} HP!";
    //How much the potion will heal for.
    public int HealStrength { get; } = 5;
    //You should have at least 1 potion when giving it to something.
    public Potion(int quantity) { Quantity = quantity; }

    public override void UseItem(BattleSystem battle, Character character)
    {
        var maxHealth = character.MaxHealthPoints;
        var currentHealth = character.CurrentHealthPoints;
        if (currentHealth == maxHealth)
        {
            Console.WriteLine("The potion had no effect!");
        }
        else
        {
            //How much the character will heal for, prevents from healing above max health.
            character.CurrentHealthPoints = Math.Min(currentHealth + HealStrength, maxHealth);
            
            Console.WriteLine($"{character.Name} healed for {HealStrength} health!");
            Quantity--;
            if (Quantity == 0)
            {
                removeItem(battle);
            }
        }
    }
}
