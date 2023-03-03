namespace HGILearning
{
    class Program
    {
        enum Operations { CountSequence = 1, CountNucleotide = 2 };

        static void Main(string[] args)
        {
            if (args.Length != 2){
                Console.WriteLine("Please provide the necessary arguments in the format \"dotnet run -- <task-id> <file-path>\" ");
                Console.WriteLine($"<task-id>: {Environment.NewLine}  '1' to get sequence count");
                Console.WriteLine("  '2' to get nucleotide count");
                Console.WriteLine($"<file-path>: absolute path to file being processed {Environment.NewLine}");
                return;
            }

            try{

                int taskId = Int32.Parse(args[0]);
                string filePath = args[1];

                switch(taskId){
                    // Get number of sequences
                    case (int)Operations.CountSequence:
                        Console.WriteLine(FASTQProcessor.NumberOfSequences(filePath));
                        break;
                    // Get number of nucleotides
                    case (int)Operations.CountNucleotide:
                        Console.WriteLine(FASTQProcessor.NumberOfNucleotides(filePath));
                        break;
                    default:
                        Console.WriteLine("Please select a valid <task-id>");
                        Console.WriteLine("\t'1' to get sequence count");
                        Console.WriteLine("\t '2' to get nucleotide count");
                        break;

                }
            }
            catch(FileNotFoundException ex){
                Console.WriteLine(ex.Message);
            }
            catch(FormatException ex){
                Console.WriteLine($"First argument must be a number, please choose 1 or 2. Error message: {ex.Message}");
            }
            catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }
    }
}
