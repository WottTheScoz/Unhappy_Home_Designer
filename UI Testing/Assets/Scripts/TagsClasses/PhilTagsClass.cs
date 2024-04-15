using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhilTagsClass
{
    public Dictionary<string, float> Tags = new Dictionary<string, float>()
    {
        {"clown", 10 },
        {"spikes", 5 },
        {"money", -10 }
    };

    public Dictionary<string, float> TagsMaxValue = new Dictionary<string, float>()
    {
        {"clown", 10 },
        {"spikes", 5 },
        {"money", -10}
    };
}