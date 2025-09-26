# 🧩 Time

Provides a set of tools for managing **timers, cooldowns, countdowns, stopwatches, and time sources**. It allows
developers to track and control time-related events in a consistent and reactive manner, making it useful for gameplay
mechanics, scheduling, and periodic updates.

The module supports flexible time representations, including fixed and
variable intervals, as well as reactive notifications for state changes.

- [Source Contracts](Sources.md) <!-- + -->
    - [ITimeSource](ITimeSource.md) <!-- + -->
    - [IDurationSource](IDurationSource.md) <!-- + -->
    - [ITickSource](ITickSource.md) <!-- + -->
    - [IStartSource](IStartSource.md) <!-- + -->
    - [IPauseSource](IPauseSource.md)
    - [ICompleteSource]()
    - [IProgressSource]()
    - [IStateSource&lt;T&gt;]()
- [Cooldowns]()
    - [ICooldown](ICooldown.md)
    - [Cooldown](Cooldown.md)
    - [RandomCooldown](RandomCooldown.md)
- [Timers]()
    - [ITimer](ITimer.md)
    - [UpTimer](UpTimer.md)
    - [DownTimer](DownTimer.md)
    - [TimerState](TimerState.md)
- [Stopwatches]()
    - [IStopwatch](IStopwatch.md)
    - [Stopwatch](Stopwatch.md)
    - [StopwatchState](StopwatchState.md)
- [Periods]()
    - [IPeriod](IPeriod.md)
    - [Period](Period.md)
    - [PeriodState](PeriodState.md)
- [Timestamps]()
    - [ITimestamp](ITimestamp.md)
    - [FixedTimestamp](FixedTimestamp.md)
- [Extensions](Extensions.md)
