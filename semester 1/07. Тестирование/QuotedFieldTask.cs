using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class QuotedFieldTaskTests
    {
        [TestCase("''", 0, "", 2)]
        [TestCase("'", 0, "", 1)]
        [TestCase("'кольцо вычетов по mod prime number'", 0, "кольцо вычетов по mod prime number", 36)]
        [TestCase("это вам просто \"кажется\", что тестик замудрёный", 15, "кажется", 9)]
        [TestCase("\"взлом\\\"кавычки\\\"\"", 0, "взлом\"кавычки\"", 18)]
        [TestCase("'escapin\\\' unescapable'", 0, "escapin' unescapable", 23)]
        [TestCase("\"колдую\\\\тебе\\\\фальш-escape\\\\\" попался", 0, "колдую\\тебе\\фальш-escape\\", 30)]
        [TestCase("'да кому нужны закрывающие кавычки", 0, "да кому нужны закрывающие кавычки", 34)]
        
        public void Test(string line, int startIndex,
            string expectedValue, int expectedLength)
        {
            var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
        }
    }

    class QuotedFieldTask
    {
        public static Token ReadQuotedField(string line, int startIndex)
        {
            var position = startIndex + 1;
            var tokenValue = new StringBuilder();
            while ((position < line.Length)
                && !(line[position] == line[startIndex]))
            {
                if (line[position] == '\\')
                    position++;
                tokenValue.Append(line[position]);
                position++;
            }
            // if reached closing bracket, position not increments in loop
            if (position < line.Length)
                position++;
            return new Token(tokenValue.ToString(),
                startIndex, position - startIndex);
        }
    }
}
