using NUnit.Framework;

namespace Atomic.Elements
{
    [TestFixture]
    public sealed class TimerTTests
    {
        [Test]
        public void Instantiate()
        {
            //Arrange:
            Timer<object> timer = new Timer<object>(5);

            //Assert:
            Assert.IsNull(timer.Value);
            Assert.AreEqual(5, timer.GetDuration());
            Assert.AreEqual(0, timer.GetCurrentTime());
        }

        [Test]
        public void Start()
        {
            //Arrange:
            Timer<object> timer = new Timer<object>(5);
            object value = new object();
            object wasEvent = null;

            //Act:
            timer.OnStarted += v => wasEvent = v;
            timer.Start(value);

            //Assert:
            Assert.AreEqual(0, timer.GetCurrentTime());
            Assert.AreEqual(value, wasEvent);
            Assert.IsTrue(timer.IsPlaying());
            Assert.AreEqual(value, timer.Value);
        }

        [Test]
        public void StartWithTime()
        {
            //Arrange:
            Timer<object> timer = new Timer<object>(5);
            object value = new object();
            object wasEvent = null;

            //Act:
            timer.OnStarted += v => wasEvent = v;
            timer.Start(3, value);

            //Assert:
            Assert.AreEqual(3, timer.GetCurrentTime());
            Assert.AreEqual(value, wasEvent);
            Assert.IsTrue(timer.IsPlaying());
            Assert.AreEqual(value, timer.Value);
        }

        [Test]
        public void WhenGetProgressOfNotStartedThenReturnZero()
        {
            Timer<object> timer = new Timer<object>(5);
            Assert.AreEqual(0, timer.GetProgress());
        }

        [Test]
        public void WhenTickNotStartedThenNothing()
        {
            //Arrange:
            Timer<object> timer = new Timer<object>(5);
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
            Timer<object> timer = new Timer<object>(5);
            float progress = -1;

            //Act:
            timer.Start(new object());
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
            Timer<object> timer = new Timer<object>(4);
            bool wasComplete = false;
            bool wasStarted = false;

            object v1 = new object();

            timer.Start(v1);
            timer.OnEnded += _ => wasComplete = true;

            for (int i = 0; i < 20; i++)
            {
                timer.Tick(deltaTime: 0.2f);
            }

            //Pre-assert:
            Assert.IsTrue(wasComplete);
            Assert.IsTrue(timer.IsEnded());

            //Act:
            object v2 = new object();
            timer.OnStarted += _ => wasStarted = true;
            timer.Start(v2);

            //Assert:
            Assert.IsTrue(wasStarted);

            Assert.AreNotEqual(v1, timer.Value);
            Assert.AreEqual(v2, timer.Value);

            Assert.IsTrue(timer.IsPlaying());
            Assert.IsFalse(timer.IsEnded());
            Assert.AreEqual(0, timer.GetCurrentTime());
        }

        [Test]
        public void Loop()
        {
            //Arrange:
            Timer<object> timer = new Timer<object>(4, true);
            bool wasComplete = false;
            bool wasStarted = false;

            object v = new object();

            //Act:
            timer.Start(v);
            timer.OnEnded += _ => wasComplete = true;
            timer.OnStarted += _ => wasStarted = true;

            for (int i = 0; i < 20; i++)
            {
                timer.Tick(deltaTime: 0.2f);
            }

            //Assert:
            Assert.IsTrue(wasComplete);
            Assert.IsTrue(wasStarted);
            Assert.AreEqual(v, timer.Value);

            Assert.IsTrue(timer.IsPlaying());
            Assert.IsFalse(timer.IsEnded());
            Assert.AreEqual(0, timer.GetCurrentTime());
        }

