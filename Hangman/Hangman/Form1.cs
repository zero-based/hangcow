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
        private string currentWord     = "";
        private string currentWordCopy = "";

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

        private void setUpWordChoice()
        {
            //reseting wrong guesses and image
            wrongGuesses     = 0;
            pictureBox.Image = hangmanAllImages[wrongGuesses];
            
            //picking up a random word from "words array"
            Random rnd   = new Random();
            int rndIndex = rnd.Next(words.Length);
            currentWord  = words[rndIndex];

            //display Hint
            hintLabel.Text = "";
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

        private void guessClick(object sender, EventArgs e)
        {
            Button choice      = sender as Button;
            choice.Enabled     = false;
            char enteredLetter = choice.Text[0];

            if (currentWord.Contains(enteredLetter))
            {
                updateCopy(enteredLetter);
                displayWord();
            }
            else
                wrongGuesses++;

            if (currentWordCopy.Equals(currentWord))
                statusLabel.Text = "You Won!";
            else if (wrongGuesses < 7)
                pictureBox.Image = hangmanAllImages[wrongGuesses];
            else
                statusLabel.Text = "Game Over!";

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadFile();
            setUpWordChoice();
        }



    }
}
