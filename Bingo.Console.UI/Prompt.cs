using System.Globalization;
using Bingo.Domain;
using Bingo.Domain.Models;
using Bingo.Core;
using Spectre.Console;

namespace Bingo.Console.UI;

internal static class Prompt
{
    public static (Card, string, string, ISettings) Input()
    {
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
        var allSame = false;
        var noBonusSkipping = false;
        var settings = new Settings();
        switch (option)
        {
            case 0:
                AnsiConsole.MarkupLineInterpolated($"[Red]Loaded Default Tower of God Configuration....[/]");
                card = new Card(4, 3, 10, 20, 1, 2);
                path = GetFilePath();
                allSame = WillTrackAllSameGuessesInStats();
                key = GetKey(card.TotalSquares);
                settings.WillCountAllSameGuessersInStats = allSame;

                return (card, path, key, settings);
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
                else if (bonusColumns == 0)
                {
                    noBonusSkipping = AllowSkippingWhenThereIsNoBonus();
                }

                var skipCharacter = GetSkipCharacter();

                allSame = WillTrackAllSameGuessesInStats();
                settings.WillCountAllSameGuessersInStats = allSame;
                settings.AllowSkippingWhenThereIsNoBonus = noBonusSkipping;
                card = new Card(columns, rows, baseSquareValue, rowValueOffset, bonusColumns, bonusMultiplier, skipCharacter);
                key = GetKey(card.TotalSquares);

                return (card, path, key, settings);
        }

        throw new Exception("Unmanageable error handling prompts.");
    }

    private static string GetFilePath()
    {
        var rule = new Rule("[white]Eg. home/user/spreadsheet.xlsx[/]").RuleStyle("red").LeftAligned();
        AnsiConsole.Write(rule);

        var path = AnsiConsole.Ask<string>("[Red]Please Enter File Path:[/]").Trim();

        while (!File.Exists(path) || System.IO.Path.GetExtension(path) != ".xlsx")
        {
            if (!File.Exists(path))
            {
                path = AnsiConsole.Ask<string>("[white]File does not exist. Please enter correct file path:[/]").Trim();
            }
            else if (System.IO.Path.GetExtension(path) != ".xlsx")
            {
                path = AnsiConsole.Ask<string>("[white]Invalid file type. Please use a file type of '.xlsx':[/]")
                    .Trim();
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
            "[white]None[/]",
            "[white]1[/]",
            "[white]2[/]",
            "[white]3[/]",
            "[white]4[/]",
            "[white]5[/]",
            "[white]6[/]",
            "[white]7[/]",
            "[white]8[/]",
            "[white]9[/]",
            "[white]10[/]"
        };

        var choices = allChoices[..(columns + 1)].ToArray();
        choices[^1] = "[white]All[/]";

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
            "[white]None[/]" => 0,
            "[white]1[/]" => 1,
            "[white]2[/]" => 2,
            "[white]3[/]" => 3,
            "[white]4[/]" => 4,
            "[white]5[/]" => 5,
            "[white]6[/]" => 6,
            "[white]7[/]" => 7,
            "[white]8[/]" => 8,
            "[white]9[/]" => 9,
            "[white]10[/]" => 10,
            "[white]All[/]" => columns,
            _ => throw new Exception("Problem occured selecting the amount of bonus columns.")
        };

        AnsiConsole.MarkupLineInterpolated($"[red]Selected:[/] {result.ToString()}");

        return result;
    }

