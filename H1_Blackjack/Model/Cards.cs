using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace H1_Blackjack.Model
{
    /// <summary>
    /// Creates a deck using 2 arrays. 
    /// 1 array has the card values and the other array has the suit
    /// Using 2 foreach statements, the 2 arrays get combined into 52 cards
    /// These cards then gets used in the controller, to play the game
    /// </summary>
    internal struct Cards
    {
        private string[] _cards; 

        public Cards(int deckSize)
        {
            // Creates a string array, the size specified in the controller (52) and calls the CreateCardDeck method
            _cards = new string[deckSize];
            CreateCardDeck();
        }

        /// <summary>
        /// Creates the card deck, based on 2 arrays
        /// </summary>
        private void CreateCardDeck()
        {
            // Creates 2 arrays, which will be used to create unique cards
            string[] cardValues = { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };
            string[] suits = { "Hearts", "Diamonds", "Spades", "Clubs" };

            //Index to keep track of the number of card in the array
            int i = 0;

            // Creates 52 unique cards, based on card value and suit arrays
            foreach (var suit in suits)
            {
                foreach (var value in cardValues)
                {
                    _cards[i] = $"{value} of {suit}";
                    i++;
                }
            }
        }

        // Returns the array of cards
        public string[] GetCards()
        {
            return _cards;
        }
    }

}

