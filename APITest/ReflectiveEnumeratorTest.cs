using System;
using System.Linq;
using API.Controllers.Pipeline.Steps;
using API.Infrastructure;
using Moq;
using Xunit;

namespace APITest
{
    public class ReflectiveEnumeratorTest
    {
        [Fact]
        public void ShouldReturnAllTypesWithIncjectedServices()
        {
            // arrange
            var provider = new Mock<IServiceProvider>();

            // act
            var steps = ReflectiveEnumerator.GetEnumerableOfType<IPipelineStep>(provider.Object);

            // assert
            Assert.Equal(4, steps.Count());
        }
    }
}