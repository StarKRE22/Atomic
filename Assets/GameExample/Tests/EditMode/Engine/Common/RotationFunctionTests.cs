using System.Collections.Generic;
using NUnit.Framework;
using Unity.Mathematics;
using UnityEngine;

namespace GameExample.Engine
{
    [TestFixture]
    public sealed class RotationFunctionTests
    {
        [TestCaseSource(nameof(AddTestCases))]
        public void RotateStep(quaternion rotation, float3 direction, float speed, quaternion exprectedResult)
        {
            RotationFunctions.RotateStep(rotation, direction, deltaTime: 0.1f, speed, out quaternion newRotation);
            Assert.IsTrue(math.length(exprectedResult.value - newRotation.value) <= 0.1f);
        }

        private static IEnumerable<TestCaseData> AddTestCases()
        {
            yield return new TestCaseData(new quaternion(0f, 0.8509035f, 0f, 0.525322f), new float3(0, 0, 1), 5, new quaternion(0f, 0.4871745f, 0f, 0.8733046f));
            yield return new TestCaseData(new quaternion(0f, 0.8509035f, 0f, 0.525322f), new float3(0, 0, 0), 5, new quaternion(0f, 0.8509035f, 0f, 0.525322f));
        }
    }
}