namespace ParseOrders
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage:  {0} input_file\n\tinput_file:\t file containing orders to parse",
                    Path.GetFileNameWithoutExtension(Environment.GetCommandLineArgs()[0]));
                Environment.Exit(-1);
            }
            if (!File.Exists(args[0]))
            {
                Console.WriteLine("File {0} does not exist.", args[0]);
                Environment.Exit(-1);
            }

            Orders parsedOrders = new();
            
            try
            {
                parsedOrders.ParseFile(args[0]);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("An uncaught error occurred: {0}", ex.Message);
                Environment.ExitCode = -1;
            }
            finally
            {
                Console.WriteLine(String.Format("{0} Orders were processed.{1}",
                    parsedOrders.OrdersList.Count, (parsedOrders.HasErrors ? "  With errors." : "")));
            }

        }
    }
}