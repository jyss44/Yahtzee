using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game {
    public partial class Yahtzee : Form {

        private Label[] diceLabels = new Label[Game.Num_Die]; // Array of dice

        private Button[] scoreButtons = new Button[(int)ScoreType.Yahtzee + 1]; // Array of score buttons

        private Label[] scoreTotals = new Label[(int)ScoreType.GrandTotal + 1]; // array of score labels

        private CheckBox[] checkBoxes = new CheckBox[Game.Num_Die]; // array of check buttons

        private Game game;

        private int NumericUpDownValue; // value for remembering the previous value of numericUpDown

        public Yahtzee() {
            InitializeComponent();
            InitializeLabelsAndButtons();
            StartNewGame();
            NumericUpDownValue = 2;
            dataGridView1.RowHeadersVisible = false;
           // UpdatePlayersDataGridView();
        }

        /// <summary>
        /// Initialized Die labels, score labels, score buttons, and check boxes
        /// </summary>
        private void InitializeLabelsAndButtons() {
            InitializeDieLabels();
            InitializeScoreLabels();
            InitializeScoreButtons();
            InitializeCheckBoxes();
        }

        /// <summary>
        /// Initialises dice.
        /// Assigns relevant dice to their corresponding element in the dice array.
        /// </summary>
        private void InitializeDieLabels() {
            diceLabels[0] = die1;
            diceLabels[1] = die2;
            diceLabels[2] = die3;
            diceLabels[3] = die4;
            diceLabels[4] = die5;
        }

        /// <summary>
        /// Initialises score labels.
        /// Assigned relavant score labels to their corresponding element in the scoreLabel array.
        /// </summary>
        private void InitializeScoreLabels() {
            scoreTotals[(int)ScoreType.Ones] = scoreLabel1;
            scoreTotals[(int)ScoreType.Twos] = scoreLabel2;
            scoreTotals[(int)ScoreType.Threes] = scoreLabel3;
            scoreTotals[(int)ScoreType.Fours] = scoreLabel4;
            scoreTotals[(int)ScoreType.Fives] = scoreLabel5;
            scoreTotals[(int)ScoreType.Sixes] = scoreLabel6;
            scoreTotals[(int)ScoreType.ThreeOfAKind] = scoreLabel7;
            scoreTotals[(int)ScoreType.FourOfAKind] = scoreLabel8;
            scoreTotals[(int)ScoreType.FullHouse] = scoreLabel9;
            scoreTotals[(int)ScoreType.SmallStraight] = scoreLabel10;
            scoreTotals[(int)ScoreType.LargeStraight] = scoreLabel11;
            scoreTotals[(int)ScoreType.Chance] = scoreLabel12;
            scoreTotals[(int)ScoreType.Yahtzee] = scoreLabel13;

            scoreTotals[(int)ScoreType.SubTotal] = subtotalDisplay;
            scoreTotals[(int)ScoreType.BonusFor63Plus] = bonusDisplay;
            scoreTotals[(int)ScoreType.SectionATotal] = upperTotalDisplay;
            scoreTotals[(int)ScoreType.YahtzeeBonus] = yahtzeeBonusDisplay;
            scoreTotals[(int)ScoreType.SectionBTotal] = lowerTotalDisplay;
            scoreTotals[(int)ScoreType.GrandTotal] = grandTotalDisplay;
        }

        /// <summary>
        /// Initializes score buttons.
        /// Assigns relavant buttons to their corresponding element in scoreButtons array.
        /// </summary>
        private void InitializeScoreButtons() {
            scoreButtons[(int)ScoreType.Ones] = button1;
            scoreButtons[(int)ScoreType.Twos] = button2;
            scoreButtons[(int)ScoreType.Threes] = button3;
            scoreButtons[(int)ScoreType.Fours] = button4;
            scoreButtons[(int)ScoreType.Fives] = button5;
            scoreButtons[(int)ScoreType.Sixes] = button6;
            scoreButtons[(int)ScoreType.ThreeOfAKind] = button7;
            scoreButtons[(int)ScoreType.FourOfAKind] = button8;
            scoreButtons[(int)ScoreType.FullHouse] = button9;
            scoreButtons[(int)ScoreType.SmallStraight] = button10;
            scoreButtons[(int)ScoreType.LargeStraight] = button11;
            scoreButtons[(int)ScoreType.Chance] = button12;
            scoreButtons[(int)ScoreType.Yahtzee] = button13;
        }

        /// <summary>
        /// Initializes check boxes
        /// Assigns relavent die check boxes to their corresponding element in checkBoxes array
        /// </summary>
        private void InitializeCheckBoxes() {
            checkBoxes[0] = die1Check;
            checkBoxes[1] = die2Check;
            checkBoxes[2] = die3Check;
            checkBoxes[3] = die4Check;
            checkBoxes[4] = die5check;
        }

        /// <summary>
        /// Method for retrieving dice array
        /// </summary>
        /// <returns> Label array of dice</returns>
        public Label[] GetDice() {
            return diceLabels;
        }

        /// <summary>
        /// Method for retrieving score array
        /// </summary>
        /// <returns> Label array of scores</returns>
        public Label[] GetScoresTotals() {
            return scoreTotals;
        }

        /// <summary>
        /// Updates playerLabel to current player name
        /// </summary>
        /// <param name="name"></param>
        public void ShowPlayerName(string name) {
            playerLabel.Text = name; // Sets player label text to name
        }

        /// <summary>
        /// Clears all text in diceLabels
        /// </summary>
        private void ClearDiceLabels() {
            for(int i = 0; i < Game.Num_Die; i++) {
                diceLabels[i].Text = " ";
            }
        }

        /// <summary>
        /// Enables rollButton
        /// </summary>
        public void EnableRollButton() {
            rollButton.Enabled = true;
        }

        /// <summary>
        /// Disables rollButton
        /// </summary>
        public void DisableRollButton() {
            rollButton.Enabled = false;
        }

        /// <summary>
        /// Enables all die check boxes
        /// </summary>
        public void EnableCheckBoxes() {
            for (int i = 0; i < Game.Num_Die; i++) {
                checkBoxes[i].Enabled = true;
            }
        }

        /// <summary>
        /// Disables all die check boxes
        /// </summary>
        public void DisableaAndClearCheckboxes() {
            for (int i = 0; i < Game.Num_Die; i++) {
                checkBoxes[i].Checked = false;
                checkBoxes[i].Enabled = false;
            }
        }

        /// <summary>
        /// Enables specified score button
        /// </summary>
        /// <param name="combo"> Which ScoreType to enable </param>
        public void EnableScoreButton(ScoreType combo) {
            if(scoreButtons[(int)combo] != null) {
                scoreButtons[(int)combo].Enabled = true;
            } 
        }

        /// <summary>
        /// Disable specified score button
        /// </summary>
        /// <param name="combo"> Specified score button </param>
        public void DisableScoreButton(ScoreType combo) {
            if (scoreButtons[(int)combo] != null ){
                scoreButtons[(int)combo].Enabled = false;
            }
        }

        /// <summary>
        /// Disables all score buttons
        /// </summary>
        private void DisableAllScoreButtons() { 
            for (int i = 0; i < (int)ScoreType.Yahtzee + 1; i++) {
                DisableScoreButton((ScoreType)i);
            }
        }

        /// <summary>
        /// Checks relavent check box in checkBox array
        /// </summary>
        /// <param name="index"> Specified checkbox </param>
        public void CheckCheckBox(int index) {
            checkBoxes[index].Checked = true;
        }

        /// <summary>
        /// Updates messageLabel with a message.
        /// </summary>
        /// <param name="message"> String containing message </param>
        public void ShowMessage(string message) {
            messageLabel.Text = message; 
        }

        /// <summary>
        /// Makes the okButton visable
        /// </summary>
        public void ShowOKButton() {
            okButton.Visible = true;
        }

        /// <summary>
        /// Hides okButton
        /// </summary>
        private void HideOKButton() {
            okButton.Visible = false;
        }

        /// <summary>
        /// Enables and disables relavent buttons for reset.
        /// Also updates roll message.
        /// Does not update player name.
        /// </summary>
        private void Reset() {
            DisableaAndClearCheckboxes();
            DisableAllScoreButtons();
            HideOKButton();
            EnableRollButton();
            ClearDiceLabels();
        }

        /// <summary>
        /// Creates new instance of Game.
        /// </summary>
        public void StartNewGame() {
            game = new Game(this);
            playerBindingSource.DataSource = game.Players;
            Reset();
            EnableNumericUpDown();
            ShowPlayerName(game.GetPlayerName(1));
            ResetLabels();
        }

        /// <summary>
        /// Event handler for New Game button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            StartNewGame();
            EnableSave();
            DisableLoad();
        }

        /// <summary>
        /// Event handler for rollbutton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rollButton_Click(object sender, EventArgs e) {
            game.RollDice();
            EnableCheckBoxes();
            DisableNumericUpDown();
            DisableLoad();
            DisableSave();
        }

        /// <summary>
        /// event handler for die checking
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dieCheck_CheckedChanged(object sender, EventArgs e) {
            // Search for die index
            int dieIndex;
            for(dieIndex = 0; dieIndex < Game.Num_Die; dieIndex ++) {
                if (checkBoxes[dieIndex] == sender) {
                    break;
                }
            }

            if( checkBoxes[dieIndex].Checked == true ) {
                game.HoldDie(dieIndex);
            } else {
                game.ReleaseDie(dieIndex);
            }

        }

        /// <summary>
        /// Event handler for clicking score buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scoreButton_Click(object sender, EventArgs e) {
            int buttonIndex;
            // Searches for button index
            for (buttonIndex = 0; buttonIndex < (int)ScoreType.Yahtzee + 1; buttonIndex++) {
                if (scoreButtons[buttonIndex] != null && scoreButtons[buttonIndex] == sender) {
                    break;
                }
            }
            game.ScoreCombination((ScoreType)buttonIndex); // scores combination
            DisableRollButton();
            DisableAllScoreButtons();
            UpdatePlayersDataGridView();
        }

        /// <summary>
        /// Event handler for ok button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, EventArgs e) {
            game.NextTurn();
            Reset();
            EnableSave();
        }

        /// <summary>
        /// Some shit with data grid view
        /// </summary>
        public void DataGridViewForm() {
            InitializeComponent();
            playerBindingSource.DataSource = game.Players;
        } 

        /// <summary>
        /// Event handler for number of players
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericUpDown1_ValueChanged(object sender, EventArgs e) {
            int difference = NumericUpDownValue - (int)numericUpDown1.Value; // calculates difference in new value vs old

            if (difference < 0) {
                string playerName = "Player " + (game.NumPlayers + 1).ToString();
                game.AddPlayer(new Player(playerName, scoreTotals)); // adds player to game

            } else {
                BindingList < Player > temp = game.Players;
                game.RemovePlayer(temp[game.numPlayers-1]); //removes player from game
            }

            NumericUpDownValue = (int) numericUpDown1.Value; // remembers previous value
        }

        /// <summary>
        /// Event handler for save button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            game.Save();
        }

        /// <summary>
        /// Event handler for load button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadToolStripMenuItem_Click(object sender, EventArgs e) {
            game = Game.Load(this);
            playerBindingSource.DataSource = game.Players;
            UpdatePlayersDataGridView();
        }

        /// <summary>
        /// Updates player data
        /// </summary>
        private void UpdatePlayersDataGridView() {
            game.Players.ResetBindings();
        }

        /// <summary>
        /// Resets all labels to blank
        /// </summary>
        private void ResetLabels() {
            for (int i = 0; i < (int)ScoreType.GrandTotal; i++) {
                scoreTotals[i].Text = " ";
            }
        }

        /// <summary>
        /// Enables save button
        /// </summary>
        private void EnableSave() {
            saveToolStripMenuItem.Enabled = true;
        }

        /// <summary>
        /// Enables load button
        /// </summary>
        private void DisableSave() {
            saveToolStripMenuItem.Enabled = false;
        }

        /// <summary>
        /// Disables load button
        /// </summary>
        private void DisableLoad() {
            loadToolStripMenuItem.Enabled = false;
        }

        private void EnableNumericUpDown() {
            numericUpDown1.Enabled = true;
        }

        /// <summary>
        /// Disables numeric updown
        /// </summary>
        private void DisableNumericUpDown() {
            numericUpDown1.Enabled = false;
        }
    }

}

