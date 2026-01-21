public class TrialInfo
{
    public int TrialNumber { get; private set; }
    public string TrialType { get; private set; }

    public TrialInfo(int trialNumber, string trialType)
    {
        TrialNumber = trialNumber;
        TrialType = trialType;
    }
}
