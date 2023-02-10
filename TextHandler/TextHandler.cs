using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BecomeADeveloperWinter2023.TextHandler
{
    public class TextHandler
    {
        public string? Text { get; set; }
        private const string unnecessaryChars = ",.:!?<>\"-=+(){}/\\_";
        private const char wordSpliter = ' ';

        public TextHandler(string text)
        {
            Text = text;
        }

        public TextHandler()
        {
        }

        private void PrepareText()
        {
            foreach (var ch in unnecessaryChars)
            {
                Text = Text.Replace(ch, wordSpliter).ToString();
            }
            Text = Regex.Replace(Text, @"\s+", " ");
        }

        #region Simplified
        //This method ignores the dact that words are usually being used multiple times in one text
        public char SolveTheProblemSimplified()
        {
            if (string.IsNullOrWhiteSpace(Text))
            {
                throw new ArgumentNullException("There is no text provided");
            }

            PrepareText();

            return SelectUniqueChar(FindUniqueCharsSimplified());
        }

        private List<char> FindUniqueCharsSimplified()
        {
            List<char> result = new List<char>();
            bool reconStep = true;
            char suspect = ' ';
            int suspectId = -1;
            List<char> localBlackList = new List<char>();

            for (int i = 0; i < Text.Length; i++)
            {
                if (reconStep)
                {
                    suspect = Text[i];
                    suspectId = i;
                    reconStep = false;
                    continue;
                }

                if (Text[i] == suspect)
                {
                    reconStep = true;
                    i = suspectId;
                    localBlackList.Add(suspect);
                    continue;
                }

                if (Text[i] != suspect)
                {
                    if (Text[i] == ' ')
                    {
                        reconStep = true;
                        if (!localBlackList.Contains(suspect))
                        {
                            result.Add(suspect);
                            localBlackList.Clear();
                        }
                    }
                    continue;
                }
            }

            return result;
        }
        #endregion

        public char SolveTheProblem()
        {
            return SelectUniqueChar(FindUniqueCharsInUniqueWords());
        }

        private List<char> FindUniqueCharsInUniqueWords()
        {
            //step 0 - prep
            PrepareText();
            //step 1 - unique words
            var words = Text.Split(' ').ToList().ConvertAll(x => x.ToLower()).Distinct().ToList();
            //step 2 - unique chars
            List<char> chars = new List<char>();

            foreach(var word in words)
            {
                char? suspect = word[0];
                int suspectId = 0;

                if (word.Length == 1)
                {
                    chars.Add(suspect.Value);
                    continue;
                }

                for (int i = 1; i < word.Length; i++)
                {
                    if (word[i] == suspect)
                    {
                        suspect = null;
                        i = suspectId;
                        continue;
                    }
                }

                if (suspect != null)
                {
                    chars.Add(suspect.Value);
                }
            }

            return chars;
        }

        private char SelectUniqueChar(List<char> selectedChars)
        {
            //I suppose that task is not case sensetive
            selectedChars = selectedChars.ConvertAll(x => char.ToLower(x));

            var trulyUnique = selectedChars.Where(x => selectedChars.Count(spare => spare == x) < 2).ToList(); ;
            return trulyUnique.First();
        }
    }
}
