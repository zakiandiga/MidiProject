using Kino;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private AnalogGlitch analogGlitch;
    private Mirror mirrorComp;

    public GameObject mainCam;
    bool contourOn;
    bool isolineOn;
    bool mirrorOn;
    bool symmetryOn;

    // Start is called before the first frame update
    void Start()
    {
        analogGlitch = mainCam.GetComponent<AnalogGlitch>();
        mirrorComp = mainCam.GetComponent<Mirror>();

        contourOn = false;
        isolineOn = false;
        mirrorOn = false;
        symmetryOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            setScanLineJitter();
        }
    }

    public void setScanLineJitter()
    {
        analogGlitch.scanLineJitter = 1;
    }

    public void setContour()
    {
        if (contourOn == false)
        {
            mainCam.GetComponent<Contour>().enabled = true;
            contourOn = true;
        } else
        {
            mainCam.GetComponent<Contour>().enabled = false;
            contourOn = false;
        }
    }

    public void setIsoline()
    {
        if (isolineOn == false)
        {
            mainCam.GetComponent<Isoline>().enabled = true;
            isolineOn = true;
        }
        else
        {
            mainCam.GetComponent<Isoline>().enabled = false;
            isolineOn = false;
        }
    }

    public void setMirror()
    {
        if (mirrorOn == false)
        {
            mainCam.GetComponent<Mirror>().enabled = true;
            mirrorOn = true;
        }
        else
        {
            mainCam.GetComponent<Mirror>().enabled = false;
            mirrorOn = false;
        }
    }

    public void setSymmetry()
    {
        if (symmetryOn == false)
        {
            mirrorComp.symmetry = true;
            symmetryOn = true;
        }
        else
        {
            mirrorComp.symmetry = false;
            symmetryOn = false;
        }
    }
}
