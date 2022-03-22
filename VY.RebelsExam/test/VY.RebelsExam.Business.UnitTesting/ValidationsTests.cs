using System;
using System.Collections.Generic;
using VY.RebelsExam.Business.Implementation.Validations;
using VY.RebelsExam.Data.Contracts.Entities;
using VY.RebelsExam.Dtos.Domain.V1;
using Xunit;

namespace VY.RebelsExam.Business.UnitTesting
{
    public class ValidationsTests
    {
        [Fact]
        public void RebelIsValid_ReturnsTrue()
        {
            RebelDto rebel = new RebelDto()
            {
                Date = DateTime.Today,
                Name = "Hans Solo",
                Planet = "Mars"
            };
            RebelValidation validations = new RebelValidation();
            IEnumerable<RebelDto> rebels = new List<RebelDto>() { rebel };
            bool result = validations.Validate(rebels);
            Assert.True(result);
        }

        [Fact]
        public void RebelNameIsInvalid_ReturnsFalse()
        {
            RebelDto rebel = new RebelDto()
            {
                Date = DateTime.Today,
                Planet = "Mars"
            };
            RebelValidation validations = new RebelValidation();
            IEnumerable<RebelDto> rebels = new List<RebelDto>() { rebel };
            bool result = validations.Validate(rebels);
            Assert.False(result);
        }

        [Fact]
        public void RebelPlanetIsInvalid_ReturnsFalse()
        {
            RebelDto rebel = new RebelDto()
            {
                Date = DateTime.Today,
                Name = "Hans Solo"
            };
            RebelValidation validations = new RebelValidation();
            IEnumerable<RebelDto> rebels = new List<RebelDto>() { rebel };
            bool result = validations.Validate(rebels);
            Assert.False(result);
        }


        [Fact]
        public void RebelDateIsInvalid_ReturnsFalse()
        {
            RebelDto rebel = new RebelDto()
            {
                Name = "Hans Solo",
                Planet = "Mars"
            };
            RebelValidation validations = new RebelValidation();
            IEnumerable<RebelDto> rebels = new List<RebelDto>() { rebel };
            bool result = validations.Validate(rebels);
            Assert.False(result);
        }
    }
}
