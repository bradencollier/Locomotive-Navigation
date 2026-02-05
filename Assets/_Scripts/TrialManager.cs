using System.Collections.Generic;
using UnityEngine;

public class TrialManager : MonoBehaviour
{
    [Header("Trial Info CSV")]
    public TextAsset trialInfoCSV;

    private List<TrialInfo> trialInfoList = new List<TrialInfo>();

    public int TrialCount => trialInfoList.Count;

    private ExperimentManager experimentManager;

    void Awake()
    {
        if (trialInfoCSV == null)
        {
            Debug.LogError("TrialInfoManager: No trialInfoCSV assigned.");
            return;
        }

        ParseCSV(trialInfoCSV.text);
        
        experimentManager = FindFirstObjectByType<ExperimentManager>();
    }

    void ParseCSV(string csvContent)
    {
        string[] lines = csvContent.Split('\n');

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] values = lines[i].Split(',');
            if (values.Length != 2) continue;

            int trialNumber = int.Parse(values[0].Trim());
            string trialType = values[1].Trim();

            trialInfoList.Add(new TrialInfo(trialNumber, trialType));
        }

        Debug.Log($"Loaded {trialInfoList.Count} trials.");
    }

    public TrialInfo GetTrialInfo(int trialNumber)
    {
        if (trialNumber <= 0 || trialNumber > trialInfoList.Count)
        {
            Debug.Log($"Invalid trial number {trialNumber}");
            return null;
        }

        return trialInfoList[trialNumber - 1];
    }

    public bool AreAllTrialsCompleted()
    {
        return experimentManager.trialNumber > trialInfoList.Count;
    }
}

