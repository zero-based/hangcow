using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.IO;

namespace Hangman
{
    public partial class HangmanForm : Form
    {

        public HangmanForm()
        {
            InitializeComponent();
        }

        private int    randomIndex;
        private int    wrongGuesses     = 0;
        private int    currentScore     = 0;
        private string currentWord      = "";
        private string currentWordCopy  = "";
        private string chosenDifficulty = "";
        private bool   isDifficultyRadioButtonChecked;
        private string[] words;
        private string[] hints;
        private string[] nicknames;
        private int   [] scores;

        List<int> usedWordsIndexes        = new List<int>();
        private Bitmap[] hangmanAllImages = {   Hangman.Properties.Resources.Hangcow_0,
                                                Hangman.Properties.Resources.Hangcow_1,
                                                Hangman.Properties.Resources.Hangcow_2,
                                                Hangman.Properties.Resources.Hangcow_3,
                                                Hangman.Properties.Resources.Hangcow_4,
                                                Hangman.Properties.Resources.Hangcow_5,
                                                Hangman.Properties.Resources.Hangcow_6};

        //primary colors
        //  Color primaryDark = System.Drawing.ColorTranslator.FromHtml("#0081cb");
        //  Color primary = System.Drawing.ColorTranslator.FromHtml("#00b0ff");
        //  Color primaryLight = System.Drawing.ColorTranslator.FromHtml("#69e2ff");

        //secondary colors
        //  Color secondaryDark = System.Drawing.ColorTranslator.FromHtml("#002f6c");
        //  Color secondary = System.Drawing.ColorTranslator.FromHtml("#01579b");
        //  Color secondaryLight = System.Drawing.ColorTranslator.FromHtml("#4f83cc");
                                            
        //Words
        private void loadWordsOfChosenDifficultyfromfile()
        {
            string[] allLines = File.ReadAllLines("Words.txt");

            int size = 0;
            foreach (string currentLine in allLines)
            {
                string[] column = currentLine.Split(':');
                if (column[0] == chosenDifficulty)
                    size++;
            }
            words = new string[size];
            hints = new string[size];


            int index = 0;
            foreach (string currentLine in allLines)
            {
                string[] column = currentLine.Split(':', ',');
                if (column[0] == chosenDifficulty) //Load words of the required difficulity only
                {
                    words[index] = column[1];
                    hints[index] = column[2];
                    index++;
                }
            }
        }

