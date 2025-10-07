class Instrument
{
    protected string name;
    public Instrument(string name)
    {
        this.name = name;
    }
    public virtual void Play()
    {
        Console.WriteLine($"Грає інструмент: {name}");
    }
    public virtual int GetDuration()
    {
        return 3;
    }
    public virtual int GetCount()
    {
        return 5;
    }
    public int GetTotalTime()
    {
        return GetDuration() * GetCount();
    }
}`