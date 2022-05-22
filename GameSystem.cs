
public class GameSystem
{
    public BattleSystem Battle = new BattleSystem();

    private Character MakeHero() //represent true programmer character with name supplied by user
    {
        Console.WriteLine("Welcome hero! Please enter your name: ");
        string input = Console.ReadLine();
        Console.Clear();
        return new TrueProgrammer(input);
    }
    //Adds each party to a list.
    Party[] monsterParties = new Party[] { OneSkeletonParty(), TwoSkeletonParty(), TwoStoneAmarokParty(), UncodedParty() };
    //sets up a monster party.
    public static Party OneSkeletonParty()
    {
        var monsterParty = new Party();
        monsterParty.PartyList = new List<Character> { new Skeleton() { Weapon = new Dagger() } };
        monsterParty.PartyItems = new List<Item> { new Potion(1) };
        return monsterParty;
    }
    public static Party TwoSkeletonParty()
    {
        var monsterParty = new Party();
        monsterParty.PartyList = new List<Character> { new Skeleton(), new Skeleton() };
        monsterParty.PartyItems = new List<Item> { new Potion(2) };
        monsterParty.PartyEquipment = new List<Equipment> { new Dagger(), new Dagger() };
        return monsterParty;
    }

    public static Party TwoStoneAmarokParty()
    {
        var monsterParty = new Party();
        monsterParty.PartyList = new List<Character> { new StoneAmarok(), new StoneAmarok() };
        return monsterParty;
    }

    public static Party UncodedParty()
    {
        var monsterParty = new Party();
        monsterParty.PartyList = new List<Character> { new UncodedOne() };
        monsterParty.PartyItems = new List<Item> { new Potion(1) };
        return monsterParty;
    }
    //Create the hero party
    private void HeroParty()
    {
        var heroParty = Battle.HeroParty;
        heroParty.PartyList = new List<Character> { MakeHero(), new Vin() };
        heroParty.PartyItems = new List<Item> { new Potion(3) };
        heroParty.PartyEquipment = new List<Equipment> { new Dagger(), new Axe() };
    }
    public void RunGame()
    {
        HeroParty();
        //Run through a series of battles equal to the amount of monster parties.
        for (int battleNumber = 1; battleNumber <= monsterParties.Length; battleNumber++)
        {
            //loop through the list of monster parties, set them to battle's monster party list.
            foreach (var monsterParty in monsterParties)
            {
                Battle.MonsterParty = monsterParty;
                Battle.runBattle();
            }
        }
        //Game is finished.
        Console.WriteLine("The game is over!\nPress any key to close.");
        Console.ReadKey();
    }
}