        private void setUpWordChoice()
        {
            //reseting all data
            resetAll();

            //picking up a random word from "words array" without repetition
            do
            {
                randomIndex = new Random().Next(words.Length);
            } while (usedWordsIndexes.Contains(randomIndex));
            currentWord = words[randomIndex];

            //display its Hint
            HintLabel.Text = "HINT: " + hints[randomIndex];

            //initialzing the currentWordCopy with blank spaces
            //according to number of letters in the original word
            for (int index = 0; index < currentWord.Length; index++)
            {
                currentWordCopy += "_";
            }
            displayWord();
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

        private void displayWord()
        {
            WordPreviewLabel.Text = "";
            for (int index = 0; index < currentWord.Length; index++)
            {
                WordPreviewLabel.Text += currentWordCopy.Substring(index, 1);
                if (index != currentWord.Length - 1)
                    WordPreviewLabel.Text += " ";
            }
        }

        private void resetAll()
        {
            //reset trials and images
            wrongGuesses     = 0;
            PictureBox.Image = hangmanAllImages[wrongGuesses];

            //reset words and hints
            currentWord           = "";
            currentWordCopy       = "";
            WordPreviewLabel.Text = "";
            HintLabel.Text        = "";


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

        private void winGame()
        {
            //display text
            WordPreviewLabel.Text = "No More Words!";
            HintLabel       .Text = "Congratulations!";

            //disable buttons
            ButtonA.Enabled = false;
            ButtonB.Enabled = false;
            ButtonC.Enabled = false;
            ButtonD.Enabled = false;
            ButtonE.Enabled = false;
            ButtonF.Enabled = false;
            ButtonG.Enabled = false;
            ButtonH.Enabled = false;
            ButtonI.Enabled = false;
            ButtonJ.Enabled = false;
            ButtonK.Enabled = false;
            ButtonL.Enabled = false;
            ButtonM.Enabled = false;
            ButtonN.Enabled = false;
            ButtonO.Enabled = false;
            ButtonP.Enabled = false;
            ButtonQ.Enabled = false;
            ButtonR.Enabled = false;
            ButtonS.Enabled = false;
            ButtonT.Enabled = false;
            ButtonU.Enabled = false;
            ButtonV.Enabled = false;
            ButtonW.Enabled = false;
            ButtonX.Enabled = false;
            ButtonY.Enabled = false;
            ButtonZ.Enabled = false;
            NextLevelButton.Enabled = false;
        }

        private void gameOver()
        {
            ButtonA.Enabled = false;
            ButtonB.Enabled = false;
            ButtonC.Enabled = false;
            ButtonD.Enabled = false;
            ButtonE.Enabled = false;
            ButtonF.Enabled = false;
            ButtonG.Enabled = false;
            ButtonH.Enabled = false;
            ButtonI.Enabled = false;
            ButtonJ.Enabled = false;
            ButtonK.Enabled = false;
            ButtonL.Enabled = false;
            ButtonM.Enabled = false;
            ButtonN.Enabled = false;
            ButtonO.Enabled = false;
            ButtonP.Enabled = false;
            ButtonQ.Enabled = false;
            ButtonR.Enabled = false;
            ButtonS.Enabled = false;
            ButtonT.Enabled = false;
            ButtonU.Enabled = false;
            ButtonV.Enabled = false;
            ButtonW.Enabled = false;
            ButtonX.Enabled = false;
            ButtonY.Enabled = false;
            ButtonZ.Enabled = false;
        }

        private void clearArraysAndLists()
        {
            Array.Resize(ref words, 0);
            Array.Resize(ref hints, 0);
            usedWordsIndexes.Clear();
        }




        //ScoreBoard & Scores
        private int getPlayerIndex(string name)
        {
            loadScoreBoardFile();
            string[] allLines = File.ReadAllLines("ScoreBoard.txt");

            int index;
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
                scores   [index] = Convert.ToInt32(column[1]);
                index++;
            }
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

        private void displayTopScoreBoarData()
        {
            string[] allLines   = File.ReadAllLines("ScoreBoard.txt");
            int numberOfPlayers = 3;

            for (int i = 0; i <= allLines.Length; i++)
            {
                for (int j = 0; j < allLines.Length - 1; j++)
                {
                    if (scores[j] < scores[j + 1])
                    {
                        int scoreTEMP = scores[j];
                        scores[j]     = scores[j + 1];
                        scores[j + 1] = scoreTEMP;

                        string nicknameTEMP = nicknames[j];
                        nicknames[j]        = nicknames[j + 1];
                        nicknames[j + 1]    = nicknameTEMP;

                    }
                }
            }
            for (int i = 0; i < numberOfPlayers; i++)
            {
                if (i == allLines.Length) break;
                TopNicknamesLabel.Text += nicknames[i] + "\n";
                TopScoresLabel   .Text += scores   [i] + "\n";
            }
        }

        private void displayRecentScoreBoardData()
        {
            string[] allLines   = File.ReadAllLines("ScoreBoard.txt");
            int numberOfPlayers = 5;
            for (int i = allLines.Length - 1; i >= allLines.Length - numberOfPlayers; i--)
            {
                if (i == allLines.Length || i < 0) break;
                RecentNicknamesLabel.Text += nicknames[i] + "\n";
                RecentScoresLabel   .Text += scores   [i] + "\n";     
            }

        }




        //Buttons
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
                ScoreLabel.Text = currentScore.ToString();
                updateCurrentPlayerScoreInFile();
            }
            else
                wrongGuesses++;

            if (currentWordCopy.Equals(currentWord))
            {
                currentScore           += 50;
                WordPreviewLabel.Text   = "You Won!";
                ScoreLabel.Text         = currentScore.ToString();
                NextLevelButton.Enabled = true;
                updateCurrentPlayerScoreInFile();
                usedWordsIndexes.Add(randomIndex);
                PictureBox.Image = Hangman.Properties.Resources.Hangcow_win;
            }
            else
            {
                if (wrongGuesses < 7)
                    PictureBox.Image = hangmanAllImages[wrongGuesses];
                else
                {
                    gameOver();
                    WordPreviewLabel.Text = "Game Over!"; 
                    HintLabel.Text        = "The Word is: " + currentWord;
                    PictureBox.Image = Hangman.Properties.Resources.FlyingCow;
                }
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            showNicknamePanel();
            NicknameTextBox.Select();
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NicknameTextBox.Text))
                NicknameLabel.Text = "Name Not Valid!";
            else if (isDifficultyRadioButtonChecked == false)
                NicknameLabel.Text = "Choose difficulty level!";
            else
            {
                NicknameTextBox.Enabled = false;
                clearArraysAndLists();
                loadWordsOfChosenDifficultyfromfile();
                setUpWordChoice();
                showGamePanel();
                if (getPlayerIndex(NicknameTextBox.Text) == -1)
                    File.AppendAllText("ScoreBoard.txt", NicknameTextBox.Text + ","
                                                       + currentScore + Environment.NewLine);
            }
        }

