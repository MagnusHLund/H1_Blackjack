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
        string dealerSecret = "";

        bool start = true;

        Cards cards = new Cards();
        View.View view = new View.View();

        public void Start()
        {
            // Infinite loop, to repeat the game
            while (true)
            {
                // This runs at the start of every game
                if (start)
                {
                    // Assgins the player and dealer with 2 cards each
                    for (int i = 0; i < 2; i++)
                    {
                        NewCards("player");
                        NewCards("dealer");
                    }

                    // Displays the cards and sets start to false, so this if statement doesnt get run again, this game
                    ShowCards();
                    start = false;
                }

                // Doesnt run the methods inside, this if statement, if the player has stood.
                // This part sometimes bugs out and spams the console with "dealers turn" infinitely
                if (!playerStand)
                {
                    Player();
                    CheckWin();
                }

                if (!dealerStand)
                {
                    Dealer();
                    CheckWin();
                }
            }
        }

        /// <summary>
        /// This method shows the cards of the dealer and player, just after their first cards has been given to them.
        /// </summary>
        void ShowCards()
        {
            view.Message("Dealer cards:");

            // Only shows one of the dealers cards
            view.RedMessage(dealerCards[1]);

            view.Message("Player cards:");

            for (int i = 0; i < 2; i++)
            {
                view.GreenMessage(playerCards[i]);
            }

            view.Message("");
        }

        void Player()
        {
            // Output message to the console
            view.GreenMessage("Players turn\nDo you wanna hit or stand?");

            // Reads the input from the user
            string input = Console.ReadLine().ToLower();

            // If user said "hit" then they get a new card
            if (input == "hit")
            {
                // Provides a new card, using the NewCards method
                NewCards("player");

                // Resets the value of the int playerTotal, because it will be recounted. Creates new int to keep track of aces.
                playerTotal = 0;
                int aces = 0;

                // for loop that runs 5 times, because how many cards we got, due to the five card rule in black jack
                for (int i = 0; i < playerCards.Length; i++)
                {
                    // If the card is not null
                    if (playerCards[i] != null)
                    {
                        // Gets the first character of the string, which is the number
                        string first = playerCards[i].Substring(0, 1);

                        // Determines values, based of the first character
                        if (first == "A")
                        {
                            aces += 1;
                            playerTotal += 11;
                        }
                        else if (first == "K" || first == "Q" || first == "J" || first == "1")
                        {
                            playerTotal += 10;
                        }
                        else
                        {
                            playerTotal += int.Parse(first);
                        }
                    }

                    // Outputs the card to the console if its not null
                    if (dealerCards[i] != null)
                    {
                        view.GreenMessage($"Player has: {playerCards[i]}");
                    }

                    // Change the 11 value from the ace, to 1, if the playerTotal goes over 21
                    while (playerTotal > 21 && aces > 0)
                    {
                        playerTotal -= 10;
                        aces--;
                    }
                }
            }
            else if (input == "stand") // If user writes "stand" then the boolean for playerStand will become true
            {
                playerStand = true;
            }
            else // Else the input is incorrect
            {
                view.GreenMessage("Input invalid, try again. Write 'hit' or 'stand'");
            }

        }

        void Dealer()
        {
            // Outputs message to the console
            view.RedMessage("Dealers turn");

            // Resets the value of the int playerTotal, because it will be recounted. Creates new int to keep track of aces
            dealerTotal = 0;
            int aces = 0;

            // for loop that runs 5 times, because how many cards we got, due to the five card rule in black jack
            for (int i = 0; i < dealerCards.Length; i++)
            {
                // If the cards arent null
                if (dealerCards[i] != null)
                {
                    // Get first character of string
                    string first = dealerCards[i].Substring(0, 1);

                    // Get values based of the first character in the string
                    if (first == "A")
                    {
                        aces += 1;
                        dealerTotal += 11;
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

                // Goes through the aces, changing their values to 1 from 11, if they cant find in 21
                while (dealerTotal > 21 && aces > 0)
                {
                    dealerTotal -= 10;
                    aces--;
                }

                // Dealer cant hit, if they got 17 or more
                if (dealerTotal >= 17)
                {
                    NewCards("dealer");
                }

                // Hides 1 of the dealers cards and doesnt display nulls
                if (i != 0 && dealerCards[i] != null)
                {
                    view.RedMessage($"Dealer has: {dealerCards[i]}");
                }
                
                // Stores the hidden dealer card
                if (i == 0)
                {
                    dealerSecret = dealerCards[i];
                }
            }

        }

        void NewCards(string turn)
        {
            Random random = new Random();

            // Creates a random value, based on the number of cards
            // Repeats if the card is null
            int rnd;
            do
            {
                rnd = random.Next(cards._cards.Length);
            } while (cards._cards[rnd] == null);

            // if its the dealers turn then change the value of the first null card and break the for loop
            if (turn == "dealer")
            {
                for (int i = 0; i < dealerCards.Length; i++)
                {
                    if (dealerCards[i] == null)
                    {
                        dealerCards[i] = cards._cards[rnd];
                        cards._cards[rnd] = null;
                        break;
                    }
                }
            }
            else
            {
                // else do the same, but for playercards instead of dealercards
                for (int i = 0; i < playerCards.Length; i++)
                {
                    if (playerCards[i] == null)
                    {
                        playerCards[i] = cards._cards[rnd];
                        cards._cards[rnd] = null;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Checks who won the game
        /// </summary>
        void CheckWin()
        {
            if (dealerTotal == 21)
            {
                view.Message("Dealer won! Blackjack.");
                GameOver();
            }
            else if (dealerTotal > 21)
            {
                view.Message("Dealer went bust! Player won.");
                GameOver();
            }
            if (playerTotal == 21)
            {
                view.Message("Player won! Blackjack.");
                GameOver();
            }
            else if (playerTotal > 21)
            {
                view.Message("Player went bust! Dealer won.");
                GameOver();
            }
        }

        /// <summary>
        /// Resets the values to default
        /// </summary>
        void GameOver()
        {
            view.Message($"Dealer secret card was: {dealerSecret}\n");

            dealerStand = false;
            playerStand = false;
            dealerSecret = "";
            playerTotal = 0;
            dealerTotal = 0;

            for (int i = 0; i < 5; i++)
            {
                dealerCards[i] = null;
                playerCards[i] = null;
            }
        }

    }
}
