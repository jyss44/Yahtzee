using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game {
    [Serializable]
    class BonusOrTotal : Score {


        /// <summary>
        /// Constructor that calls parent's/Score's constructor 
        /// </summary>
        /// <param name="myLabel"> Label of score </param>
        public BonusOrTotal(Label myLabel) : base (myLabel) {
        }
    }
}