        private void NextLevelButton_Click(object sender, EventArgs e)
        {
            if (usedWordsIndexes.Count == words.Length) //no more words exist in the array
            {
                winGame();
                clearArraysAndLists();
            }
            else
                setUpWordChoice();
        }

        private void ScoreboardButton_Click(object sender, EventArgs e)
        {
            showScoreBoardPanel();
            loadScoreBoardFile();
            displayRecentScoreBoardData();
            displayTopScoreBoarData();
        }

        private void HowToPlayButton_Click(object sender, EventArgs e)
        {
            showHowToPlayPanel();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            showMainMenuPanel();

            //reset arrays and list
            clearArraysAndLists();

            //reset GamePanel data 
            currentScore = 0;
            resetAll();

            //reset NicknamePanel data
            NicknameTextBox  .Text    = "";
            NicknameTextBox  .Enabled = true;
            EasyRadioButton  .Checked = false;
            MediumRadioButton.Checked = false;
            HardRadioButton  .Checked = false;
            
            //reset ScoreBoardPanel data
            TopNicknamesLabel   .Text = "";
            TopScoresLabel      .Text = "";
            RecentNicknamesLabel.Text = "";
            RecentScoresLabel   .Text = "";
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }




        //Radio buttons
        private void EasyRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            chosenDifficulty = "easy";
            isDifficultyRadioButtonChecked = true;
        }

        private void MediumRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            chosenDifficulty = "medium";
            isDifficultyRadioButtonChecked = true;
        }

        private void HardRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            chosenDifficulty = "hard";
            isDifficultyRadioButtonChecked = true;
        }




        //Panels
        private void showMainMenuPanel()
        {
            MainMenuPanel  .Visible = true;
            GamePanel      .Visible = false;
            NicknamePanel  .Visible = false;
            ScoreBoardPanel.Visible = false;
            HowToPlayPanel .Visible = false;
        }

        private void showGamePanel()
        {
            MainMenuPanel  .Visible = false;
            GamePanel      .Visible = true;
            NicknamePanel  .Visible = false;
            ScoreBoardPanel.Visible = false;
            HowToPlayPanel .Visible = false;
        }

        private void showNicknamePanel()
        {
            MainMenuPanel  .Visible = false;
            GamePanel      .Visible = false;
            NicknamePanel  .Visible = true;
            ScoreBoardPanel.Visible = false;
            HowToPlayPanel .Visible = false;
        }

        private void showScoreBoardPanel()
        {
            MainMenuPanel  .Visible = false;
            GamePanel      .Visible = false;
            NicknamePanel  .Visible = false;
            ScoreBoardPanel.Visible = true;
            HowToPlayPanel .Visible = false;
        }

        private void showHowToPlayPanel()
        {
            MainMenuPanel  .Visible = false;
            GamePanel      .Visible = false;
            NicknamePanel  .Visible = false;
            ScoreBoardPanel.Visible = false;
            HowToPlayPanel .Visible = true;
        }



        //KeyDown event
        private void HangmanForm_KeyDown(object sender, KeyEventArgs e)
        {
            //MainMenuPanel Keydown event
            if (MainMenuPanel.Visible == true && e.KeyCode == Keys.Enter)
                StartButton_Click(sender, e);
            if (MainMenuPanel.Visible == true && e.KeyCode == Keys.Escape)
                ExitButton_Click(sender, e);

            //NicknamePanel Keydown event
            if (NicknamePanel.Visible == true && e.KeyCode == Keys.Enter)
                PlayButton_Click(sender, e);

            //BackButton Keydown event
            if ((GamePanel     .Visible == true  ||
                NicknamePanel  .Visible == true  ||
                ScoreBoardPanel.Visible == true  ||
                HowToPlayPanel. Visible == true) &&
                e.KeyCode               == Keys.Escape)
                BackButton_Click(sender, e);

            //GamePanel Keydown event
            if (GamePanel.Visible == true)
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        NextLevelButton_Click(sender, e);
                        break;

                    case Keys.A:
                        guessClick(ButtonA, e);
                        break;

                    case Keys.B:
                        guessClick(ButtonB, e);
                        break;

                    case Keys.C:
                        guessClick(ButtonC, e);
                        break;

                    case Keys.D:
                        guessClick(ButtonD, e);
                        break;

                    case Keys.E:
                        guessClick(ButtonE, e);
                        break;

                    case Keys.F:
                        guessClick(ButtonF, e);
                        break;

                    case Keys.G:
                        guessClick(ButtonG, e);
                        break;

                    case Keys.H:
                        guessClick(ButtonH, e);
                        break;

                    case Keys.I:
                        guessClick(ButtonI, e);
                        break;

                    case Keys.J:
                        guessClick(ButtonJ, e);
                        break;

                    case Keys.K:
                        guessClick(ButtonK, e);
                        break;

                    case Keys.L:
                        guessClick(ButtonL, e);
                        break;

                    case Keys.M:
                        guessClick(ButtonM, e);
                        break;

                    case Keys.N:
                        guessClick(ButtonN, e);
                        break;

                    case Keys.O:
                        guessClick(ButtonO, e);
                        break;

                    case Keys.P:
                        guessClick(ButtonP, e);
                        break;

                    case Keys.Q:
                        guessClick(ButtonQ, e);
                        break;

                    case Keys.R:
                        guessClick(ButtonR, e);
                        break;

                    case Keys.S:
                        guessClick(ButtonS, e);
                        break;

                    case Keys.T:
                        guessClick(ButtonT, e);
                        break;

                    case Keys.U:
                        guessClick(ButtonU, e);
                        break;

                    case Keys.V:
                        guessClick(ButtonV, e);
                        break;

                    case Keys.W:
                        guessClick(ButtonW, e);
                        break;

                    case Keys.X:
                        guessClick(ButtonX, e);
                        break;

                    case Keys.Y:
                        guessClick(ButtonY, e);
                        break;

                    case Keys.Z:
                        guessClick(ButtonY, e);
                        break;
                }
            }
        }

    }
}