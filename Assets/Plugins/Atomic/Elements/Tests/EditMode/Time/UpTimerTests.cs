using NUnit.Framework;

namespace Atomic.Elements
{
    [TestFixture]
    public sealed class UpTimerTests
    {
        [Test]
        public void Instantiate()
        {
            //Arrange:
            UpTimer timer = new UpTimer(5);

            //Assert:
            Assert.AreEqual(5, timer.GetDuration());
            Assert.AreEqual(0, timer.GetTime());
        }

        [Test]
        public void Start()
        {
            //Arrange:
            UpTimer timer = new UpTimer(5);
            bool wasEvent = false;
            TimerState stateChanged = default;

            //Act:
            timer.OnStateChanged += s => stateChanged = s;
            timer.OnStarted += () => wasEvent = true;
            timer.Start();

            //Assert:
            Assert.AreEqual(TimerState.PLAYING, stateChanged);
            Assert.AreEqual(TimerState.PLAYING, timer.CurrentState);
            Assert.AreEqual(0, timer.GetTime());
            Assert.IsTrue(wasEvent);
            Assert.IsTrue(timer.IsStarted());
        }

        [Test]
        public void StartWithTime()
        {
            //Arrange:
            UpTimer timer = new UpTimer(5);
            bool wasEvent = false;

            //Act:
            timer.OnStarted += () => wasEvent = true;
            timer.Start(3);

            //Assert:
            Assert.AreEqual(3, timer.GetTime());
            Assert.IsTrue(wasEvent);
            Assert.IsTrue(timer.IsStarted());
        }
        
        [Test]
        public void Play()
        {
            //Arrange:
            UpTimer timer = new UpTimer(5);
            bool wasEvent = false;

            //Act:
            timer.OnStarted += () => wasEvent = true;
            timer.Start(3);

            //Assert:
            Assert.AreEqual(3, timer.GetTime());
            Assert.IsTrue(wasEvent);
            Assert.IsTrue(timer.IsStarted());
        }

        [Test]
        public void WhenGetProgressOfNotStartedThenReturnZero()
        {
            UpTimer timer = new UpTimer(5);
            Assert.AreEqual(0, timer.GetProgress());
        }

        [Test]
        public void WhenTickNotStartedThenNothing()
        {
            //Arrange:
            UpTimer timer = new UpTimer(5);
            bool wasTimeEvent = false;
            bool wasProgressEvent = false;

            //Act:
            timer.OnTimeChanged += _ => wasTimeEvent = true;
            timer.OnProgressChanged += _ => wasProgressEvent = true;
            timer.Tick(deltaTime: 0.5f);

            //Assert:
            Assert.IsFalse(timer.IsStarted());
            Assert.IsFalse(wasTimeEvent);
            Assert.IsFalse(wasProgressEvent);

            Assert.AreEqual(0, timer.GetTime());
            Assert.AreEqual(0, timer.GetProgress());
        }

        [Test]
        public void WhenTickThenProgressChanged()
        {
            //Arrange:
            UpTimer timer = new UpTimer(5);
            float progress = -1;

            //Act:
            timer.Start();
            timer.OnProgressChanged += p => progress = p;
            timer.Tick(deltaTime: 1);

            //Assert:
            Assert.AreEqual(0.2f, progress, float.Epsilon);
            Assert.AreEqual(0.2f, timer.GetProgress(), float.Epsilon);
        }

        [Test]
        public void WhenStartTimerFromEndedStateThenWillPlaying()
        {
            //Arrange:
            UpTimer timer = new UpTimer(4);
            bool wasComplete = false;
            bool wasStarted = false;

            timer.Start();
            timer.OnCompleted += () => wasComplete = true;

            for (int i = 0; i < 20; i++)
            {
                timer.Tick(deltaTime: 0.2f);
            }

            //Pre-assert:
            Assert.IsTrue(wasComplete);
            Assert.IsTrue(timer.IsCompleted());

            //Act:
            timer.OnStarted += () => wasStarted = true;
            timer.Start();

            //Assert:
            Assert.IsTrue(wasStarted);

            Assert.IsTrue(timer.IsStarted());
            Assert.IsFalse(timer.IsCompleted());
            Assert.AreEqual(0, timer.GetTime());
        }

