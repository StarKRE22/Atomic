using NUnit.Framework;

namespace Atomic.Elements
{
    public sealed class InternalUtilsTests
    {
        // [TestCase(0, true)]
        // [TestCase(1, true)]
        // [TestCase(2, true)]
        // [TestCase(3, true)]
        // [TestCase(5, true)]
        // [TestCase(7, true)]
        // [TestCase(11, true)]
        // [TestCase(4, false)]
        // [TestCase(9, false)]
        // [TestCase(15, false)]
        // [TestCase(25, false)]
        // [TestCase(29, true)]
        // [TestCase(97, true)]
        // [TestCase(100, false)]
        // [TestCase(101, true)]
        // public void IsPrime(int number, bool expected)
        // {
        //     Assert.AreEqual(expected, InternalUtils.IsPrime(number), $"Failed for number: {number}");
        // }
        //
        // [TestCase(0, 2)]
        // [TestCase(1, 2)]
        // [TestCase(2, 3)]
        // [TestCase(3, 5)]
        // [TestCase(4, 5)]
        // [TestCase(5, 7)]
        // [TestCase(10, 11)]
        // [TestCase(15, 17)]
        // [TestCase(20, 23)]
        // [TestCase(50, 53)]
        // [TestCase(97, 101)]
        // [TestCase(100, 101)]
        // [TestCase(150, 151)]
        // [TestCase(200, 211)]
        // public void NextPrime(int input, int expected)
        // {
        //     Assert.AreEqual(expected, InternalUtils.GetPrime(input), $"Failed for input: {input}");
        // }
    }
}