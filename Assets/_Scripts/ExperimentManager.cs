using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.InputSystem;

public class ExperimentManager : MonoBehaviour
{

    public DataLogger dataLogger;
    public int participantID = 0;
    public int trialNumber = 1;
    public float curTrialTime = 0.0f;
    public bool paused;

    public TrialManager trialManager;


    void Awake()
    {
        bool overrideDataFile = participantID == 0; // ptpnt ID 0 is a dummy ID, so we can override the data file in that case since we don't care about preserving the data
        dataLogger = new DataLogger(DataLogger.GetCurrentPath() + $"/_Data/ptpnt{participantID}_log_data.csv", overrideDataFile);
        dataLogger.WriteLine("perturbation_occuring");

        trialManager = FindFirstObjectByType<TrialManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            Debug.Log(trialManager.GetTrialInfo(trialNumber)?.TrialType);
            IncrementTrialNumber();
        }
    }

    public void IncrementTrialNumber() 
    {
        trialNumber++;
        Debug.Log($"Trial number incremented to: {trialNumber}");
    }

    public void LogData()
    {

    }

    public void EndExperiment()
    {
        // put black screen here
    }
}
