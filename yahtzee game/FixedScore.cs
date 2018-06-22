using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game {
    [Serializable]
    class FixedScore : Combination {

        private ScoreType scoreType;

        /// <summary>
        /// Default construtor for FixedScore class
        /// </summary>
        /// <param name="myScoreType"> Scoretype of object </param>
        /// <param name="myLabel"> Label representing scoreType </param>
        public FixedScore(ScoreType myScoreType, Label myLabel) : base(myLabel) {
            scoreType = myScoreType;
        }

        /// <summary>
        /// Caluculates score of player
        /// </summary>
        /// <param name="dieValues"></param>
        public override void CalculateScore(int[] dieValues) {
            CheckForYahtzee(dieValues);
            switch (scoreType) {
                case ScoreType.Yahtzee:
                    ScoreYahtzee(dieValues);
                    break;
                case ScoreType.LargeStraight:
                    ScoreLargeStraight(dieValues);
                    break;
                case ScoreType.SmallStraight:
                    ScoreSmallStraight(dieValues);
                    break;
                case ScoreType.FullHouse:
                    ScoreFullHouse(dieValues);
                    break;
            }
        }

        /// <summary>
        /// Scores a player based on Yahtzee rules.
        /// </summary>
        /// <param name="dieValues"> Values of dice </param>
        private void ScoreYahtzee(int[] dieValues) {
            if (isYahtzee) {
                Points = 50;
            } else {
                Points = 0;
            }
        }

        /// <summary>
        /// Scores a player based of Large straight rules
        /// </summary>
        /// <param name="dieValues"> values of dice </param>
        private void ScoreLargeStraight(int[] dieValues) {
            if (thisIsLargeStraight(dieValues)) {
                Points = 40;
            } else {
                Points = 0;
            }
        }

        /// <summary>
        /// Scores a player based on both small straight rules
        /// </summary>
        /// <param name="dieValues"></param>
        private void ScoreSmallStraight(int[] dieValues) {
            if (thisIsSmallStraight(dieValues)) {
                Points = 25;
            } else {
                Points = 0;
            }
        }

        /// <summary>
        /// Scores a player based on fullhouse rules
        /// </summary>
        /// <param name="dieValues"></param>
        private void ScoreFullHouse (int[] dieValues) {
            if (thisIsFullHouse(dieValues) && !isYahtzee) { // will give a score of zero if a Yahtzee is rolled
                Points = 25;
            } else {
                Points = 0;
            }
        }

        /// <summary>
        /// Method for determining whether a set of die values is a large straight.
        /// </summary>
        /// <param name="dieValues"> Values of dice </param>
        /// <returns> True is values is a large straight, false otherwise. </returns>
        private bool thisIsLargeStraight(int[] dieValues) {
            Sort(dieValues);
            for (int i = 0; i < dieValues.Length - 1; i++) {
                if (dieValues[i] != dieValues[i + 1] - 1) {
                    return false;
                }
            }
            return true;
        }

        private int[] removeDuplicates(int[] dieValues) {
            int[] result = dieValues;

            for(int i = 0; i < dieValues.Length; i++) {// loops through each die
                for( int j = i + 1; j < dieValues.Length; j++) { // loops through each subsequent die
                    if(dieValues[i] == dieValues[j]) {
                        result[j] = 0; // set corresponding element in result array to zero
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Method for determining whether a set of die values is a small straight.
        /// </summary>
        /// <param name="dieValues"> Values of dice </param>
        /// <returns> True is values are small straight, false otherwise. </returns>
        private bool thisIsSmallStraight(int[] dieValues) {
            bool isSmallStraight;
            dieValues = removeDuplicates(dieValues);

            // Checks the die values for the two possible cases of a small straight.
            isSmallStraight = SmallStraight(0, dieValues) || SmallStraight(1, dieValues);

            return isSmallStraight;

        }

        /// <summary>
        /// Checks if a set of die values are a small straight.
        /// Does so in reference to a starting position
        /// </summary>
        /// <param name="startPosition"> starting position in array </param>
        /// <param name="dieValues"> values of dice </param>
        /// <returns></returns>
        private bool SmallStraight(int startPosition, int[] dieValues) {
            Sort(dieValues);
            int endPosition = dieValues.Length - (startPosition + 1);

            for (int i = startPosition; i < endPosition; i++) {
                if (dieValues[i] != dieValues[i + 1] - 1) {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Method for determining whether a set of die values is a full house.
        /// </summary>
        /// <param name="dieValues"> Values of dice </param>
        /// <returns> True is values is a full house, false otherwise. </returns>
        private bool thisIsFullHouse(int[] dieValues) {
            Sort(dieValues);
            int arrayLength = dieValues.Length;
            int firstValue = dieValues[0];
            int lastValue = dieValues[arrayLength - 1];
            bool isFullHouse;

            isFullHouse = (dieValues[1] == dieValues[0]) && // Does the 2nd value match the first?
                          (dieValues[3] == dieValues[4]) && // Does the 2nd last value match the last?
                          (dieValues[2] == dieValues[0] || dieValues[2] == dieValues[4]);
                            // Does the middle value match either the first or last value?

            return isFullHouse;
        }


        public void PlayYahtzeeJoker() {
            switch (scoreType) {
                case ScoreType.SmallStraight:
                case ScoreType.FullHouse:
                    Points = 25;
                    break;

                case ScoreType.LargeStraight:
                    Points = 40;
                    break;
            }
        } 

       /* private void CheckJoker() {
            check if yahtzeeNumber > 0;
            check if corresponding ScoreType[i] is done;
            if both true, return true;
        }*/
        
    }
}