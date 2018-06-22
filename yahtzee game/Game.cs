using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Yahtzee_Game {

    public enum ScoreType {
        Ones, Twos, Threes, Fours, Fives, Sixes,
        SubTotal, BonusFor63Plus, SectionATotal,
        ThreeOfAKind, FourOfAKind, FullHouse,
        SmallStraight, LargeStraight, Chance, Yahtzee,
        YahtzeeBonus, SectionBTotal, GrandTotal
    }
    [Serializable]
    class Game {
        public const int Num_Die = 5;
        public int numPlayers; // this is going to change
        private BindingList<Player> players;
        private int currentPlayerIndex;
        private Player currentPlayer;
        private Die[] dice;
        private int playersFinished;
        private int numRolls;
        [NonSerialized]
        private Yahtzee form;
        [NonSerialized]
        private Label[] dieLabels;
        public static string defaultPath = Environment.CurrentDirectory;
        private static string savedGameFile = defaultPath + "\\YahtzeeGame.dat";

        private string[] messages = {
            "Roll 1",
            "Roll 2 or choose a combination to score",
            "Roll 3 or choose a combination to score",
            "Choose a combination to score"
        };

        /// <summary>
        /// Constructor for Game
        /// </summary>
        /// <param name="myForm"> Application form </param>
        public Game(Yahtzee myForm) {
            numPlayers = 2;
            form = myForm;
            currentPlayerIndex = 0;
            players = new BindingList<Player>();
            for (int i = 0; i < numPlayers; i++) {
                players.Add(new Player("Player " + (i + 1), form.GetScoresTotals()));
            }

            currentPlayer = players[currentPlayerIndex];
            dice = new Die[Num_Die];

            dieLabels = form.GetDice();
            for (int i = 0; i < Num_Die; i++) {
                dice[i] = new Die(dieLabels[i]);
            }

            playersFinished = 0;
            numRolls = 0;
        }

        /// <summary>
        /// Advances turn of player
        /// Updates player name, and enables and disables all relevant buttons
        /// </summary>
        public void NextTurn() {
            // Change player, and update label
            PlayersFinished();
            EndGame();
            currentPlayerIndex = (currentPlayerIndex + 1) % numPlayers;
            currentPlayer = players[currentPlayerIndex];
            form.ShowPlayerName(currentPlayer.Name);

            // Shows player's score
            players[currentPlayerIndex].ShowScores();

            // Reset numRolls, and update message label
            numRolls = 0;
            UpdateRollMessage();

        }

        /// <summary>
        /// Rolls all dice, and updates their value into the form window.
        /// Enables and disables score buttons as nessecary. 
        /// </summary>
        public void RollDice() {
            // roll each die
            for (int i = 0; i < Num_Die; i++) {
                if (dice[i].Active) {
                    dice[i].Roll();
                }
                form.GetDice()[i].Text = dice[i].FaceValue().ToString(); // updates dice label
            }

            // Enable Score buttons
            EnableAllScoreButtons();

            numRolls++; // increment numRolls

            UpdateRollMessage(); // update message label

            // update message label
            form.ShowMessage(messages[numRolls]);

            // Disable roll button if neccesary
            if (numRolls == 3) {
                form.DisableRollButton();
            }
        }

        /// <summary>
        /// Enables all score buttons that have not already been done.
        /// </summary>
        private void EnableAllScoreButtons() {
            for (int i = 0; i < (int)ScoreType.Yahtzee + 1; i++) {
                if (currentPlayer.IsAvaliable((ScoreType)i)) {
                    form.EnableScoreButton((ScoreType)i);
                }
            }
        }

        /// <summary>
        /// Updates message based on number of rolls
        /// </summary>
        private void UpdateRollMessage() {
            form.ShowMessage(messages[numRolls]);
        }

        /// <summary>
        /// Method for holding die. 
        /// </summary>
        /// <param name="dieNo"></param>
        public void HoldDie(int dieNo) {
            dice[dieNo].Active = false;
        }

        /// <summary>
        /// Method for releasing die
        /// </summary>
        /// <param name="dieNo"></param>
        public void ReleaseDie(int dieNo) {
            dice[dieNo].Active = true;
        }

        /// <summary>
        /// Scores the player, based on the scoretype chosen.
        /// </summary>
        /// <param name="score"> Type of score to score </param>
        public void ScoreCombination(ScoreType score) {
            players[currentPlayerIndex].ScoreCombination(score, GetDiceValue(dice));
            form.ShowOKButton();
        }

        private int[] GetDiceValue(Die[] dice) {
            int[] results = new int[dice.Length];

            for (int i = 0; i < dice.Length; i++) {
                results[i] = dice[i].FaceValue();
            }

            return results;
        }

        /// <summary>
        /// Returns player name
        /// </summary>
        /// <param name="playerNum"> Index number of player </param>
        /// <returns></returns>
        public string GetPlayerName(int playerNum) {
            return players[playerNum - 1].Name;
        }

        public BindingList<Player> Players {
            get {
                return players;
            }
        }

        public int NumPlayers {
            get {
                return numPlayers;
            }
            set {
                numPlayers = value;
            }
        }

        public void AddPlayer(Player player) {
            players.Add(player);
            numPlayers++;
        }

        public void RemovePlayer(Player player) {
            players.Remove(player);
            numPlayers--;
        }

        /// <summary>
        /// Load a saved game from the default save game file
        /// </summary>
        /// <param name="form">the GUI form</param>
        /// <returns>the saved game</returns>
        public static Game Load(Yahtzee form) {
            Game game = null;
            if (File.Exists(savedGameFile)) {
                try {
                    Stream bStream = File.Open(savedGameFile, FileMode.Open);
                    BinaryFormatter bFormatter = new BinaryFormatter();
                    game = (Game)bFormatter.Deserialize(bStream);
                    bStream.Close();
                    game.form = form;
                    game.ContinueGame();
                    return game;
                } catch {
                    MessageBox.Show("Error reading saved game file.\nCannot load saved game.");
                }
            } else {
                MessageBox.Show("No current saved game.");
            }
            return null;
        }

        /// <summary>
        /// Save the current game to the default save file
        /// </summary>
        public void Save() {
            try {
                Stream bStream = File.Open(savedGameFile, FileMode.Create);
                BinaryFormatter bFormatter = new BinaryFormatter();
                bFormatter.Serialize(bStream, this);
                bStream.Close();
                MessageBox.Show("Game saved");
            } catch (Exception) {

                //   MessageBox.Show(e.ToString());
                MessageBox.Show("Error saving game.\nNo game saved.");
            }
        }

        /// <summary>
        /// Continue the game after loading a saved game
        /// 
        /// Assumes game was saved at the start of a player's turn before they had rolled dice.
        /// </summary>
        private void ContinueGame() {
            LoadLabels(form);
            for (int i = 0; i < dice.Length; i++) {
                dice[i].Active = true;
            }

            form.ShowPlayerName(currentPlayer.Name);
            form.EnableRollButton();
            form.EnableCheckBoxes();
            // can replace string with whatever you used
            form.ShowMessage("Roll 1");
            currentPlayer.ShowScores();
        }//end ContinueGame

        /// <summary>
        /// Link the labels on the GUI form to the dice and players
        /// </summary>
        /// <param name="form"></param>
        private void LoadLabels(Yahtzee form) {
            Label[] diceLabels = form.GetDice();
            for (int i = 0; i < dice.Length; i++) {
                dice[i].Load(diceLabels[i]);
            }
            for (int i = 0; i < players.Count; i++) {
                players[i].Load(form.GetScoresTotals());
            }

        }

        private void PlayersFinished() {
            if (currentPlayer.IsFinished() == true) {
                playersFinished++;
            }
        }

        private void EndGame() {
            if (playersFinished == numPlayers) {

                int playerIndex;
                int[] playerScores = new int[numPlayers];
                int i = 0;
                string[] winners = new string[numPlayers];

                for (playerIndex = 0; playerIndex < numPlayers; playerIndex++) { //loop through all players
                    playerScores[playerIndex] = players[playerIndex].GrandTotal; //add all scores into the empty array
                }
                Array.Sort(playerScores); //sort the player scores
                int highestScore = playerScores[numPlayers - 1]; //highest score will be last value of the player scores array

                for (playerIndex = 0; playerIndex < numPlayers; playerIndex++) { //loop through players
                    if (players[playerIndex].GrandTotal == highestScore) {
                        winners[i] = players[playerIndex].Name; //add player name to winners array
                        i += 1; //increment position in array so previous player's name does not get overwritten
                    }
                }

                //convert the array input values to string values to annouce in mesagebox
                string toDisplay = string.Join(Environment.NewLine, winners); 
                if (MessageBox.Show("The game is now over and the winner is: /n" + toDisplay + ". Would you like to play again?",
                    "The game is now over", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                    form.StartNewGame();
                }
            }
        }
    }
}
