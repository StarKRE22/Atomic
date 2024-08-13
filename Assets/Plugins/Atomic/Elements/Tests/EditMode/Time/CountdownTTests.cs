using NUnit.Framework;

namespace Atomic.Elements
{
    [TestFixture]
    public sealed class CountdownTTests
    {
        [Test]
        public void Instantiate()
        {
            //Arrange:
            Countdown<object> countdown = new Countdown<object>(5);

            //Assert:
            Assert.IsNull(countdown.Value);
            Assert.AreEqual(5, countdown.GetDuration());
            Assert.AreEqual(0, countdown.GetCurrentTime());
        }

        [Test]
        public void Start()
        {
            //Arrange:
            Countdown<object> countdown = new Countdown<object>(5);
            object value = new object();
            object wasEvent = null;

            //Act:
            countdown.OnStarted += v => wasEvent = v;
            countdown.Start(value);

            //Assert:
            Assert.AreEqual(5, countdown.GetCurrentTime());
            Assert.AreEqual(value, wasEvent);
            Assert.IsTrue(countdown.IsPlaying());
            Assert.AreEqual(value, countdown.Value);
        }

        [Test]
        public void StartWithTime()
        {
            //Arrange:
            Countdown<object> countdown = new Countdown<object>(5);
            object value = new object();
            object wasEvent = null;

            //Act:
            countdown.OnStarted += v => wasEvent = v;
            countdown.Start(3, value);

            //Assert:
            Assert.AreEqual(3, countdown.GetCurrentTime());
            Assert.AreEqual(value, wasEvent);
            Assert.IsTrue(countdown.IsPlaying());
            Assert.AreEqual(value, countdown.Value);
        }

        [Test]
        public void WhenGetProgressOfNotStartedThenReturnZero()
        {
            Countdown<object> countdown = new Countdown<object>(5);
            Assert.AreEqual(0, countdown.GetProgress());
        }

        [Test]
        public void WhenTickNotStartedThenNothing()
        {
            //Arrange:
            Countdown<object> countdown = new Countdown<object>(5);
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
            Countdown<object> countdown = new Countdown<object>(5);
            float progress = -1;

            //Act:
            countdown.Start(new object());
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
            Countdown<object> countdown = new Countdown<object>(4);
            bool wasComplete = false;
            bool wasStarted = false;

            object v1 = new object();

            countdown.Start(v1);
            countdown.OnEnded += _ => wasComplete = true;

            for (int i = 0; i < 20; i++)
            {
                countdown.Tick(deltaTime: 0.2f);
            }

            //Pre-assert:
            Assert.IsTrue(wasComplete);
            Assert.IsTrue(countdown.IsEnded());

            //Act:
            object v2 = new object();
            countdown.OnStarted += _ => wasStarted = true;
            countdown.Start(v2);

            //Assert:
            Assert.IsTrue(wasStarted);

            Assert.AreNotEqual(v1, countdown.Value);
            Assert.AreEqual(v2, countdown.Value);

            Assert.IsTrue(countdown.IsPlaying());
            Assert.IsFalse(countdown.IsEnded());
            Assert.AreEqual(4, countdown.GetCurrentTime());
        }

        [Test]
        public void Loop()
        {
            //Arrange:
            Countdown<object> countdown = new Countdown<object>(4, true);
            bool wasComplete = false;
            bool wasStarted = false;

            object v = new object();

            //Act:
            countdown.Start(v);
            countdown.OnEnded += _ => wasComplete = true;
            countdown.OnStarted += _ => wasStarted = true;

            for (int i = 0; i < 20; i++)
            {
                countdown.Tick(deltaTime: 0.2f);
            }

            //Assert:
            Assert.IsTrue(wasComplete);
            Assert.IsTrue(wasStarted);
            Assert.AreEqual(v, countdown.Value);

            Assert.IsTrue(countdown.IsPlaying());
            Assert.IsFalse(countdown.IsEnded());
            Assert.AreEqual(4, countdown.GetCurrentTime());
        }

