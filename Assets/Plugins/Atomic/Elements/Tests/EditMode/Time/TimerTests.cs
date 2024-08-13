using NUnit.Framework;

namespace Atomic.Elements
{
    [TestFixture]
    public sealed class TimerTests
    {
        [Test]
        public void Instantiate()
        {
            //Arrange:
            Timer timer = new Timer(5);

            //Assert:
            Assert.AreEqual(5, timer.GetDuration());
            Assert.AreEqual(0, timer.GetCurrentTime());
        }

        [Test]
        public void Start()
        {
            //Arrange:
            Timer timer = new Timer(5);
            bool wasEvent = false;
            Timer.State stateChanged = default;

            //Act:
            timer.OnStateChanged += s => stateChanged = s;
            timer.OnStarted += () => wasEvent = true;
            timer.Start();

            //Assert:
            Assert.AreEqual(Timer.State.PLAYING, stateChanged);
            Assert.AreEqual(Timer.State.PLAYING, timer.CurrentState);
            Assert.AreEqual(0, timer.GetCurrentTime());
            Assert.IsTrue(wasEvent);
            Assert.IsTrue(timer.IsPlaying());
        }

        [Test]
        public void StartWithTime()
        {
            //Arrange:
            Timer timer = new Timer(5);
            bool wasEvent = false;

            //Act:
            timer.OnStarted += () => wasEvent = true;
            timer.Start(3);

            //Assert:
            Assert.AreEqual(3, timer.GetCurrentTime());
            Assert.IsTrue(wasEvent);
            Assert.IsTrue(timer.IsPlaying());
        }

        [Test]
        public void WhenGetProgressOfNotStartedThenReturnZero()
        {
            Timer timer = new Timer(5);
            Assert.AreEqual(0, timer.GetProgress());
        }

        [Test]
        public void WhenTickNotStartedThenNothing()
        {
            //Arrange:
            Timer timer = new Timer(5);
            bool wasTimeEvent = false;
            bool wasProgressEvent = false;

            //Act:
            timer.OnCurrentTimeChanged += _ => wasTimeEvent = true;
            timer.OnProgressChanged += _ => wasProgressEvent = true;
            timer.Tick(deltaTime: 0.5f);

            //Assert:
            Assert.IsFalse(timer.IsPlaying());
            Assert.IsFalse(wasTimeEvent);
            Assert.IsFalse(wasProgressEvent);

            Assert.AreEqual(0, timer.GetCurrentTime());
            Assert.AreEqual(0, timer.GetProgress());
        }

        [Test]
        public void WhenTickThenProgressChanged()
        {
            //Arrange:
            Timer timer = new Timer(5);
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
            Timer timer = new Timer(4);
            bool wasComplete = false;
            bool wasStarted = false;

            timer.Start();
            timer.OnEnded += () => wasComplete = true;

            for (int i = 0; i < 20; i++)
            {
                timer.Tick(deltaTime: 0.2f);
            }

            //Pre-assert:
            Assert.IsTrue(wasComplete);
            Assert.IsTrue(timer.IsEnded());

            //Act:
            timer.OnStarted += () => wasStarted = true;
            timer.Start();

            //Assert:
            Assert.IsTrue(wasStarted);

            Assert.IsTrue(timer.IsPlaying());
            Assert.IsFalse(timer.IsEnded());
            Assert.AreEqual(0, timer.GetCurrentTime());
        }

        [Test]
        public void Loop()
        {
            //Arrange:
            Timer timer = new Timer(4, true);
            bool wasComplete = false;
            bool wasStarted = false;

            //Act:
            timer.Start();
            timer.OnEnded += () => wasComplete = true;
            timer.OnStarted += () => wasStarted = true;

            for (int i = 0; i < 20; i++)
            {
                timer.Tick(deltaTime: 0.2f);
            }

            //Assert:
            Assert.IsTrue(wasComplete);
            Assert.IsTrue(wasStarted);

            Assert.IsTrue(timer.IsPlaying());
            Assert.IsFalse(timer.IsEnded());
            Assert.AreEqual(0, timer.GetCurrentTime());
        }

        [Test]
        public void OnEnded()
        {
            //Arrange:
            Timer timer = new Timer(4);
            bool wasComplete = false;
            Timer.State stateChanged = default;

            //Act:
            timer.Start();
            timer.OnEnded += () => wasComplete = true;
            timer.OnStateChanged += s => stateChanged = s;

            for (int i = 0; i < 20; i++)
            {
                timer.Tick(deltaTime: 0.2f);
            }

            //Assert:
            Assert.IsTrue(wasComplete);
            Assert.AreEqual(Timer.State.ENDED, stateChanged);
            Assert.AreEqual(Timer.State.ENDED, timer.GetCurrentState());

            Assert.AreEqual(1, timer.GetProgress());
            Assert.AreEqual(4, timer.GetCurrentTime());
            Assert.AreEqual(4, timer.GetDuration());

            Assert.IsFalse(timer.IsPlaying());
            Assert.IsFalse(timer.IsPaused());
            Assert.IsTrue(timer.IsEnded());
        }

