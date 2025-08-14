using NUnit.Framework;

namespace Atomic.Elements
{
    public class CooldownTests
    {
        [Test]
        public void Initializes_WithFullTime()
        {
            var cd = new Cooldown(10);
            Assert.AreEqual(10, cd.GetDuration());
            Assert.AreEqual(10, cd.GetTime());
            Assert.IsFalse(cd.IsCompleted());
        }

        [Test]
        public void Initializes_WithCustomCurrentTime()
        {
            var cd = new Cooldown(10, 3);
            Assert.AreEqual(10, cd.GetDuration());
            Assert.AreEqual(3, cd.GetTime());
        }

        [Test]
        public void IsExpired_ReturnsTrue_WhenTimeZero()
        {
            var cd = new Cooldown(5, 0);
            Assert.IsTrue(cd.IsCompleted());
        }

        [Test]
        public void GetProgress_ReturnsCorrectValue()
        {
            var cd = new Cooldown(8, 4);
            Assert.AreEqual(0.5f, cd.GetProgress(), 1e-5f);
        }

        [Test]
        public void Tick_DecreasesTime_AndFiresEvents()
        {
            var cd = new Cooldown(5);
            float? currentTime = null;
            float? progress = null;
            bool expired = false;

            cd.OnTimeChanged += t => currentTime = t;
            cd.OnProgressChanged += p => progress = p;
            cd.OnCompleted += () => expired = true;

            cd.Tick(5);

            Assert.AreEqual(0, cd.GetTime());
            Assert.AreEqual(0, currentTime);
            Assert.AreEqual(0, progress);
            Assert.IsTrue(expired);
        }

        [Test]
        public void SetProgress_UpdatesTime_AndFiresEvents()
        {
            var cd = new Cooldown(10);
            float? changedTime = null;
            float? changedProgress = null;

            cd.OnTimeChanged += t => changedTime = t;
            cd.OnProgressChanged += p => changedProgress = p;

            cd.SetProgress(0.25f);

            Assert.AreEqual(2.5f, changedTime);
            Assert.AreEqual(0.25f, changedProgress);
        }

        [Test]
        public void Reset_SetsToFull_IfNotAlreadyFull()
        {
            var cd = new Cooldown(10, 5);
            float? changedTime = null;
            float? changedProgress = null;

            cd.OnTimeChanged += t => changedTime = t;
            cd.OnProgressChanged += p => changedProgress = p;

            cd.ResetTime();

            Assert.AreEqual(10, changedTime);
            Assert.AreEqual(1f, changedProgress);
        }

        [Test]
        public void SetDuration_UpdatesDuration_AndFiresEvents()
        {
            var cd = new Cooldown(10);
            cd.SetTime(5);

            float? changedDuration = null;
            float? changedProgress = null;

            cd.OnDurationChanged += d => changedDuration = d;
            cd.OnProgressChanged += p => changedProgress = p;

            cd.SetDuration(20);

            Assert.AreEqual(20, cd.GetDuration());
            Assert.AreEqual(20, changedDuration);
            Assert.AreEqual(0.25f, changedProgress);
        }

        [Test]
        public void ToString_ContainsDurationAndCurrent()
        {
            var cd = new Cooldown(7, 3);
            string output = cd.ToString();
            StringAssert.Contains("7", output);
            StringAssert.Contains("3", output);
        }
    }
}