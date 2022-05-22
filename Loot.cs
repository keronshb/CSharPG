public class Loot
{
    /// <summary>
    /// Returns items and equipment if the enemy party has any left and adds them to the Hero party, or increases their quantity if they exist.
    /// Doesn't add any equipment that's already equipped
    /// </summary>
    /// <param name="battle"></param>
    public static void LootItem(BattleSystem battle)
    {
        var monsterItems = battle.MonsterParty.PartyItems;
        var hasItems = battle.MonsterParty.PartyItems != null;
        var heroItems = battle.HeroParty.PartyItems;
        var monsterEquipment = battle.MonsterParty.PartyEquipment;
        var hasEquipment = battle.MonsterParty.PartyEquipment != null;
        var heroEquipment = battle.HeroParty.PartyEquipment;
        var monsters = battle.MonsterParty.PartyList;

        if (hasItems)
        {

            foreach (var monsterItem in monsterItems)
            {
                //searches for an item in the hero item list to see if it already exists
                var heroItemSearch = (from heroItem in heroItems
                                      where heroItem.Name == monsterItem.Name
                                      select heroItem).FirstOrDefault();
                //if there is no item that matches, just add the item from the monster item list to the hero item list
                if (heroItemSearch == null)
                {
                    heroItems.Add(monsterItem);
                }
                //otherwise, increase the found hero item by the quanitity of the monster item that matches
                else
                {
                    heroItemSearch.Quantity += monsterItem.Quantity;
                }
            }
        }

        if (hasEquipment)
        {
            foreach (var equipment in monsterEquipment)
            {
                //adds equipment to the hero equipment list if any exist.
                heroEquipment.Add(equipment);
            }
        }
    }
}
