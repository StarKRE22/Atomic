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
            Assert.AreEqual(0, countdown.GetTime());
        }

        [Test]
        public void Start()
        {
            //Arrange:
            Countdown countdown = new Countdown(5);
            bool wasEvent = false;
            CountdownState stateChanged = default;
            
            //Act:
            countdown.OnStarted += () => wasEvent = true;
            countdown.OnStateChanged += s => stateChanged = s; 
            countdown.Start();

            //Assert:
            Assert.IsTrue(wasEvent);
            Assert.AreEqual(CountdownState.PLAYING, stateChanged);
            Assert.AreEqual(CountdownState.PLAYING, countdown.GetState());
            Assert.AreEqual(5, countdown.GetTime());
            Assert.IsTrue(countdown.IsStarted());
        }

        [Test]
        public void Play()
        {
            //Arrange:
            Countdown countdown = new Countdown(5);
            bool wasEvent = false;
            CountdownState stateChanged = default;

            countdown.OnStarted += () => wasEvent = true;
            countdown.OnStateChanged += s => stateChanged = s;

            //Act:
            countdown.SetTime(2);
            countdown.Start();

            //Assert:
            Assert.AreEqual(CountdownState.PLAYING, stateChanged);
            Assert.AreEqual(CountdownState.PLAYING, countdown.GetState());
            Assert.AreEqual(2, countdown.GetTime());
            Assert.IsTrue(wasEvent);
            Assert.IsTrue(countdown.IsStarted());
            
            //Act:
            countdown.Tick(deltaTime: 0.5f);
            countdown.Tick(deltaTime: 0.5f);
            countdown.Tick(deltaTime: 0.5f);
            countdown.Tick(deltaTime: 0.5f);

            Assert.IsTrue(countdown.IsExpired());
        }

        [Test]
        public void StartWithTime()
        {
            //Arrange:
            Countdown countdown = new Countdown(5);
            bool wasEvent = false;
            CountdownState stateChanged = default;

            //Act:
            countdown.OnStarted += () => wasEvent = true;
            countdown.OnStateChanged += s => stateChanged = s;
            countdown.Start(3);

            //Assert:
            Assert.AreEqual(CountdownState.PLAYING, stateChanged);
            Assert.AreEqual(CountdownState.PLAYING, countdown.GetState());
            Assert.AreEqual(3, countdown.GetTime());
            Assert.IsTrue(wasEvent);
            Assert.IsTrue(countdown.IsStarted());
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
            countdown.OnTimeChanged += _ => wasTimeEvent = true;
            countdown.OnProgressChanged += _ => wasProgressEvent = true;
            countdown.Tick(deltaTime: 0.5f);

            //Assert:
            Assert.IsFalse(countdown.IsStarted());
            Assert.IsFalse(wasTimeEvent);
            Assert.IsFalse(wasProgressEvent);

            Assert.AreEqual(0, countdown.GetTime());
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
            CountdownState stateChanged = default;

            countdown.Start();
            countdown.OnExpired += () => wasComplete = true;

            for (int i = 0; i < 20; i++)
            {
                countdown.Tick(deltaTime: 0.2f);
            }

            //Pre-assert:
            Assert.IsTrue(wasComplete);
            Assert.IsTrue(countdown.IsExpired());
            Assert.AreEqual(CountdownState.EXPIRED, countdown.GetState());

            //Act:
            countdown.OnStateChanged += s => stateChanged = s;
            countdown.OnStarted += () => wasStarted = true;
            countdown.Start();

            //Assert:
            Assert.IsTrue(wasStarted);
            Assert.AreEqual(CountdownState.PLAYING, countdown.GetState());
            Assert.AreEqual(CountdownState.PLAYING, stateChanged);

            Assert.IsTrue(countdown.IsStarted());
            Assert.IsFalse(countdown.IsExpired());
            Assert.AreEqual(4, countdown.GetTime());
        }

        [Test]
        public void OnEnded()
        {
            //Arrange:
            Countdown countdown = new Countdown(4);
            bool wasComplete = false;
            CountdownState stateChanged = default;
                
            //Act:
            countdown.Start();
            countdown.OnExpired += () => wasComplete = true;
            countdown.OnStateChanged += s => stateChanged = s;

            for (int i = 0; i < 20; i++)
            {
                countdown.Tick(deltaTime: 0.2f);
            }

            //Assert:
            Assert.IsTrue(wasComplete);

            Assert.AreEqual(CountdownState.EXPIRED, stateChanged);
            Assert.AreEqual(CountdownState.EXPIRED, countdown.GetState());
            
            Assert.AreEqual(1, countdown.GetProgress());
            Assert.AreEqual(0, countdown.GetTime());
            Assert.AreEqual(4, countdown.GetDuration());

            Assert.IsFalse(countdown.IsStarted());
            Assert.IsFalse(countdown.IsPaused());
            Assert.IsTrue(countdown.IsExpired());
        }

        [Test]
        public void Pause()
        {
            //Arrange:
            Countdown countdown = new Countdown(1);
            bool wasPause = false;
            CountdownState stateChanged = default;

            countdown.Start();

            //Act:
            countdown.OnStateChanged += s => stateChanged = s;
            countdown.OnPaused += () => wasPause = true;
            countdown.Pause();

            //Assert:
            Assert.IsTrue(wasPause);
            Assert.AreEqual(CountdownState.PAUSED, stateChanged);
            Assert.AreEqual(CountdownState.PAUSED, countdown.GetState());
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
            countdown.OnTimeChanged += _ => timeChanged = true;
            countdown.OnExpired += () => completed = true;

            countdown.Pause();

            for (int i = 0; i < 5; i++)
            {
                countdown.Tick(deltaTime: 0.2f);
            }

            //Assert:
            Assert.IsTrue(countdown.IsPaused());
            Assert.AreEqual(0.6f, countdown.GetTime(), 1E-2f);
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
            CountdownState stateChanged = default;

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
            Assert.AreEqual(CountdownState.PLAYING, stateChanged);
            Assert.AreEqual(CountdownState.PLAYING, countdown.GetState());

            Assert.IsTrue(!countdown.IsPaused());
            Assert.IsTrue(countdown.IsStarted());
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
            Assert.AreEqual(CountdownState.IDLE, countdown.GetState());
            Assert.IsFalse(countdown.IsStarted());
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
            countdown.OnTimeChanged += t => currentTime = t;
            countdown.Tick(deltaTime: 0.5f);

            //Assert:
            Assert.AreEqual(4.5f, currentTime, 1E-2f);
            Assert.AreEqual(4.5f, countdown.GetTime(), 1E-2f);
        }

        [Test]
        public void Stop()
        {
            //Arrange:
            Countdown countdown = new Countdown(5);
            bool wasStop = false;
            CountdownState stateChanged = default;

            countdown.Start();

            //Act:
            countdown.OnStateChanged += s => stateChanged = s;
            countdown.OnStopped += () => wasStop = true;
            countdown.Stop();

            //Assert:
            Assert.IsTrue(wasStop);
            Assert.AreEqual(CountdownState.IDLE, countdown.GetState());
            Assert.AreEqual(CountdownState.IDLE, stateChanged);

            Assert.IsFalse(countdown.IsStarted());
            Assert.IsTrue(countdown.IsIdle());

            Assert.AreEqual(5, countdown.GetDuration());
            Assert.AreEqual(0, countdown.GetTime());
        }

        [Test]
        public void Restart()
        {
            //Arrange:
            Countdown countdown = new Countdown(10);

            bool canceled = false;
            bool started = false;

            countdown.Start();
            countdown.Tick(deltaTime: 1);
            countdown.Tick(deltaTime: 1);

            //Pre-assert:
            Assert.AreEqual(8, countdown.GetTime());
            Assert.AreEqual(0.2f, countdown.GetProgress());

            //Act:
            countdown.OnStopped += () => canceled = true;
            countdown.OnStarted += () => started = true;
            countdown.Restart();

            //Assert:
            Assert.IsTrue(canceled);
            Assert.IsTrue(started);

            Assert.AreEqual(10, countdown.GetDuration());
            Assert.AreEqual(10, countdown.GetTime());
            Assert.AreEqual(0, countdown.GetProgress());

            Assert.IsTrue(countdown.IsStarted());
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
            Assert.AreEqual(8, countdown.GetTime());
            Assert.AreEqual(0.2f, countdown.GetProgress());

            //Act:
            countdown.OnStopped += () => canceled = true;
            countdown.OnStarted += () => started = true;
            countdown.Restart(5);

            //Assert:
            Assert.IsTrue(canceled);
            Assert.IsTrue(started);

            Assert.AreEqual(10, countdown.GetDuration());
            Assert.AreEqual(5, countdown.GetTime());
            Assert.AreEqual(0.5f, countdown.GetProgress());

            Assert.IsTrue(countdown.IsStarted());
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
            Assert.IsFalse(countdown.IsStarted());
            Assert.IsTrue(countdown.IsIdle());
            Assert.AreEqual(5, countdown.Duration);
            Assert.AreEqual(0, countdown.CurrentTime);
        }
    }
}