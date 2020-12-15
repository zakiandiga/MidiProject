using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAnnouncer : MonoBehaviour
{
    public static event Action<EventAnnouncer> OnParalyzeOn;
    public static event Action<EventAnnouncer> OnParalyzeOff;
    public static event Action<EventAnnouncer> OnGiantGrowthOn;
    public static event Action<EventAnnouncer> OnGiantGrowthOff;
    public static event Action<EventAnnouncer> OnRandomJump;
    public static event Action<EventAnnouncer> OnSpawn;

    public static event Action<EventAnnouncer> OnKick;

    //Attack power -> rigidbody force/Damage

    public void KickDrum(float f)
    {
        if(OnKick != null)
        {
            OnKick(this);
        }
    }

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

    public void RandomJump(float f)
    {
        if (OnRandomJump != null)
        {
            OnRandomJump(this);
        }
    }
        
    public void Spawn(float f)
    {
        if(OnSpawn != null)
        {
            OnSpawn(this);
        }
    }

}
