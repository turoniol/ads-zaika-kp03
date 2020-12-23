using System;
static class Printer
{
  private static ConsoleColor consoleColor;

  public enum Color
  {
    Black = ConsoleColor.Black,
    Red = ConsoleColor.Red,
    White = ConsoleColor.White,
    Yellow = ConsoleColor.Yellow,
    Green = ConsoleColor.Green,
    Gray = ConsoleColor.Gray,
    Blue = ConsoleColor.Blue
  }

  public static void Print(string text) => Console.WriteLine(text);

  public static void PrintError(string text)
  {
    PrintColor(text, Color.Red);
  }

  public static void PrintColor(string text, Color c)
  {
    ConsoleColor current = Console.ForegroundColor;
    Console.ForegroundColor = (ConsoleColor)c;
    Print(text);
    Console.ForegroundColor = current;
  }

  public static void BeginColored(Color c)
  {
    consoleColor = Console.ForegroundColor;
    Console.ForegroundColor = (ConsoleColor)c;
  }

  public static void EndColored()
  {
    Console.ForegroundColor = consoleColor;
  }
}
