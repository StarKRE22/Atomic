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
            Assert.AreEqual(StopwatchState.IDLE, stopwatch.GetState());
            Assert.AreEqual(0, stopwatch.GetTime());
        }
        
        [Test]
        public void Start()
        {
            //Arrange:
            Stopwatch stopwatch = new Stopwatch();
            bool wasEvent = false;
            StopwatchState stateChanged = default;
        
            //Act:
            stopwatch.OnStarted += () => wasEvent = true;
            stopwatch.OnStateChanged += s => stateChanged = s;
            stopwatch.Start();
        
            //Assert:
            Assert.IsTrue(wasEvent);
            Assert.AreEqual(StopwatchState.PLAYING, stateChanged);
            Assert.AreEqual(StopwatchState.PLAYING, stopwatch.GetState());
            Assert.AreEqual(0, stopwatch.GetTime());
            
            Assert.IsTrue(stopwatch.IsStarted());
        }
        
        [Test]
        public void Play()
        {
            //Arrange:
            Stopwatch stopwatch = new Stopwatch();
            bool wasEvent = false;
            StopwatchState stateChanged = default;

            stopwatch.OnStarted += () => wasEvent = true;
            stopwatch.OnStateChanged += s => stateChanged = s;

            //Act:
            stopwatch.Start(4);

            //Assert:
            Assert.IsTrue(stopwatch.IsStarted());

            Assert.AreEqual(StopwatchState.PLAYING, stateChanged);
            Assert.AreEqual(StopwatchState.PLAYING, stopwatch.GetState());
            Assert.AreEqual(4, stopwatch.GetTime());

            Assert.IsTrue(wasEvent);
        }
        
        [Test]
        public void WhenTickNotStartedThenNothing()
        {
            //Arrange:
            Stopwatch stopwatch = new Stopwatch();
            bool wasTimeEvent = false;
        
            //Act:
            stopwatch.OnTimeChanged += _ => wasTimeEvent = true;
            stopwatch.Tick(deltaTime: 0.5f);
        
            //Assert:
            Assert.IsFalse(stopwatch.IsStarted());
            Assert.IsFalse(wasTimeEvent);
        
            Assert.AreEqual(0, stopwatch.GetTime());
        }
        
        
        [Test]
        public void Pause()
        {
            //Arrange:
            Stopwatch stopwatch = new Stopwatch();
            bool wasPause = false;
            StopwatchState stateChanged = default;
        
            stopwatch.Start();
        
            //Act:
            stopwatch.OnStateChanged += s => stateChanged = s;
            stopwatch.OnPaused += () => wasPause = true;
            stopwatch.Pause();
        
            //Assert:
            Assert.IsTrue(wasPause);
            Assert.AreEqual(StopwatchState.PAUSED, stateChanged);
            Assert.AreEqual(StopwatchState.PAUSED, stopwatch.GetState());
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
            stopwatch.OnTimeChanged += _ => timeChanged = true;
        
            stopwatch.Pause();
        
            for (int i = 0; i < 5; i++)
            {
                stopwatch.Tick(deltaTime: 0.2f);
            }
        
            //Assert:
            Assert.IsTrue(stopwatch.IsPaused());
            Assert.AreEqual(0.2f, stopwatch.GetTime(), 1e-2);
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
            StopwatchState stateChanged = default;
        
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
            Assert.AreEqual(StopwatchState.PLAYING, stateChanged);
            Assert.AreEqual(StopwatchState.PLAYING, stopwatch.GetState());
        
            Assert.IsFalse(stopwatch.IsPaused());
            Assert.IsTrue(stopwatch.IsStarted());
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
            Assert.AreEqual(StopwatchState.IDLE, stopwatch.GetState());
            Assert.IsFalse(stopwatch.IsStarted());
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
            stopwatch.OnTimeChanged += t => currentTime = t;
            stopwatch.Tick(deltaTime: 0.5f);
        
            //Assert:
            Assert.AreEqual(0.5f, currentTime, 1e-2);
            Assert.AreEqual(0.5f, stopwatch.GetTime(), 1e-2);
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
            Assert.AreEqual(1, stopwatch.GetTime());
        }

        [Test]
        public void Stop()
        {
            //Arrange:
            Stopwatch stopwatch = new Stopwatch();
            bool wasStop = false;
            StopwatchState stateChanged = default;
        
            stopwatch.Start();
        
            //Act:
            stopwatch.OnStateChanged += s => stateChanged = s;
            stopwatch.OnStopped += () => wasStop = true;
            stopwatch.Stop();
        
            //Assert:
            Assert.IsTrue(wasStop);
            Assert.AreEqual(StopwatchState.IDLE, stopwatch.GetState());
            Assert.AreEqual(StopwatchState.IDLE, stateChanged);
        
            Assert.IsFalse(stopwatch.IsStarted());
            Assert.IsTrue(stopwatch.IsIdle());
        
            Assert.AreEqual(0, stopwatch.GetTime());
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
            Assert.IsFalse(stopwatch.IsStarted());
            Assert.IsTrue(stopwatch.IsIdle());
            Assert.AreEqual(0, stopwatch.Time);
        }
    }
}