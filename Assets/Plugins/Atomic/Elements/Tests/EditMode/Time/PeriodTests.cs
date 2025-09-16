using NUnit.Framework;

namespace Atomic.Elements
{
    [TestFixture]
    public sealed class PeriodTests
    {
        [Test]
        public void Instantiate()
        {
            //Arrange:
            Period period = new Period(5);

            //Assert:
            Assert.AreEqual(5, period.GetDuration());
            Assert.AreEqual(0, period.GetTime());
        }

        [Test]
        public void Start()
        {
            //Arrange:
            Period period = new Period(5);
            bool wasEvent = false;
            PeriodState stateChanged = default;
        
            //Act:
            period.OnStarted += () => wasEvent = true;
            period.OnStateChanged += s => stateChanged = s;
            period.Start();
        
            //Assert:
            Assert.IsTrue(wasEvent);
            Assert.AreEqual(PeriodState.PLAYING, stateChanged);
            Assert.AreEqual(PeriodState.PLAYING, period.GetState());
            Assert.AreEqual(0, period.GetTime());
            
            Assert.IsTrue(period.IsStarted());
        }
        
        
        [Test]
        public void StartWithTime()
        {
            //Arrange:
            Period period = new Period(5);
            bool wasEvent = false;
            PeriodState stateChanged = default;
        
            //Act:
            period.OnStarted += () => wasEvent = true;
            period.OnStateChanged += s => stateChanged = s;
            period.Start(3);
        
            //Assert:
            Assert.AreEqual(PeriodState.PLAYING, stateChanged);
            Assert.AreEqual(PeriodState.PLAYING, period.GetState());
            Assert.AreEqual(3, period.GetTime());
            Assert.IsTrue(wasEvent);
            Assert.IsTrue(period.IsStarted());
        }
        
        [Test]
        public void Play()
        {
            //Arrange:
            Period period = new Period(5);
            bool wasEvent = false;
            PeriodState stateChanged = default;
        
            period.OnStarted += () => wasEvent = true;
            period.OnStateChanged += s => stateChanged = s;

            //Act:
            period.Start(2);

            //Assert:
            Assert.AreEqual(PeriodState.PLAYING, stateChanged);
            Assert.AreEqual(PeriodState.PLAYING, period.GetState());
            Assert.AreEqual(2, period.GetTime());
            
            Assert.IsTrue(wasEvent);
            Assert.IsTrue(period.IsStarted());
            
            //Act:
            period.Tick(deltaTime: 0.5f);
            period.Tick(deltaTime: 0.5f);
            period.Tick(deltaTime: 0.5f);
            period.Tick(deltaTime: 0.5f);
            period.Tick(deltaTime: 0.5f);
            period.Tick(deltaTime: 0.5f);

            Assert.AreEqual(0, period.Time, 1e-2);
        }
        
        [Test]
        public void WhenGetProgressOfNotStartedThenReturnZero()
        {
            Period period = new Period(5);
            Assert.AreEqual(0, period.GetProgress());
        }

        [Test]
        public void WhenTickNotStartedThenNothing()
        {
            //Arrange:
            Period period = new Period(5);
            bool wasTimeEvent = false;
            bool wasProgressEvent = false;
        
            //Act:
            period.OnTimeChanged += _ => wasTimeEvent = true;
            period.OnProgressChanged += _ => wasProgressEvent = true;
            period.Tick(deltaTime: 0.5f);
        
            //Assert:
            Assert.IsFalse(period.IsStarted());
            Assert.IsFalse(wasTimeEvent);
            Assert.IsFalse(wasProgressEvent);
        
            Assert.AreEqual(0, period.GetTime());
            Assert.AreEqual(0, period.GetProgress());
        }

        [Test]
        public void WhenTickThenProgressChanged()
        {
            //Arrange:
            Period period = new Period(5);
            float progress = -1;
        
            //Act:
            period.Start();
            period.OnProgressChanged += p => progress = p;
            period.Tick(deltaTime: 1);
        
            //Assert:
            Assert.AreEqual(0.2f, progress, float.Epsilon);
            Assert.AreEqual(0.2f, period.GetProgress(), float.Epsilon);
        }
        
        [Test]
        public void OnCycle()
        {
            //Arrange:
            Period period = new Period(4);
            bool wasCycle = false;
        
            //Act:
            period.Start();
            period.OnPeriod += () => wasCycle = true;
            
            //Pre-Assert:
            Assert.AreEqual(PeriodState.PLAYING, period.GetState());
            
            for (int i = 0; i < 20; i++)
            {
                period.Tick(deltaTime: 0.2f);
            }
        
            //Assert:
            Assert.IsTrue(wasCycle);
            Assert.AreEqual(PeriodState.PLAYING, period.GetState());
        
            Assert.AreEqual(0, period.GetProgress());
            Assert.AreEqual(0, period.GetTime());
            Assert.AreEqual(4, period.GetDuration());
        
            Assert.IsTrue(period.IsStarted());
            Assert.IsFalse(period.IsPaused());
        }

        [Test]
        public void Pause()
        {
            //Arrange:
            Period period = new Period(1);
            bool wasPause = false;
            PeriodState stateChanged = default;
        
            period.Start();
        
            //Act:
            period.OnStateChanged += s => stateChanged = s;
            period.OnPaused += () => wasPause = true;
            period.Pause();
        
            //Assert:
            Assert.IsTrue(wasPause);
            Assert.AreEqual(PeriodState.PAUSED, stateChanged);
            Assert.AreEqual(PeriodState.PAUSED, period.GetState());
            Assert.IsTrue(period.IsPaused());
        }

