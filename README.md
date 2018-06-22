# Yahtzee
A windows form application for playing Yahtzee

![GUi image](SampleImage.png)

Up to six players can play a game according to the rules of [Yahtzee](http://www.yahtzee.org.uk/rules.html).

Users are able to save & load games to a file, in case players want to continue playing at a later time. 

## Source code structure
This project was designed around the principles of object oriented programming. An executable can be found in the [release folder](https://github.com/jyss88/Yahtzee/tree/master/Release). The source code can be found in the folder [yahtzee game](https://github.com/jyss88/Yahtzee/tree/master/yahtzee%20game).

### Game
The Game class represents the game of Yahtzee itself. It handles:

* Creation of Player objects
* Advancement of player turns
* Updating of GUI messages (player scores, information displays)
* Player interactions with GUI (rolling dice, selecting scores)
* Loading and saving of games

### Die
The Die class represents a die for use in the game of Yahtzee. A dice can be 'rolled', to obtain a random number between 1 and 6. 

Die objects also have an attached Windows Form label, which is automatically updated with the value of the die.

### Player
The Player class represents a player of the game. A Player object contains a name, a grand total score, and an array of Score objects. 

The Player class mainly handles the calculation of an individual player's score. 

### Score
The Score class is an abstract class representing the different score types in the Yahtzee game. 

A Score object computes the individual score value, based on the face value of input dice. Score objects are tied to a 

Score is subclassed to the following:

#### Combination
The Combination class is an abstract subclass of Score, which represents single dice combination. Combination is subclassed to the following:

##### CountingCombination
The CountingCombination class represents a scoring combination that counts the number of a single die value in the five dice. 
In terms of Yahtzee scores, this corresponds to Ones, Twos, Threes, etc.

##### FixedScore
The FixedScore class represents scoring combinations that have a fixed number as a score. This includes a Small Straight, Large Straight, Full House, and Yahtzee.

##### TotalOfDice
The TotalOfDice class represents scoring combinations where the score is the total of the dice face values. This includes 3 of a kind, 4 of a kind, and Chance combinations.

#### BonusOrTotal 
This is a subclass of the Score class. This represents a score which is either a bonus, a sub-total, or total score of a section. This includes the sub total, a 63+ points bonus, totals for section A and B, a Yahtzee bonus, and the grand total score.

## Acknowledgements
This project was submitted as an assessment piece for the subject "Programming Principles" (course code CAB201) at Queensland University of Technology, whereupon it recieved a grade of high distinction. It was completed in collaboration with fellow QUT student [Sandra Finnow](https://github.com/sandrafinow)
