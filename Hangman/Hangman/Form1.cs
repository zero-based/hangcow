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

        private Bitmap[] hangmanAllImages = {Hangman.Properties.Resources.Hangman_0,
                                             Hangman.Properties.Resources.Hangman_1,
                                             Hangman.Properties.Resources.Hangman_2,
                                             Hangman.Properties.Resources.Hangman_3,
                                             Hangman.Properties.Resources.Hangman_4,
                                             Hangman.Properties.Resources.Hangman_5,
                                             Hangman.Properties.Resources.Hangman_6};
        private int wrongGuesses = 0;
        private string[] words;
        private string currentWord;
        private string currentWordCopy;

        public Form1()
        {
            InitializeComponent();
        }


        private void loadWordsFromFile()
        {
            string[] allLines = File.ReadAllLines("Words.txt");

            words = new string[allLines.Length];

            int index = 0; 
            foreach (string currentLine in allLines)
            {
                string[] column = currentLine.Split(',');
                words[index++] = column[0];
            }

        }

        private void setUpWordChoice()
        {
            wrongGuesses = 0;
            pictureBox.Image = hangmanAllImages[wrongGuesses];
            

            Random rnd = new Random();
            int rndIndex = rnd.Next(words.Length);

            currentWord = words[rndIndex];

            currentWordCopy = "";

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
                wordPreviewLabel.Text += " ";
            }

        }


        private void guessClick(object sender, EventArgs e)
        {
            Button choice  = sender as Button;
            choice.Enabled = false;

            if (currentWord.Contains(choice.Text))
            {
                char enteredLetter         = choice.Text[0];
                char[] currentWordTEMP     = currentWord.ToCharArray();
                char[] currentWordCopyTEMP = currentWordCopy.ToCharArray();

                for (int index = 0; index < currentWordTEMP.Length; index++)
                {
                    if (enteredLetter == currentWordTEMP[index])
                        currentWordCopyTEMP[index] = enteredLetter;
                }

                currentWordCopy = new string(currentWordCopyTEMP);
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
            loadWordsFromFile();
            setUpWordChoice();
        }



    }
}
