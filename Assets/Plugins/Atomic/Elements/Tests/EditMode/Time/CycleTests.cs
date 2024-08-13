using NUnit.Framework;

namespace Atomic.Elements
{
    [TestFixture]
    public sealed class CycleTests
    {
        [Test]
        public void Instantiate()
        {
            //Arrange:
            Cycle cycle = new Cycle(5);

            //Assert:
            Assert.AreEqual(5, cycle.GetDuration());
            Assert.AreEqual(0, cycle.GetCurrentTime());
        }

        [Test]
        public void Start()
        {
            //Arrange:
            Cycle cycle = new Cycle(5);
            bool wasEvent = false;
            Cycle.State stateChanged = default;
        
            //Act:
            cycle.OnStarted += () => wasEvent = true;
            cycle.OnStateChanged += s => stateChanged = s;
            cycle.Start();
        
            //Assert:
            Assert.IsTrue(wasEvent);
            Assert.AreEqual(Cycle.State.PLAYING, stateChanged);
            Assert.AreEqual(Cycle.State.PLAYING, cycle.GetCurrentState());
            Assert.AreEqual(0, cycle.GetCurrentTime());
            
            Assert.IsTrue(cycle.IsPlaying());
        }
        
        
        [Test]
        public void StartWithTime()
        {
            //Arrange:
            Cycle cycle = new Cycle(5);
            bool wasEvent = false;
            Cycle.State stateChanged = default;
        
            //Act:
            cycle.OnStarted += () => wasEvent = true;
            cycle.OnStateChanged += s => stateChanged = s;
            cycle.Start(3);
        
            //Assert:
            Assert.AreEqual(Cycle.State.PLAYING, stateChanged);
            Assert.AreEqual(Cycle.State.PLAYING, cycle.GetCurrentState());
            Assert.AreEqual(3, cycle.GetCurrentTime());
            Assert.IsTrue(wasEvent);
            Assert.IsTrue(cycle.IsPlaying());
        }
        
        
        [Test]
        public void WhenGetProgressOfNotStartedThenReturnZero()
        {
            Cycle cycle = new Cycle(5);
            Assert.AreEqual(0, cycle.GetProgress());
        }

        [Test]
        public void WhenTickNotStartedThenNothing()
        {
            //Arrange:
            Cycle cycle = new Cycle(5);
            bool wasTimeEvent = false;
            bool wasProgressEvent = false;
        
            //Act:
            cycle.OnCurrentTimeChanged += _ => wasTimeEvent = true;
            cycle.OnProgressChanged += _ => wasProgressEvent = true;
            cycle.Tick(deltaTime: 0.5f);
        
            //Assert:
            Assert.IsFalse(cycle.IsPlaying());
            Assert.IsFalse(wasTimeEvent);
            Assert.IsFalse(wasProgressEvent);
        
            Assert.AreEqual(0, cycle.GetCurrentTime());
            Assert.AreEqual(0, cycle.GetProgress());
        }

        [Test]
        public void WhenTickThenProgressChanged()
        {
            //Arrange:
            Cycle cycle = new Cycle(5);
            float progress = -1;
        
            //Act:
            cycle.Start();
            cycle.OnProgressChanged += p => progress = p;
            cycle.Tick(deltaTime: 1);
        
            //Assert:
            Assert.AreEqual(0.2f, progress, float.Epsilon);
            Assert.AreEqual(0.2f, cycle.GetProgress(), float.Epsilon);
        }
        
        [Test]
        public void OnCycle()
        {
            //Arrange:
            Cycle cycle = new Cycle(4);
            bool wasCycle = false;
        
            //Act:
            cycle.Start();
            cycle.OnCycle += () => wasCycle = true;
            
            //Pre-Assert:
            Assert.AreEqual(Cycle.State.PLAYING, cycle.GetCurrentState());
            
            for (int i = 0; i < 20; i++)
            {
                cycle.Tick(deltaTime: 0.2f);
            }
        
            //Assert:
            Assert.IsTrue(wasCycle);
            Assert.AreEqual(Cycle.State.PLAYING, cycle.GetCurrentState());
        
            Assert.AreEqual(0, cycle.GetProgress());
            Assert.AreEqual(0, cycle.GetCurrentTime());
            Assert.AreEqual(4, cycle.GetDuration());
        
            Assert.IsTrue(cycle.IsPlaying());
            Assert.IsFalse(cycle.IsPaused());
        }

        [Test]
        public void Pause()
        {
            //Arrange:
            Cycle cycle = new Cycle(1);
            bool wasPause = false;
            Cycle.State stateChanged = default;
        
            cycle.Start();
        
            //Act:
            cycle.OnStateChanged += s => stateChanged = s;
            cycle.OnPaused += () => wasPause = true;
            cycle.Pause();
        
            //Assert:
            Assert.IsTrue(wasPause);
            Assert.AreEqual(Cycle.State.PAUSED, stateChanged);
            Assert.AreEqual(Cycle.State.PAUSED, cycle.GetCurrentState());
            Assert.IsTrue(cycle.IsPaused());
        }