        [Test]
        public void OnEnded()
        {
            //Arrange:
            Countdown<string> countdown = new Countdown<string>(4);
            string wasComplete = string.Empty;
            Countdown<string>.State stateChanged = default;

            //Act:
            countdown.Start("Vasya");
            countdown.OnStateChanged += s => stateChanged = s;
            countdown.OnEnded += v => wasComplete = v;

            for (int i = 0; i < 20; i++)
            {
                countdown.Tick(deltaTime: 0.2f);
            }

            //Assert:
            Assert.AreEqual(Countdown<string>.State.ENDED, stateChanged);
            Assert.AreEqual(Countdown<string>.State.ENDED, countdown.GetCurrentState());

            Assert.AreEqual("Vasya", wasComplete);
            Assert.AreEqual("Vasya", countdown.GetCurrentValue());

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
            Countdown<string> countdown = new Countdown<string>(1);
            bool wasPause = false;

            //Act:
            countdown.Start("Vasya");
            countdown.OnPaused += () => wasPause = true;
            countdown.Pause();

            //Assert:
            Assert.IsTrue(wasPause);
            Assert.AreEqual("Vasya", countdown.GetCurrentValue());
            Assert.IsTrue(countdown.IsPaused());
        }

        [Test]
        public void WhenTickInPausedThenNothing()
        {
            //Arrange:
            Countdown<string> countdown = new Countdown<string>(0.8f);
            bool progressChanged = false;
            bool timeChanged = false;
            bool completed = false;

            //Act:
            countdown.Start("Vasya");
            countdown.Tick(0.2f);
            countdown.OnProgressChanged += _ => progressChanged = true;
            countdown.OnCurrentTimeChanged += _ => timeChanged = true;
            countdown.OnEnded += _ => completed = true;

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
            Countdown<string> countdown = new Countdown<string>(0.8f);
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
            Countdown<string> countdown = new Countdown<string>(1);
            bool wasResume = false;

            countdown.OnResumed += () => wasResume = true;
            countdown.Start(null);
            countdown.Pause();

            //Pre-assert:
            Assert.IsTrue(countdown.IsPaused());

            //Act:
            countdown.Resume();

            //Assert:
            Assert.IsTrue(wasResume);
            Assert.IsTrue(!countdown.IsPaused());
            Assert.IsTrue(countdown.IsPlaying());
        }

        [Test]
        public void WhenResumeThatNotStartedThenNothing()
        {
            //Arrange:
            Countdown<string> countdown = new Countdown<string>(1);
            bool wasResume = false;

            //Act:
            countdown.OnResumed += () => wasResume = true;
            countdown.Resume();

            //Assert:
            Assert.IsFalse(wasResume);
            Assert.IsFalse(countdown.IsPlaying());
            Assert.IsFalse(countdown.IsPaused());
        }


        [Test]
        public void WhenTickThenCurrentTimeChanged()
        {
            //Arrange:
            Countdown<string> countdown = new Countdown<string>(5);
            float currentTime = -1;

            //Act:
            countdown.Start(null);
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
            Countdown<string> countdown = new Countdown<string>(5);
            bool wasEvent = false;

            countdown.Start("Vasya");
            countdown.Tick(deltaTime: 1);
            countdown.OnStarted += _ => wasEvent = true;

            //Act:
            countdown.Start("Petya");

            //Assert:
            Assert.IsFalse(wasEvent);
            Assert.AreEqual("Vasya", countdown.GetCurrentValue());
            Assert.AreNotEqual("Petya", countdown.GetCurrentValue());

            Assert.AreEqual(4, countdown.GetCurrentTime());
            Assert.AreEqual(5, countdown.GetDuration());
        }


        [Test]
        public void Stop()
        {
            //Arrange:
            Countdown<string> countdown = new Countdown<string>(5);
            bool wasStop = false;
            countdown.Start("Vasya");

            //Act:
            countdown.OnStopped += _ => wasStop = true;
            countdown.Stop();

            //Assert:
            Assert.IsTrue(wasStop);
            Assert.IsFalse(countdown.IsPlaying());
            Assert.IsTrue(countdown.IsIdle());

            Assert.AreNotEqual("Vasya", countdown.GetCurrentValue());
            Assert.IsNull(countdown.GetCurrentValue());
            Assert.AreEqual(5, countdown.GetDuration());
            Assert.AreEqual(0, countdown.GetCurrentTime());
        }

