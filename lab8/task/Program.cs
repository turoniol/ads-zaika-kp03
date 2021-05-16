using BinaryTrees;
using System;

RBTree tree = new RBTree();
while (true)
{
    try
    {
        Console.WriteLine("Enter value (\'help\' for helpbox): ");
        var command = Console.ReadLine().Trim();

        if (string.IsNullOrEmpty(command)) break;
        if (command == "clear")
        {
            tree = new RBTree();
            Console.WriteLine("The tree succesfully cleared!");
            continue;
        }
        else if (command == "help")
        {
            OnHelpbox();
            continue;
        }
        else if (command == "draw")
        {
            OnDraw();
            continue;
        }
        tree.Insert(int.Parse(command));
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

void OnHelpbox()
{
    Console.WriteLine(
@"
HELPBOX:
enter integer number to add this value in tree,
enter empty line to finish,
enter 'clear' to clear the tree,
enter 'draw' to save the tree as image
"
    );
}

void OnDraw()
{
    TreeVisualiser.Draw(tree, "tree.bmp", 595, 10, 600, 50);
    Console.WriteLine(@"Saved as 'tree.bmp'");
}