        [Test]
        public void WhenTickInPausedThenNothing()
        {
            //Arrange:
            Cycle cycle = new Cycle(0.8f);
            bool progressChanged = false;
            bool timeChanged = false;
            bool wasCycle = false;
        
            //Act:
            cycle.Start();
            cycle.Tick(0.2f);
            cycle.OnProgressChanged += _ => progressChanged = true;
            cycle.OnCurrentTimeChanged += _ => timeChanged = true;
            cycle.OnCycle += () => wasCycle = true;
        
            cycle.Pause();
        
            for (int i = 0; i < 5; i++)
            {
                cycle.Tick(deltaTime: 0.2f);
            }
        
            //Assert:
            Assert.IsTrue(cycle.IsPaused());
            Assert.AreEqual(0.2f, cycle.GetCurrentTime(), 1e-2);
            Assert.AreEqual(0.25f, cycle.GetProgress(), 1e-2);
        
            Assert.IsFalse(progressChanged);
            Assert.IsFalse(timeChanged);
            Assert.IsFalse(wasCycle);
        }
       
        
        [Test]
        public void WhenPauseCycleThatNotStartedThenNothing()
        {
            //Arrange:
            Cycle cycle = new Cycle(0.8f);
            bool wasPause = false;
        
            //Act:
            cycle.OnPaused += () => wasPause = true;
            cycle.Pause();
        
            //Assert:
            Assert.IsFalse(wasPause);
            Assert.IsFalse(cycle.IsPaused());
        }
        
        [Test]
        public void Resume()
        {
            //Arrange:
            Cycle cycle = new Cycle(1);
            bool wasResume = false;
            Cycle.State stateChanged = default;
        
            cycle.Start();
            cycle.Pause();
        
            //Pre-assert:
            Assert.IsTrue(cycle.IsPaused());
        
            //Act:
            cycle.OnStateChanged += s => stateChanged = s;
            cycle.OnResumed += () => wasResume = true;
            cycle.Resume();
        
            //Assert:
            Assert.IsTrue(wasResume);
            Assert.AreEqual(Cycle.State.PLAYING, stateChanged);
            Assert.AreEqual(Cycle.State.PLAYING, cycle.GetCurrentState());
        
            Assert.IsFalse(cycle.IsPaused());
            Assert.IsTrue(cycle.IsPlaying());
        }
        
        [Test]
        public void WhenResumeThatNotStartedThenNothing()
        {
            //Arrange:
            Cycle cycle = new Cycle(1);
            bool wasResume = false;
        
            //Act:
            cycle.OnResumed += () => wasResume = true;
            cycle.Resume();
        
            //Assert:
            Assert.IsFalse(wasResume);
            Assert.AreEqual(Cycle.State.IDLE, cycle.GetCurrentState());
            Assert.IsFalse(cycle.IsPlaying());
            Assert.IsFalse(cycle.IsPaused());
        }
        
        
        [Test]
        public void WhenTickThenCurrentTimeChanged()
        {
            //Arrange:
            Cycle cycle = new Cycle(5);
            float currentTime = -1;
        
            //Act:
            cycle.Start();
            cycle.OnCurrentTimeChanged += t => currentTime = t;
            cycle.Tick(deltaTime: 0.5f);
        
            //Assert:
            Assert.AreEqual(0.5f, currentTime, 1e-2);
            Assert.AreEqual(0.5f, cycle.GetCurrentTime(), 1e-2);
        }
        
        [Test]
        public void WhenStartCycleThatAlreadyStartedThenFailed()
        {
            //Arrange:
            Cycle cycle = new Cycle(5);
            bool wasEvent = false;
        
            cycle.Start();
            cycle.Tick(deltaTime: 1);
            cycle.OnStarted += () => wasEvent = true;
        
            //Act:
            cycle.Start();
        
            //Assert:
            Assert.IsFalse(wasEvent);
            Assert.AreEqual(1, cycle.GetCurrentTime());
            Assert.AreEqual(5, cycle.GetDuration());
        }
        
        [Test]
        public void Stop()
        {
            //Arrange:
            Cycle cycle = new Cycle(5);
            bool wasStop = false;
            Cycle.State stateChanged = default;
        
            cycle.Start();
        
            //Act:
            cycle.OnStateChanged += s => stateChanged = s;
            cycle.OnStopped += () => wasStop = true;
            cycle.Stop();
        
            //Assert:
            Assert.IsTrue(wasStop);
            Assert.AreEqual(Cycle.State.IDLE, cycle.GetCurrentState());
            Assert.AreEqual(Cycle.State.IDLE, stateChanged);
        
            Assert.IsFalse(cycle.IsPlaying());
            Assert.IsTrue(cycle.IsIdle());
        
            Assert.AreEqual(5, cycle.GetDuration());
            Assert.AreEqual(0, cycle.GetCurrentTime());
        }
        
        
        [Test]
        public void WhenStopNotStartedCycleThenNoEvent()
        {
            //Arrange:
            Cycle cycle = new Cycle(10);
            bool wasEvent = false;
        
            //Act:
            cycle.OnStopped += () => wasEvent = true;
            cycle.Stop();
        
            //Assert:
            Assert.IsFalse(wasEvent);
        }
        
        [Test]
        public void WhenResumeProductionThatIsNotPausedThenNothing()
        {
            //Arrange:
            Cycle cycle = new Cycle(10);
            bool wasResume = false;
            cycle.OnResumed += () => wasResume = true;
            cycle.Start();
        
            //Pre-assert:
            Assert.IsTrue(!cycle.IsPaused());
        
            //Act:
            cycle.Resume();
        
            //Assert:
            Assert.IsFalse(wasResume);
        }
        
        
        [Test]
        public void WhenStopPausedCycleThenWillIdle()
        {
            //Arrange:
            Cycle cycle = new Cycle(5);
            bool canceled = false;
        
            cycle.Start();
            cycle.Pause();
        
            Assert.IsTrue(cycle.IsPaused());
        
            //Act:
            cycle.OnStopped += () => canceled = true;
            cycle.Stop();
        
            //Assert:
            Assert.IsTrue(canceled);
        
            Assert.IsFalse(cycle.IsPaused());
            Assert.IsFalse(cycle.IsPlaying());
            Assert.IsTrue(cycle.IsIdle());
            Assert.AreEqual(5, cycle.Duration);
            Assert.AreEqual(0, cycle.CurrentTime);
        }
    }
}