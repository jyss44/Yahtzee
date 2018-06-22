using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game {
    [Serializable]
    class CountingCombination : Combination {

        private int dieValue;

        /// <summary>
        /// Default constructor for Counting Combination
        /// </summary>
        /// <param name=""></param>
        /// <param name="myLabel"> Label representin the score object on the gui</param>
        public CountingCombination(ScoreType scoreType, Label myLabel) : base(myLabel) {
            dieValue = (int)scoreType + 1; 
        }

        /// <summary>
        /// Calculates the score based on the rules of Yahtzee
        /// </summary>
        /// <param name="dieValues"> Values of dice </param>
        public override void CalculateScore(int[] dieValues) {
            int count = 0; 
            for (int i = 0; i < dieValues.Length; i++) {
                if (dieValues[i] == dieValue) {
                    count++;
                }
            }
            Points = dieValue * count;
            CheckForYahtzee(dieValues);
        }
    }
}
