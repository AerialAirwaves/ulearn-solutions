using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class FieldParserTaskTests
    {
        public static void Test(string input, string[] expectedResult)
        {
            var actualResult = FieldsParserTask.ParseLine(input);
            Assert.AreEqual(expectedResult.Length, actualResult.Count);
            for (int i = 0; i < expectedResult.Length; ++i)
            {
                Assert.AreEqual(expectedResult[i], actualResult[i].Value);
            }
        }

        [TestCase(" ", new string[] { })]
        [TestCase("'", new[] { "" })]
        [TestCase("text", new[] { "text" })]
        [TestCase("hello world", new[] { "hello", "world" })]
        [TestCase("'hello'\"world\"", new[] { "hello", "world" })]
        [TestCase("\"hello 'world'\"", new[] { "hello 'world'" })]
        [TestCase("'hello \"world\"'", new[] { "hello \"world\"" })]
        [TestCase("\"vzlom escape\\\\\"", new[] { "vzlom escape\\" })]
        [TestCase("'no closing quote", new[] { "no closing quote" })]
        [TestCase("simple_field_escape\\\\test", new[] { "simple_field_escape\\\\test" })]
        [TestCase("'escapin\\' unescapable'", new[] { "escapin' unescapable" })]
        [TestCase("\"escapin\\\" unescapable\"", new[] { "escapin\" unescapable" })]
        [TestCase("  foo  ", new[] { "foo" })]
        [TestCase("a''", new[] { "a", "" })]
        [TestCase("''a", new[] { "", "a" })]
        [TestCase("' ", new[] { " " })]
        public static void RunTests(string input, string[] expectedOutput)
        {
            Test(input, expectedOutput);
        }
    }

    public class FieldsParserTask
    {
        public static Token ReadSimpleField(string line, int startIndex)
        {
            var result = new StringBuilder();
            var position = startIndex;
            while ((position < line.Length) && !((line[position] == ' ')
                || (line[position] == '\'') || (line[position] == '"')))
            {
                result.Append(line[position]);
                position++;
            }
            return new Token(result.ToString(), startIndex, result.Length);
        }

        public static List<Token> ParseLine(string line)
        {
            var result = new List<Token>();
            var position = 0;
            while (position < line.Length)
            {
                var field = new Token(null, -1, -1);
                if ((line[position] == '\'') || (line[position] == '\"'))
                    field = ReadQuotedField(line, position);
                else if (line[position] != ' ')
                    field = ReadSimpleField(line, position);
                if (field.Value == null)
                    position++;
                else
                {
                    position += field.Length;
                    result.Add(field);
                }
            }
            return result;
        }
        
        private static Token ReadField(string line, int startIndex)
        {
            return new Token(line, 0, line.Length);
        }

        public static Token ReadQuotedField(string line, int startIndex)
        {
            return QuotedFieldTask.ReadQuotedField(line, startIndex);
        }
    }
}