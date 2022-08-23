using FactChallenge.Infrastructure.Core.DataSource;
using FactChallenge.Infrastructure.Core.Interfaces;
using FactChallenge.Infrastructure.Core.Services;
using FactChallenge.Infrastructure.Model;

namespace FactChallenge.Tests
{
    public class FactServiceTest
    {
        private IFactService _factService;
        private IDataPath _dataPath;


        [SetUp]
        public void Setup()
        {
            _dataPath = new DataPath();
            _factService = new FactService(_dataPath);
        }

        [Test]
        public async Task ShouldBeAbleTo_CreateCatFact()
        {
            CatFactModel catFact = new()
            {
                Length = 83,
                Fact = "Cats have 'nine lives' thanks to a flexible spine and powerful leg and back muscles"
            };

            var result = await _factService.CreateFact(catFact);

            Assert.That(catFact, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(catFact, Is.SameAs(result));
            });
        }

        [Test]
        public async Task ShouldBeAbleTo_GetRandomCatFact()
        {

            int maxLength = 200;
            var result = await _factService.GetRandomFacts(maxLength);

            Assert.That(result?.Length, Is.GreaterThan(0));
            Assert.That(result, Is.Not.EqualTo(null));
        }

        [Test]
        public async Task ShouldBeAbleTo_GetAllFacts()
        {

            int maxLength = 200;

            var result = await _factService.GetFacts(1, maxLength);

            Assert.That(result.Count, Is.GreaterThanOrEqualTo(0));
            Assert.That(maxLength, Is.GreaterThanOrEqualTo(0));

        }
    }
}