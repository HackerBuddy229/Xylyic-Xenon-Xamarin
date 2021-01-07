using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace WordProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            const string filename = "nouns.list";
            var words = GetWords(filename);

            words = words.Select(ModifyWord).ToList();

            WriteWords(filename, words);
        }

        private static void WriteWords(string filename, List<string> words)
        {
            using var stream = new FileStream(filename, FileMode.Create);
            using var sw = new StreamWriter(stream);

            words.ForEach(sw.WriteLine);
        }

        private static List<string> GetWords(string filename)
        {
            var words = new List<string>();
            using var stream = new FileStream(filename, FileMode.Open);
            using var sr = new StreamReader(stream);
            while (true)
            {
                var nextWord = sr.ReadLine();
                if (nextWord == null)
                    break;

                words.Add(nextWord);
            }

            return words;
        }

        private static string ModifyWord(string word)
        {
            var endingLetters = word.TakeLast(word.Length - 1);
            var builder = new StringBuilder();
            foreach (var letter in endingLetters)
                builder.Append(letter);

            return word[0].ToString().ToUpper() + builder.ToString();
        }

    }
}
