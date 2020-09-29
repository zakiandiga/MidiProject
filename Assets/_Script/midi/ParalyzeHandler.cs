using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalyzeHandler : MonoBehaviour
{
    public static event Action<ParalyzeHandler> OnParalyzeOn;
    public static event Action<ParalyzeHandler> OnParalyzeOff;
    public static event Action<ParalyzeHandler> OnGiantGrowthOn;
    public static event Action<ParalyzeHandler> OnGiantGrowthOff;

    //Attack power -> rigidbody force/Damage

    public void GiantGrowthOn(float f)
    {
        f = 0;
        if(OnGiantGrowthOn != null)
        {
            OnGiantGrowthOn(this);
        }
    }

    public void GiantGrowthOff(float f)
    {
        f = 0;
        if (OnGiantGrowthOff != null)
        {
            OnGiantGrowthOff(this);
        }
    }

    public void ParalyzeOn(float f)
    {
        f = 0;

        if(OnParalyzeOn != null)
        {
            OnParalyzeOn(this);
        }
    }

    public void ParalyzeOff(float f)
    {
        f = 0;

        if (OnParalyzeOff != null)
        {
            OnParalyzeOff(this);
        }
    }

}
