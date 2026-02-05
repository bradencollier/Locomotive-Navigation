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
    public GameObject XRorigin;

    public TrialManager trialManager;

    void Awake()
    {
        bool overrideDataFile = participantID == 0; // ptpnt ID 0 is a dummy ID, so we can override the data file in that case since we don't care about preserving the data
        dataLogger = new DataLogger(DataLogger.GetCurrentPath() + $"/_Data/ptpnt{participantID}_log_data.csv", overrideDataFile);
        dataLogger.WriteLine("x_pos,y_pos,z_pos,x_rot,y_rot,z_rot,perturbation_occuring,trial_number,trial_type,time");

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

    void FixedUpdate()
    {
        curTrialTime += Time.fixedDeltaTime;
    }

    void LateUpdate()
    {
        if (Time.frameCount > 200)
        {
            LogData();
        }
    }

    void LogData()
    {
        string data = "";
        data += XRorigin.transform.position.x + ",";
        data += XRorigin.transform.position.y + ",";
        data += XRorigin.transform.position.z + ",";
        data += XRorigin.transform.rotation.x + ",";
        data += XRorigin.transform.rotation.y + ",";
        data += XRorigin.transform.rotation.z + ",";
        data += "filler" + ",";
        data += trialNumber + ",";
        data += trialManager.GetTrialInfo(trialNumber)?.TrialType + ",";
        data += curTrialTime + ",";

        dataLogger.WriteLine(data);
    }

    public void IncrementTrialNumber() 
    {
        trialNumber++;
        Debug.Log($"Trial number incremented to: {trialNumber}");
    }

    public void EndExperiment()
    {
        // put black screen here
    }
}
