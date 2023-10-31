using System;
using System.Collections.Generic;

namespace WorldOfZuul.Models;

public class Disaster
{
    private static Random _random = new Random();

    private List<string> _disasters = new List<string>
    {
        "A sudden earthquake shakes the ground beneath you!",
        "A violent storm brews, rain pouring heavily.",
        "A wildfire breaks out nearby, spreading rapidly.",
        "A flood starts to rise, submerging everything in its path.",
        "A tornado forms in the distance, approaching quickly.",
        "A severe drought affects the region, drying up water sources.",
        "A massive blizzard engulfs the area, reducing visibility to zero.",
        "A volcanic eruption occurs, spewing lava and ash everywhere.",
        "A tsunami warning is issued as the waters begin to rise.",
        "A meteor shower turns deadly as a large meteorite crashes nearby."
    };

    public string TriggerRandomDisaster()
    {
        int index = _random.Next(_disasters.Count);
        return _disasters[index];
    }
}