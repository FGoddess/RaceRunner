using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "Player Level", menuName = "Create Player Level")]
public class PlayerLevel : ScriptableObject
{
    [SerializeField] private int _level;

    public string Level => _level.ToString();
    public int LevelNumber => _level;

    public void IncreaseLevel()
    {
        _level++;

#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }
}
