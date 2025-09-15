class Temperature
{
    private double celsius; 
    public double Celsius
    {
        get => celsius;
        set => celsius = value;
    }
    public double Fahrenheit
    {
        get => Celsius * 9 / 5 + 32;
    }
    public double this[int i]
    {
        get => i == 0 ? Celsius : Fahrenheit;
        set
        {
            if (i == 0) Celsius = value;
            else if (i == 1) Celsius = (value - 32) * 5 / 9;
        }
    }
    public static bool operator >(Temperature a, Temperature b) => a.Celsius > b.Celsius;
    public static bool operator <(Temperature a, Temperature b) => a.Celsius < b.Celsius;
    public static bool operator ==(Temperature a, Temperature b) => a.Celsius == b.Celsius;
    public static bool operator !=(Temperature a, Temperature b) => a.Celsius != b.Celsius;
    public override bool Equals(object obj) => obj is Temperature t && this == t;
    public override int GetHashCode() => Celsius.GetHashCode();
}

