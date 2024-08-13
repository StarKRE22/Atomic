using NUnit.Framework;

namespace Atomic.Elements
{
    [TestFixture]
    public sealed class StopwatchTests
    {
        [Test]
        public void Instantiate()
        {
            //Arrange:
            Stopwatch stopwatch = new Stopwatch();

            //Assert:
            Assert.AreEqual(Stopwatch.State.IDLE, stopwatch.GetCurrentState());
            Assert.AreEqual(0, stopwatch.GetCurrentTime());
        }
        
        [Test]
        public void Start()
        {
            //Arrange:
            Stopwatch stopwatch = new Stopwatch();
            bool wasEvent = false;
            Stopwatch.State stateChanged = default;
        
            //Act:
            stopwatch.OnStarted += () => wasEvent = true;
            stopwatch.OnStateChanged += s => stateChanged = s;
            stopwatch.Start();
        
            //Assert:
            Assert.IsTrue(wasEvent);
            Assert.AreEqual(Stopwatch.State.PLAYING, stateChanged);
            Assert.AreEqual(Stopwatch.State.PLAYING, stopwatch.GetCurrentState());
            Assert.AreEqual(0, stopwatch.GetCurrentTime());
            
            Assert.IsTrue(stopwatch.IsPlaying());
        }
        
        [Test]
        public void WhenTickNotStartedThenNothing()
        {
            //Arrange:
            Stopwatch stopwatch = new Stopwatch();
            bool wasTimeEvent = false;
        
            //Act:
            stopwatch.OnCurrentTimeChanged += _ => wasTimeEvent = true;
            stopwatch.Tick(deltaTime: 0.5f);
        
            //Assert:
            Assert.IsFalse(stopwatch.IsPlaying());
            Assert.IsFalse(wasTimeEvent);
        
            Assert.AreEqual(0, stopwatch.GetCurrentTime());
        }
        
        
        [Test]
        public void Pause()
        {
            //Arrange:
            Stopwatch stopwatch = new Stopwatch();
            bool wasPause = false;
            Stopwatch.State stateChanged = default;
        
            stopwatch.Start();
        
            //Act:
            stopwatch.OnStateChanged += s => stateChanged = s;
            stopwatch.OnPaused += () => wasPause = true;
            stopwatch.Pause();
        
            //Assert:
            Assert.IsTrue(wasPause);
            Assert.AreEqual(Stopwatch.State.PAUSED, stateChanged);
            Assert.AreEqual(Stopwatch.State.PAUSED, stopwatch.GetCurrentState());
            Assert.IsTrue(stopwatch.IsPaused());
        }
        
        [Test]
        public void WhenTickInPausedThenNothing()
        {
            //Arrange:
            Stopwatch stopwatch = new Stopwatch();
            bool timeChanged = false;
        
            //Act:
            stopwatch.Start();
            stopwatch.Tick(0.2f);
            stopwatch.OnCurrentTimeChanged += _ => timeChanged = true;
        
            stopwatch.Pause();
        
            for (int i = 0; i < 5; i++)
            {
                stopwatch.Tick(deltaTime: 0.2f);
            }
        
            //Assert:
            Assert.IsTrue(stopwatch.IsPaused());
            Assert.AreEqual(0.2f, stopwatch.GetCurrentTime(), 1e-2);
            Assert.IsFalse(timeChanged);
        }
        
        [Test]
        public void WhenPauseStopwatchThatNotStartedThenNothing()
        {
            //Arrange:
            Stopwatch stopwatch = new Stopwatch();
            bool wasPause = false;
        
            //Act:
            stopwatch.OnPaused += () => wasPause = true;
            stopwatch.Pause();
        
            //Assert:
            Assert.IsFalse(wasPause);
            Assert.IsFalse(stopwatch.IsPaused());
            Assert.IsTrue(stopwatch.IsIdle());
        }
        
        [Test]
        public void Resume()
        {
            //Arrange:
            Stopwatch stopwatch = new Stopwatch();
            bool wasResume = false;
            Stopwatch.State stateChanged = default;
        
            stopwatch.Start();
            stopwatch.Pause();
        
            //Pre-assert:
            Assert.IsTrue(stopwatch.IsPaused());
        
            //Act:
            stopwatch.OnStateChanged += s => stateChanged = s;
            stopwatch.OnResumed += () => wasResume = true;
            stopwatch.Resume();
        
            //Assert:
            Assert.IsTrue(wasResume);
            Assert.AreEqual(Stopwatch.State.PLAYING, stateChanged);
            Assert.AreEqual(Stopwatch.State.PLAYING, stopwatch.GetCurrentState());
        
            Assert.IsFalse(stopwatch.IsPaused());
            Assert.IsTrue(stopwatch.IsPlaying());
        }
        