        [Test]
        public void OnEnded()
        {
            //Arrange:
            UpTimer timer = new UpTimer(4);
            bool wasComplete = false;
            TimerState stateChanged = default;

            //Act:
            timer.Start();
            timer.OnCompleted += () => wasComplete = true;
            timer.OnStateChanged += s => stateChanged = s;

            for (int i = 0; i < 20; i++)
            {
                timer.Tick(deltaTime: 0.2f);
            }

            //Assert:
            Assert.IsTrue(wasComplete);
            Assert.AreEqual(TimerState.COMPLETED, stateChanged);
            Assert.AreEqual(TimerState.COMPLETED, timer.GetState());

            Assert.AreEqual(1, timer.GetProgress());
            Assert.AreEqual(4, timer.GetTime());
            Assert.AreEqual(4, timer.GetDuration());

            Assert.IsFalse(timer.IsStarted());
            Assert.IsFalse(timer.IsPaused());
            Assert.IsTrue(timer.IsCompleted());
        }

        [Test]
        public void Pause()
        {
            //Arrange:
            UpTimer timer = new UpTimer(1);
            bool wasPause = false;

            //Act:
            timer.Start();
            timer.OnPaused += () => wasPause = true;
            timer.Pause();

            //Assert:
            Assert.IsTrue(wasPause);
            Assert.IsTrue(timer.IsPaused());
        }

        [Test]
        public void WhenTickInPausedThenNothing()
        {
            //Arrange:
            UpTimer timer = new UpTimer(0.8f);
            bool progressChanged = false;
            bool timeChanged = false;
            bool completed = false;

            //Act:
            timer.Start();
            timer.Tick(0.2f);
            timer.OnProgressChanged += _ => progressChanged = true;
            timer.OnTimeChanged += _ => timeChanged = true;
            timer.OnCompleted += () => completed = true;

            timer.Pause();

            for (int i = 0; i < 5; i++)
            {
                timer.Tick(deltaTime: 0.2f);
            }

            //Assert:
            Assert.IsTrue(timer.IsPaused());
            Assert.AreEqual(0.2f, timer.GetTime(), 1e-2);
            Assert.AreEqual(0.25f, timer.GetProgress(), 1e-2);

            Assert.IsFalse(progressChanged);
            Assert.IsFalse(timeChanged);
            Assert.IsFalse(completed);
        }

        [Test]
        public void WhenPauseTimerThatNotStartedThenNothing()
        {
            //Arrange:
            UpTimer timer = new UpTimer(0.8f);
            bool wasPause = false;

            //Act:
            timer.OnPaused += () => wasPause = true;
            timer.Pause();

            //Assert:
            Assert.IsFalse(wasPause);
            Assert.IsFalse(timer.IsPaused());
        }

        [Test]
        public void Resume()
        {
            //Arrange:
            UpTimer timer = new UpTimer(1);
            bool wasResume = false;

            timer.OnResumed += () => wasResume = true;
            timer.Start();
            timer.Pause();

            //Pre-assert:
            Assert.IsTrue(timer.IsPaused());

            //Act:
            timer.Resume();

            //Assert:
            Assert.IsTrue(wasResume);
            Assert.IsTrue(!timer.IsPaused());
            Assert.IsTrue(timer.IsStarted());
        }

        [Test]
        public void WhenResumeThatNotStartedThenNothing()
        {
            //Arrange:
            UpTimer timer = new UpTimer(1);
            bool wasResume = false;

            //Act:
            timer.OnResumed += () => wasResume = true;
            timer.Resume();

            //Assert:
            Assert.IsFalse(wasResume);
            Assert.IsFalse(timer.IsStarted());
            Assert.IsFalse(timer.IsPaused());
        }

