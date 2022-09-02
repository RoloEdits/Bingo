namespace Bingo;

internal static class Utilities
{
    public static string StringFormat(this string formatedString)
    {
        if (formatedString is null)
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
