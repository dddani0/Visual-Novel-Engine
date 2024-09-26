public class TextData
{
    /// <summary>
    /// this is a summary.
    /// </summary>
    /// <param name="data">text data, which is to be written out.</param>
    /// <param name="cps">characters per second</param>
    public TextData(string data, int cps)
    {
        content = data;
        cpsTextSpeed = cps;
        secondTimer = new Timer(1);
    }

    public bool isFinished() => currentIdx == maxIdx;
    public void writeToScreen()
    {
        if (isFinished() is true) return;
        if (secondTimer.OnCooldown)
        {

        }
        else
        {
            for (int i = 0; i < length; i++)
            {

            }
            secondTimer.ResetTimer();
        }
    }
    private Timer secondTimer { get; }
    private string content { get; }
    private int cpsTextSpeed { get; }
    private int currentIdx { get; set; }
    int increment()
    {
        currentIdx++;
        if (currentIdx == maxIdx) return 0;
        return currentIdx;
    }
    private const int maxIdx { get; }
}