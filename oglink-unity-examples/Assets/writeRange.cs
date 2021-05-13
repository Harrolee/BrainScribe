using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using UnityEngine;
using System.Threading;
using Looxid.Link;

public class writeRange : MonoBehaviour
{

    public float csvIncrements = 5;//minutes
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
        // whats in the featureIndexList?
         
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


    private void OnApplicationQuit()
    {
        // stop writing to csvs
        keepWriting = false;

    }

    private void Start()
    {
        //print(Looxid.Link.EEGSensorID);
        LooxidLinkManager.Instance.Initialize();



        // after 30minutes, create a new set of CSVs:
        var autoEvent = new AutoResetEvent(false);

        var timer = new Timer(
            newCSVs,
            null,
            TimeSpan.FromMinutes(5),
            TimeSpan.FromMinutes(5));
    }

    // create a file and write a header to it
    static async Task WriteHeader(string filePath, string fileName, string header)
    {
        using StreamWriter outputFile = new StreamWriter(Path.Combine(filePath, fileName));
        await outputFile.WriteAsync(header);
    }

    // we'll want to asynchronously write to file


    static async Task WriteCSVs()
    {

    }


    void newCSVs(System.Object stateInfo)
    {
        // for starters, do this:

        // create a new file:
        // write a header to that file:
        string filePath = "C:\\Temp";
        string fileName = "Delta";
        string header = "AF3, AF4, AF7, AF8, Fp1, Fp2";
        Header header = await WriteHeader(filePath, fileName, header);



        // create lines for every second of data


        // every x seconds until 5minutes have elapsed:
        featureIndexList2 = LooxidLinkData.Instance.GetEEGFeatureIndexData(x);
        for (int i = 0; i < featureIndexList2.Count; i++)
        {
            // one foreach loop like this for all of the ranges
            // add each of these to a string
            line.AppendFormat("{0},{1},{2},{3},{4},{5}", featureIndexList2[i].Delta(EEGSensorID.AF3),
                                featureIndexList2[i].Delta(EEGSensorID.AF4), featureIndexList2[i].Delta(EEGSensorID.AF7),
                                featureIndexList2[i].Delta(EEGSensorID.AF8), featureIndexList2[i].Delta(EEGSensorID.Fp1),
                                featureIndexList2[i].Delta(EEGSensorID.Fp2)
                                );
            // append this line to the Delta csv
        }
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
    List<EEGFeatureIndex> featureIndexList2;
    float x = 3f;
    EEGSensorID[] sensorIDs = new EEGSensorID[] {
            EEGSensorID.AF3, EEGSensorID.AF4, EEGSensorID.AF7,
            EEGSensorID.AF8, EEGSensorID.Fp1, EEGSensorID.Fp2
        };

    private void OnEnable()
    {


    }

    void buildCSV()
    {

    }




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
