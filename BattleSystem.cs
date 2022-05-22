public partial class BattleSystem
{
    //battle system should probably represent the parties, but if this was a full game then maybe some other system would hold the parties
    public Party HeroParty { get; set; } = new Party();
    public Party MonsterParty { get; set; } = new Party();
    //what the active party attacking is.
    public Party ActiveParty { get; set; }
    public ICharacterActions Actions { get; private set; }
    //Check to see if the battle should continue
    private bool IsDefeated => HeroParty.PartyList.Count == 0 || MonsterParty.PartyList.Count == 0;
    //runs a battle
    public void runBattle()
    {
        while(!IsDefeated)
        {
            //puts each party into a list, runs through each.
            foreach (var party in new[] {HeroParty, MonsterParty})
            {
                foreach (var character in party.PartyList.ToList())
                {
                    ActiveParty = party;
                    party.CharacterDeathCheck(character);
                    if (IsDefeated) break;
                    var alive = character.IsAlive;
                    if(alive)
                    {
                        BattleStatusDisplay.BattleDisplay(this, character);
                        Console.WriteLine("");
                        ColoredText.TextHighlight("It's ", $"{character.Name}'s ", "turn!\n");
                        DoAction(character);
                    }
                }
                if (IsDefeated)
                {
                    Loot.LootItem(this); //takes unused items and unequipped equipment from monsters
                    break;
                }
            }
        }
    }

    public void DoAction(Character character)
    {
        Actions = ChoiceMenu(character);
        Actions.DoAction(this, character);
    }
    
    public ICharacterActions ChoiceMenu(Character character) //Menu for the character or CPU to select a move
    {
        var playerType = character._playerType;
        int input;
        if (playerType == PlayerType.CPU) //cpu picks random action
        {
            return ComputerPlayer.ComputerAction(this, character);
        }
        else
        {
            while (true)
            {
                Console.WriteLine("What will you do?\n" +
                  "1 for nothing.\n" +
                  "2 for an attack.\n" +
                  "3 to use an item.\n" +
                  "4 to equip a weapon.");
                if (Int32.TryParse(Console.ReadLine(), out input))
                {
                    Console.Clear();
                    break;
                }
                else Console.WriteLine("An invalid key was entered. Please try again.\n");
            }
        }
        //returns action based on input.
        return input switch
        {
            1 => new NothingAction(),
            2 => new AttackAction(),
            3 => new ItemAction(),
            4 => new EquipAction(),
            _ => new NothingAction()
        };
    }
}
