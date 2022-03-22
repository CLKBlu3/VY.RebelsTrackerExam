using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using VY.RebelsExam.Business.Contracts.Validations;
using VY.RebelsExam.Business.Implementation.Services;
using VY.RebelsExam.Data.Contracts.Entities;
using VY.RebelsExam.Data.Contracts.Repositories;
using VY.RebelsExam.Dtos.Domain.V1;
using Xunit;

namespace VY.RebelsExam.Business.UnitTesting
{
    public class RebelServiceTests
    {
        [Fact]
        public void RebelListWithExistingRebels_ReturnsUpdatedValuesForDuplicatedElements()
        {
            IEnumerable<Rebel> ogRebels = new List<Rebel>()
            {
                new Rebel(){ Name = "Hans Solo", Date = DateTime.Now, Planet = "Mars" }
            };
            IEnumerable<Rebel> newRebels = new List<Rebel>()
            {
                new Rebel() { Name = "Hans Solo", Date = DateTime.Now, Planet = "Jupiter"}
            };
            Mock<IMapper> mapper = new Mock<IMapper>();
            Mock<IRebelRepository> repo = new Mock<IRebelRepository>();
            Mock<IValidation<RebelDto>> validations = new Mock<IValidation<RebelDto>>();
            Mock<ILogger<RebelService>> logger = new Mock<ILogger<RebelService>>();

            RebelService rs = new RebelService(mapper.Object, logger.Object, repo.Object, validations.Object);
            List<Rebel> result = rs.UpdateRebels(ogRebels, newRebels);

            bool res = false;
            for(int i = 0; i < result.Count; ++i)
            {
                if(result[i].Name == newRebels.ElementAt(0).Name && result[i].Planet == newRebels.ElementAt(0).Planet)
                {
                    res = true;
                }
            }
            Assert.True(result.Any());
            Assert.True(result.Count == 1);
            Assert.True(res);
        }

        [Fact]
        public void RebelListWithNewRebels_ReturnsUpdatedListWithAllRebels()
        {
            IEnumerable<Rebel> ogRebels = new List<Rebel>()
            {
                new Rebel(){ Name = "Hans Solo", Date = DateTime.Now, Planet = "Mars" }
            };
            IEnumerable<Rebel> newRebels = new List<Rebel>()
            {
                new Rebel() { Name = "Luke", Date = DateTime.Now, Planet = "Jupiter"}
            };
            Mock<IMapper> mapper = new Mock<IMapper>();
            Mock<IRebelRepository> repo = new Mock<IRebelRepository>();
            Mock<IValidation<RebelDto>> validations = new Mock<IValidation<RebelDto>>();
            Mock<ILogger<RebelService>> logger = new Mock<ILogger<RebelService>>();

            RebelService rs = new RebelService(mapper.Object, logger.Object, repo.Object, validations.Object);
            List<Rebel> result = rs.UpdateRebels(ogRebels, newRebels);

            bool res = result.Any(x => x.Name.Equals(ogRebels.First().Name));
            bool res2 = result.Any(x => x.Name.Equals(newRebels.First().Name));

            Assert.True(result.Any());
            Assert.True(result.Count == 2);
            Assert.True(res&res2);
        }

        [Fact]
        public void AddingNewRebelToEmptyListOfRebels_ReturnsAddedRebel()
        {
            IEnumerable<Rebel> ogRebels = new List<Rebel>()
            {
            };
            IEnumerable<Rebel> newRebels = new List<Rebel>()
            {
                new Rebel() { Name = "Hans Solo", Date = DateTime.Now, Planet = "Jupiter"}
            };
            Mock<IMapper> mapper = new Mock<IMapper>();
            Mock<IRebelRepository> repo = new Mock<IRebelRepository>();
            Mock<IValidation<RebelDto>> validations = new Mock<IValidation<RebelDto>>();
            Mock<ILogger<RebelService>> logger = new Mock<ILogger<RebelService>>();

            RebelService rs = new RebelService(mapper.Object, logger.Object, repo.Object, validations.Object);
            List<Rebel> result = rs.UpdateRebels(ogRebels, newRebels);

            bool res = result.Any(x => x.Name.Equals(newRebels.First().Name));
            Assert.True(result.Any());
            Assert.True(result.Count == 1);
            Assert.True(res);
        }
    }
}