        [Test]
        public void Pause()
        {
            //Arrange:
            Timer timer = new Timer(1);
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
            Timer timer = new Timer(0.8f);
            bool progressChanged = false;
            bool timeChanged = false;
            bool completed = false;

            //Act:
            timer.Start();
            timer.Tick(0.2f);
            timer.OnProgressChanged += _ => progressChanged = true;
            timer.OnCurrentTimeChanged += _ => timeChanged = true;
            timer.OnEnded += () => completed = true;

            timer.Pause();

            for (int i = 0; i < 5; i++)
            {
                timer.Tick(deltaTime: 0.2f);
            }

            //Assert:
            Assert.IsTrue(timer.IsPaused());
            Assert.AreEqual(0.2f, timer.GetCurrentTime(), 1e-2);
            Assert.AreEqual(0.25f, timer.GetProgress(), 1e-2);

            Assert.IsFalse(progressChanged);
            Assert.IsFalse(timeChanged);
            Assert.IsFalse(completed);
        }

        [Test]
        public void WhenPauseTimerThatNotStartedThenNothing()
        {
            //Arrange:
            Timer timer = new Timer(0.8f);
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
            Timer timer = new Timer(1);
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
            Assert.IsTrue(timer.IsPlaying());
        }

        [Test]
        public void WhenResumeThatNotStartedThenNothing()
        {
            //Arrange:
            Timer timer = new Timer(1);
            bool wasResume = false;

            //Act:
            timer.OnResumed += () => wasResume = true;
            timer.Resume();

            //Assert:
            Assert.IsFalse(wasResume);
            Assert.IsFalse(timer.IsPlaying());
            Assert.IsFalse(timer.IsPaused());
        }

        [Test]
        public void WhenTickThenCurrentTimeChanged()
        {
            //Arrange:
            Timer timer = new Timer(5);
            float currentTime = -1;

            //Act:
            timer.Start();
            timer.OnCurrentTimeChanged += t => currentTime = t;
            timer.Tick(deltaTime: 0.5f);

            //Assert:
            Assert.AreEqual(0.5f, currentTime, 1e-2);
            Assert.AreEqual(0.5f, timer.GetCurrentTime(), 1e-2);
        }

        [Test]
        public void WhenStartTimerThatAlreadyStartedThenFailed()
        {
            //Arrange:
            Timer timer = new Timer(5);
            bool wasEvent = false;

            timer.Start();
            timer.Tick(deltaTime: 1);
            timer.OnStarted += () => wasEvent = true;

            //Act:
            bool success = timer.Start();

            //Assert:
            Assert.IsFalse(success);
            Assert.IsFalse(wasEvent);
            Assert.AreEqual(1, timer.GetCurrentTime());
            Assert.AreEqual(5, timer.GetDuration());
        }

        [Test]
        public void Stop()
        {
            //Arrange:
            Timer timer = new Timer(5);
            bool wasStop = false;
            timer.Start();

            //Act:
            timer.OnStopped += () => wasStop = true;
            timer.Stop();

            //Assert:
            Assert.IsTrue(wasStop);
            Assert.IsFalse(timer.IsPlaying());
            Assert.IsTrue(timer.IsIdle());

            Assert.AreEqual(5, timer.GetDuration());
            Assert.AreEqual(0, timer.GetCurrentTime());
        }

        [Test]
        public void ForceStart()
        {
            //Arrange:
            Timer timer = new Timer(10);

            bool canceled = false;
            bool started = false;

            timer.Start();
            timer.Tick(deltaTime: 1);
            timer.Tick(deltaTime: 1);

            //Pre-assert:
            Assert.AreEqual(2, timer.GetCurrentTime());
            Assert.AreEqual(0.2f, timer.GetProgress());

            //Act:
            timer.OnStopped += () => canceled = true;
            timer.OnStarted += () => started = true;
            timer.ForceStart();

            //Assert:
            Assert.IsTrue(canceled);
            Assert.IsTrue(started);

            Assert.AreEqual(10, timer.GetDuration());
            Assert.AreEqual(0, timer.GetCurrentTime());
            Assert.AreEqual(0, timer.GetProgress());

            Assert.IsTrue(timer.IsPlaying());
        }

        [Test]
        public void ForceStartWithTime()
        {
            //Arrange:
            Timer timer = new Timer(10);

            bool canceled = false;
            bool started = false;

            timer.Start();
            timer.Tick(deltaTime: 1);
            timer.Tick(deltaTime: 1);

            //Pre-assert:
            Assert.AreEqual(2, timer.GetCurrentTime());
            Assert.AreEqual(0.2f, timer.GetProgress());

            //Act:
            timer.OnStopped += () => canceled = true;
            timer.OnStarted += () => started = true;
            timer.ForceStart(5);

            //Assert:
            Assert.IsTrue(canceled);
            Assert.IsTrue(started);

            Assert.AreEqual(10, timer.GetDuration());
            Assert.AreEqual(5, timer.GetCurrentTime());
            Assert.AreEqual(0.5f, timer.GetProgress());

            Assert.IsTrue(timer.IsPlaying());
        }

        [Test]
        public void WhenStopNotStartedTimerThenNoEvent()
        {
            //Arrange:
            Timer timer = new Timer(10);
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
            Timer timer = new Timer(10);
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
            Timer timer = new Timer(5);
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
            Assert.IsFalse(timer.IsPlaying());
            Assert.IsTrue(timer.IsIdle());
            Assert.AreEqual(5, timer.Duration);
            Assert.AreEqual(0, timer.CurrentTime);
        }
    }
}