using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.CodeAnalysis.Syntax
{
    //precedence of token, to built the tree
    public class SyntaxPrecedence
    {

        public int GetBinaryOperatorPrecedence(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                    return 1;

                case SyntaxKind.StarToken:
                case SyntaxKind.SlashToken:
                    return 2;

                default:
                    return 0;
            }
        }
        public int GetUnaryOperatorPrecedence(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                    return 3;

                default:
                    return 0;
            }
        }

    }
}
