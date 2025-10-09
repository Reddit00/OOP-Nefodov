abstract class Instrument
{
    protected string compositionName;
    protected int sec;
    public Instrument(string compositionName, int sec)
    {
        this.compositionName = compositionName;
        this.sec = sec;
    }
    public abstract void Play();
    public int GetDuration()
    {
        return sec;
    }
    ~Instrument()
    {
        Console.WriteLine($"Інструмент з композицією \"{compositionName}\" знищено.");
    }
}
