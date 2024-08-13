using NUnit.Framework;

namespace Atomic.Elements
{
    [TestFixture]
    public sealed class CountdownTests
    {
        [Test]
        public void Instantiate()
        {
            //Arrange:
            Countdown countdown = new Countdown(5);

            //Assert:
            Assert.AreEqual(5, countdown.GetDuration());
            Assert.AreEqual(0, countdown.GetCurrentTime());
        }

        [Test]
        public void Start()
        {
            //Arrange:
            Countdown countdown = new Countdown(5);
            bool wasEvent = false;
            Countdown.State stateChanged = default;
            
            //Act:
            countdown.OnStarted += () => wasEvent = true;
            countdown.OnStateChanged += s => stateChanged = s; 
            countdown.Start();

            //Assert:
            Assert.IsTrue(wasEvent);
            Assert.AreEqual(Countdown.State.PLAYING, stateChanged);
            Assert.AreEqual(Countdown.State.PLAYING, countdown.GetCurrentState());
            Assert.AreEqual(5, countdown.GetCurrentTime());
            Assert.IsTrue(countdown.IsPlaying());
        }

        [Test]
        public void StartWithTime()
        {
            //Arrange:
            Countdown countdown = new Countdown(5);
            bool wasEvent = false;
            Countdown.State stateChanged = default;

            //Act:
            countdown.OnStarted += () => wasEvent = true;
            countdown.OnStateChanged += s => stateChanged = s;
            countdown.Start(3);

            //Assert:
            Assert.AreEqual(Countdown.State.PLAYING, stateChanged);
            Assert.AreEqual(Countdown.State.PLAYING, countdown.GetCurrentState());
            Assert.AreEqual(3, countdown.GetCurrentTime());
            Assert.IsTrue(wasEvent);
            Assert.IsTrue(countdown.IsPlaying());
        }

        [Test]
        public void WhenGetProgressOfNotStartedThenReturnZero()
        {
            Countdown countdown = new Countdown(5);
            Assert.AreEqual(0, countdown.GetProgress());
        }

        [Test]
        public void WhenTickNotStartedThenNothing()
        {
            //Arrange:
            Countdown countdown = new Countdown(5);
            bool wasTimeEvent = false;
            bool wasProgressEvent = false;

            //Act:
            countdown.OnCurrentTimeChanged += _ => wasTimeEvent = true;
            countdown.OnProgressChanged += _ => wasProgressEvent = true;
            countdown.Tick(deltaTime: 0.5f);

            //Assert:
            Assert.IsFalse(countdown.IsPlaying());
            Assert.IsFalse(wasTimeEvent);
            Assert.IsFalse(wasProgressEvent);

            Assert.AreEqual(0, countdown.GetCurrentTime());
            Assert.AreEqual(0, countdown.GetProgress());
        }

        [Test]
        public void WhenTickThenProgressChanged()
        {
            //Arrange:
            Countdown countdown = new Countdown(5);
            float progress = -1;

            //Act:
            countdown.Start();
            countdown.OnProgressChanged += p => progress = p;
            countdown.Tick(deltaTime: 1);

            //Assert:
            Assert.AreEqual(0.2f, progress, float.Epsilon);
            Assert.AreEqual(0.2f, countdown.GetProgress(), float.Epsilon);
        }

        [Test]
        public void WhenStartCountdownFromEndedStateThenWillPlaying()
        {
            //Arrange:
            Countdown countdown = new Countdown(4);
            bool wasComplete = false;
            bool wasStarted = false;
            Countdown.State stateChanged = default;

            countdown.Start();
            countdown.OnEnded += () => wasComplete = true;

            for (int i = 0; i < 20; i++)
            {
                countdown.Tick(deltaTime: 0.2f);
            }

            //Pre-assert:
            Assert.IsTrue(wasComplete);
            Assert.IsTrue(countdown.IsEnded());
            Assert.AreEqual(Countdown.State.ENDED, countdown.GetCurrentState());

            //Act:
            countdown.OnStateChanged += s => stateChanged = s;
            countdown.OnStarted += () => wasStarted = true;
            countdown.Start();

            //Assert:
            Assert.IsTrue(wasStarted);
            Assert.AreEqual(Countdown.State.PLAYING, countdown.GetCurrentState());
            Assert.AreEqual(Countdown.State.PLAYING, stateChanged);

            Assert.IsTrue(countdown.IsPlaying());
            Assert.IsFalse(countdown.IsEnded());
            Assert.AreEqual(4, countdown.GetCurrentTime());
        }

        [Test]
        public void Loop()
        {
            //Arrange:
            Countdown countdown = new Countdown(4, true);
            bool wasComplete = false;
            bool wasStarted = false;

            //Act:
            countdown.Start();
            countdown.OnEnded += () => wasComplete = true;
            countdown.OnStarted += () => wasStarted = true;

            for (int i = 0; i < 20; i++)
            {
                countdown.Tick(deltaTime: 0.2f);
            }

            //Assert:
            Assert.IsTrue(wasComplete);
            Assert.IsTrue(wasStarted);

            Assert.IsTrue(countdown.IsPlaying());
            Assert.IsFalse(countdown.IsEnded());
            Assert.AreEqual(4, countdown.GetCurrentTime());
        }

