using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using System.IO;
using UnityEngine;
using Looxid.Link;

public class writeRange : MonoBehaviour
{
    bool keepWriting = true;
    string filePath = "C:\\temp";
    string delimiter = ",";
    StringBuilder line = new StringBuilder();

    /*
     *
     *  
     * 
     *  
     *  while (keepWriting)
     *  
         *  for (5mins) do this:
         *      - construct a new line of the csv
         *      - open the csv file (with path as file)
         *          - write that line to the csv file
         *      - close the csv file
     * 
     * 
    */

    /*
    [Header("Message")]
    public bool displayLinkMessage;
    public CanvasGroup DisconnecetdPanel;
    public CanvasGroup SensorOffPanel;
    public CanvasGroup NoiseSignalPanel;
    */

    private void OnApplicationQuit()
    {
        // stop writing to csvs
        keepWriting = false;

    }

    private void Start()
    {
        //print(Looxid.Link.EEGSensorID);
        LooxidLinkManager.Instance.Initialize();
        // could replace the following with println statements
        //LooxidLinkManager.Instance.SetDisplayDisconnectedMessage(displayLinkMessage);
        //LooxidLinkManager.Instance.SetDisplayNoiseSignalMessage(displayLinkMessage);
        //LooxidLinkManager.Instance.SetDisplaySensorOffMessage(displayLinkMessage);
    }

    /*
     * There gonna be 5 csvs written here. 
     *  We're gonna write a class that will be used four times, once for every range.
     *  This class will be called writeRange
     *      It will need to contain a method to build a line
     *          this line will be made of six things:
     *          - AF3, AF4, Fp1, Fp2, AF8, AF8??? six things? really Lee?
     * 
     * buildLine will need to take all 6 sensors and make a line out of them.
     *  How do we get the sensors? Do we get em 
     */


    /*
    private void OnEnable()
    {
        LooxidLinkData.OnReceiveEEGFeatureIndexes += OnReceiveEEGFeatureIndexes;
    }
    
    void OnReceiveEEGFeatureIndexes(EEGFeatureIndex featureIndex)
    {
        Debug.Log("AF3_Alpha: " + featureIndex.Alpha(EEGSensorID.AF3));
    }
    */




   /* private void buildLine()
    {
        try
        {

        }
        catch (Exception ex)
        {
            print("it broke: " + ex);
        }
    }
   */
}
