[System.Serializable]
public class RankData
{
    public float CheckpointScore { get; set; }
    public int CheckpointId { get; set; }
    public int LapCount { get; set; }
    public float MinDistance { get; set; } = float.MaxValue;
}
