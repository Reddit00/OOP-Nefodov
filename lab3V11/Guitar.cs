class Guitar : Instrument
{
    public Guitar(string compositionName, int sec) : base(compositionName, sec) { }
    public override void Play()
    {
        Console.WriteLine($"Гітара грає \"{compositionName}\" ({sec} сек.)");
    }
}