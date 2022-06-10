using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bingo
{
    internal class Utilities
    {
        public static string StringFormat(string? formatedString)
        {
            // Redundant NULL input handling to allow graceful exit.
            if (formatedString == null)
            {
                Console.Clear();
                AsciiTitle();
                Console.WriteLine("Error: Invalid Input. Press any key to exit program...");
                Console.ReadKey();
                Console.ResetColor();
                Environment.Exit(1);
            }
            return formatedString.Replace(" ", "").Replace("\r\n", "").Replace("\n", "").ToUpper();
        }

        // Holder for the Ascii Art Title
        public static void AsciiTitle()
        {
            Console.WriteLine(@"                                                           .,.                                      
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
                                                .,.        .,.       .,.");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
