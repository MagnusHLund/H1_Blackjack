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
        bool isNewGame = true;

        // Player and dealer can maximum have 5 cards, due to the 5 card rule
        string[] dealerCards = new string[5];
        string[] playerCards = new string[5];

        bool playerStand;
        bool dealerStand;

        int playerTotal;
        int dealerTotal;

        string[] cardsArray;
        View.View view = new View.View();

        /// <summary>
        /// Black jack stars from this method and will repeat forever.
        /// If its a new game, the dealer and the player are assigned 2 cards each.
        /// Once they have been assigned 2 cards in the beginning of the game, this method will just call the Player and Dealer methods, to play their turns.
        /// </summary>
        public void Start()
        {
            // Infinite loop
            while (true)
            {
                // If its a new game, then player and dealer gets 2 card each
                if (isNewGame)
                {
                    // Resets the card deck, so it has no "D" (deleted) cards
                    Cards cards = new Cards(52);
                    cardsArray = cards.GetCards();

                    for (int i = 0; i < 2; i++)
                    {
                        GetCard(true);
                        GetCard(false);
                    }

                    isNewGame = false;
                }

                // Displays the cards of both dealer and player
                ShowCards();

                // If player stands, then the player wont be able to play anymore, this match
                if (!playerStand)
                {
                    Player();
                }

                // If dealer stands, then the dealer cant play this match. Dealer always stands, once they have a total of 17 or above
                if (!dealerStand)
                {
                    // Gets card for the dealer
                    GetCard(false);
                }

                // If both stand, then figure out who is closest to 21
                if (dealerStand && playerStand)
                {
                    int dealerDifference = 0;
                    dealerDifference -= dealerTotal - 21;

                    int playerDifference = 0;
                    playerDifference -= playerTotal - 21;

                    if (playerTotal < dealerTotal)
                    {
                        view.Message($"Dealer is closest to 21 ({dealerTotal}), and won! Player had {playerTotal}");
                    }
                    else if (playerTotal > dealerTotal)
                    {
                        view.Message($"Player is closest to 21 ({playerTotal}), and won! Dealer had {dealerTotal}");
                    }
                    else
                    {
                        view.Message("Draw! Dealer and player has same total!");

                    }

                    // Shows the card of both the player and dealer
                    OutputCards(false);
                    OutputCards(true);

                    // Resets the game
                    Reset();
                }
            }
        }

        /// <summary>
        /// Pulls a random card, gets the total value of cards, based on the first character in the string, which is the card name.
        /// This method also figures out who wins
        /// isPlayerTurn parameter, makes it easy to call the method, for just the player or dealer.
        /// </summary>
        /// <param name="isPlayersTurn"></param>
        public void GetCard(bool isPlayersTurn)
        {
            // Creates a random and that random picks a card which is not "D" (deleted)
            Random random = new Random();
            int rdm;

            do
            {
                rdm = random.Next(cardsArray.Length);
            } while (cardsArray[rdm] == "D");


            // if the parameter is false, when the method is called, then we enter the if statement here
            if (!isPlayersTurn)
            {
                // We take the value from the cardArray and put into the dealerCards array. Then we delete the value from cardsArray, but setting the value to "D" (deleted)
                for (int i = 0; i < dealerCards.Length; i++)
                {
                    if (dealerCards[i] == null)
                    {
                        dealerCards[i] = cardsArray[rdm];

                        cardsArray[rdm] = "D";

                        break;
                    }
                }

                // These 2 variables calculates the total value of the cards
                dealerTotal = 0;
                int aces = 0;

                // Foreach goes through each of the cards that the dealer has
                foreach (string cards in dealerCards)
                {
                    // If the array position is not null then we progress through this if statement
                    if (cards != null)
                    {
                        // We get the first character in the string, which we use to get the dealerTotal value with.
                        string checker = cards.Substring(0, 1).ToLower();

                        if (checker == "a")
                        {
                            dealerTotal += 11;
                            aces++;
                        }
                        else if (checker == "k" || checker == "q" || checker == "j" || checker == "1")
                        {
                            dealerTotal += 10;
                        }
                        else
                        {
                            dealerTotal += byte.Parse(checker);
                        }

                        // Converts aces 11 value, to 1, if the dealerTotal goes beyond 21.
                        while (dealerTotal > 21 && aces > 0)
                        {
                            dealerTotal -= 10;
                            aces--;
                        }

                        // This what happens for the dealer, based on the total value.
                        if (dealerTotal > 21)
                        {
                            OutputCards(false);

                            view.Message("Dealer is bust");
                            Reset();
                        }
                        else if (dealerTotal == 21)
                        {
                            OutputCards(false);

                            view.Message("Dealer has black jack!");
                            Reset();
                        }
                        else if (dealerTotal >= 17) // dealer has to stand, once they reach 17 or above.
                        {
                            view.Message("Dealer stands");
                            dealerStand = true;
                        }
                    }
                }

            }
            else // This is for the player cards
            {
                // Goes through each of the playercards, if a position is null then it gets overwritten by the card value from the cardsArray.
                // Then cardsArray gets a deleted card, with "D" and we break out of the loop.
                for (int i = 0; i < playerCards.Length; i++)
                {
                    if (playerCards[i] == null)
                    {
                        playerCards[i] = cardsArray[rdm];

                        cardsArray[rdm] = "D";

                        break;
                    }
                }

                // Then we calculate the total value of the players cards
                playerTotal = 0;
                int aces = 0;

                // Goes through each card and takes the first character of the cards string
                foreach (string cards in playerCards)
                {
                    if (cards != null)
                    {
                        string checker = cards.Substring(0, 1).ToLower();

                        // Checks what the first character is, and increases the value of playerTotal, with an amount based on which card it is
                        if (checker == "a")
                        {
                            playerTotal += 11;
                            aces++;
                        }
                        else if (checker == "k" || checker == "q" || checker == "j" || checker == "1")
                        {
                            playerTotal += 10;
                        }
                        else
                        {
                            playerTotal += byte.Parse(checker);
                        }

                        // Converts 11 value aces to 1 value aces, if playerTotal is above 21
                        while (playerTotal > 21 && aces > 0)
                        {
                            playerTotal -= 10;
                            aces--;
                        }

                        // Determines if the total player value is a bust or a win
                        if (playerTotal > 21)
                        {
                            for (int i = playerCards.Length - 1; i >= 0; i--)
                            {
                                if (playerCards[i] != null)
                                {
                                    OutputCards(true);
                                    break;
                                }
                            }

                            view.Message("Player is bust");
                            Reset();
                        }
                        else if (playerTotal == 21)
                        {
                            for (int i = playerCards.Length - 1; i >= 0; i--)
                            {
                                if (playerCards[i] != null)
                                {
                                    OutputCards(true);
                                    break;
                                }
                            }

                            view.Message("Player has black jack!");
                            Reset();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets player input, for player decisions
        /// </summary>
        void Player()
        {
            view.Message("Do you wanna hit or stand?");

            // Creates a string which gets the player input in lowercase.
            string input;

            while (true)
            {
                input = Console.ReadLine().ToLower();

                // If the string is "hit" then we get a new card.
                // If its "stand" then the player cant make more decisions, this match.
                if (input == "hit")
                {
                    GetCard(true);
                    break;
                }
                else if (input == "stand")
                {
                    playerStand = true;
                    break;
                }
                else // If its not "hit" or "stand" then its an error and gets an error message
                {
                    view.Message("Input is invalid. Write 'hit' or 'stand'");
                }
            }

        }

        /// <summary>
        /// Displays the cards in the console, excluding null value cards
        /// </summary>
        void ShowCards()
        {
            for (int i = 0; i < dealerCards.Length; i++)
            {
                if (dealerCards[i] != null && i != 0)
                {
                    view.RedMessage($"Dealer cards: {dealerCards[i]}");
                }
            }

            for (int i = 0; i < playerCards.Length; i++)
            {
                if (playerCards[i] != null)
                {
                    view.GreenMessage($"Player cards: {playerCards[i]}");
                }
            }
        }

        /// <summary>
        /// Resets the game, to start a new match.
        /// User has to press enter to play again.
        /// </summary>
        void Reset()
        {
            view.RedMessage($"Dealer secret card was: {dealerCards[0]}");
            view.UserInput("Press enter to try again");

            isNewGame = true;

            for (int i = 0; i < dealerCards.Length; i++)
            {
                dealerCards[i] = null;
                playerCards[i] = null;
            }

            playerStand = false;
            dealerStand = false;
        }

        /// <summary>
        /// This outputs the cards in play to the console
        /// </summary>
        /// <param name="isPlayer"></param>
        void OutputCards(bool isPlayer)
        {
            if (isPlayer)
            {
                for (int i = playerCards.Length - 1; i >= 0; i--)
                {
                    if (playerCards[i] != null)
                    {
                        view.GreenMessage($"Player cards: {playerCards[i]}");
                    }
                }
            }
            else
            {
                for (int i = 0; i < dealerCards.Length; i++)
                {
                    if (dealerCards[i] != null)
                    {
                        view.RedMessage($"Dealer cards: {dealerCards[i]}");
                        break;
                    }
                }
            }
        }
    }
}
