using NUnit.Framework;

namespace Atomic.Elements
{
    [TestFixture]
    public sealed class DownTimerTests
    {
        [Test]
        public void Instantiate()
        {
            //Arrange:
            DownTimer countdown = new DownTimer(5);

            //Assert:
            Assert.AreEqual(5, countdown.GetDuration());
            Assert.AreEqual(0, countdown.GetTime());
        }

        [Test]
        public void Start()
        {
            //Arrange:
            DownTimer countdown = new DownTimer(5);
            bool wasEvent = false;
            TimerState stateChanged = default;
            
            //Act:
            countdown.OnStarted += () => wasEvent = true;
            countdown.OnStateChanged += s => stateChanged = s; 
            countdown.Start();

            //Assert:
            Assert.IsTrue(wasEvent);
            Assert.AreEqual(TimerState.PLAYING, stateChanged);
            Assert.AreEqual(TimerState.PLAYING, countdown.GetState());
            Assert.AreEqual(5, countdown.GetTime());
            Assert.IsTrue(countdown.IsStarted());
        }

        [Test]
        public void Play()
        {
            //Arrange:
            DownTimer countdown = new DownTimer(5);
            bool wasEvent = false;
            TimerState stateChanged = default;

            countdown.OnStarted += () => wasEvent = true;
            countdown.OnStateChanged += s => stateChanged = s;

            //Act:
            countdown.Start(2);

            //Assert:
            Assert.AreEqual(TimerState.PLAYING, stateChanged);
            Assert.AreEqual(TimerState.PLAYING, countdown.GetState());
            Assert.AreEqual(2, countdown.GetTime());
            Assert.IsTrue(wasEvent);
            Assert.IsTrue(countdown.IsStarted());
            
            //Act:
            countdown.Tick(deltaTime: 0.5f);
            countdown.Tick(deltaTime: 0.5f);
            countdown.Tick(deltaTime: 0.5f);
            countdown.Tick(deltaTime: 0.5f);

            Assert.IsTrue(countdown.IsCompleted());
        }

        [Test]
        public void StartWithTime()
        {
            //Arrange:
            DownTimer countdown = new DownTimer(5);
            bool wasEvent = false;
            TimerState stateChanged = default;

            //Act:
            countdown.OnStarted += () => wasEvent = true;
            countdown.OnStateChanged += s => stateChanged = s;
            countdown.Start(3);

            //Assert:
            Assert.AreEqual(TimerState.PLAYING, stateChanged);
            Assert.AreEqual(TimerState.PLAYING, countdown.GetState());
            Assert.AreEqual(3, countdown.GetTime());
            Assert.IsTrue(wasEvent);
            Assert.IsTrue(countdown.IsStarted());
        }

        [Test]
        public void WhenGetProgressOfNotStartedThenReturnZero()
        {
            DownTimer countdown = new DownTimer(5);
            Assert.AreEqual(0, countdown.GetProgress());
        }

        [Test]
        public void WhenTickNotStartedThenNothing()
        {
            //Arrange:
            DownTimer countdown = new DownTimer(5);
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
            DownTimer countdown = new DownTimer(5);
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
            DownTimer countdown = new DownTimer(4);
            bool wasComplete = false;
            bool wasStarted = false;
            TimerState stateChanged = default;

            countdown.Start();
            countdown.OnCompleted += () => wasComplete = true;

            for (int i = 0; i < 20; i++)
            {
                countdown.Tick(deltaTime: 0.2f);
            }

            //Pre-assert:
            Assert.IsTrue(wasComplete);
            Assert.IsTrue(countdown.IsCompleted());
            Assert.AreEqual(TimerState.COMPLETED, countdown.GetState());

            //Act:
            countdown.OnStateChanged += s => stateChanged = s;
            countdown.OnStarted += () => wasStarted = true;
            countdown.Start();

            //Assert:
            Assert.IsTrue(wasStarted);
            Assert.AreEqual(TimerState.PLAYING, countdown.GetState());
            Assert.AreEqual(TimerState.PLAYING, stateChanged);

            Assert.IsTrue(countdown.IsStarted());
            Assert.IsFalse(countdown.IsCompleted());
            Assert.AreEqual(4, countdown.GetTime());
        }

