# 📌 Using Observe Extension for IReactiveValue\<T>

It is recommended to use the [Observe](Extensions.md/#observe) extension method and store (cache) the returned
subscription handle for proper disposal or reuse.

```csharp
public sealed class ScorePresenter : IDisposable
{
    private readonly IReactiveValue<int> _score;
    private readonly Text _scoreText;
    private readonly Subscription<int> _subscription; //struct
    
    public ScorePresenter(IReactiveValue<int> score, Text scoreText)
    {
        _score = score;
        _scoreText = scoreText;

        // Subscribe and initialize UI with current value
        _subscription = _score.Observe(this.OnScoreChanged);
    }
    
    public void Dispose()
    {
        // Unsubscribe to avoid memory leaks
        _subscription.Dispose();
    }
    
    private void OnScoreChanged(int score)
    {
        // Update UI text
        _scoreText.text = "Score: " + score;
    }
}
```