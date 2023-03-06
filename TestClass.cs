using Xunit;

namespace HGILearning
{
    public class TestClass
    {
        [Theory]
        // [InlineData(1, @"C:\Users\USER\Documents\Ebuka\Work\Sanger\HGI-Coding-Task\hgi-learning\samples\sample_fastq2.txt")]
        [InlineData(0, @"\samples\sample_fastq.txt")]
        [InlineData(1, @"\samples\sample_fastq2.txt")]
        [InlineData(3, @"\samples\sample_fastq3.txt")]
        [InlineData(1, @"\samples\sample_fastq2.txt.gz")]
        [InlineData(6, @"\samples\sample_fastq4.txt.gz")]
        public void PassSequenceCountTest(int count, string filePath)
        {
            string absoluteFilePath = $"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName}{filePath}";
            Assert.Equal(count, FASTQProcessor.NumberOfSequences(absoluteFilePath));
        }

        [Theory]
        [InlineData(0, @"\samples\sample_fastq.txt")]
        [InlineData(60, @"\samples\sample_fastq2.txt")]
        [InlineData(180, @"\samples\sample_fastq3.txt")]
        [InlineData(60, @"\samples\sample_fastq2.txt.gz")]
        [InlineData(360, @"\samples\sample_fastq4.txt.gz")]
        public void PassNucleotideCountTest(int count, string filePath)
        {
            string absoluteFilePath = $"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName}{filePath}";
            Assert.Equal(count, FASTQProcessor.NumberOfNucleotides(absoluteFilePath));
        }
    }
}