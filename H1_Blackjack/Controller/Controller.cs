using H1_Blackjack.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_Blackjack.Controller
{
    internal class Controller
    {
        string[] dealerCards = new string[5];
        string[] playerCards = new string[5];

        bool dealerStand;
        bool playerStand;

        int playerTotal;
        int dealerTotal;

        View.View view = new View.View();

        public void Start()
        {
            // Entry. calls dealer and then player and checks wins, repeat in while loop
            bool start = true;

            while (true)
            {
                if (start)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        NewCards("dealer");
                        NewCards("player");
                    }

                    start = false;
                }

                Dealer();
                CheckWin();
                Player();
                CheckWin();
            }
        }

        void Dealer()
        {
            // dealer dealer, Calls the NewCards method. dealer cannot get another card, if their total card value is above 17

            if (!dealerStand || playerCards.Length != 5)
            {
                view.message("Dealers turn");

                dealerTotal = 0;
                int aces = 0;

                foreach (string card in dealerCards)
                {
                    string backup = card;
                    string first = backup.Substring(0, 1);

                    if (first == "A")
                    {
                        aces += 11;
                    }
                    else if (first == "K" || first == "Q" || first == "J" || first == "1")
                    {
                        dealerTotal += 10;
                    }
                    else
                    {
                        dealerTotal += int.Parse(first);
                    }
                }

                while (dealerTotal > 21 && aces > 0)
                {
                    dealerTotal -= 10;
                    aces--;
                }
            }
        }

        void Player()
        {
            // Player gets to decide if they wanna hit or stay, and depending on the answer

            if (!playerStand || playerCards.Length != 5)
            {
                view.message("Do you wanna hit or stand?");

                string input = Console.ReadLine().ToLower();

                if (input == "hit")
                {

                }
                else if (input == "stand")
                {
                    playerStand = true;
                }
                else
                {
                    view.message("Input invalid, try again. Write 'hit' or 'stand'");
                }
            }
        }

        void NewCards(string turn)
        {
            // Picks new cards and puts them in arrays

            Cards cards = new Cards();

            Random random = new Random();

            int rnd = random.Next(cards._cards.Length);

            // This for loop runs 5 times, due to the 5 card rule in blackjack
            for (int i = 0; i < 5; i++)
            {
                if (turn == "dealer")
                {
                    if (dealerCards[i] == null)
                    {
                        dealerCards[i] = cards._cards[rnd];

                        if (cards._cards[rnd] == "D")
                        {
                            continue;
                        }

                        cards._cards[i] = "D"; // Deleted)

                        for (int j = 0; j < cards._cards.Length; j++)
                            Console.WriteLine(cards._cards[j]);
                        break;
                    }
                }
                else
                {
                    if (playerCards[i] == "")
                    {
                        playerCards[i] = cards._cards[rnd];
                        break;
                    }
                }
            }

        }

        void CheckWin()
        {
            if (playerStand || dealerStand)
            {
                
            }
        }

    }
}
