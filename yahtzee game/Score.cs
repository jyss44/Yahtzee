using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game {
    [Serializable]
    abstract class Score {
        private int points;
        [NonSerialized] private Label label;
        protected bool done;

        /// <summary>
        /// Default constructor for score
        /// </summary>
        /// <param name="myLabel"> Label of score </param>
        public Score( Label myLabel ) {
            label = myLabel;
            points = 0;
            done = false;
        }

        /// <summary>
        /// Property for accessing and mutating points
        /// </summary>
        public int Points {
            get {
                return points;
            }
            set {
                points = value;
                done = true;
                label.Text = points.ToString(); 
            }
        }

        /// <summary>
        /// Property for accessing done
        /// </summary>
        public bool Done {
            get {
                return done;
            }
        }

        /// <summary>
        /// Shows score in relevant label
        /// </summary>
        public void ShowScore() {
            if(done) {
                label.Text = points.ToString();
            } else {
                label.Text = " ";
            }
        }

        public void Load(Label label) {
            this.label = label;
        } //end Load
    }
}
