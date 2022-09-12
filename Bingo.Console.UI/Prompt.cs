using Bingo.Library;
using Spectre.Console;

namespace Bingo.Console.UI;

internal static class Prompt
{
    // TODO - Implement Spectre prompts. 3x3min - 10x10max.
    public static (Card, string, string) Input()
    {
        // TODO - prompt user if they want to have stats or not, and if they do whether to include those with all the same guess
        Ascii.Title();

        const string option1 = "Load Default Configuration";
        const string option2 = "Enter Custom Configuration";

        var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[Red]Select an option:[/]")
                    .HighlightStyle("red")
                    .PageSize(3)
                    .MoreChoicesText("[grey](Move up and down with arrow keys)[/]")
                    .AddChoices(new[]
                    {
                        option1, option2,
                    })) switch
            {
                option1 => 0,
                option2 => 1,
                _ => throw new Exception("Error handling configuration selection")
            };

        Card card;
        string path;
        string key;
        switch (option)
        {
            case 0:
                AnsiConsole.MarkupLineInterpolated($"[Red]Loaded Default Configuration....[/]");
                card = new Card(4, 3, 10, 20, 1, 2);
                path = GetFilePath();
                key = GetKey(card.TotalSquares);
                return (card, path, key);
            case 1:
                path = GetFilePath();
                var columns = GetColumnAmount();
                var rows = GetRowAmount();
                var baseSquareValue = GetBaseSquareValue();
                var rowValueOffset = GetRowValueOffset();
                var bonusColumns = GetBonusColumnAmount(columns);
                byte bonusMultiplier = 1;
                if (bonusColumns != 0 && bonusColumns != columns)
                {
                    bonusMultiplier = GetBonusMultiplier();
                }

                card = new Card(columns, rows, baseSquareValue, rowValueOffset, bonusColumns, bonusMultiplier);
                key = GetKey(card.TotalSquares);
                return (card, path, key);
        }

