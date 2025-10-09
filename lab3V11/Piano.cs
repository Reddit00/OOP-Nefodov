class Piano : Instrument
{
    public Piano(string compositionName, int sec) : base(compositionName, sec) { }
    public override void Play()
    {
        Console.WriteLine($"Піаніно грає \"{compositionName}\" ({sec} сек.)");
    }
}