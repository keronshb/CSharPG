public class Party
{
    //Game should be able to represent a party with a collection of characters
    public List<Character> PartyList { get; set; } = new List<Character>();
    //The collective list of items that the party holds
    public List<Item> PartyItems { get; set; } = new List<Item>();
    //The collective list of equipment that the party holds
    public List <Equipment> PartyEquipment { get; set; } = new List<Equipment>();

    public bool PartyTurn { get; private set; }
    //Shortcut to add character to party
    public void AddToParty(Character character) { PartyList.Add(character); }
    //Shortcut to remove character to party
    public void RemoveFromParty(Character character) { PartyList.Remove(character); }

    /// <summary>
    /// See if a character dies, if so state they died and remove them from the party list.
    /// </summary>
    /// <param name="character"></param>
    public void CharacterDeathCheck(Character character)
    {
        character.DeathCheck();
        var alive = character.IsAlive;
        if (!alive)
        {
            RemoveFromParty(character);
            Console.WriteLine($"{character} has died!");
        }
    }
    /// <summary>
    /// Get the list of items by Name and Quanitity, order them, and store them all in a string for display.
    /// </summary>
    /// <returns></returns>
    public string GetandOrderItems()
    {
        var itemList = from i in PartyItems
                       select $"{i.Name} x{i.Quantity}";
        string items = "";
        foreach (var item in itemList)
        {
            items = String.Join(", ", itemList);
        }

        return items;
    }

    /// <summary>
    /// Get the list of Equipment by Name, order them, and store them all in a string for display.
    /// </summary>
    /// <returns></returns>
    public string GetandOrderEquipment()
    {
        var equipmentList = from i in PartyEquipment
                       select $"{i.Name}";
        string equipment = "";
        foreach (var item in equipmentList)
        {
            equipment = String.Join(", ", equipmentList);
        }

        return equipment;
    }

    public void TakeTurn() { if (PartyTurn) PartyTurn = false; }
    public void TurnUp() { if (!PartyTurn) PartyTurn = true; }
}
