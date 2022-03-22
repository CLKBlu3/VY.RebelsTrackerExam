using System.IO;
using System.Linq;
using VY.RebelsExam.Data.Implementation.Repositories;
using Xunit;

namespace VY.Rebels.Exam.Data.UnitTesting
{
    public class RebelRepositoryTests
    {
        [Fact]
        public async void ReadRebelsFile_ReturnsListWithAllRebels()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "RebelRepository.json");
            RebelRepository rep = new RebelRepository(path);
            var res = await rep.GetAll();
            Assert.NotNull(res);
            Assert.False(res.HasExceptions());
            Assert.False(res.HasErrors());
            Assert.True(res.Result != null);
            Assert.Equal(4, res.Result.Count());
        }
        [Fact]
        public async void ReadNonExistingFile_ReturnsEmptyListWithNoErrors()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "NonExistingFile123456789098765.json");
            RebelRepository rep = new RebelRepository(path);
            var res = await rep.GetAll();
            Assert.NotNull(res);
            Assert.False(res.HasErrors());
            Assert.Empty(res.Result);
        }
    }
}
