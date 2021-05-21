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


    public float csvIncrements = 5;//minutes
    public float DataSeconds = 5;
    bool keepWriting = true;
    string filePath = "C:\\temp";
    string delimiter = ",";
    StringBuilder line = new StringBuilder();

    List<EEGFeatureIndex> featureIndexList;
    // every three seconds, write three seconds to a csv.

    public float SecondsToCapture = 150; 
    public float CSVTime = 10; // 300 secs is 5mins
    public float BrainIncrements = 5; //secs
    float incrementCounter = 0;

    string baseFileName = "Delta";
    string header = "AF3, AF4, AF7, AF8, Fp1, Fp2\n";

    /*
    List<EEGFeatureIndex> featureIndexList2;
    EEGSensorID[] sensorIDs = new EEGSensorID[] {
            EEGSensorID.AF3, EEGSensorID.AF4, EEGSensorID.AF7,
            EEGSensorID.AF8, EEGSensorID.Fp1, EEGSensorID.Fp2
        };
    */

    void Start()
    {
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
        print("out of loop");
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
        int arrayLength = 6;
        int[,] test_array = new int[6,6]{ { 0, 1, 2, 3, 4, 5 }, { 'a', 'b', 'c', 'd', 'e', 'f' }, { 'a', 'b', 'c', 'd', 'e', 'f' }, { 'a', 'b', 'c', 'd', 'e', 'f' }, { 'a', 'b', 'c', 'd', 'e', 'f' }, { 'a', 'b', 'c', 'd', 'e', 'f' } };
        for (int i = 0; i < arrayLength; i++)
        {
            print(i);
            line.AppendFormat("{0},{1},{2},{3},{4},{5}\n", test_array[0, i], test_array[1, i], test_array[2, i], test_array[3, i], test_array[4, i], test_array[5, i]);
        }
        return line;
        /*
        featureIndexList2 = LooxidLinkData.Instance.GetEEGFeatureIndexData(BrainIncrements);
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
        */
    }

    // create a file, write a header to it, return the StreamWriter object
    StreamWriter writeHeader(string filePath, string fileName, string header)
    {
        StreamWriter outputFile = new StreamWriter(Path.Combine(filePath, fileName));
        outputFile.Write(header);
        return outputFile;
    }
}