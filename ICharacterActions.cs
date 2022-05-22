//Interface for character actions, runs actions.  Takes in a battle system for various checks and a character who will be taking the action
public interface ICharacterActions { public void DoAction(BattleSystem battle, Character character); }
//The character did nothing this round for some reason
public class NothingAction : ICharacterActions
{
    public void DoAction(BattleSystem battle, Character character)
    {
        Console.WriteLine($"{character.Name} did NOTHING.");
    }
}

//The character attacked
public class AttackAction : ICharacterActions
{
    public void DoAction(BattleSystem battle, Character character)
    {
        Character target = Selections.SelectTarget(battle, character);
        AttackController attack = new AttackController(character, target);
    }
}

//The character tried to use an item.
public class ItemAction : ICharacterActions
{
    public void DoAction(BattleSystem battle, Character character)
    {
        var hasItems = battle.ActiveParty.PartyItems.Any();
        if (!hasItems) Console.WriteLine($"{character.Name} tried to use an item, but there were no items to use!");
        else
        {
            var item = Selections.SelectItem(battle, character);
            item.UseItem(battle, character);
        }
    }
}

//The character tried to equip something.
public class EquipAction : ICharacterActions
{
    public void DoAction(BattleSystem battle, Character character)
    {
        var hasEquipment = battle.ActiveParty.PartyEquipment.Any();
        if (!hasEquipment) Console.WriteLine($"{character.Name} tried to equip something, but there was no equipment!");
        else
        {
            var equip = Selections.SelectEquipment(battle, character);
            equip.EquipWeapon(battle, character);
        }
    }
}
