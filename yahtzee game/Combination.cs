using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game {
    [Serializable]
    abstract class Combination : Score {

        protected bool isYahtzee;
        protected int yahtzeeNumber;

        /// <summary>
        /// Constructor, passes Label parameter to parent constructor
        /// where Label object represents a Score on the GUI
        /// </summary>
        /// <param name="myLabel"></param>
        public Combination(Label myLabel) : base(myLabel) {
            //   isYahtzee = false;
            //   yahtzeeNumber = 0;
        }

        /// <summary>
        /// Abstract method for calculating score of player
        /// </summary>
        /// <param name="dieValues"> Values of dice</param>
        abstract public void CalculateScore(int[] dieValues);

        /// <summary>
        /// Service method for sorting arrays
        /// </summary>
        /// <param name="myArray"> array of integers to be sorted </param>
        public void Sort(int[] myArray) {
            Array.Sort(myArray);
        }

        public bool IsYahtzee {
            get {
                return isYahtzee;
            }
            set {
                isYahtzee = value;
            }
        }

        public int YahtzeeNumber {
            get {
                return yahtzeeNumber;
            }
            set {
                yahtzeeNumber = value;
            }
        }

        public void CheckForYahtzee(int[] dieValues) {

            Sort(dieValues);
            int reference = dieValues[0]; // first value of die
            bool isEqual = false;

            for (int i = 1; i < dieValues.Length; i++) { // loops through each die and compares value to reference
                isEqual = dieValues[i] == reference;
            }

            if (isEqual) {
                isYahtzee = true;
                yahtzeeNumber = dieValues[0];
            }
        }
    }
}
