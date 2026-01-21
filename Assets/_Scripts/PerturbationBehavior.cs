using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class PerturbationBehavior : MonoBehaviour
{
    public GameObject starfield;
    public float perturbationTime;

    private TrialManager trialManager;
    private ExperimentManager experimentManager;

    public string currentTrialType = "";

    public float rot1deg = 5f;
    public float rot2deg = 10f;
    public float rot3deg = 15f;
    public float rot4deg = 20f;

    void Awake()
    {
        trialManager = FindFirstObjectByType<TrialManager>();
        experimentManager = FindFirstObjectByType<ExperimentManager>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            CheckPerturbation();
        }
    }

    public void RotateZ(float degrees, float duration)
    {
        StartCoroutine(RotateRoutine(degrees, duration));
    }

    public void CheckPerturbation()
    {
        if (trialManager.AreAllTrialsCompleted()) // Check if all trials are completed, if so, stop
                {
                    Debug.Log("All trials have been completed.");
                    // TODO: Need a way to tell the participant that all trials have been completed
                    return; // Stop further processing
                }
                TrialInfo currentTrial = trialManager.GetTrialInfo(experimentManager.trialNumber);
                currentTrialType = currentTrial?.TrialType;
                Debug.Log($"Current trial number: {experimentManager.trialNumber}");
                Debug.Log($"Current trial type: {currentTrial?.TrialType}");

                if (currentTrial != null)
                {
                    switch (currentTrialType)
                    {
                        case "control":
                            Debug.Log("Control. No perturbation applied.");
                            experimentManager.IncrementTrialNumber();
                            break;
                        case "rotR1":
                            ApplyPerturbation(currentTrialType);
                            break;
                        case "rotR2":
                            ApplyPerturbation(currentTrialType);
                            break;
                        case "rotR3":
                            ApplyPerturbation(currentTrialType);
                            break;
                        case "rotR4":
                            ApplyPerturbation(currentTrialType);
                            break;
                        case "rotL1":
                            ApplyPerturbation(currentTrialType);
                            break;
                        case "rotL2":
                            ApplyPerturbation(currentTrialType);
                            break;
                        case "rotL3":
                            ApplyPerturbation(currentTrialType);
                            break;
                        case "rotL4":
                            ApplyPerturbation(currentTrialType);
                            break;
                    }
                }
    }

    public void ApplyPerturbation(string trialType)
    {
        switch (trialType)
        {
            case "rotR1":
                RotateZ(-rot1deg, perturbationTime);
                Debug.Log($"Clockwise rotation, magnitude of {rot1deg} degrees. Perturbation beginning.");
                experimentManager.IncrementTrialNumber();
                break;
            case "rotR2":
                RotateZ(-rot2deg, perturbationTime);
                Debug.Log($"Clockwise rotation, magnitude of {rot2deg} degrees. Perturbation beginning.");
                experimentManager.IncrementTrialNumber();
                break;
            case "rotR3":
                RotateZ(-rot3deg, perturbationTime);
                Debug.Log($"Clockwise rotation, magnitude of {rot3deg} degrees. Perturbation beginning.");
                experimentManager.IncrementTrialNumber();
                break;
            case "rotR4":
                RotateZ(-rot4deg, perturbationTime);
                Debug.Log($"Clockwise rotation, magnitude of {rot4deg} degrees. Perturbation beginning.");
                experimentManager.IncrementTrialNumber();
                break;
            case "rotL1":
                RotateZ(rot1deg, perturbationTime);
                Debug.Log($"Counter-Clockwise rotation, magnitude of {rot1deg} degrees. Perturbation beginning.");
                experimentManager.IncrementTrialNumber();
                break;
            case "rotL2":
                RotateZ(rot2deg, perturbationTime);
                Debug.Log($"Counter-Clockwise rotation, magnitude of {rot2deg} degrees. Perturbation beginning.");
                experimentManager.IncrementTrialNumber();
                break;
            case "rotL3":
                RotateZ(rot3deg, perturbationTime);
                Debug.Log($"Counter-Clockwise rotation, magnitude of {rot3deg} degrees. Perturbation beginning.");
                experimentManager.IncrementTrialNumber();
                break;
            case "rotL4":
                RotateZ(rot4deg, perturbationTime);
                Debug.Log($"Counter-Clockwise rotation, magnitude of {rot4deg} degrees. Perturbation beginning.");
                experimentManager.IncrementTrialNumber();
                break;
        }
           
    }

    IEnumerator RotateRoutine(float degrees, float duration)
    {
        float elapsed = 0f;
        float startZ = starfield.transform.eulerAngles.z;
        float targetZ = startZ + degrees;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float z = Mathf.Lerp(startZ, targetZ, t);

            Vector3 euler = starfield.transform.eulerAngles;
            euler.z = z;
            starfield.transform.eulerAngles = euler;

            elapsed += Time.deltaTime;
            yield return null;
        }

        Vector3 finalEuler = starfield.transform.eulerAngles;
        finalEuler.z = targetZ;
        starfield.transform.eulerAngles = finalEuler;
    }
}
