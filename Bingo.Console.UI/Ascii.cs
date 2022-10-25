﻿using System.Text;
using Spectre.Console;

namespace Bingo.Console.UI;

internal static class Ascii
{
    private const string _logo = @"
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

    private const string _logoHighestRes = @"                                                                                                                                                                                   
                                                                                                                                                                                   
                                                                                                                                                                                   
                                                                                        ,,,                                                                                        
                                                                                       ,,,,,                                                                                       
                                                                                   .,,,,,,,,,,,,,,                                                                                 
                                                                                ,,,,,,........ .,,,,,,,                                                                            
                                                                            ,,,.......................,,,,,.                                                                       
                                                                        ,,,...............................,,,,,                                                                    
                                                                    ,,,,........ .,,,,,,,,,,,,,,,, ..........,,,,,                                                                 
                                                                .,,,,........ ,,,,,,,,,,,,,,,,,,,,,,,, ....... ,,,,,                                                               
                                                              ,,,, ........,,,,,,,,,,,,,,,,,,,,,,,,,,,,,........ ,,,,,                                                             
                                                            ,,,,..........,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,, ....... ,,,,,                                                           
                                                          ,,,............,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,, .........,,,,,                                                         
                                                        ,,, ........... ,,,,,,,,,,,,,,,........,,,,,,,,,,,,,..........,,,,,                                                        
                                                      ,,, ............. ,,,,,,,,,,,,,,.........,,,,,,,,,,,,, ...........,,,,,,,,,,                                                 
                                                 ,,,,,,,,,............. ,,,,,,,,,,,,,,,........,,,,,,,,,,,,,...........,,,,,,,,                                                    
                                              .,,,,,,,,,,,,, ...........,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,..........,,,,,,,,,,,,,.                                                
                                       .,,,,,,,,,,,,,,,,,,,,,,............,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,, ........,,,,,,,,,,,,,,,,,,,,,,,,,                                       
                                   .,,,,,,,,,,,,,,,,,,,,,,,,,,,,,......... ,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,....... ,,,,,,,,,,,,. ...... .,,,,,,,,,,                                  
                               .,,,,,,,......................,,,,,,,.........,,,,,,,,,,,,,,,,,,,,,,,,, ........,,,,,,,...................... ,,,,,,,.                              
                            ,,,,,,................................,,,,,,........ ,,,,,,,,,,,,,,,,,, ........,,,,,,...............................,,,,,,                            
                         ,,,,,,...........  ,,,,,,,,,,,,  .......... ,,,,,,,..........       .......... ,,,,,,,...........  ,,,,,,,,,,. ...........,,,,,,.                         
                      ,,,,,,.......... ,,,,,,,,,,,,,,,,,,,,,, ..........,,,,,,,,,. .............. .,,,,,,,,,.......... ,,,,,,,,,,,,,,,,,,,, ..........,,,,,.                       
                    ,,,,,............,,,,,,,,,,,,,,,,,,,,,,,,,,,...........,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,...........,,,,,,,,,,,,,,,,,,,,,,,,,..........,,,,,                      
                  ,,,,............ ,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,........... ,,,,,,,,,,,,,,,,,,,,,,,,,,............,,,,,,,,,,,,,,,,,,,,,,,,,,,,...........,,,,,                    
          ,,,,,,,,,, ............ ,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,, ............,,,,,,,,,,,,,,,,,,,,,, ............,,,,,,,,,,,,...... ,,,,,,,,,,,,..........,,,,,,,,,,,             
            .,,,,,, .............,,,,,,,,,,,,.........,,,,,,,,,,,,, ..............,,,,,,,,,...,,,,,,,........... ,,,,,,,,,,,,......... ,,,,,,,,,, ........ ,,,,,,,,,               
                 .,,,........... ,,,,,,,,,,,,.........,,,,,,,,,,,,, ............,,,,,,            .,,,.......... ,,,,,,,,,,,,......... ,,,,,,,,,, .......,,,,,,                    
                   ,,,,..........,,,,,,,,,,,,,.......,,,,,,,,,,,,,, ..........,,,,,,                .,,,.........,,,,,,,,,,,,,...... ,,,,,,,,,,,, ......,,,,,                      
                     ,,,,........ ,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,, .........,,,,,,                     ,,, ...... ,,,,,,,,,,,,,,,,,,,,,,,,,,,,,, .... ,,,,,,                       
                      ,,,,,....... ,,,,,,,,,,,,,,,,,,,,,,,,,,,,,, ........,,,,,,                         ,,,.......,,,,,,,,,,,,,,,,,,,,,,,,,,,, ....,,,,,, ,,,                     
                    ,,,.,,,,,........,,,,,,,,,,,,,,,,,,,,,,,,,,,....... ,,,,,,                             .,,,.......,,,,,,,,,,,,,,,,,,,,,,,.... ,,,,,,                           
                           ,,,,, ..... ,,,,,,,,,,,,,,,,,,,,,, .......,,,,,,,                                  ,,,,.......,,,,,,,,,,,,,,,,......,,,,,,,                             
                              ,,,,, ......  ,,,,,,,,,,,,. ....... ,,,,,,,                                       .,,,, ........      ........,,,,,,,,,,                             
                            ,,,..,,,,,, ..................... ,,,,,,,.                                             ,,,,,,. .............,,,,,,,,                                   
                                    .,,,,,,,,. ....... .,,,,,,,,.                                                     .,,,,,,,,,,,,,,,,,,,,,                                       
                                              .,,,,,,,,,,.                                                                   .,,,,,,,,                                             
                                               ,,                                                                                   ,                                              
                                                                                                                                                                                   
                                                                                                                                                                                   
                                                                                                                                                                                   
                                                                                                                                                                                   
                                                                  .,,,.               .,,,.               .,,,.                                                                   
                                                                 ,,,,,,.             ,,,,,,,             ,,,,,,,                                                                   
                                                                  .,,,.               .,,,.               .,,,.";

