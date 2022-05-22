public static class ComputerPlayer
{
    public static ICharacterActions ComputerAction(BattleSystem battle, Character character)
    {
        Random random = new Random();
        var itemCoinFlip = random.Next(0, 2); //half the time use an item
        var equipmentCoinFlip = random.Next(1, 5); //quarter of the time, equip something
        int halfHealth = (int)(character.MaxHealthPoints * 0.5); //rounds the health number
        var hasItems = battle.ActiveParty.PartyItems.Any();
        var hasEquipment = battle.ActiveParty.PartyEquipment.Any();
        var characterHasEquipment = character.Weapon != null;
        //heal if half health, half the time!
        if (itemCoinFlip == 1 && character.CurrentHealthPoints == halfHealth && hasItems)
        {
            return new ItemAction();
        }
        //equip 1/4th of the time, if they have something to equip and if they don't have anything equipped
        if (equipmentCoinFlip == 1 && hasEquipment && !characterHasEquipment)
        {
            return new EquipAction();
        }
        //attack if they're alive.
        else if (character.IsAlive)
        {
            return new AttackAction();
        }

        else return new NothingAction();
    }
}