        [Test]
        public void OnEnded()
        {
            //Arrange:
            Timer<string> timer = new Timer<string>(4);
            string wasComplete = string.Empty;
            Timer<string>.State stateChanged = default;

            //Act:
            timer.Start("Vasya");
            timer.OnStateChanged += s => stateChanged = s;
            timer.OnEnded += v => wasComplete = v;

            for (int i = 0; i < 20; i++)
            {
                timer.Tick(deltaTime: 0.2f);
            }

            //Assert:
            Assert.AreEqual(Timer<string>.State.ENDED, stateChanged);
            Assert.AreEqual(Timer<string>.State.ENDED, timer.GetCurrentState());

            Assert.AreEqual("Vasya", wasComplete);
            Assert.AreEqual("Vasya", timer.GetCurrentValue());

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
            Timer<string> timer = new Timer<string>(1);
            bool wasPause = false;

            //Act:
            timer.Start("Vasya");
            timer.OnPaused += () => wasPause = true;
            timer.Pause();

            //Assert:
            Assert.IsTrue(wasPause);
            Assert.AreEqual("Vasya", timer.GetCurrentValue());
            Assert.IsTrue(timer.IsPaused());
        }

        [Test]
        public void WhenTickInPausedThenNothing()
        {
            //Arrange:
            Timer<string> timer = new Timer<string>(0.8f);
            bool progressChanged = false;
            bool timeChanged = false;
            bool completed = false;

            //Act:
            timer.Start("Vasya");
            timer.Tick(0.2f);
            timer.OnProgressChanged += _ => progressChanged = true;
            timer.OnCurrentTimeChanged += _ => timeChanged = true;
            timer.OnEnded += _ => completed = true;

            timer.Pause();

            for (int i = 0; i < 5; i++)
            {
                timer.Tick(deltaTime: 0.2f);
            }

            //Assert:
            Assert.IsTrue(timer.IsPaused());
            Assert.AreEqual(0.2f, timer.GetCurrentTime(), 1E-2f);
            Assert.AreEqual(0.25f, timer.GetProgress(), 1E-2f);

            Assert.IsFalse(progressChanged);
            Assert.IsFalse(timeChanged);
            Assert.IsFalse(completed);
        }

        [Test]
        public void WhenPauseTimerThatNotStartedThenNothing()
        {
            //Arrange:
            Timer<string> timer = new Timer<string>(0.8f);
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
            Timer<string> timer = new Timer<string>(1);
            bool wasResume = false;

            timer.OnResumed += () => wasResume = true;
            timer.Start(null);
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
            Timer<string> timer = new Timer<string>(1);
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
            Timer<string> timer = new Timer<string>(5);
            float currentTime = -1;

            //Act:
            timer.Start(null);
            timer.OnCurrentTimeChanged += t => currentTime = t;
            timer.Tick(deltaTime: 0.5f);
            timer.Tick(deltaTime: 0.5f);

            //Assert:
            Assert.AreEqual(1, currentTime, 1e-2);
            Assert.AreEqual(1, timer.GetCurrentTime(), 1e-2);
        }


        [Test]
        public void WhenStartTimerThatAlreadyStartedThenFailed()
        {
            //Arrange:
            Timer<string> timer = new Timer<string>(5);
            bool wasEvent = false;

            timer.Start("Vasya");
            timer.Tick(deltaTime: 1);
            timer.OnStarted += _ => wasEvent = true;

            //Act:
            timer.Start("Petya");

            //Assert:
            Assert.IsFalse(wasEvent);
            Assert.AreEqual("Vasya", timer.GetCurrentValue());
            Assert.AreNotEqual("Petya", timer.GetCurrentValue());

            Assert.AreEqual(1, timer.GetCurrentTime());
            Assert.AreEqual(5, timer.GetDuration());
        }


        [Test]
        public void Stop()
        {
            //Arrange:
            Timer<string> timer = new Timer<string>(5);
            bool wasStop = false;
            timer.Start("Vasya");

            //Act:
            timer.OnStopped += _ => wasStop = true;
            timer.Stop();

            //Assert:
            Assert.IsTrue(wasStop);
            Assert.IsFalse(timer.IsPlaying());
            Assert.IsTrue(timer.IsIdle());

            Assert.AreNotEqual("Vasya", timer.GetCurrentValue());
            Assert.IsNull(timer.GetCurrentValue());
            Assert.AreEqual(5, timer.GetDuration());
            Assert.AreEqual(0, timer.GetCurrentTime());
        }

