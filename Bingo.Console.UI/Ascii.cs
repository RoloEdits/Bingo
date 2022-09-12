using System.Text;
using Spectre.Console;

namespace Bingo.Console.UI;

internal static class Ascii
{
    public const string Logo = @"
                                                           .,.
                                                        .,*****,,.                                  
                                                    .,,,.........,,,,.                              
                                                ..,,,..,,,*****,,...,,,,.                           
                                              .,,,..,,************,,...,,,.                         
                                            .,,,...,,******,,,*****,,...,,*,.                       
                                         .,,,......,,****,,...,,****,....,,**,,.                    
                                      .,,,***,,....,,*******,,*****,,...,,***,,,...                 
                                 .,,**,,,,,,,**,,...,,************,,..,,***,,,,,,,,**,,.            
                              .,*,,............,,,*,,..,,******,,...,,*,,............,,*,,          
                           .,*,,....,,*******,,....,,**,,,.....,,,**,,...,,,******,,...,,**.        
                          ,,,....,,************,,....,,***********,,...,,************,,...,,,.      
                         *,,.....,****,,..,,****,,.....,,**,,,,,*,,....,****,,...,,***,....,**   
                          ,,,...,,****,,..,,****,,....,,,,.     .,,,...,*****,..,,****,..,,,,       
                           .,,,..,,*************,...,,*,.         .,,,.,,************,,,,*,,       
                             .,,,,.,,*********,,..,,*,.             ..,,..,,******,,.,,*,.          
                              .,,,,,,..........,,*,..                  .,*,,.....,,,*,,..         
                                    .,,,**,,,,..                          ..,,,,*,..                
                                                                                                    
                                                .,.        .,.       .,.                                     
                                               .,,,.      .,,,.     .,,,.                            
                                                .,.        .,.       .,.";

    public static void Title()
    {
        // TODO - Better solution to center logo
        // var spaces = new StringBuilder();
        //
        // for (var i = 0; i < (Console.WindowWidth / 2); i++)
        // {
        //     spaces.Append(' ');
        // }

        AnsiConsole.MarkupInterpolated($"[red]{Logo}[/]");
        AnsiConsole.Write(Environment.NewLine);
        AnsiConsole.Write(Environment.NewLine);
        AnsiConsole.Write(Environment.NewLine);
        AnsiConsole.Write(Environment.NewLine);
    }
}
