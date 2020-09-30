using Kino;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private AnalogGlitch analogGlitch;

    public GameObject mainCam;
    bool contourOn;

    // Start is called before the first frame update
    void Start()
    {
        analogGlitch = mainCam.GetComponent<AnalogGlitch>();

        contourOn = false;
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
}