        [Test]
        public void ForceStart()
        {
            //Arrange:
            Timer<string> timer = new Timer<string>(10);

            string canceled = string.Empty;
            string started = string.Empty;

            timer.Start("Vasya");
            timer.Tick(deltaTime: 1);
            timer.Tick(deltaTime: 1);

            //Pre-assert:
            Assert.AreEqual(2, timer.GetCurrentTime());
            Assert.AreEqual(0.2f, timer.GetProgress());

            //Act:
            timer.OnStopped += v => canceled = v;
            timer.OnStarted += v => started = v;
            timer.ForceStart("Petya");

            //Assert:
            Assert.AreEqual("Vasya", canceled);
            Assert.AreEqual("Petya", started);

            Assert.AreEqual(10, timer.GetDuration());
            Assert.AreEqual(0, timer.GetCurrentTime());
            Assert.AreEqual(0, timer.GetProgress());

            Assert.IsTrue(timer.IsPlaying());
        }

        [Test]
        public void ForceStartWithTime()
        {
            //Arrange:
            Timer<string> timer = new Timer<string>(10);

            string canceled = string.Empty;
            string started = string.Empty;

            timer.Start("Vasya");
            timer.Tick(deltaTime: 1);
            timer.Tick(deltaTime: 1);

            //Pre-assert:
            Assert.AreEqual(2, timer.GetCurrentTime());
            Assert.AreEqual(0.2f, timer.GetProgress());

            //Act:
            timer.OnStopped += v => canceled = v;
            timer.OnStarted += v => started = v;
            timer.ForceStart(5, "Petya");

            //Assert:
            Assert.AreEqual("Vasya", canceled);
            Assert.AreEqual("Petya", started);

            Assert.AreEqual(10, timer.GetDuration());
            Assert.AreEqual(5, timer.GetCurrentTime());
            Assert.AreEqual(0.5f, timer.GetProgress());

            Assert.IsTrue(timer.IsPlaying());
        }
        
        [Test]
        public void WhenStopNotStartedTimerThenNoEvent()
        {
            //Arrange:
            Timer<string> timer = new Timer<string>(10);
            bool wasEvent = false;
        
            //Act:
            timer.OnStopped += _ => wasEvent = true;
            bool success = timer.Stop();

            //Assert:
            Assert.IsFalse(success);
            Assert.IsFalse(wasEvent);
        }
        
        
        [Test]
        public void WhenResumeProductionThatIsNotPausedThenNothing()
        {
            //Arrange:
            Timer<string> timer = new Timer<string>(10);
            bool wasResume = false;
            timer.OnResumed += () => wasResume = true;
            timer.Start("Vasya");
        
            //Pre-assert:
            Assert.IsTrue(!timer.IsPaused());
        
            //Act:
            bool resumed = timer.Resume();

            //Assert:
            Assert.IsTrue(timer.IsPlaying());
            Assert.AreEqual("Vasya", timer.GetCurrentValue());
            Assert.IsFalse(resumed);
            Assert.IsFalse(wasResume);
        }
        
        [Test]
        public void WhenStopPausedTimerThenWillIdle()
        {
            //Arrange:
            Timer<string> timer = new Timer<string>(5);
            string canceled = string.Empty;
        
            timer.Start("Vasya");
            timer.Pause();
            
            Assert.IsTrue(timer.IsPaused());
        
            //Act:
            timer.OnStopped += v => canceled = v;
            timer.Stop();
        
            //Assert:
            Assert.AreEqual("Vasya", canceled);
            
            Assert.IsFalse(timer.IsPaused());
            Assert.IsFalse(timer.IsPlaying());
            Assert.IsTrue(timer.IsIdle());
            
            Assert.IsNull(timer.GetCurrentValue());
            Assert.AreEqual(5, timer.Duration);
            Assert.AreEqual(0, timer.CurrentTime);
        }
    }
}