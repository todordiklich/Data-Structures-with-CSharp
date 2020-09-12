namespace Problem04.BalancedParentheses
{
    using System;
    using System.Collections.Generic;

    public class BalancedParenthesesSolve : ISolvable
    {
        public bool AreBalanced(string parentheses)
        {
            Stack<char> openBrackets = new Stack<char>();

            if (String.IsNullOrEmpty(parentheses) || parentheses.Length % 2 == 1)
            {
                return false;
            }

            foreach (char currentBracket in parentheses)
            {
                char expectedBracket = default;

                switch (currentBracket)
                {
                    case ')':
                        expectedBracket = '(';
                        break;
                    case ']':
                        expectedBracket = '[';
                        break;
                    case '}':
                        expectedBracket = '{';
                        break;
                    default:
                        openBrackets.Push(currentBracket);
                        break;
                }

                if (expectedBracket == default)
                {
                    continue;
                }

                if (openBrackets.Pop() != expectedBracket)
                {
                    return false;
                }
            }

                return openBrackets.Count == 0;
        }
    }
}
