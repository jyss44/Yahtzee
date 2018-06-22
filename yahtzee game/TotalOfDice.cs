using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game {
    [Serializable]
    class TotalOfDice : Combination {

        private int numberOfOneKind;

        /// <summary>
        /// Constructor for TotalOfDice
        /// </summary>
        /// <param name="scoreType"></param>
        /// <param name="myLabel"></param>
        public TotalOfDice(ScoreType scoreType, Label myLabel) : base(myLabel) {
            if (scoreType == ScoreType.Chance) {
                numberOfOneKind = 0;
            } else {
                numberOfOneKind = (int)scoreType - 6;
            }
        }

        /// <summary>
        /// Calculates the score of a given set of die values
        /// </summary>
        /// <param name="dieValues"> values of dice </param>
        public override void CalculateScore(int[] dieValues) {
            Sort(dieValues);
            if (numberOfOneKind == 0) {
                Points = scoreChance(dieValues);

            } else {
                Points = scoreThreeOrMoreOfAKind(dieValues);
            }
            CheckForYahtzee(dieValues);
        }

        /// <summary>
        /// Scores a chance
        /// </summary>
        /// <param name="dieValues"></param>
        /// <returns></returns>
        private int scoreChance(int[] dieValues) {
            return dieValues.Sum();
        }

        /// <summary>
        /// Scores a three or more of a kind
        /// </summary>
        /// <param name="dieValues"></param>
        /// <returns></returns>
        private int scoreThreeOrMoreOfAKind(int[] dieValues) {
            if (IsThreeOrMore(dieValues)) {
                return dieValues.Sum();
            } else {
                return 0;
            }
        }

        /// <summary>
        /// Determines whether or not a given set of die values is is three or more of a kind
        /// </summary>
        /// <param name="dieValues"> set of die values </param>
        /// <returns></returns>
        private bool IsThreeOrMore(int[] dieValues) {
            int count, dieValue;
            for (int i = 0; i < dieValues.Length; i++) { // loops through each die in array
                count = 0;
                dieValue = dieValues[i];
                for (int j = 0; j < dieValues.Length; j++) { // loops through each die and compares them
                    if (dieValues[j] == dieValue) {
                        count++;
                    }

                    if (count >= numberOfOneKind) { // returns true if dieValue occurs the required amount of times
                        return true;
                    }// end if
                } // end for
            } // end for

            return false; // returns false otherwise
        } //end IsThreeOrMore

    }
}
