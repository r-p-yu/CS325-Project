using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTester : MonoBehaviour
{
    public CropPlot[] plot;

    public void Press()
    {
        foreach (var plot in plot)
        {
            plot.Plant(1);
        }
    }

    public void Grow()
    {
        foreach (var plot in plot)
        {
            plot.Grow();
        }
    }

    public void Harvest()
    {
        foreach (var plot in plot)
        {
            plot.Harvest();
        }
    }
}
