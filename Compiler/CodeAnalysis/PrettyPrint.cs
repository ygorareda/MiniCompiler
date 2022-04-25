using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.CodeAnalysis
{
    public class PrettyPrint
    {
         public void PrettyPrintTree(SyntaxNode node, string indent = "")
        {
            Console.Write(indent);
            Console.Write(node.Kind);

            if (node is SyntaxToken t && t._value != null)
            {
                Console.Write(" ");
                Console.Write(t._value);
            }

            Console.WriteLine();

            indent += "    ";

            foreach (var child in node.GetChildren())
            {
                PrettyPrintTree(child, indent);
            }
        }
    }
}