        [Test]
        public void WhenTickInPausedThenNothing()
        {
            //Arrange:
            Period period = new Period(0.8f);
            bool progressChanged = false;
            bool timeChanged = false;
            bool wasCycle = false;
        
            //Act:
            period.Start();
            period.Tick(0.2f);
            period.OnProgressChanged += _ => progressChanged = true;
            period.OnTimeChanged += _ => timeChanged = true;
            period.OnPeriod += () => wasCycle = true;
        
            period.Pause();
        
            for (int i = 0; i < 5; i++)
            {
                period.Tick(deltaTime: 0.2f);
            }
        
            //Assert:
            Assert.IsTrue(period.IsPaused());
            Assert.AreEqual(0.2f, period.GetTime(), 1e-2);
            Assert.AreEqual(0.25f, period.GetProgress(), 1e-2);
        
            Assert.IsFalse(progressChanged);
            Assert.IsFalse(timeChanged);
            Assert.IsFalse(wasCycle);
        }
       
        
        [Test]
        public void WhenPauseCycleThatNotStartedThenNothing()
        {
            //Arrange:
            Period period = new Period(0.8f);
            bool wasPause = false;
        
            //Act:
            period.OnPaused += () => wasPause = true;
            period.Pause();
        
            //Assert:
            Assert.IsFalse(wasPause);
            Assert.IsFalse(period.IsPaused());
        }
        
        [Test]
        public void Resume()
        {
            //Arrange:
            Period period = new Period(1);
            bool wasResume = false;
            PeriodState stateChanged = default;
        
            period.Start();
            period.Pause();
        
            //Pre-assert:
            Assert.IsTrue(period.IsPaused());
        
            //Act:
            period.OnStateChanged += s => stateChanged = s;
            period.OnResumed += () => wasResume = true;
            period.Resume();
        
            //Assert:
            Assert.IsTrue(wasResume);
            Assert.AreEqual(PeriodState.PLAYING, stateChanged);
            Assert.AreEqual(PeriodState.PLAYING, period.GetState());
        
            Assert.IsFalse(period.IsPaused());
            Assert.IsTrue(period.IsStarted());
        }
        
        [Test]
        public void WhenResumeThatNotStartedThenNothing()
        {
            //Arrange:
            Period period = new Period(1);
            bool wasResume = false;
        
            //Act:
            period.OnResumed += () => wasResume = true;
            period.Resume();
        
            //Assert:
            Assert.IsFalse(wasResume);
            Assert.AreEqual(PeriodState.IDLE, period.GetState());
            Assert.IsFalse(period.IsStarted());
            Assert.IsFalse(period.IsPaused());
        }
        
        
        [Test]
        public void WhenTickThenCurrentTimeChanged()
        {
            //Arrange:
            Period period = new Period(5);
            float currentTime = -1;
        
            //Act:
            period.Start();
            period.OnTimeChanged += t => currentTime = t;
            period.Tick(deltaTime: 0.5f);
        
            //Assert:
            Assert.AreEqual(0.5f, currentTime, 1e-2);
            Assert.AreEqual(0.5f, period.GetTime(), 1e-2);
        }
        
        [Test]
        public void WhenStartCycleThatAlreadyStartedThenFailed()
        {
            //Arrange:
            Period period = new Period(5);
            bool wasEvent = false;
        
            period.Start();
            period.Tick(deltaTime: 1);
            period.OnStarted += () => wasEvent = true;
        
            //Act:
            period.Start();
        
            //Assert:
            Assert.IsFalse(wasEvent);
            Assert.AreEqual(1, period.GetTime());
            Assert.AreEqual(5, period.GetDuration());
        }
        
        [Test]
        public void Stop()
        {
            //Arrange:
            Period period = new Period(5);
            bool wasStop = false;
            PeriodState stateChanged = default;
        
            period.Start();
        
            //Act:
            period.OnStateChanged += s => stateChanged = s;
            period.OnStopped += () => wasStop = true;
            period.Stop();
        
            //Assert:
            Assert.IsTrue(wasStop);
            Assert.AreEqual(PeriodState.IDLE, period.GetState());
            Assert.AreEqual(PeriodState.IDLE, stateChanged);
        
            Assert.IsFalse(period.IsStarted());
            Assert.IsTrue(period.IsIdle());
        
            Assert.AreEqual(5, period.GetDuration());
            Assert.AreEqual(0, period.GetTime());
        }
        
        
        [Test]
        public void WhenStopNotStartedCycleThenNoEvent()
        {
            //Arrange:
            Period period = new Period(10);
            bool wasEvent = false;
        
            //Act:
            period.OnStopped += () => wasEvent = true;
            period.Stop();
        
            //Assert:
            Assert.IsFalse(wasEvent);
        }
        
        [Test]
        public void WhenResumeProductionThatIsNotPausedThenNothing()
        {
            //Arrange:
            Period period = new Period(10);
            bool wasResume = false;
            period.OnResumed += () => wasResume = true;
            period.Start();
        
            //Pre-assert:
            Assert.IsTrue(!period.IsPaused());
        
            //Act:
            period.Resume();
        
            //Assert:
            Assert.IsFalse(wasResume);
        }
        
        
        [Test]
        public void WhenStopPausedCycleThenWillIdle()
        {
            //Arrange:
            Period period = new Period(5);
            bool canceled = false;
        
            period.Start();
            period.Pause();
        
            Assert.IsTrue(period.IsPaused());
        
            //Act:
            period.OnStopped += () => canceled = true;
            period.Stop();
        
            //Assert:
            Assert.IsTrue(canceled);
        
            Assert.IsFalse(period.IsPaused());
            Assert.IsFalse(period.IsStarted());
            Assert.IsTrue(period.IsIdle());
            Assert.AreEqual(5, period.Duration);
            Assert.AreEqual(0, period.Time);
        }
    }
}