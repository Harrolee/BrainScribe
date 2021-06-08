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
    StringBuilder line = new StringBuilder();

    float incrementCounter = 0;

    string baseFileName = "Delta";
    string header = "AF3, AF4, AF7, AF8, Fp1, Fp2\n";

    
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

    string buildHeader()
    {
        string[] ranges = { "Alpha", "Beta", "Delta", "Gamma" };
        StringBuilder header = new StringBuilder();
        for range in ranges {
            header.AppendFormat(":{0},{1},{2},{3},{4},{5}", );
        }
        
        

        return header;
    }

    IEnumerator buildCSV(string fileTag)
    {
        StringBuilder newLine;
        //build header
        string fileName = baseFileName + fileTag;
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

    StringBuilder addScanSet()
    {
        featureIndexList2 = LooxidLinkData.Instance.GetEEGFeatureIndexData(BrainIncrements);
        for (int i = 0; i < featureIndexList2.Count; i++)
        {

            // Delta
            // one foreach loop like this for all of the ranges
            // add each of these to a string
            line.AppendFormat("{0},{1},{2},{3},{4},{5}", featureIndexList2[i].Delta(EEGSensorID.AF3),
                                featureIndexList2[i].Delta(EEGSensorID.AF4), featureIndexList2[i].Delta(EEGSensorID.AF7),
                                featureIndexList2[i].Delta(EEGSensorID.AF8), featureIndexList2[i].Delta(EEGSensorID.Fp1),
                                featureIndexList2[i].Delta(EEGSensorID.Fp2)
                                );
            // append this line to the Delta csv

            //Alpha
            line.AppendFormat("{0},{1},{2},{3},{4},{5}", featureIndexList2[i].Alpha(EEGSensorID.AF3),
                    featureIndexList2[i].Alpha(EEGSensorID.AF4), featureIndexList2[i].Alpha(EEGSensorID.AF7),
                    featureIndexList2[i].Alpha(EEGSensorID.AF8), featureIndexList2[i].Alpha(EEGSensorID.Fp1),
                    featureIndexList2[i].Alpha(EEGSensorID.Fp2)
                    );

        }
        return line;
    }

    // create a file, write a header to it, return the StreamWriter object
    StreamWriter writeHeader(string filePath, string fileName, string header)
    {
        StreamWriter outputFile = new StreamWriter(Path.Combine(filePath, fileName));
        outputFile.Write(header);
        return outputFile;
    }
}