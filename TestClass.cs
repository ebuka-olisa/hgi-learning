using Xunit;

namespace HGILearning
{
    public class TestClass
    {
        [Theory]
        [InlineData(0, @"C:\Users\USER\Documents\Ebuka\Work\Sanger\HGI-Coding-Task\hgi-learning\samples\sample_fastq.txt")]
        [InlineData(1, @"C:\Users\USER\Documents\Ebuka\Work\Sanger\HGI-Coding-Task\hgi-learning\samples\sample_fastq2.txt")]
        [InlineData(3, @"C:\Users\USER\Documents\Ebuka\Work\Sanger\HGI-Coding-Task\hgi-learning\samples\sample_fastq3.txt")]
        [InlineData(1, @"C:\Users\USER\Documents\Ebuka\Work\Sanger\HGI-Coding-Task\hgi-learning\samples\sample_fastq2.txt.gz")]
        [InlineData(6, @"C:\Users\USER\Documents\Ebuka\Work\Sanger\HGI-Coding-Task\hgi-learning\samples\sample_fastq4.txt.gz")]
        public void PassSequenceCountTest(int count, string filePath)
        {
            Assert.Equal(count, FASTQProcessor.NumberOfSequences(filePath));
        }

        [Theory]
        [InlineData(0, @"C:\Users\USER\Documents\Ebuka\Work\Sanger\HGI-Coding-Task\hgi-learning\samples\sample_fastq.txt")]
        [InlineData(60, @"C:\Users\USER\Documents\Ebuka\Work\Sanger\HGI-Coding-Task\hgi-learning\samples\sample_fastq2.txt")]
        [InlineData(180, @"C:\Users\USER\Documents\Ebuka\Work\Sanger\HGI-Coding-Task\hgi-learning\samples\sample_fastq3.txt")]
        [InlineData(60, @"C:\Users\USER\Documents\Ebuka\Work\Sanger\HGI-Coding-Task\hgi-learning\samples\sample_fastq2.txt.gz")]
        [InlineData(360, @"C:\Users\USER\Documents\Ebuka\Work\Sanger\HGI-Coding-Task\hgi-learning\samples\sample_fastq4.txt.gz")]
        public void PassNucleotideCountTest(int count, string filePath)
        {
            Assert.Equal(count, FASTQProcessor.NumberOfNucleotides(filePath));
        }
    }
}