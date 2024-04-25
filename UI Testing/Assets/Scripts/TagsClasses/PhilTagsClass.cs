using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhilTagsClass
{
    public Dictionary<string, float> Tags = new Dictionary<string, float>()
    {
        {"clown", 10 },
        {"spikes", 5 },
        {"money", -10 },
        {"grandma", -10}, 
        {"blood", 5}, 
        {"cold", -5}, 
        {"eye", -1}
    };

    public Dictionary<string, float> TagsMaxValue = new Dictionary<string, float>()
    {
        {"clown", 10 },
        {"spikes", 5 },
        {"money", -10},
        {"grandma", -10},
        {"blood", 5}, 
        {"cold", -5}, 
        {"eye", -1}
    };

    /*public Dictionary<string, float> Tags
    {
        get
        {
            return TagsMaxValue;
        } 
    }*/
}    