        [Test]
        public void WhenResumeThatNotStartedThenNothing()
        {
            //Arrange:
            Stopwatch stopwatch = new Stopwatch();
            bool wasResume = false;
        
            //Act:
            stopwatch.OnResumed += () => wasResume = true;
            stopwatch.Resume();
        
            //Assert:
            Assert.IsFalse(wasResume);
            Assert.AreEqual(Stopwatch.State.IDLE, stopwatch.GetCurrentState());
            Assert.IsFalse(stopwatch.IsPlaying());
            Assert.IsFalse(stopwatch.IsPaused());
            Assert.IsTrue(stopwatch.IsIdle());
        }
        
        [Test]
        public void WhenTickThenCurrentTimeChanged()
        {
            //Arrange:
            Stopwatch stopwatch = new Stopwatch();
            float currentTime = -1;
        
            //Act:
            stopwatch.Start();
            stopwatch.OnCurrentTimeChanged += t => currentTime = t;
            stopwatch.Tick(deltaTime: 0.5f);
        
            //Assert:
            Assert.AreEqual(0.5f, currentTime, 1e-2);
            Assert.AreEqual(0.5f, stopwatch.GetCurrentTime(), 1e-2);
        }
        
        [Test]
        public void WhenStartStopwatchThatAlreadyStartedThenFailed()
        {
            //Arrange:
            Stopwatch stopwatch = new Stopwatch();
            bool wasEvent = false;
        
            stopwatch.Start();
            stopwatch.Tick(deltaTime: 1);
            stopwatch.OnStarted += () => wasEvent = true;
        
            //Act:
            stopwatch.Start();
        
            //Assert:
            Assert.IsFalse(wasEvent);
            Assert.AreEqual(1, stopwatch.GetCurrentTime());
        }

        [Test]
        public void Stop()
        {
            //Arrange:
            Stopwatch stopwatch = new Stopwatch();
            bool wasStop = false;
            Stopwatch.State stateChanged = default;
        
            stopwatch.Start();
        
            //Act:
            stopwatch.OnStateChanged += s => stateChanged = s;
            stopwatch.OnStopped += () => wasStop = true;
            stopwatch.Stop();
        
            //Assert:
            Assert.IsTrue(wasStop);
            Assert.AreEqual(Stopwatch.State.IDLE, stopwatch.GetCurrentState());
            Assert.AreEqual(Stopwatch.State.IDLE, stateChanged);
        
            Assert.IsFalse(stopwatch.IsPlaying());
            Assert.IsTrue(stopwatch.IsIdle());
        
            Assert.AreEqual(0, stopwatch.GetCurrentTime());
        }
        
        [Test]
        public void WhenStopNotStartedStopwatchThenNoEvent()
        {
            //Arrange:
            Stopwatch stopwatch = new Stopwatch();
            bool wasEvent = false;
        
            //Act:
            stopwatch.OnStopped += () => wasEvent = true;
            stopwatch.Stop();
        
            //Assert:
            Assert.IsFalse(wasEvent);
        }
        
        [Test]
        public void WhenResumeStopwatchThatIsNotPausedThenNothing()
        {
            //Arrange:
            Stopwatch stopwatch = new Stopwatch();
            bool wasResume = false;
            stopwatch.OnResumed += () => wasResume = true;
            stopwatch.Start();
        
            //Pre-assert:
            Assert.IsTrue(!stopwatch.IsPaused());
        
            //Act:
            stopwatch.Resume();
        
            //Assert:
            Assert.IsFalse(wasResume);
        }
        
        [Test]
        public void WhenStopPausedStopwatchThenWillIdle()
        {
            //Arrange:
            Stopwatch stopwatch = new Stopwatch();
            bool canceled = false;
        
            stopwatch.Start();
            stopwatch.Pause();
        
            Assert.IsTrue(stopwatch.IsPaused());
        
            //Act:
            stopwatch.OnStopped += () => canceled = true;
            stopwatch.Stop();
        
            //Assert:
            Assert.IsTrue(canceled);
        
            Assert.IsFalse(stopwatch.IsPaused());
            Assert.IsFalse(stopwatch.IsPlaying());
            Assert.IsTrue(stopwatch.IsIdle());
            Assert.AreEqual(0, stopwatch.CurrentTime);
        }
    }
}