        throw new Exception("Unmanageable error handling prompts.");
    }

    private static string GetFilePath()
    {
        var rule = new Rule("[white]Eg. home/user/spreadsheet.xlsx[/]").RuleStyle("red").LeftAligned();
        AnsiConsole.Write(rule);

        var path = AnsiConsole.Ask<string>("[Red]Please Enter File Path: [/]").Trim();

        while (!File.Exists(path) || System.IO.Path.GetExtension(path) != ".xlsx")
        {
            if (!File.Exists(path))
            {
                path = AnsiConsole.Ask<string>("[white]File does not exist. Please enter correct file path: [/]").Trim();
            }
            else if (System.IO.Path.GetExtension(path) != ".xlsx")
            {
                path = AnsiConsole.Ask<string>("[white]Invalid file type. Please use a file type of '.xlsx': [/]").Trim();
            }
        }

        return path;
    }

    private static byte GetColumnAmount()
    {
        var rule = new Rule("[white]Columns[/]").RuleStyle("red").LeftAligned();
        AnsiConsole.Write(rule);
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[Red]Select number of columns.[/]")
                .HighlightStyle("red")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down with arrow keys)[/]")
                .AddChoices(new[]
                {
                    "3", "4", "5", "6", "7", "8", "9","10"
                }));

        byte result = selection switch
        {
            "3" => 3,
            "4" => 4,
            "5" => 5,
            "6" => 6,
            "7" => 7,
            "8" => 8,
            "9" => 9,
            "10" => 10,
            _ => throw new Exception("Problem occured selecting the amount of columns.")
        };

        AnsiConsole.MarkupLineInterpolated($"[red]Selected:[/] {result.ToString()}");

        return result;
    }

    private static byte GetRowAmount()
    {
        var rule = new Rule("[white]Rows[/]").RuleStyle("red").LeftAligned();
        AnsiConsole.Write(rule);

        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[Red]Select number of rows.[/]")
                .HighlightStyle("red")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down with arrow keys)[/]")
                .AddChoices(new[]
                {
                    "3", "4", "5", "6", "7", "8", "9", "10"
                }));

        byte result = selection switch
        {
            "3" => 3,
            "4" => 4,
            "5" => 5,
            "6" => 6,
            "7" => 7,
            "8" => 8,
            "9" => 9,
            "10" => 10,
            _ => throw new Exception("Problem occured selecting the amount of rows.")
        };

        AnsiConsole.MarkupLineInterpolated($"[red]Selected:[/] {result.ToString()}");

        return result;
    }

    private static byte GetBaseSquareValue()
    {

        var rule = new Rule("[white]1 to 255[/]").RuleStyle("red").LeftAligned();
        AnsiConsole.Write(rule);

        return (byte)AnsiConsole.Prompt(
            new TextPrompt<int>("[red]How much will the base value be for a square? [/]")
                .PromptStyle("white")
                .ValidationErrorMessage("[white]Invalid amount[/]")
                .Validate(age =>
                {
                    return age switch
                    {
                        <= 0 => ValidationResult.Error("Must at least be a value of 1"),
                        > byte.MaxValue => ValidationResult.Error("Must not exceed 255"),
                        _ => ValidationResult.Success(),
                    };
                }));
    }

    private static int GetRowValueOffset()
    {
        var rule = new Rule("[white]-100 to 100[/]").RuleStyle("red").LeftAligned();
        AnsiConsole.Write(rule);
        return AnsiConsole.Prompt(
            new TextPrompt<int>("[red]How much will each row be offset by?[/]")
                .PromptStyle("white")
                .ValidationErrorMessage("[white]Invalid amount[/]")
                .Validate(age =>
                {
                    return age switch
                    {
                        < -100 => ValidationResult.Error("Must not be below -100"),
                        > 100 => ValidationResult.Error("Must not exceed 100"),
                        _ => ValidationResult.Success(),
                    };
                }));
    }

    private static byte GetBonusColumnAmount(byte columns)
    {
        var rule = new Rule("[white]Bonus Column Amount[/]").RuleStyle("red").LeftAligned();
        AnsiConsole.Write(rule);

        var allChoices = new[]
        {
            "[Red]None[/]", "[Red]1[/]", "[Red]2[/]", "[Red]3[/]", "[Red]4[/]", "[Red]5[/]", "[Red]6[/]",
            "[Red]7[/]", "[Red]8[/]", "[Red]9[/]", "[Red]10[/]"
        };

        var choices = allChoices[..(columns + 1)].ToArray();
        choices[^1] = "[Red]All[/]";

            var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[Red]Select the number of bonus columns.[/]")
                .HighlightStyle("red")
                .PageSize(columns + 1)
                .MoreChoicesText(
                    $"[grey](Move up and down with arrow keys){Environment.NewLine}(Selecting the same amount as the columns selection will make it so that you can skip on any square.)[/]")
                .AddChoices(choices));

        byte result = selection switch
        {
            "[Red]None[/]" => 0,
            "[Red]1[/]" => 1,
            "[Red]2[/]" => 2,
            "[Red]3[/]" => 3,
            "[Red]4[/]" => 4,
            "[Red]5[/]" => 5,
            "[Red]6[/]" => 6,
            "[Red]7[/]" => 7,
            "[Red]8[/]" => 8,
            "[Red]9[/]" => 9,
            "[Red]10[/]" => 10,
            "[Red]All[/]" => columns,
            _ => throw new Exception("Problem occured selecting the amount of bonus columns.")
        };

        AnsiConsole.MarkupLineInterpolated($"[red]Selected:[/] {result.ToString()}");

        return result;
    }

    private static byte GetBonusMultiplier()
    {
        // TODO - If user chose all columns as bonus, default to 1. and not prompt.
        var rule = new Rule("[white]1 to 100. 2 is 2x, etc.[/]").RuleStyle("red").LeftAligned();
        AnsiConsole.Write(rule);
        return (byte)AnsiConsole.Prompt(
            new TextPrompt<int>("[red]How much will the bonus multiplier be?[/]")
                .PromptStyle("white")
                .ValidationErrorMessage("[white]Invalid amount[/]")
                .Validate(age =>
                {
                    return age switch
                    {
                        < 1 => ValidationResult.Error("Must at least 1"),
                        > 100 => ValidationResult.Error("Must not exceed 100"),
                        _ => ValidationResult.Success(),
                    };
                }));
    }

    private static string GetKey(int squares)
    {
        // TODO - See about prompting per row and have them be entered individually
        var rule = new Rule("[white]Read Top to Bottom, Left to Right[/]").RuleStyle("red").LeftAligned();
        AnsiConsole.Write(rule);

        var key = AnsiConsole.Ask<string>("[Red]Please Enter Key: [/]").StringFormat();

        while (key.Length != (squares))
        {
            AnsiConsole.MarkupLineInterpolated($"[white]Invalid key. Make sure you enter for {squares.ToString()} squares. You entered {key.Length.ToString()}[/]");
            key = AnsiConsole.Ask<string>("[Red]Please Enter Key: [/]").StringFormat();
        }

        return key;
    }

    public static void InvalidGuessers(Game game)
    {// TODO - Make a table instead of just this text
        System.Console.Clear();
        Ascii.Title();
        AnsiConsole.MarkupLineInterpolated($"Detected: Players with incorrect amount of guesses!");
        AnsiConsole.MarkupLineInterpolated($"Make sure each player has guessed for {game.Card.TotalSquares.ToString()} squares.");
        foreach (var incorrectGuesser in game.InvalidGuesses)
        {
            AnsiConsole.MarkupLineInterpolated(
                $"'{incorrectGuesser.Name}' in row {incorrectGuesser.Row.ToString()} guessed for {incorrectGuesser.GuessAmount.ToString()} squares");
        }

        System.Console.WriteLine("Please resolve issue and try again.");
        System.Console.Write("Press Enter to exit....");
        System.Console.ReadKey(true);
        System.Console.WriteLine(Environment.NewLine);
        System.Console.ResetColor();
        Environment.Exit(5);
    }

    public static void End(Game game)
    {
        System.Console.Clear();
        Ascii.Title();
        AnsiConsole.MarkupLineInterpolated(
            $"[red]Successfully finished scoring [/]{game.Card.TotalSquares.ToString()} [red]Squares for [/]{game.Players.Count.ToString()} [red]Players in [/]{game.Stats.ScoreCalculationTime.ToString()}[red] milliseconds.[/]");
        AnsiConsole.MarkupLineInterpolated($"[red]Press any key to exit...[/]");
        System.Console.ReadKey(true);
        System.Console.Write(Environment.NewLine);
        System.Console.ResetColor();
        Environment.Exit(0);
    }
}
