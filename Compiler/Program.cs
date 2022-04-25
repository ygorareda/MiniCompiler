using Compiler.CodeAnalysis;
using Compiler.CodeAnalysis.Syntax;

while (true)
    {
        Console.Write(">");
        var line = Console.ReadLine();
        if (string.IsNullOrEmpty(line)){
            return;
        }

        var parser = new Parser(line);
        var syntaxTree = parser.Parse();

        //var printTree = new PrettyPrint();
        //printTree.PrettyPrintTree(syntaxTree.Root);

        if (!syntaxTree.Diagnostics.Any())
        {
            var e = new Evaluator(syntaxTree.Root);
            var result = e.Evaluate();
            Console.WriteLine(result);
        }
        else
        {
            foreach (var diagnostic in syntaxTree.Diagnostics)
            {
                Console.WriteLine(diagnostic);
            }
        }

    }
