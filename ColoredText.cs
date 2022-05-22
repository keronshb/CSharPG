public static class ColoredText
{
    /// <summary>
    /// Produces a colored console text.  Takes in the text to be colored and the color you want the text, does a writeline with it.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="color"></param>
    public static void TextWriteLine(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
    }
    /// <summary>
    /// Produces a colored console text.  Takes in the text to be colored and the color you want the text, does a write with it.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="color"></param>
    public static void TextWrite(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(text);
    }
    /// <summary>
    /// Produces highlighted text in yellow.  Put the text you wish to highlight in the highlight variable.
    /// </summary>
    /// <param name="beginning"></param>
    /// <param name="highlight"></param>
    /// <param name="end"></param>
    public static void TextHighlight(string beginning, string highlight, string end)
    {
        TextWrite(beginning, ConsoleColor.White);
        TextWrite(highlight, ConsoleColor.Yellow);
        TextWrite(end, ConsoleColor.White);
    }

}
