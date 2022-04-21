using System;
using HangmanRenderer.Renderer;

namespace Hangman.Core.Game
{
    public class HangmanGame
    {
        private GallowsRenderer _renderer;
        private string _wordGuessed;
      //  private string _guessProgress;
        private string _addGuess;
        private string[] wordList = { "umbrella", "dinosaur", "bird", "sleep", "jealous", "hospital", "story", "book", "reading", "motorcycle", "queen", "tomato",
                                  "atmosphere", "library", "music", "massage", "party", "restaurant", "paint", "homework" };
        private char[] _wordPlacement;
        private int numLives = 6;
        //bool playerWon = true;

        public HangmanGame()
        {
            _renderer = new GallowsRenderer();
            
            //Generates word at random
            Random randomWord = new Random();
            var index = randomWord.Next(wordList.Length); //number of words I have in array
            _wordGuessed = wordList[index];
            _wordPlacement = _wordGuessed.ToCharArray();
        }

    
        public void Run()
        {
            string wordPlacement = string.Empty;

            //Characters on the dashes
            for (int i = 0; i < _wordGuessed.Length; i++)
            {
                _wordPlacement[i] = '-';
            }

            while (numLives > 0 && numLives <= 6) 
            {
                _renderer.Render(5, 5, numLives); //this draws the figure of the man for each character wrong and the position of the renderer(gallows)

                Console.SetCursorPosition(0, 13);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("Your current guess: ");
                Console.WriteLine(_wordPlacement);

                Console.SetCursorPosition(0, 15);

                Console.ForegroundColor = ConsoleColor.Green;

                Console.Write("What is your next guess: ");
                var nextGuess = char.Parse(Console.ReadLine()); //allow the character to pass through the string 
                //Console.ReadLine interprets the input of user

                //Create a boolean for if user guess correctly
                bool correctLetter = false;
                {
                    for (var index = 0; index < _wordGuessed.Length; index++)
                    {
                        if (nextGuess == _wordGuessed[index])
                        {
                            _wordPlacement[index] = nextGuess;
                            correctLetter = true;
                        }
                    }

                    if (!correctLetter) //if letter incorrect, lives will be taken away
                    {
                        numLives--;
                        _renderer.Render(5, 5, numLives);
                    }
                }
                wordPlacement = new string(_wordPlacement); // make a construct to take the character and change it into a string

                if (wordPlacement == _wordGuessed)
                {
                    Console.SetCursorPosition(0, 20);
                    Console.WriteLine("Well Done!");
                   // break;
                }
               
            }

            if (wordPlacement != _wordGuessed)
            {
                Console.SetCursorPosition(0, 20);
                Console.WriteLine("Sorry, you lose!");
                Console.WriteLine("The correct guessed word supposed to be: " + _wordGuessed);

            }
        }

    }
}
