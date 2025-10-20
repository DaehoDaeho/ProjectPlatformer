using UnityEngine;

[CreateAssetMenu(fileName = "LevelMeta", menuName = "Game/Level Meta", order = 0)]
public class LevelMeta : ScriptableObject
{
    public string displayName;
    public string sceneName;
    public string levelId;
    public Sprite preview;
    public bool initiallyLocked = false;
}
