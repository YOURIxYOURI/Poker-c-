using poker;

List<card> deck = new List<card>();
List<card> player1 = new List<card>();
List<card> player2 = new List<card>();

string[] tmp_symbol = {"9","10","J","Q","K","A"};
string[] tmp_color = {"Trefl","Pik","karo","kier"};


void drawCard(List<card> cards)
{
    Random rand = new Random();
    int index = rand.Next(deck.Count);
    cards.Add(deck[index]);
    cards.RemoveAt(index);
}
void GameSet()
{
    foreach (string tmp_c in tmp_color)
    {
        foreach (string tmp_s in tmp_symbol)
        {
            deck.Add(new(tmp_s, tmp_c));
        }
    };
    for (int i = 1; i< 11; i++)
    {
        Random rand = new Random();
        int index = rand.Next(deck.Count);
        if (i % 2 == 0)
        {
            drawCard(player2);
        }
        else
        {
            drawCard(player1);
        }
    }
}
void displayHand(int who)
{
    for (int i = 0; i < 5; i++)
    {
        if (who == 1)
        {
            player1[i].details();
        }
        else
        {
            player2[i].details();
        }
    }
}
bool ChangeCards(int who)
{
    Console.WriteLine("Podaj karty(1-5) które chcesz wymienić odzielając je pojedyńczymi spacjami");
    string choosed = Console.ReadLine();
    IList<string> which_cards = choosed.Split(' ');
    foreach(string card in which_cards)
    {
        int reapeted = 0;
        foreach (string c in which_cards)
        {
            if(card == c)
            {
                reapeted++;
            }
        }
        if (reapeted > 1)
            Console.WriteLine("nie używaj powtórek");
            return false;
        reapeted = 0;
    }

    return true;
}

    GameSet();
    






