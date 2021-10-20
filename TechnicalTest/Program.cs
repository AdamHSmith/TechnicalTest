using System;
using System.Collections.Generic;
using System.Text;

namespace TechnicalTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter your string");
            string userInput = Console.ReadLine();

            var formattedInput = RemovePunctuation(userInput);
            var words = formattedInput.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            var anagrams = FindAnagram(words);
            string printString = "Anagrams: ";

            if(anagrams.Count > 0)
            foreach (var anagramList in anagrams)
            {              
                    var anagramString = $"[{String.Join(" , ", anagramList)}]";
                    printString = printString + " " + anagramString;               
            }
            else 
            {
                printString = printString + "No Anagrams Found";
            }
            Console.WriteLine(printString);
            


        }

        public static string RemovePunctuation(string input)
        {
            var inputString = input.ToLower();
            var sb = new StringBuilder();

            foreach (char c in inputString)
            {
                if (!char.IsPunctuation(c))
                {
                    sb.Append(c);
                }
                else
                {
                    sb.Append(" ");
                }
            }            

            return  sb.ToString();
        }

        public static List<List<string>> FindAnagram(string[] words)
        {
            List<char[]> allWords = new List<char[]>();
            List<List<string>> anagrams = new List<List<string>>();

            foreach (var word in words)
            {
                var charArray = word.ToCharArray();
                allWords.Add(charArray);
            }

            
            foreach (var primaryWord in allWords)
            {

                var doesAnagramCoreExist = false;
                var primaryWordString = new string(primaryWord);
                if(anagrams.Count > 0)
                {
                    foreach(var array in anagrams)
                    {
                        var anagramFound = array.Contains(primaryWordString);
                        if (anagramFound)
                        { 
                            doesAnagramCoreExist = true; 
                        }
                    }
                }

                if (!doesAnagramCoreExist)
                {
                    List<string> primaryWordAnagrams = new List<string>();
                    primaryWordAnagrams.Add(primaryWordString);

                    foreach (var secondaryWord in allWords)
                    {
                        if (primaryWord.Length == secondaryWord.Length)
                        {
                            var secondaryWordString = new string(secondaryWord);

                            if (primaryWordString != secondaryWordString)
                            {
                                char[] primaryTemp = primaryWordString.ToCharArray();
                                char[] secondaryTemp = secondaryWordString.ToCharArray();

                                Array.Sort(primaryTemp);
                                Array.Sort(secondaryTemp);


                                string NewWord1 = new string(primaryTemp);
                                string NewWord2 = new string(secondaryTemp);


                                if ((NewWord1 == NewWord2) && (!primaryWordAnagrams.Contains(secondaryWordString)))
                                {
                                    primaryWordAnagrams.Add(secondaryWordString);
                                }

                            }
                        }
                    }

                    if (primaryWordAnagrams.Count > 1)
                    {
                        anagrams.Add(primaryWordAnagrams);
                    }
                }


            }

            return anagrams;
        }

    }
}
