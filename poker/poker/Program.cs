using poker;

List<card> deck = new List<card>();
Player player1 = new Player { name = "1"};
Player player2 = new Player { name = "2"};

string[] tmp_symbol = {"9","10","J","Q","K","A"};
string[] tmp_color = {"Trefl","Pik","karo","kier"};
int[] tmp_value = {9,10,11,12,13,14};
string whatWins1="";
string whatWins2="";

void drawCard(List<card> cards)
{
    Random rand = new Random();
    int index = rand.Next(deck.Count);
    cards.Add(deck[index]);
    deck.RemoveAt(index);
}
void GameSet()
{
    foreach (string tmp_c in tmp_color)
    {
        for (int i = 0; i < tmp_symbol.Length;i++)
        {
            deck.Add(new(tmp_symbol[i], tmp_c, tmp_value[i]));
        }
    };
    for (int i = 1; i< 11; i++)
    {
        Random rand = new Random();
        int index = rand.Next(deck.Count);
        if (i % 2 == 0)
        {
            drawCard(player2.cards);
        }
        else
        {
            drawCard(player1.cards);
        }
    }
}
void displayHand(List<card> cards)
{
    for (int i = 0; i < 5; i++)
    {
        cards[i].details();
    }
}
bool ChangeCards(List<card> cards)
{
    Console.WriteLine("Podaj karty(1-5) które chcesz wymienić odzielając je pojedyńczymi spacjami");
    string choosed = Console.ReadLine();
    if(choosed == "") { return false; }
    List<string> which_cards = choosed.Split(' ').ToList();
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
        {
            Console.WriteLine("nie używaj powtórek");
            return true;
        }
        reapeted = 0;
    }
    which_cards.Sort();
    for(int i = which_cards.Count;i > 0; i--)
    {
        int which = int.Parse(which_cards[i-1]);

        cards.RemoveAt(which-1);
        drawCard(cards);
    }
    return false;
}

bool checkColor(List<card> cards)
{
    if ((cards[0].color == cards[1].color)&& (cards[0].color == cards[2].color)&& (cards[0].color == cards[3].color)&& (cards[0].color == cards[4].color)) 
    {
        return true;
    }
    return false;
}

bool checkOrder(List<card> cards)
{
    bool isOrder = true;
    for (int i = 1; i < cards.Count-1; i++)
    {
        if (cards[i-1].value != cards[i].value - 1)
        {
            isOrder = false; break;
        }
    }
    return isOrder;
}

int checkPair(List<card> cards)
{
    int howManyPairs = 0;
    bool isFullPair = false;
    bool isFullThree = false;
    int points = 0;
    foreach(card card in cards)
    {
        int repeted = -1;
        foreach( card c in cards)
        {
            if(card.value == c.value)
            {
                repeted++;
            }
        }
        if(repeted == 1)
        {
            howManyPairs++;
            isFullPair = true;
            points += 100 + card.value * 2;
        }
        if(repeted == 2)
        {
            isFullThree= true;
            points += 300 + card.value * 3;
        }
        if(repeted == 3)
        {
            points += 800 +card.value* 4;
        }
    }
    if(isFullThree && isFullPair)
    {
        points += 200;
    }
    return points;
}

int checkHand(List<card> cards)
{
    List<card> hand = cards.OrderBy(o => o.value).ToList();
    int points = 0;
    if(checkColor(hand) && checkOrder(hand))
    {
        foreach(card card in hand)
        {
            if(card.value == 14)
            {
                points += 1100;
                
                return points;
            }
            else
            {
                points += 1000;
                return points;
            }
        }
    }
    if (checkColor(hand))
    {
        points += 470;
        return points;
    }
    if (checkOrder(hand))
    {
        foreach (card card in hand)
        {
            if (card.value == 14)
            {
                points += 452;
                return points;
            }
            else
            {
                points += 451;
                return points;
            }
        }
    }
    points = checkPair(hand);
    if(points != 0)
    {
        return points;
    }
    return points = hand[4].value;
}

void Game()
{
    GameSet();
    Console.WriteLine("Jeśli jesteś graczem 1 kliknij ENTER");
    Console.ReadLine();
    displayHand(player1.cards);
    bool changed = true;
    while (changed)
    {
        changed = ChangeCards(player1.cards);
    }
    displayHand(player1.cards);
    Console.WriteLine("Jeśli zapoznałeś się z kartami wciśnij ENTER");
    Console.ReadLine();
    Console.Clear();
    Console.WriteLine("Jeśli jesteś graczem 2 kliknij ENTER");
    Console.ReadLine();
    displayHand(player2.cards);
    changed = true;
    while (changed)
    {
        changed = ChangeCards(player2.cards);
    }
    displayHand(player2.cards);
    Console.WriteLine("Jeśli zapoznałeś się z kartami wciśnij ENTER");
    Console.ReadLine();
    Console.Clear();
    if (checkHand(player1.cards) > checkHand(player2.cards))
    {
        Console.WriteLine($"Wygrał gracz 1 z {whatWins1}");
    }
    else if (checkHand(player1.cards) < checkHand(player2.cards))
    {
        Console.WriteLine($"Wygrał gracz 2 z {whatWins2}");
    }
    else
    {
        Console.WriteLine("REMIS");
    }
}

Game();
    






