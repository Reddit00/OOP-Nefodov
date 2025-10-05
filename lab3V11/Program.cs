using System;
using System.Collections.Generic;

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
}

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


class Program
{
    static void Main()
    {
        List<Instrument> instruments = new List<Instrument>
        {
            new Guitar("Fender"),
            new Piano("Yamaha"),
            new Guitar("Gibson"),
            new Piano("Casio")
        };

        int total = 0;

        foreach (var ins in instruments)
        {
            ins.Play();
            int time = ins.GetDuration() * ins.GetCount();
            Console.WriteLine($"Час: {time} хв\n");
            total += time;
        }

        Console.WriteLine($"Загальна тривалість концерту: {total} хв");
    }
}
