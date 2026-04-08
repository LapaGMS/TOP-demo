using UnityEngine;

public class CharacterLoader : MonoBehaviour
{
    public PartSelector head;
    public PartSelector hair;
    public PartSelector eyes;
    public PartSelector mouth;
    public PartSelector body;

    void Start()
    {
        LoadPart(head, "headIndex", "headColor");
        LoadPart(hair, "hairIndex", "hairColor");
        LoadPart(eyes, "eyesIndex", "eyesColor");
        LoadPart(mouth, "mouthIndex", "mouthColor");
        LoadPart(body, "bodyIndex", "bodyColor");
    }

    void LoadPart(PartSelector part, string indexKey, string colorKey)
    {
        int index = PlayerPrefs.GetInt(indexKey, 0);
        part.SetIndex(index);

        Color color = LoadColor(colorKey);
        part.SetColor(color);

        Debug.Log(indexKey + ": " + index + ", " + colorKey + ": " + color);
    }

    Color LoadColor(string key)
    {
        return new Color(
            PlayerPrefs.GetFloat(key + "_R", 1f),
            PlayerPrefs.GetFloat(key + "_G", 1f),
            PlayerPrefs.GetFloat(key + "_B", 1f),
            PlayerPrefs.GetFloat(key + "_A", 1f)
        );
    }
}
