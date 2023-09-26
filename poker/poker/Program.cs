using poker;

deck deck = new();
Player player1 = new Player { name = "1", whatInHand=""};
Player player2 = new Player { name = "2", whatInHand=""};

string[] tmp_symbol = {"9","10","J","Q","K","A"};
string[] tmp_color = {"Trefl","Pik","karo","kier"};
int[] tmp_value = {9,10,11,12,13,14};

void drawCard(Player p)
{
    Random rand = new Random();
    int index = rand.Next(deck.cards.Count);
    p.cards.Add(deck.cards[index]);
    deck.cards.RemoveAt(index);
}
void ShuffleDeck()
{
    Random rng = new Random();
    var shuffledDeck = deck.cards.OrderBy(a => rng.Next()).ToList();
    deck.cards = shuffledDeck;
}
void GameSet()
{
    foreach (string tmp_c in tmp_color)
    {
        for (int i = 0; i < tmp_symbol.Length;i++)
        {
            deck.cards.Add(new(tmp_symbol[i], tmp_c, tmp_value[i]));
        }
    };
    for (int i = 1; i< 11; i++)
    {
        Random rand = new Random();
        int index = rand.Next(deck.cards.Count);
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
void displayHand(List<card> cards)
{
    for (int i = 0; i < 5; i++)
    {
        cards[i].details();
    }
}
bool ChangeCards(Player p)
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

        p.cards.RemoveAt(which-1);
        drawCard(p);
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

int checkPair(Player p)
{
    int howManyPairs = 0;
    bool isFullPair = false;
    bool isFullThree = false;
    int points = 0;
    foreach(card card in p.cards)
    {
        int repeted = -1;
        foreach( card c in p.cards)
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
            p.whatInHand = "Three of kind";
        }
        if(repeted == 3)
        {
            points += 800 +card.value* 4;
            p.whatInHand = "Four of kind";
        }
    }
    if(isFullThree && isFullPair)
    {
        points += 200;
        p.whatInHand = "Full House";
    }
    if (howManyPairs == 4)
    {
        p.whatInHand = "Two Pair";
    }
    else if (howManyPairs == 2)
    {
        p.whatInHand = "One Pair";
    }
    return points;
}

int checkHand(Player p)
{
    List<card> hand = p.cards.OrderBy(o => o.value).ToList();
    int points = 0;
    if(checkColor(hand) && checkOrder(hand))
    {
        foreach(card card in hand)
        {
            if(card.value == 14)
            {
                points += 1100;
                p.whatInHand = "Royal flush";
                return points;
            }
            else
            {
                points += 1000;
                p.whatInHand = " StraigthFlush";
                return points;
            }
        }
    }
    if (checkColor(hand))
    {
        points += 470;
        p.whatInHand = "Flush";
        return points;
    }
    if (checkOrder(hand))
    {
        foreach (card card in hand)
        {
            if (card.value == 14)
            {
                points += 452;
                p.whatInHand = "straigth"; 
                return points;
            }
            else
            {
                points += 451;
                p.whatInHand = "straigth";
                return points;
            }
        }
    }
    points = checkPair(p);
    if(points != 0)
    {
        return points;
    }
    p.whatInHand = "High card";
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
        changed = ChangeCards(player1);
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
        changed = ChangeCards(player2);
    }
    displayHand(player2.cards);
    Console.WriteLine("Jeśli zapoznałeś się z kartami wciśnij ENTER");
    Console.ReadLine();
    Console.Clear();
    if (checkHand(player1) > checkHand(player2))
    {
        Console.WriteLine($"Wygrał gracz 1 z {player1.whatInHand}");
    }
    else if (checkHand(player1) < checkHand(player2))
    {
        Console.WriteLine($"Wygrał gracz 2 z {player2.whatInHand}");
    }
    else
    {
        Console.WriteLine("REMIS");
    }
}

Game();
    






