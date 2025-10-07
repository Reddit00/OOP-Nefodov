class Piano : Instrument
{
    public Piano(string name) : base(name) { }
    public override void Play()
    {
        Console.WriteLine($"Піаніно {name} грає.");
    }
    public override int GetDuration()
    {
        return 5;
    }
    public override int GetCount()
    {
        return 4;
    }
}