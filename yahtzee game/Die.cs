using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Yahtzee_Game {
    [Serializable]
    class Die {
        private int faceValue;
        private bool active;
        [NonSerialized] private Label label;
        private static Random random = new Random();
        private static string rollFileName = Game.defaultPath + "\\basictestrolls.txt"; // Debug file
        [NonSerialized] private static StreamReader rollFile = new StreamReader(rollFileName);
        private static bool DEBUG = true;
        private const int NumFaces = 6;

        /// <summary>
        /// Default contrustor for dice
        /// </summary>
        /// <param name="myLabel"> Label representing dice on Gui</param>
        public Die(Label myLabel) {
            label = myLabel;
            faceValue = 1;
            active = true;
        }

        /// <summary>
        /// Method for retrieving die face value
        /// </summary>
        /// <returns> face value of die</returns>
        public int FaceValue() {
            return faceValue;
        }

        /// <summary>
        /// Property indicating if the die can be rolled or not
        /// </summary>
        public bool Active {
            get {
                return active;
            }
            set {
                active = value;
            }
        }

        /// <summary>
        /// Rolls the die
        /// </summary>
        public void Roll() {
            if (!DEBUG) {
               faceValue = random.Next(1, NumFaces + 1);
            } else {
                faceValue = int.Parse(rollFile.ReadLine());
                label.Text = faceValue.ToString();
                label.Refresh();
            }
        }

        public void Load(Label label) {
            this.label = label;
            if (faceValue == 0) {
                label.Text = string.Empty;
            } else {
                label.Text = faceValue.ToString();
            }
        }//end Load
    }
}
