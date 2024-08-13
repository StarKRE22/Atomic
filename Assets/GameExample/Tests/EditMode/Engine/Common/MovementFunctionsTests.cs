using System.Collections.Generic;
using NUnit.Framework;
using Unity.Mathematics;

namespace GameExample.Engine
{
    [TestFixture]
    public sealed class MovementFunctionsTests
    {
        [TestCaseSource(nameof(AddTestCases))]
        public void MoveStep(float3 position, float3 direction, float speed, float3 expectedPosition)
        {
            MovementFunctions.MoveStep(position, direction, speed, deltaTime: 0.1f, out float3 newPosition);
            Assert.AreEqual(expectedPosition, newPosition);
        }

        private static IEnumerable<TestCaseData> AddTestCases()
        {
            yield return new TestCaseData(new float3(), new float3(0, 0, 1), 5, new float3(0, 0, 0.5f));
            yield return new TestCaseData(new float3(-10, -10, -10), new float3(1, 0, -1), 3, new float3(-9.7f, -10, -10.3f));
        }
    }
}