    private const string _logo256 = @"                                                                                      
                                                                                      
                                                                                      
                                                                                      
                                                                                      
                                          ..                                          
                                         ,,,,,                                        
                                     ,,.........,,,                                   
                                 ,,.....,,,,,,......,,.                               
                             .,,....,,,,,,,,,,,,,,....,,,                             
                           ,,......,,,,,,,,,,,,,,,,,....,,,                           
                         .,.......,,,,,,,.....,,,,,,......,,.                         
                        ,,,.......,,,,,,,,...,,,,,,,.....,,,,                         
                .,,,,,,,,,,,,,.....,,,,,,,,,,,,,,,,....,,,,,,,,,,,,,,,                
             ,,,..............,,,.....,,,,,,,,,,,....,,,,.............,,,             
         .,,.......,,,,,,,,......,,,,............,,,,......,,,,,,,,......,,,          
       ,,,......,,,,,,,,,,,,,,......,,,,,,,,,,,,,,,.....,,,,,,,,,,,,,,.....,,.        
   ,,,,,...... ,,,,,,...,,,,,,,.......,,,,,,,,,,.......,,,,,,.....,,,,,.....,,,,,     
       ,,.....,,,,,,.....,,,,,,,......,,,       ,,.....,,,,,,.....,,,,,....,,,        
        .,,....,,,,,,,,,,,,,,,,.....,,,           ,,....,,,,,,,,,,,,,, ..,,,,         
        . .,,....,,,,,,,,,,,,.....,,,               .,....,,,,,,,,,,...,,,            
            .,,,......,,.......,,,                     ,,...........,,,,,,            
                 ,,,,,,,,,,,,.                            .,,,,,,,,.                  
                     ..                                                               
                                                                                      
                                          .         .                                 
                               ,,,       ,,,       ,,,";
    public static void Title()
    {
        // TODO - Better solution to center logo
        // var spaces = new StringBuilder();
        //
        // for (var i = 0; i < (Console.WindowWidth / 2); i++)
        // {
        //     spaces.Append(' ');
        // }

        AnsiConsole.MarkupInterpolated($"[red]{_logo256}[/]");
        AnsiConsole.Write(Environment.NewLine);
        AnsiConsole.Write(Environment.NewLine);
        AnsiConsole.Write(Environment.NewLine);
        AnsiConsole.Write(Environment.NewLine);
    }
}
