using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Hangman
{
    public partial class Form1 : Form
    {
        private int wrongGuesses = 0;
        private string[] words;
        private string[] hints;
        private string[] nicknames;
        private int[] scores;
        private string currentWord     = "";
        private string currentWordCopy = "";
        private int currentScore = 0;

        private Bitmap[] hangmanAllImages = {Hangman.Properties.Resources.Hangman_0,
                                             Hangman.Properties.Resources.Hangman_1,
                                             Hangman.Properties.Resources.Hangman_2,
                                             Hangman.Properties.Resources.Hangman_3,
                                             Hangman.Properties.Resources.Hangman_4,
                                             Hangman.Properties.Resources.Hangman_5,
                                             Hangman.Properties.Resources.Hangman_6};

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadWordsFile();
            setUpWordChoice();
        }




        private void loadWordsFile()
        {
            string[] allLines = File.ReadAllLines("Words.txt");

            words = new string[allLines.Length];
            hints = new string[allLines.Length];

            int index = 0;
            foreach (string currentLine in allLines)
            {
                string[] column = currentLine.Split(',');
                words[index] = column[0];
                hints[index] = column[1];
                index++;
            }

        }

        private void loadScoreBoardFile()
        {
            File.AppendAllText("ScoreBoard.txt", "");  //creates the file if it doesn't exist
            string[] allLines = File.ReadAllLines("ScoreBoard.txt");

            nicknames = new string[allLines.Length];
            scores    = new int   [allLines.Length];

            int index = 0;
            foreach (string currentLine in allLines)
            {
                string[] column  = currentLine.Split(',');
                nicknames[index] = column[0];
                scores[index]    = Convert.ToInt32(column[1]);
                index++;
            }

        }

        private int getPlayerIndex(string name)
        {
            int index;

            loadScoreBoardFile();
            string[] allLines = File.ReadAllLines("ScoreBoard.txt");

            for (int i = 0; i < allLines.Length; i++)
            {
                if (name == nicknames[i]) //old player index
                {
                    index = i;
                    return index;
                }
            }

            index = -1;  //new player
            return index;
        }

        private void updateCurrentPlayerScoreInFile()
        {
            int playerIndex     = getPlayerIndex(NicknameTextBox.Text);
            scores[playerIndex] = currentScore;
            string[] allLines   = File.ReadAllLines("ScoreBoard.txt");

            //remove old data
            File.WriteAllText("ScoreBoard.txt", "");

            //export the new data
            for (int i = 0; i < allLines.Length; i++)
            {
                File.AppendAllText("ScoreBoard.txt", nicknames[i] + "," + scores[i] + Environment.NewLine);
            }
        }

        private void resetAll()
        {
            //reset trials and images
            wrongGuesses = 0;
            PictureBox.Image = hangmanAllImages[wrongGuesses];

            //reset words and hints
            currentWord           = "";
            currentWordCopy       = "";
            wordPreviewLabel.Text = "";
            hintLabel.Text        = "";

            //reseting buttons
            ButtonA.Enabled = true;
            ButtonB.Enabled = true;
            ButtonC.Enabled = true;
            ButtonD.Enabled = true;
            ButtonE.Enabled = true;
            ButtonF.Enabled = true;
            ButtonG.Enabled = true;
            ButtonH.Enabled = true;
            ButtonI.Enabled = true;
            ButtonJ.Enabled = true;
            ButtonK.Enabled = true;
            ButtonL.Enabled = true;
            ButtonM.Enabled = true;
            ButtonN.Enabled = true;
            ButtonO.Enabled = true;
            ButtonP.Enabled = true;
            ButtonQ.Enabled = true;
            ButtonR.Enabled = true;
            ButtonS.Enabled = true;
            ButtonT.Enabled = true;
            ButtonU.Enabled = true;
            ButtonV.Enabled = true;
            ButtonW.Enabled = true;
            ButtonX.Enabled = true;
            ButtonY.Enabled = true;
            ButtonZ.Enabled = true;
            NextLevelButton.Enabled = false;
        }

        private void setUpWordChoice()
        {
            //reseting all data
            resetAll();

            //picking up a random word from "words array"
            Random rnd   = new Random();
            int rndIndex = rnd.Next(0, words.Length);
            currentWord  = words[rndIndex];

            //display its Hint
            hintLabel.Text = "HINT: " + hints[rndIndex];

            //initialzing the currentWordCopy with blank spaces
            //according to number of letters in the original word
            for (int index = 0; index < currentWord.Length; index++)
            {
                currentWordCopy += "_";
            }
            displayWord();
        }

        private void displayWord()
        {
            wordPreviewLabel.Text = "";
            for (int index = 0; index < currentWord.Length; index++)
            {
                wordPreviewLabel.Text += currentWordCopy.Substring(index, 1);
                if (index != currentWord.Length - 1)
                    wordPreviewLabel.Text += " ";
            }

        }

        private void updateCopy(char letter)
        {

            char[] currentWordTEMP     = currentWord.ToCharArray();
            char[] currentWordCopyTEMP = currentWordCopy.ToCharArray();

            for (int index = 0; index < currentWordTEMP.Length; index++)
            {
                if (letter == currentWordTEMP[index])
                    currentWordCopyTEMP[index] = letter;
            }

            currentWordCopy = new string(currentWordCopyTEMP);

        }

        private void displayScoreBoardData()
        {
            string[] allLines = File.ReadAllLines("ScoreBoard.txt");

            AllNicknamesLabel.Text += "\n";
            AllScoresLabel.   Text += "\n"; 

            for (int i = 0; i < allLines.Length; i++)
            {
                AllNicknamesLabel.Text += nicknames[i] + "\n";
                AllScoresLabel.   Text += scores   [i] + "\n";     
            }
        }




        //Panels
        private void showGamePanel()
        {
            GamePanel.Visible       = true;
            NicknamePanel.Visible   = false;
            MainMenuPanel.Visible   = false;
            ScoreBoardPanel.Visible = false;
        }

        private void showMainMenuPanel()
        {
            MainMenuPanel.Visible   = true;
            GamePanel.Visible       = false;
            NicknamePanel.Visible   = false;
            ScoreBoardPanel.Visible = false;
        }

        private void showNicknamePanel()
        {
            NicknamePanel.Visible   = true;
            GamePanel.Visible       = false;
            MainMenuPanel.Visible   = false;
            ScoreBoardPanel.Visible = false;
        }

        private void showScoreBoardPanel()
        {
            ScoreBoardPanel.Visible = true;
            GamePanel.Visible       = false;
            NicknamePanel.Visible   = false;
            MainMenuPanel.Visible   = false;
        }



        //Buttons Events
        private void guessClick(object sender, EventArgs e)
        {
            Button choice      = sender as Button;
            choice.Enabled     = false;
            char enteredLetter = choice.Text[0];

            if (currentWord.Contains(enteredLetter))
            {
                updateCopy(enteredLetter);
                displayWord();
                currentScore   += 5;
                ScoreLabel.Text = "";
                ScoreLabel.Text = currentScore.ToString();
                updateCurrentPlayerScoreInFile();
            }
            else
                wrongGuesses++;

            if (currentWordCopy.Equals(currentWord))
            {
                currentScore           += 50;
                wordPreviewLabel.Text   = "";
                wordPreviewLabel.Text   = "You Won!";
                ScoreLabel.Text         = "";
                ScoreLabel.Text         = currentScore.ToString();
                NextLevelButton.Enabled = true;
                updateCurrentPlayerScoreInFile();
            }
            else
            {
                if (wrongGuesses < 7)
                    PictureBox.Image = hangmanAllImages[wrongGuesses];
                else
                {
                    wordPreviewLabel.Text = "";
                    wordPreviewLabel.Text = "Game Over!";
                }
            }

        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            showNicknamePanel();
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(NicknameTextBox.Text))
                NicknameLabel.Text = "Name Not Valid!";
            else
            {
                showGamePanel();
                if (getPlayerIndex(NicknameTextBox.Text) == -1)
                    File.AppendAllText("ScoreBoard.txt", NicknameTextBox.Text + "," + currentScore + Environment.NewLine);
            }

        }

        private void NextLevelButton_Click(object sender, EventArgs e)
        {
            setUpWordChoice();
        }

        private void ScoreboardButton_Click(object sender, EventArgs e)
        {
            showScoreBoardPanel();
            loadScoreBoardFile();
            displayScoreBoardData();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            showMainMenuPanel();
            updateCurrentPlayerScoreInFile();

            //reset GamePanel data
            currentScore         = 0;
            NicknameTextBox.Text = "";
            resetAll();

            //reset ScoreBoard Panel data
            AllNicknamesLabel.Text = "Nicknames";
            AllScoresLabel.   Text = "Scores"; 
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }



    }
    
}
