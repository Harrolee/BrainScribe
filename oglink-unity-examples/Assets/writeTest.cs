using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using UnityEngine;
using System.Threading;
using Looxid.Link;

public class writeTest : MonoBehaviour
{
    public float SecondsToCapture = 150;
    public float CSVTime = 10; // 300 secs is 5mins
    public float BrainIncrements = 5; //secs

    string filePath = "C:\\temp";

    float incrementCounter = 0;

    string baseFileName = "Delta";

    
    List<EEGFeatureIndex> featureIndexList2;
    EEGSensorID[] sensorIDs = new EEGSensorID[] {
            EEGSensorID.AF3, EEGSensorID.AF4, EEGSensorID.AF7,
            EEGSensorID.AF8, EEGSensorID.Fp1, EEGSensorID.Fp2
        };
    

    void Start()
    {
        LooxidLinkManager.Instance.Initialize();
        string fileTag = "1";
        cycle(fileTag);
    }

    void cycle(string fileTag)
    {
        StartCoroutine(buildCSV(fileTag));
    }

    IEnumerator buildCSV(string fileTag)
    {
        StringBuilder newLine;
        //build header
        string fileName = baseFileName + fileTag;
        string header = buildHeader();
        StreamWriter outputFile = writeHeader(filePath, fileName, header);
        
        while (incrementCounter < CSVTime) // create a new row every x seconds with x seconds of data
        {
            yield return new WaitForSecondsRealtime(BrainIncrements);
            incrementCounter += BrainIncrements;
            
            newLine = addScanSet();
            outputFile.Write(newLine);
            print(incrementCounter);
            print(CSVTime);
        }
        // close the file
        outputFile.Close();

        // change the filename so that we create a new file rather than write over our old one.
        // increment the fileTag
        fileTag = Convert.ToString(Convert.ToInt32(fileTag) + 1);
        if (fileTag != Convert.ToString(SecondsToCapture/incrementCounter)){
            cycle(fileTag);
        }
    }

    /* Header:
     * Every sensor id for every frequency range:
     *      ie: Alpha:AF3, Alpha:AF4, Alpha:AF7, Alpha:AF8, Alpha:Fp1, Alpha:Fp2, Beta:AF3, Beta:AF4, Beta:AF7, etc
     */
    string buildHeader()
    {
        string[] ranges = { "Alpha", "Beta", "Delta", "Gamma" };
        StringBuilder header = new StringBuilder();
        foreach (string range in ranges)
        {
            header.AppendFormat("{0}:AF3, {0}:AF4, {0}:AF7, {0}:AF8, {0}:Fp1, {0}:Fp2", range);
        }
        return header.ToString();
    }

    StringBuilder addScanSet()
    {
        StringBuilder row = new StringBuilder();
        featureIndexList2 = LooxidLinkData.Instance.GetEEGFeatureIndexData(BrainIncrements);
        // every object in 
        /*
        foreach (EEGFeatureIndex featureIndex in featureIndexList2)
        {

        }*/
        for (int i = 0; i < featureIndexList2.Count; i++)
        {
            //Alpha
            row.AppendFormat("{0},{1},{2},{3},{4},{5}", featureIndexList2[i].Alpha(EEGSensorID.AF3),
                    featureIndexList2[i].Alpha(EEGSensorID.AF4), featureIndexList2[i].Alpha(EEGSensorID.AF7),
                    featureIndexList2[i].Alpha(EEGSensorID.AF8), featureIndexList2[i].Alpha(EEGSensorID.Fp1),
                    featureIndexList2[i].Alpha(EEGSensorID.Fp2)
                    );

            //Beta
            row.AppendFormat("{0},{1},{2},{3},{4},{5}", featureIndexList2[i].Beta(EEGSensorID.AF3),
                    featureIndexList2[i].Beta(EEGSensorID.AF4), featureIndexList2[i].Beta(EEGSensorID.AF7),
                    featureIndexList2[i].Beta(EEGSensorID.AF8), featureIndexList2[i].Beta(EEGSensorID.Fp1),
                    featureIndexList2[i].Beta(EEGSensorID.Fp2)
                    );

            // Delta
            row.AppendFormat("{0},{1},{2},{3},{4},{5}", featureIndexList2[i].Delta(EEGSensorID.AF3),
                                featureIndexList2[i].Delta(EEGSensorID.AF4), featureIndexList2[i].Delta(EEGSensorID.AF7),
                                featureIndexList2[i].Delta(EEGSensorID.AF8), featureIndexList2[i].Delta(EEGSensorID.Fp1),
                                featureIndexList2[i].Delta(EEGSensorID.Fp2)
                                );
            // append this row to the Delta csv

            //Gamma
            row.AppendFormat("{0},{1},{2},{3},{4},{5}", featureIndexList2[i].Gamma(EEGSensorID.AF3),
                    featureIndexList2[i].Gamma(EEGSensorID.AF4), featureIndexList2[i].Gamma(EEGSensorID.AF7),
                    featureIndexList2[i].Gamma(EEGSensorID.AF8), featureIndexList2[i].Gamma(EEGSensorID.Fp1),
                    featureIndexList2[i].Gamma(EEGSensorID.Fp2)
                    );

            //Theta
            row.AppendFormat("{0},{1},{2},{3},{4},{5}", featureIndexList2[i].Theta(EEGSensorID.AF3),
                    featureIndexList2[i].Theta(EEGSensorID.AF4), featureIndexList2[i].Theta(EEGSensorID.AF7),
                    featureIndexList2[i].Theta(EEGSensorID.AF8), featureIndexList2[i].Theta(EEGSensorID.Fp1),
                    featureIndexList2[i].Theta(EEGSensorID.Fp2)
                    );

        }
        return row;
    }

    // create a file, write a header to it, return the StreamWriter object
    StreamWriter writeHeader(string filePath, string fileName, string header)
    {
        StreamWriter outputFile = new StreamWriter(Path.Combine(filePath, fileName));
        outputFile.Write(header);
        return outputFile;
    }
}