using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Elements
{
    public sealed partial class ReactiveHashSetTests
    {
        [TestCaseSource(nameof(ContainsTestCases))]
        public bool Contains(ReactiveHashSet<string> set, string value)
        {
            return set.Contains(value);
        }

        private static IEnumerable<TestCaseData> ContainsTestCases()
        {
            var set = new ReactiveHashSet<string>("Vasya", "Petya", "Masha");
            yield return new TestCaseData(set, "Vasya").Returns(true);
            yield return new TestCaseData(set, "Petya").Returns(true);
            yield return new TestCaseData(set, "Masha").Returns(true);
            yield return new TestCaseData(set, "Ivan").Returns(false);
            yield return new TestCaseData(set, null).Returns(false);
        }
    }
}