        [Test]
        public void WhenTickThenCurrentTimeChanged()
        {
            //Arrange:
            UpTimer timer = new UpTimer(5);
            float currentTime = -1;

            //Act:
            timer.Start();
            timer.OnTimeChanged += t => currentTime = t;
            timer.Tick(deltaTime: 0.5f);

            //Assert:
            Assert.AreEqual(0.5f, currentTime, 1e-2);
            Assert.AreEqual(0.5f, timer.GetTime(), 1e-2);
        }

        [Test]
        public void Stop()
        {
            //Arrange:
            UpTimer timer = new UpTimer(5);
            bool wasStop = false;
            timer.Start();

            //Act:
            timer.OnStopped += () => wasStop = true;
            timer.Stop();

            //Assert:
            Assert.IsTrue(wasStop);
            Assert.IsFalse(timer.IsStarted());
            Assert.IsTrue(timer.IsIdle());

            Assert.AreEqual(5, timer.GetDuration());
            Assert.AreEqual(0, timer.GetTime());
        }

        [Test]
        public void Restart()
        {
            //Arrange:
            UpTimer timer = new UpTimer(10);

            bool canceled = false;
            bool started = false;

            timer.Start();
            timer.Tick(deltaTime: 1);
            timer.Tick(deltaTime: 1);

            //Pre-assert:
            Assert.AreEqual(2, timer.GetTime());
            Assert.AreEqual(0.2f, timer.GetProgress());

            //Act:
            timer.OnStopped += () => canceled = true;
            timer.OnStarted += () => started = true;
            timer.Restart();

            //Assert:
            Assert.IsTrue(canceled);
            Assert.IsTrue(started);

            Assert.AreEqual(10, timer.GetDuration());
            Assert.AreEqual(0, timer.GetTime());
            Assert.AreEqual(0, timer.GetProgress());

            Assert.IsTrue(timer.IsStarted());
        }

        [Test]
        public void ForceStartWithTime()
        {
            //Arrange:
            UpTimer timer = new UpTimer(10);

            bool canceled = false;
            bool started = false;

            timer.Start();
            timer.Tick(deltaTime: 1);
            timer.Tick(deltaTime: 1);

            //Pre-assert:
            Assert.AreEqual(2, timer.GetTime());
            Assert.AreEqual(0.2f, timer.GetProgress());

            //Act:
            timer.OnStopped += () => canceled = true;
            timer.OnStarted += () => started = true;
            timer.Restart(5);

            //Assert:
            Assert.IsTrue(canceled);
            Assert.IsTrue(started);

            Assert.AreEqual(10, timer.GetDuration());
            Assert.AreEqual(5, timer.GetTime());
            Assert.AreEqual(0.5f, timer.GetProgress());

            Assert.IsTrue(timer.IsStarted());
        }

        [Test]
        public void WhenStopNotStartedTimerThenNoEvent()
        {
            //Arrange:
            UpTimer timer = new UpTimer(10);
            bool wasEvent = false;

            //Act:
            timer.OnStopped += () => wasEvent = true;
            timer.Stop();

            //Assert:
            Assert.IsFalse(wasEvent);
        }

        [Test]
        public void WhenResumeProductionThatIsNotPausedThenNothing()
        {
            //Arrange:
            UpTimer timer = new UpTimer(10);
            bool wasResume = false;
            timer.OnResumed += () => wasResume = true;
            timer.Start();

            //Pre-assert:
            Assert.IsTrue(!timer.IsPaused());

            //Act:
            timer.Resume();

            //Assert:
            Assert.IsFalse(wasResume);
        }

        [Test]
        public void WhenStopPausedTimerThenWillIdle()
        {
            //Arrange:
            UpTimer timer = new UpTimer(5);
            bool canceled = false;

            timer.Start();
            timer.Pause();

            Assert.IsTrue(timer.IsPaused());

            //Act:
            timer.OnStopped += () => canceled = true;
            timer.Stop();

            //Assert:
            Assert.IsTrue(canceled);

            Assert.IsFalse(timer.IsPaused());
            Assert.IsFalse(timer.IsStarted());
            Assert.IsTrue(timer.IsIdle());
            Assert.AreEqual(5, timer.Duration);
            Assert.AreEqual(0, timer.Time);
        }
    }
}