using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordUnscrambler.data;
using WordUnscrambler.workers;

namespace WordUnscrambler
{
    class Program
    {
        private static readonly FileReader _fileReader = new FileReader();
        private static readonly WordMatcher _wordMatcher = new WordMatcher();


        static void Main(string[] args)
        {

            try
            {
                bool continueWordUnscramble = true;

                do
                {
                    Console.WriteLine(constans.OptionsOnHowToEnterScrambledWords);
                    var option = Console.ReadLine() ?? string.Empty;
                    switch (option.ToUpper())
                    {
                        case constans.File:
                            Console.Write(constans.EnterScrambledWordsViaFile);
                            ExecuteScrambleWordsInFileScenario();
                            break;
                        case constans.Manual:
                            Console.Write(constans.EnterScrambledWordsManually);
                            ExecuteScrambelWordsManualEntryScenario();
                            break;
                        default:
                            Console.WriteLine(constans.EnterScrambledWordsNotRecognized);
                            break;
                    }
                    var continueDecision = string.Empty;
                    do
                    {
                        Console.WriteLine(constans.OptionsOnContinuingTheProgram);
                        continueDecision = (Console.ReadLine() ?? string.Empty);
                    } while (
                    !continueDecision.Equals(constans.Yes, StringComparison.OrdinalIgnoreCase) &&
                    !continueDecision.Equals(constans.No, StringComparison.OrdinalIgnoreCase));

                    continueWordUnscramble = continueDecision.Equals(constans.Yes, StringComparison.OrdinalIgnoreCase);

                } while (continueWordUnscramble);
            }

            catch (Exception ex)
            {

                Console.WriteLine(constans.ErrorProgramWillBeTerminated + ex.Message);
            }
        }


        private static void ExecuteScrambelWordsManualEntryScenario()
        {
            var manualInput = Console.ReadLine() ?? string.Empty;
            string[] scrambleWords = manualInput.Split(',');
            DisplayMatchedUnscrambleWords(scrambleWords);
        }

        private static void ExecuteScrambleWordsInFileScenario()
        {
            try
            {
                var filename = Console.ReadLine() ?? string.Empty;
                string[] scrambleWords = _fileReader.Read(filename);
                DisplayMatchedUnscrambleWords(scrambleWords);
            }
            catch (Exception ex)
            {

                Console.WriteLine(constans.ErrorScrambledWordsCannotBeLoaded + ex);
            }

        }
        private static void DisplayMatchedUnscrambleWords(string[] scrambleWords)
        {
            string[] wordList = _fileReader.Read(constans.WordListFileName);

            List<MatchedWord> matchedWords = _wordMatcher.Match(scrambleWords, wordList);

            if (matchedWords.Any())
            {
                foreach (var matchedWord in matchedWords)
                {
                    Console.WriteLine(constans.MatchFound, matchedWord.ScrambledWord, matchedWord.Word);

                }

            }
            else
            {
                Console.WriteLine(constans.MatchNotFound);
            }

        }


    }
}

