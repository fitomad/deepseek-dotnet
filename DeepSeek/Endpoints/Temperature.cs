using System.ComponentModel;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Fitomad.DeepSeek.Endpoints;

public enum Temperature
{
    Precise,
    SomePrecise,
    Regular,
    SomeRandom,
    Random
}

public static class TemperatureExtension
{
    public static double GetValue(this Temperature temperature)
    {
        var value = temperature switch
        {
            Temperature.Precise => 0.0,
            Temperature.SomePrecise => 0.5,
            Temperature.Regular => 1.0,
            Temperature.SomeRandom => 1.5,
            Temperature.Random => 2.0,
            _ => 1.0
        };

        return value;
    }
}