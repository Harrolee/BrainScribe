﻿using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using System.IO;
using UnityEngine;
using System.Threading;
using Looxid.Link;

public class writeRange : MonoBehaviour
{

    public float DataSeconds = 5;
    bool keepWriting = true;
    string filePath = "C:\\temp";
    string delimiter = ",";
    StringBuilder line = new StringBuilder();

    List<EEGFeatureIndex> featureIndexList;

    void Update()
    {
        // Returns EEG feature index data from last 3 seconds
         featureIndexList = LooxidLinkData.Instance.GetEEGFeatureIndexData(1.0f);
    }




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


        // after 30minutes, create a new set of CSVs:
        var autoEvent = new AutoResetEvent(false);

        var timer = new Timer(
            buildCSVs,
            null,
            TimeSpan.FromMinutes(5),
            TimeSpan.FromMinutes(5));
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
     *     public enum EEGSensorID
            {
                AF3 = 0,
                AF4 = 1,
                Fp1 = 2,
                Fp2 = 3,
                AF7 = 4,
                AF8 = 5
            }
     * 
     * 
     * 
     */

    private void OnEnable()
    {
        LooxidLinkData.OnReceiveEEGFeatureIndexes += OnReceiveEEGFeatureIndexes;
    }

    // write a coroutine to call this every 3 seconds
    List<EEGFeatureIndex> featureIndexList2 = LooxidLinkData.Instance.GetEEGFeatureIndexData(3.0f);


    public void buildCSVs(System.Object stateInfo)
    {
        AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
    }













    class CSVBuilder
    {
        int invokeCount;
        public void buildCSVs(System.Object stateInfo)
        {
            AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
        }
        // take featureIndexData and make one csv for every range
        void OnReceiveEEGFeatureIndexes(EEGFeatureIndex featureIndexData)
        {
            // make one line for every second
            //CSV line:
            line.AppendFormat("{0},{1},{2},{3},{4},{5}", featureIndexData.Delta(EEGSensorID.AF3),
                                featureIndexData.Delta(EEGSensorID.AF4), featureIndexData.Delta(EEGSensorID.AF7),
                                featureIndexData.Delta(EEGSensorID.AF8), featureIndexData.Delta(EEGSensorID.Fp1),
                                featureIndexData.Delta(EEGSensorID.Fp2)
                                );



        }
    }

    




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