        [Test]
        public void ForceStart()
        {
            //Arrange:
            Countdown<string> countdown = new Countdown<string>(10);

            string canceled = string.Empty;
            string started = string.Empty;

            countdown.Start("Vasya");
            countdown.Tick(deltaTime: 1);
            countdown.Tick(deltaTime: 1);

            //Pre-assert:
            Assert.AreEqual(8, countdown.GetCurrentTime());
            Assert.AreEqual(0.2f, countdown.GetProgress());

            //Act:
            countdown.OnStopped += v => canceled = v;
            countdown.OnStarted += v => started = v;
            countdown.ForceStart("Petya");

            //Assert:
            Assert.AreEqual("Vasya", canceled);
            Assert.AreEqual("Petya", started);

            Assert.AreEqual(10, countdown.GetDuration());
            Assert.AreEqual(10, countdown.GetCurrentTime());
            Assert.AreEqual(0, countdown.GetProgress());

            Assert.IsTrue(countdown.IsPlaying());
        }

        [Test]
        public void ForceStartWithTime()
        {
            //Arrange:
            Countdown<string> countdown = new Countdown<string>(10);

            string canceled = string.Empty;
            string started = string.Empty;

            countdown.Start("Vasya");
            countdown.Tick(deltaTime: 1);
            countdown.Tick(deltaTime: 1);

            //Pre-assert:
            Assert.AreEqual(8, countdown.GetCurrentTime());
            Assert.AreEqual(0.2f, countdown.GetProgress());

            //Act:
            countdown.OnStopped += v => canceled = v;
            countdown.OnStarted += v => started = v;
            countdown.ForceStart(5, "Petya");

            //Assert:
            Assert.AreEqual("Vasya", canceled);
            Assert.AreEqual("Petya", started);

            Assert.AreEqual(10, countdown.GetDuration());
            Assert.AreEqual(5, countdown.GetCurrentTime());
            Assert.AreEqual(0.5f, countdown.GetProgress());

            Assert.IsTrue(countdown.IsPlaying());
        }
        
        [Test]
        public void WhenStopNotStartedCountdownThenNoEvent()
        {
            //Arrange:
            Countdown<string> countdown = new Countdown<string>(10);
            bool wasEvent = false;
        
            //Act:
            countdown.OnStopped += _ => wasEvent = true;
            bool success = countdown.Stop();

            //Assert:
            Assert.IsFalse(success);
            Assert.IsFalse(wasEvent);
        }
        
        
        [Test]
        public void WhenResumeProductionThatIsNotPausedThenNothing()
        {
            //Arrange:
            Countdown<string> countdown = new Countdown<string>(10);
            bool wasResume = false;
            countdown.OnResumed += () => wasResume = true;
            countdown.Start("Vasya");
        
            //Pre-assert:
            Assert.IsTrue(!countdown.IsPaused());
        
            //Act:
            bool resumed = countdown.Resume();

            //Assert:
            Assert.IsTrue(countdown.IsPlaying());
            Assert.AreEqual("Vasya", countdown.GetCurrentValue());
            Assert.IsFalse(resumed);
            Assert.IsFalse(wasResume);
        }
        
        [Test]
        public void WhenStopPausedCountdownThenWillIdle()
        {
            //Arrange:
            Countdown<string> countdown = new Countdown<string>(5);
            string canceled = string.Empty;
        
            countdown.Start("Vasya");
            countdown.Pause();
            
            Assert.IsTrue(countdown.IsPaused());
        
            //Act:
            countdown.OnStopped += v => canceled = v;
            countdown.Stop();
        
            //Assert:
            Assert.AreEqual("Vasya", canceled);
            
            Assert.IsFalse(countdown.IsPaused());
            Assert.IsFalse(countdown.IsPlaying());
            Assert.IsTrue(countdown.IsIdle());
            
            Assert.IsNull(countdown.GetCurrentValue());
            Assert.AreEqual(5, countdown.Duration);
            Assert.AreEqual(0, countdown.CurrentTime);
        }
    }
}