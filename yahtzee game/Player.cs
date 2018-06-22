using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game {
    [Serializable]
    class Player {
        private string name;
        private int combinationsToDo;
        private Score[] scores;
        private int grandTotal;

        /// <summary>
        /// Default constructor for player
        /// </summary>
        /// <param name="myName"> Name of player</param>
        /// <param name="myLabels"> Labels representing the scores of the player </param>
        public Player(string myName, Label[] myLabels) {
            name = myName;
            combinationsToDo = 13;
            InitializeScoreArray(myLabels);
            grandTotal = 0;
        }

        /// <summary>
        /// Method for initializing the scores of a player
        /// </summary>
        /// <param name="myLabels"> Labels representing the scores </param>
        private void InitializeScoreArray(Label[] myLabels) {
            scores = new Score[myLabels.Length];

            foreach (ScoreType i in Enum.GetValues(typeof(ScoreType))) {
                switch (i) {
                    case ScoreType.Ones:
                    case ScoreType.Twos:
                    case ScoreType.Threes:
                    case ScoreType.Fours:
                    case ScoreType.Fives:
                    case ScoreType.Sixes:
                        scores[(int)i] = new CountingCombination(i, myLabels[(int)i]);
                        break;
                    case ScoreType.SmallStraight:
                    case ScoreType.LargeStraight:
                    case ScoreType.FullHouse:
                    case ScoreType.Yahtzee:
                        scores[(int)i] = new FixedScore(i, myLabels[(int)i]);
                        break;
                    case ScoreType.ThreeOfAKind:
                    case ScoreType.FourOfAKind:
                    case ScoreType.Chance:
                        scores[(int)i] = new TotalOfDice(i, myLabels[(int)i]);
                        break;
                    case ScoreType.SubTotal:
                    case ScoreType.BonusFor63Plus:
                    case ScoreType.SectionATotal:
                    case ScoreType.SectionBTotal:
                    case ScoreType.YahtzeeBonus:
                    case ScoreType.GrandTotal:
                        scores[(int)i] = new BonusOrTotal(myLabels[(int)i]);
                        break;

                }
            }

        }

        /// <summary>
        /// Property representing the name of the player
        /// </summary>
        public string Name {
            get {
                return name;
            }
            set {
                name = value;
            }
        }

        /// <summary>
        /// Calculates the score for the specified scoring combination 
        /// given the five face values of the dice
        /// </summary>
        /// <param name="scoreType"></param>
        /// <param name="dieValues"></param>
        public void ScoreCombination(ScoreType scoreType, int[] dieValues) {
            int jokerIndex = 0;
            ((Combination)scores[(int)scoreType]).CalculateScore(dieValues); // scores player
            combinationsToDo--;

            // Applies bonus points if needed
            if (scoreType != ScoreType.Yahtzee && ((Combination)scores[(int)scoreType]).IsYahtzee) {
                if (scores[(int)ScoreType.Yahtzee].Done) {
                    scores[(int)ScoreType.YahtzeeBonus].Points = 100;
                }
            }

            jokerIndex = ((Combination)scores[(int)scoreType]).YahtzeeNumber - 1;

            if (scores[(int)scoreType] is FixedScore && ((Combination)scores[(int)scoreType]).IsYahtzee && scores[jokerIndex].Done ) { // if requirements for Yahtzee joker are met (scoretype is fixed, score is yahtzee
                                                                                                                                       // and required combination is done
                ((FixedScore)scores[(int)scoreType]).PlayYahtzeeJoker();
            }

            // Calculates the totals
            SubTotal();
            TotalUpperSection();
            TotalLowerSection();
            GrandTotal = TotalOfPoints();
        }

        /// <summary>
        /// Calculates sub total
        /// </summary>
        private void SubTotal() {
            int total = 0;
            for (int i = 0; i <= (int)ScoreType.Sixes; i++) {
                total += scores[i].Points;
            }

            scores[(int)ScoreType.SubTotal].Points = total;
        }

        /// <summary>
        /// Totals the upper section of the player.
        /// Also gives a bonus if the number of points is greater than 63
        /// </summary>
        private void TotalUpperSection() {
            if(scores[(int)ScoreType.SubTotal].Points >= 63) {
                scores[(int)ScoreType.BonusFor63Plus].Points = 35;
            }

            scores[(int)ScoreType.SectionATotal].Points = scores[(int)ScoreType.SubTotal].Points + scores[(int)ScoreType.BonusFor63Plus].Points;
        }

        /// <summary>
        /// Totals the lower section
        /// </summary>
        private void TotalLowerSection() {
            int total = 0;
            for (int i = (int) ScoreType.ThreeOfAKind; i <= (int)ScoreType.Yahtzee; i++) {
                total += scores[i].Points;
            }
            scores[(int)ScoreType.SectionBTotal].Points = total + scores[(int)ScoreType.YahtzeeBonus].Points;
        }

        /// <summary>
        /// Totals all points.
        /// Includes the lower total, upper total, and bonuses
        /// </summary>
        /// <returns></returns>
        private int TotalOfPoints() {
            int total = scores[(int)ScoreType.SectionATotal].Points + scores[(int)ScoreType.SectionBTotal].Points;
            scores[(int)ScoreType.GrandTotal].Points = total;
            return total;
        }

        /// <summary>
        /// Property which accesses or modifies the grand total
        /// </summary>
        public int GrandTotal {
            get {
                return grandTotal;
            }
            set {
                grandTotal = value;
            }
        }

        /// <summary>
        /// Method which indicates if the given scoretype is avaliable or not
        /// </summary>
        /// <param name="scoreType"> Scoretype to be checked </param>
        /// <returns></returns>
        public bool IsAvaliable(ScoreType scoreType) {
            return !scores[(int)scoreType].Done;
        }

        /// <summary>
        /// Shows all scores of player
        /// </summary>
        public void ShowScores() {
            for (int i = 0; i <= (int)ScoreType.GrandTotal; i++) {
                scores[i].ShowScore();
            }
        }

        /// <summary>
        /// Determines whether the player has finished or not
        /// </summary>
        /// <returns></returns>
        public bool IsFinished() {
            return combinationsToDo == 0;
        }

        public void Load(Label[] scoreTotals) {
            for (int i = 0; i < scores.Length; i++) {
                scores[i].Load(scoreTotals[i]);
            }
        }//end Load
    }
}