        [Test]
        public void OnEnded()
        {
            //Arrange:
            Countdown countdown = new Countdown(4);
            bool wasComplete = false;
            Countdown.State stateChanged = default;
                
            //Act:
            countdown.Start();
            countdown.OnEnded += () => wasComplete = true;
            countdown.OnStateChanged += s => stateChanged = s;

            for (int i = 0; i < 20; i++)
            {
                countdown.Tick(deltaTime: 0.2f);
            }

            //Assert:
            Assert.IsTrue(wasComplete);

            Assert.AreEqual(Countdown.State.ENDED, stateChanged);
            Assert.AreEqual(Countdown.State.ENDED, countdown.GetCurrentState());
            
            Assert.AreEqual(1, countdown.GetProgress());
            Assert.AreEqual(0, countdown.GetCurrentTime());
            Assert.AreEqual(4, countdown.GetDuration());

            Assert.IsFalse(countdown.IsPlaying());
            Assert.IsFalse(countdown.IsPaused());
            Assert.IsTrue(countdown.IsEnded());
        }

        [Test]
        public void Pause()
        {
            //Arrange:
            Countdown countdown = new Countdown(1);
            bool wasPause = false;
            Countdown.State stateChanged = default;

            countdown.Start();

            //Act:
            countdown.OnStateChanged += s => stateChanged = s;
            countdown.OnPaused += () => wasPause = true;
            countdown.Pause();

            //Assert:
            Assert.IsTrue(wasPause);
            Assert.AreEqual(Countdown.State.PAUSED, stateChanged);
            Assert.AreEqual(Countdown.State.PAUSED, countdown.GetCurrentState());
            Assert.IsTrue(countdown.IsPaused());
        }

        [Test]
        public void WhenTickInPausedThenNothing()
        {
            //Arrange:
            Countdown countdown = new Countdown(0.8f);
            bool progressChanged = false;
            bool timeChanged = false;
            bool completed = false;

            //Act:
            countdown.Start();
            countdown.Tick(0.2f);
            countdown.OnProgressChanged += _ => progressChanged = true;
            countdown.OnCurrentTimeChanged += _ => timeChanged = true;
            countdown.OnEnded += () => completed = true;

            countdown.Pause();

            for (int i = 0; i < 5; i++)
            {
                countdown.Tick(deltaTime: 0.2f);
            }

            //Assert:
            Assert.IsTrue(countdown.IsPaused());
            Assert.AreEqual(0.6f, countdown.GetCurrentTime(), 1E-2f);
            Assert.AreEqual(0.25f, countdown.GetProgress(), 1E-2f);

            Assert.IsFalse(progressChanged);
            Assert.IsFalse(timeChanged);
            Assert.IsFalse(completed);
        }

        [Test]
        public void WhenPauseCountdownThatNotStartedThenNothing()
        {
            //Arrange:
            Countdown countdown = new Countdown(0.8f);
            bool wasPause = false;

            //Act:
            countdown.OnPaused += () => wasPause = true;
            countdown.Pause();

            //Assert:
            Assert.IsFalse(wasPause);
            Assert.IsFalse(countdown.IsPaused());
        }

        [Test]
        public void Resume()
        {
            //Arrange:
            Countdown countdown = new Countdown(1);
            bool wasResume = false;
            Countdown.State stateChanged = default;

            countdown.Start();
            countdown.Pause();

            //Pre-assert:
            Assert.IsTrue(countdown.IsPaused());

            //Act:
            countdown.OnStateChanged += s => stateChanged = s;
            countdown.OnResumed += () => wasResume = true;
            countdown.Resume();

            //Assert:
            Assert.IsTrue(wasResume);
            Assert.AreEqual(Countdown.State.PLAYING, stateChanged);
            Assert.AreEqual(Countdown.State.PLAYING, countdown.GetCurrentState());

            Assert.IsTrue(!countdown.IsPaused());
            Assert.IsTrue(countdown.IsPlaying());
        }

        [Test]
        public void WhenResumeThatNotStartedThenNothing()
        {
            //Arrange:
            Countdown countdown = new Countdown(1);
            bool wasResume = false;

            //Act:
            countdown.OnResumed += () => wasResume = true;
            countdown.Resume();

            //Assert:
            Assert.IsFalse(wasResume);
            Assert.AreEqual(Countdown.State.IDLE, countdown.GetCurrentState());
            Assert.IsFalse(countdown.IsPlaying());
            Assert.IsFalse(countdown.IsPaused());
        }

        [Test]
        public void WhenTickThenCurrentTimeChanged()
        {
            //Arrange:
            Countdown countdown = new Countdown(5);
            float currentTime = -1;

            //Act:
            countdown.Start();
            countdown.OnCurrentTimeChanged += t => currentTime = t;
            countdown.Tick(deltaTime: 0.5f);

            //Assert:
            Assert.AreEqual(4.5f, currentTime, 1E-2f);
            Assert.AreEqual(4.5f, countdown.GetCurrentTime(), 1E-2f);
        }