        [Test]
        public void OnEnded()
        {
            //Arrange:
            DownTimer countdown = new DownTimer(4);
            bool wasComplete = false;
            TimerState stateChanged = default;
                
            //Act:
            countdown.Start();
            countdown.OnCompleted += () => wasComplete = true;
            countdown.OnStateChanged += s => stateChanged = s;

            for (int i = 0; i < 20; i++)
            {
                countdown.Tick(deltaTime: 0.2f);
            }

            //Assert:
            Assert.IsTrue(wasComplete);

            Assert.AreEqual(TimerState.COMPLETED, stateChanged);
            Assert.AreEqual(TimerState.COMPLETED, countdown.GetState());
            
            Assert.AreEqual(1, countdown.GetProgress());
            Assert.AreEqual(0, countdown.GetTime());
            Assert.AreEqual(4, countdown.GetDuration());

            Assert.IsFalse(countdown.IsStarted());
            Assert.IsFalse(countdown.IsPaused());
            Assert.IsTrue(countdown.IsCompleted());
        }

        [Test]
        public void Pause()
        {
            //Arrange:
            DownTimer countdown = new DownTimer(1);
            bool wasPause = false;
            TimerState stateChanged = default;

            countdown.Start();

            //Act:
            countdown.OnStateChanged += s => stateChanged = s;
            countdown.OnPaused += () => wasPause = true;
            countdown.Pause();

            //Assert:
            Assert.IsTrue(wasPause);
            Assert.AreEqual(TimerState.PAUSED, stateChanged);
            Assert.AreEqual(TimerState.PAUSED, countdown.GetState());
            Assert.IsTrue(countdown.IsPaused());
        }

        [Test]
        public void WhenTickInPausedThenNothing()
        {
            //Arrange:
            DownTimer countdown = new DownTimer(0.8f);
            bool progressChanged = false;
            bool timeChanged = false;
            bool completed = false;

            //Act:
            countdown.Start();
            countdown.Tick(0.2f);
            countdown.OnProgressChanged += _ => progressChanged = true;
            countdown.OnTimeChanged += _ => timeChanged = true;
            countdown.OnCompleted += () => completed = true;

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
            DownTimer countdown = new DownTimer(0.8f);
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
            DownTimer countdown = new DownTimer(1);
            bool wasResume = false;
            TimerState stateChanged = default;

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
            Assert.AreEqual(TimerState.PLAYING, stateChanged);
            Assert.AreEqual(TimerState.PLAYING, countdown.GetState());

            Assert.IsTrue(!countdown.IsPaused());
            Assert.IsTrue(countdown.IsStarted());
        }

        [Test]
        public void WhenResumeThatNotStartedThenNothing()
        {
            //Arrange:
            DownTimer countdown = new DownTimer(1);
            bool wasResume = false;

            //Act:
            countdown.OnResumed += () => wasResume = true;
            countdown.Resume();

            //Assert:
            Assert.IsFalse(wasResume);
            Assert.AreEqual(TimerState.IDLE, countdown.GetState());
            Assert.IsFalse(countdown.IsStarted());
            Assert.IsFalse(countdown.IsPaused());
        }

        [Test]
        public void WhenTickThenCurrentTimeChanged()
        {
            //Arrange:
            DownTimer countdown = new DownTimer(5);
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
            DownTimer countdown = new DownTimer(5);
            bool wasStop = false;
            TimerState stateChanged = default;

            countdown.Start();

            //Act:
            countdown.OnStateChanged += s => stateChanged = s;
            countdown.OnStopped += () => wasStop = true;
            countdown.Stop();

            //Assert:
            Assert.IsTrue(wasStop);
            Assert.AreEqual(TimerState.IDLE, countdown.GetState());
            Assert.AreEqual(TimerState.IDLE, stateChanged);

            Assert.IsFalse(countdown.IsStarted());
            Assert.IsTrue(countdown.IsIdle());

            Assert.AreEqual(5, countdown.GetDuration());
            Assert.AreEqual(0, countdown.GetTime());
        }

        [Test]
        public void Restart()
        {
            //Arrange:
            DownTimer countdown = new DownTimer(10);

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
        public void RestartWithTime()
        {
            //Arrange:
            DownTimer countdown = new DownTimer(10);

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
            DownTimer countdown = new DownTimer(10);
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
            DownTimer countdown = new DownTimer(10);
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
            DownTimer countdown = new DownTimer(5);
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