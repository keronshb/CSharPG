public static class BattleStatusDisplay
{
    /// <summary>
    /// Produces a visual display of the current battle including characters, their equipment, items, stats and the active character attacking
    /// </summary>
    /// <param name="battle"></param>
    /// <param name="activeCharacter"></param>
    public static void BattleDisplay(BattleSystem battle, Character activeCharacter)
    {
        var heroParty = battle.HeroParty;
        var monsterParty = battle.MonsterParty;
        Console.WriteLine("");
        DisplayLines();
        Display(heroParty, activeCharacter);
        Display(monsterParty, activeCharacter);
    }
    //stylistic lines for the battle layout
    private static void DisplayLines()
    {
        ColoredText.TextWriteLine("+----------------------------+", ConsoleColor.White);
    }
    /// <summary>
    /// Loops through the parties and displays them.  Shows which character is active in yellow.  Also shows party items, equipment
    /// and if characters have anything equipped.
    /// </summary>
    /// <param name="party"></param>
    /// <param name="activeCharacter"></param>
    private static void Display(Party party, Character activeCharacter)
    {
        var partyList = party.PartyList;
        var partyItems = party.GetandOrderItems();
        var partyEquipment = party.GetandOrderEquipment();
        foreach (var characters in partyList)
        {
            var hasEquipment = characters.Weapon != null;
            if (characters == activeCharacter)
            {
                ColoredText.TextWriteLine($" => {characters.Name} ({characters.CurrentHealthPoints}/{characters.MaxHealthPoints})", ConsoleColor.Yellow);
                if (hasEquipment) ColoredText.TextWriteLine($"    Equipped: {characters.Weapon.Name}", ConsoleColor.Yellow);
            }
            else
            {
                ColoredText.TextWriteLine($" {characters.Name} ({characters.CurrentHealthPoints}/{characters.MaxHealthPoints})", ConsoleColor.White);
                if (hasEquipment) ColoredText.TextWriteLine($" Equipped: {characters.Weapon.Name}", ConsoleColor.White);
            }
        }
        Console.WriteLine("");
        ColoredText.TextWriteLine($" Items: {partyItems}", ConsoleColor.White);
        ColoredText.TextWriteLine($" Equipment: {partyEquipment}", ConsoleColor.White);
        DisplayLines();
    }
}
