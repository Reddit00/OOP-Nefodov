class Guitar : Instrument
{
    public Guitar(string name) : base(name) { }
    public override void Play()
    {
        Console.WriteLine($"Гітара {name} грає.");
    }
    public override int GetDuration()
    {
        return 4;
    }
    public override int GetCount()
    {
        return 6;
    }
}