    private static byte GetBonusMultiplier()
    {
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

    private static char GetSkipCharacter()
    {
        var rule = new Rule("[white]Single Characters Only[/]").RuleStyle("red").LeftAligned();
        AnsiConsole.Write(rule);

        var holder = AnsiConsole.Ask<string>("[Red]Please Enter Designated Skip Character:[/]").StringFormat();

        while (holder.Length > 1)
        {
            AnsiConsole.MarkupLineInterpolated( $"[white]Invalid Input. Make sure you only enter a single character. You entered {holder.Length.ToString()}.[/]");
            holder = AnsiConsole.Ask<string>("[Red]Please Enter Designated Skip Character:[/]").StringFormat();
        }

        return holder.ToUpper()[0];
    }

    private static string GetKey(int squares)
    {
        // TODO - See about prompting per row and have them be entered individually
        // TODO: Try to set up validation here so prompts can happen without exiting and entering input all over again
        var rule = new Rule("[white]Read Top to Bottom, Left to Right[/]").RuleStyle("red").LeftAligned();
        AnsiConsole.Write(rule);

        var key = AnsiConsole.Ask<string>("[Red]Please Enter Key:[/]").StringFormat();

        while (key.Length != (squares))
        {
            AnsiConsole.MarkupLineInterpolated(
                $"[white]Invalid key. Make sure you enter for {squares.ToString()} squares. You entered {key.Length.ToString()}[/]");
            key = AnsiConsole.Ask<string>("[Red]Please Enter Key:[/]").StringFormat();
        }

        return key;
    }

    private static bool WillTrackAllSameGuessesInStats()
    {
        var rule = new Rule("[white]Stat Tracking[/]").RuleStyle("red").LeftAligned();
        AnsiConsole.Write(rule);

        if (!AnsiConsole.Confirm("[red]Include those that guessed for all the same answer on each square?[/]"))
        {
            return false;
        }

        return true;
    }

    private static bool AllowSkippingWhenThereIsNoBonus()
    {
        var rule = new Rule("[white]Allow Skips[/]").RuleStyle("red").LeftAligned();
        AnsiConsole.Write(rule);

        if (!AnsiConsole.Confirm("[red]Allow the option of skipping when any squares even when there is no Bonus?[/]"))
        {
            return false;
        }

        return true;
    }

    public static void InvalidGuessers(List<InvalidGuesser> invalidGuessers, Game game)
    {
        System.Console.Clear();
        Ascii.Title();
        AnsiConsole.MarkupLineInterpolated($"Detected: Players with incorrect amount of guesses!");
        AnsiConsole.MarkupLineInterpolated(
            $"Make sure each player has guessed for {game.Card.TotalSquares.ToString()} squares.");
        foreach (var incorrectGuesser in invalidGuessers)
        {
            AnsiConsole.MarkupLineInterpolated(
                $"'{incorrectGuesser.Name}' guessed for {incorrectGuesser.GuessAmount.ToString()} squares");
        }

        System.Console.WriteLine("Please resolve issue and try again.");
        System.Console.Write("Press Enter to exit....");
        System.Console.ReadKey(true);
        System.Console.WriteLine(Environment.NewLine);
        System.Console.ResetColor();
        Environment.Exit(5);
    }

    public static void CannotOpenFile(Exception exception)
    {
        System.Console.Clear();
        Ascii.Title();
        AnsiConsole.MarkupLineInterpolated( $"[red]Error: {exception.Message}[/]");
        System.Console.ReadKey(true);
        Environment.Exit(6);
    }

    public static void NoPlayers(Exception exception)
    {
        System.Console.Clear();
        Ascii.Title();
        AnsiConsole.MarkupLineInterpolated( $"[red]Error: {exception.Message}[/]");
        System.Console.ReadKey(true);
        Environment.Exit(7);
    }

    public static void MultipleGuessesOfSamePlayer(Exception exception)
    {
        System.Console.Clear();
        Ascii.Title();
        AnsiConsole.MarkupLineInterpolated( $"[red]Error: {exception.Message}[/]");
        System.Console.ReadKey(true);
        Environment.Exit(8);
    }

    public static void End(Game game)
    {
        System.Console.Clear();
        Ascii.Title();
        AnsiConsole.MarkupLineInterpolated(
            $"[red]Successfully finished scoring [/]{game.Card.TotalSquares.ToString()} [red]Squares for [/]{game?.Players?.Count.ToString()} [red]Players in [/]{game?.Stats.ScoreCalculationTime.ToString(CultureInfo.InvariantCulture)}[red] milliseconds.[/]");
        AnsiConsole.MarkupLineInterpolated($"[red]Press any key to exit...[/]");
        System.Console.ReadKey(true);
        Environment.Exit(0);
    }
}
