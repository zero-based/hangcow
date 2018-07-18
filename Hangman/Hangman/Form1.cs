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
        private string currentWord = "";
        private string currentWordCopy = "";
        private int score = 0;

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

        private void loadFile()
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

        private void resetAll()
        {
            //reset trials and images
            wrongGuesses = 0;
            PictureBox.Image = hangmanAllImages[wrongGuesses];

            //reset words and hints
            currentWord = "";
            currentWordCopy = "";
            wordPreviewLabel.Text = "";
            hintLabel.Text = "";

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
            Random rnd = new Random();
            int rndIndex = rnd.Next(0, words.Length);
            currentWord = words[rndIndex];

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

            char[] currentWordTEMP = currentWord.ToCharArray();
            char[] currentWordCopyTEMP = currentWordCopy.ToCharArray();

            for (int index = 0; index < currentWordTEMP.Length; index++)
            {
                if (letter == currentWordTEMP[index])
                    currentWordCopyTEMP[index] = letter;
            }

            currentWordCopy = new string(currentWordCopyTEMP);

        }

        private void guessClick(object sender, EventArgs e)
        {
            Button choice = sender as Button;
            choice.Enabled = false;
            char enteredLetter = choice.Text[0];

            if (currentWord.Contains(enteredLetter))
            {
                updateCopy(enteredLetter);
                displayWord();
                score += 5;
                ScoreLabel.Text = "";
                ScoreLabel.Text = score.ToString();
            }
            else
                wrongGuesses++;

            if (currentWordCopy.Equals(currentWord))
            {
                score += 50;
                wordPreviewLabel.Text = "";
                wordPreviewLabel.Text = "You Won!";
                ScoreLabel.Text = "";
                ScoreLabel.Text = score.ToString();
                NextLevelButton.Enabled = true;
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

        private void Form1_Load(object sender, EventArgs e)
        {
            loadFile();
            setUpWordChoice();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            GamePanel.Visible     = false;
            NicknamePanel.Visible = true;
            MainMenuPanel.Visible = false;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            GamePanel.Visible     = false;
            NicknamePanel.Visible = false;
            MainMenuPanel.Visible = true;
        }

        private void NextLevelButton_Click(object sender, EventArgs e)
        {
            setUpWordChoice();
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            GamePanel.Visible     = true;
            NicknamePanel.Visible = false;
            MainMenuPanel.Visible = false;
        }

    }
    
}
