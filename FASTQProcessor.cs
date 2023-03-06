using System.IO.Compression;

namespace HGILearning
{
    public class FASTQProcessor
    {
        // Calculates the number of sequences in a FASTQ file
        public static int NumberOfSequences(string filePath)
        {
            int nonBlankLineCounter = 0;

            if (File.Exists(filePath))
            {
                if (Path.GetExtension(filePath).Equals(".gz"))
                {
                    nonBlankLineCounter = CountSequencesInCompressedFile(filePath);
                }
                else
                {
                    nonBlankLineCounter = CountSequencesInTextFile(filePath);
                }
            }
            else throw new FileNotFoundException($"Could not find file at: {filePath}");

            // Division by 4 must only be done on a number that is not zero
            if (nonBlankLineCounter > 0)
                return nonBlankLineCounter / 4;
            else
                return 0;
        }

        // Count sequences in an uncompressed file format
        private static int CountSequencesInTextFile(string filePath)
        {
            int nonBlankLineCounter = 0;

            // Read the file one line at a time
            // This helps with memory management
            foreach (string line in File.ReadLines(filePath))
            {
                // increase this count if the line found is not blank
                if (!string.IsNullOrWhiteSpace(line))
                {
                    nonBlankLineCounter++;
                }
            }

            return nonBlankLineCounter;
        }

        // Count sequences in a compressed file format
        private static int CountSequencesInCompressedFile(string filePath)
        {
            int nonBlankLineCounter = 0;

            using (Stream fileStream = File.OpenRead(filePath), zippedStream = new GZipStream(fileStream, CompressionMode.Decompress))
            {
                using (StreamReader reader = new StreamReader(zippedStream))
                {
                    // work with reader
                    while (!reader.EndOfStream)
                    {
                        string? line = reader.ReadLine();
                        if (line != null && !string.IsNullOrWhiteSpace(line))
                            nonBlankLineCounter++;
                    }
                }
            }

            return nonBlankLineCounter;
        }



        // Calculates the total number of nucleotides in a FASTQ file
        public static int NumberOfNucleotides(string filePath)
        {
            int nucleotideCount = 0;

            if (File.Exists(filePath))
            {
                if (Path.GetExtension(filePath).Equals(".gz"))
                {
                    nucleotideCount = CountNucleotidesInCompressedFile(filePath);
                }
                else
                {
                    nucleotideCount = CountNucleotidesInPlainFile(filePath);
                }
            }
            else throw new FileNotFoundException($"Could not find file at: {filePath}");

            return nucleotideCount;
        }

        private static int CountNucleotidesInPlainFile(string filePath)
        {
            bool newSequenceStarted = false; // flag to indicate if we have started reading a new sequence in the file
            int nucleotideCount = 0;

            // Read the file one line at a time
            // This helps with memory management
            foreach (string line in File.ReadLines(filePath))
            {
                if (!string.IsNullOrWhiteSpace(line) && newSequenceStarted)
                {
                    nucleotideCount += line.Length; // add length of this nucleotide to the total count
                    newSequenceStarted = false;
                }
                if (IsSequenceFound(line))
                {
                    newSequenceStarted = true;
                    continue;
                }

                (nucleotideCount, newSequenceStarted) = ProcessNucleotideCount(newSequenceStarted, line, nucleotideCount);
            }

            return nucleotideCount;
        }

        private static int CountNucleotidesInCompressedFile(string filePath)
        {
            bool newSequenceStarted = false; // flag to indicate if we have started reading a new sequence in the file
            int nucleotideCount = 0;

            using (Stream fileStream = File.OpenRead(filePath), zippedStream = new GZipStream(fileStream, CompressionMode.Decompress))
            {
                using (StreamReader reader = new StreamReader(zippedStream))
                {
                    // work with reader
                    while (!reader.EndOfStream)
                    {
                        string? line = reader.ReadLine();
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            if (IsSequenceFound(line))
                            {
                                newSequenceStarted = true;
                                continue;
                            }

                            (nucleotideCount, newSequenceStarted) = ProcessNucleotideCount(newSequenceStarted, line, nucleotideCount);

                            if (newSequenceStarted)
                            {
                                nucleotideCount += line.Length; // add length of this nucleotide to the total count
                                newSequenceStarted = false;
                            }
                        }
                    }
                }
            }

            return nucleotideCount;
        }

        private static bool IsSequenceFound(string line)
        {
            if (line.StartsWith("@"))
                return true;
            else return false;
        }

        private static Tuple<int, bool> ProcessNucleotideCount(bool newSequenceFound, string line, int counter)
        {
            if (newSequenceFound)
            {
                counter += line.Length;
                newSequenceFound = false;
            }

            return new Tuple<int, bool>(counter, newSequenceFound);
        }

    }
}