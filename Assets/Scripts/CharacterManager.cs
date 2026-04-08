using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public PartSelector selectedPart;

    public Button[] colorButtons;

    private Color[] colors;

    public PartSelector head;
    public PartSelector hair;
    public PartSelector eyes;
    public PartSelector mouth;
    public PartSelector body;

    private void Start()
    {
        colors = new Color[colorButtons.Length];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = colorButtons[i].image.color;
        }
    }

    public void SetColor(int index)
    {
        if (selectedPart != null)
        {
            selectedPart.SetColor(colors[index]);
        }
    }

    public void SelectPart(PartSelector part)
    {
        selectedPart = part;
    }

    public void SaveCharacter()
    {
        PlayerPrefs.SetInt("headIndex", head.GetIndex());
        PlayerPrefs.SetInt("hairIndex", hair.GetIndex());
        PlayerPrefs.SetInt("eyesIndex", eyes.GetIndex());
        PlayerPrefs.SetInt("mouthIndex", mouth.GetIndex());
        PlayerPrefs.SetInt("bodyIndex", body.GetIndex());

        SaveColor("headColor", head.GetColor());
        SaveColor("hairColor", hair.GetColor());
        SaveColor("eyesColor", eyes.GetColor());
        SaveColor("mouthColor", mouth.GetColor());
        SaveColor("bodyColor", body.GetColor());

        PlayerPrefs.Save();

        SceneManager.LoadScene("Game");
    }

    void SaveColor(string key, Color color)
    {
        PlayerPrefs.SetFloat(key + "_R", color.r);
        PlayerPrefs.SetFloat(key + "_G", color.g);
        PlayerPrefs.SetFloat(key + "_B", color.b);
        PlayerPrefs.SetFloat(key + "_A", color.a);
    }
}