        [Test]
        public void WhenStartCountdownThatAlreadyStartedThenFailed()
        {
            //Arrange:
            Countdown countdown = new Countdown(5);
            bool wasEvent = false;

            countdown.Start();
            countdown.Tick(deltaTime: 1);
            countdown.OnStarted += () => wasEvent = true;

            //Act:
            countdown.Start();

            //Assert:
            Assert.IsFalse(wasEvent);
            Assert.AreEqual(4, countdown.GetCurrentTime());
            Assert.AreEqual(5, countdown.GetDuration());
        }

        [Test]
        public void Stop()
        {
            //Arrange:
            Countdown countdown = new Countdown(5);
            bool wasStop = false;
            Countdown.State stateChanged = default;

            countdown.Start();

            //Act:
            countdown.OnStateChanged += s => stateChanged = s;
            countdown.OnStopped += () => wasStop = true;
            countdown.Stop();

            //Assert:
            Assert.IsTrue(wasStop);
            Assert.AreEqual(Countdown.State.IDLE, countdown.GetCurrentState());
            Assert.AreEqual(Countdown.State.IDLE, stateChanged);

            Assert.IsFalse(countdown.IsPlaying());
            Assert.IsTrue(countdown.IsIdle());

            Assert.AreEqual(5, countdown.GetDuration());
            Assert.AreEqual(0, countdown.GetCurrentTime());
        }

        [Test]
        public void ForceStart()
        {
            //Arrange:
            Countdown countdown = new Countdown(10);

            bool canceled = false;
            bool started = false;

            countdown.Start();
            countdown.Tick(deltaTime: 1);
            countdown.Tick(deltaTime: 1);

            //Pre-assert:
            Assert.AreEqual(8, countdown.GetCurrentTime());
            Assert.AreEqual(0.2f, countdown.GetProgress());

            //Act:
            countdown.OnStopped += () => canceled = true;
            countdown.OnStarted += () => started = true;
            countdown.ForceStart();

            //Assert:
            Assert.IsTrue(canceled);
            Assert.IsTrue(started);

            Assert.AreEqual(10, countdown.GetDuration());
            Assert.AreEqual(10, countdown.GetCurrentTime());
            Assert.AreEqual(0, countdown.GetProgress());

            Assert.IsTrue(countdown.IsPlaying());
        }

        [Test]
        public void ForceStartWithTime()
        {
            //Arrange:
            Countdown countdown = new Countdown(10);

            bool canceled = false;
            bool started = false;

            countdown.Start();
            countdown.Tick(deltaTime: 1);
            countdown.Tick(deltaTime: 1);

            //Pre-assert:
            Assert.AreEqual(8, countdown.GetCurrentTime());
            Assert.AreEqual(0.2f, countdown.GetProgress());

            //Act:
            countdown.OnStopped += () => canceled = true;
            countdown.OnStarted += () => started = true;
            countdown.ForceStart(5);

            //Assert:
            Assert.IsTrue(canceled);
            Assert.IsTrue(started);

            Assert.AreEqual(10, countdown.GetDuration());
            Assert.AreEqual(5, countdown.GetCurrentTime());
            Assert.AreEqual(0.5f, countdown.GetProgress());

            Assert.IsTrue(countdown.IsPlaying());
        }

        [Test]
        public void WhenStopNotStartedCountdownThenNoEvent()
        {
            //Arrange:
            Countdown countdown = new Countdown(10);
            bool wasEvent = false;

            //Act:
            countdown.OnStopped += () => wasEvent = true;
            countdown.Stop();

            //Assert:
            Assert.IsFalse(wasEvent);
        }

        [Test]
        public void WhenResumeProductionThatIsNotPausedThenNothing()
        {
            //Arrange:
            Countdown countdown = new Countdown(10);
            bool wasResume = false;
            countdown.OnResumed += () => wasResume = true;
            countdown.Start();

            //Pre-assert:
            Assert.IsTrue(!countdown.IsPaused());

            //Act:
            countdown.Resume();

            //Assert:
            Assert.IsFalse(wasResume);
        }

        [Test]
        public void WhenStopPausedCountdownThenWillIdle()
        {
            //Arrange:
            Countdown countdown = new Countdown(5);
            bool canceled = false;

            countdown.Start();
            countdown.Pause();

            Assert.IsTrue(countdown.IsPaused());

            //Act:
            countdown.OnStopped += () => canceled = true;
            countdown.Stop();

            //Assert:
            Assert.IsTrue(canceled);

            Assert.IsFalse(countdown.IsPaused());
            Assert.IsFalse(countdown.IsPlaying());
            Assert.IsTrue(countdown.IsIdle());
            Assert.AreEqual(5, countdown.Duration);
            Assert.AreEqual(0, countdown.CurrentTime);
        }
    }
}