﻿using Compiler.CodeAnalysis;
using Compiler.CodeAnalysis.Binding;
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
        var binder = new Binder();
        var boundExpression = binder.BindExpression(syntaxTree.Root);

        var diagnostics = syntaxTree.Diagnostics.Concat(binder.Diagnostics).ToArray();

        //var printTree = new PrettyPrint();
        //printTree.PrettyPrintTree(syntaxTree.Root);

        if (!syntaxTree.Diagnostics.Any())
        {
            var e = new Evaluator(boundExpression);
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
