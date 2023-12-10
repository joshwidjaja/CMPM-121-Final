using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantDefinitionLanguage : MonoBehaviour
{
    public void Name(string name);
    public void Color(string color);
    public void GrowsWhen(Func<GrowthContext, bool> growsWhen);
}

public class GrowthContext
{
    // to be specified
}
