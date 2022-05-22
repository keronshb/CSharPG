public static class Selections
{
    /// <summary>
    /// Returns a Character element.
    /// Uses a battlesystem element to select the party.
    /// Uses a character element to check to see if that character belongs to the party and to see if it's a computer player.
    /// </summary>
    /// <param name="battle"></param>
    /// <param name="character"></param>
    /// <returns></returns>
    public static Character SelectTarget(BattleSystem battle, Character character)
    {
        var computerPlayer = character._playerType == PlayerType.CPU;
        var inMonsterParty = battle.MonsterParty.PartyList.Contains(character);
        int partyInput;
        if (computerPlayer && inMonsterParty) partyInput = 1; //Have the monster party computer select the Hero party
        else if (computerPlayer && !inMonsterParty) partyInput = 2; //Have hero party computer select the Monster Party
        else
        {
            while (true)
            {
                //Select the party you wish to target
                //Allows self targeting for various things like attacks, items, etc if needed.
                Console.WriteLine("Pick the party you wish to target. 1 for the Hero Party, 2 for the Monster Party.");
                if (Int32.TryParse(Console.ReadLine(), out partyInput))
                {
                    Console.Clear();
                    break;
                }
                else Console.WriteLine("An invalid key was entered. Please try again.\n");
            }
        }

        var targetParty = partyInput switch
        {
            1 => battle.HeroParty,
            2 => battle.MonsterParty,
            _ => battle.MonsterParty
        };
        //stores the target party list, will eventually become the target character to return
        var target = targetParty.PartyList;
        while (true)
        {
            int input = 0;
            if (computerPlayer)
            {
                Random random = new Random();
                //random select for monster party based on the length of the chosen party
                random.Next(0, target.Count);
            }
            else
            {
                while (true)
                {
                    Console.WriteLine("Pick the character you wish to target");
                    //shows a list of the characters to target
                    for (int i = 0; i < target.Count; i++)
                    {
                        Console.WriteLine($"{i}: {target[i].Name}");
                    }
                    //input validation
                    if (Int32.TryParse(Console.ReadLine(), out input))
                    {
                        Console.Clear();
                        break;
                    }
                    else Console.WriteLine("An invalid key was entered. Please try again.\n");
                }
            }
            //check to see if the index is null
            var exists = target.ElementAtOrDefault(input) != null;
            //returns the character of the target party list
            if (exists) return target[input];
            else Console.WriteLine("That character doesn't exist. Try again.\n");
        }
    }

    /// <summary>
    /// Returns Item element.
    /// Takes in a battlesystem element to check the active party's items
    /// Takes in a character element to use the item on that character.
    /// </summary>
    /// <param name="battle"></param>
    /// <param name="character"></param>
    /// <returns></returns>
    public static Item SelectItem(BattleSystem battle, Character character)
    {
        //check the active party's items
        var activePartyItems = battle.ActiveParty.PartyItems;
        int partyInput;
        while (true)
        {
            if (character._playerType == PlayerType.CPU)
            {
                //just uses the first item they have for now
                return activePartyItems[0];
            }
            else
            {
                while (true)
                {
                    //Pick the item you want to use
                    Console.WriteLine("Select the item you want to use.");
                    for (int i = 0; i < activePartyItems.Count; i++)
                    {
                        Console.WriteLine($"{i}: {activePartyItems[i].Name} x{activePartyItems[i].Quantity}\n" +
                                                 $"{activePartyItems[i].Description}\n");
                    }
                    if (Int32.TryParse(Console.ReadLine(), out partyInput)) break;
                    else Console.WriteLine("An invalid key was entered. Please try again.\n");
                }
            }
            //check to see if the index is null
            var exists = activePartyItems.ElementAtOrDefault(partyInput) != null;
            //returns the item of the party item list
            if (exists) return activePartyItems[partyInput];
            else Console.WriteLine("That item doesn't exist. Try again.\n");
        }

    }
    /// <summary>
    /// Returns Equipment element.
    /// Takes a battle as a paramter to check active party equipment
    /// Takes a character to equip the equipment.
    /// </summary>
    /// <param name="battle"></param>
    /// <param name="character"></param>
    /// <returns></returns>
    public static Equipment SelectEquipment(BattleSystem battle, Character character)
    {
        //check the active party's equipment
        var activePartyEquipment = battle.ActiveParty.PartyEquipment;
        int partyInput;
        if (character._playerType == PlayerType.CPU)
        {
            //just equips the first thing they have for now
            return activePartyEquipment[0];
        }
        else
        {
            //Pick the equipment you want to use.
            Console.WriteLine("Select the Equipment you want to equip.");
            for (int i = 0; i < activePartyEquipment.Count; i++)
            {
                Console.WriteLine($"{i}: {activePartyEquipment[i].Name}\n" +
                                         $"{activePartyEquipment[i].Description}\n");
            }
            partyInput = Convert.ToInt32(Console.ReadLine());
        }
        return activePartyEquipment[partyInput];
    }
}
