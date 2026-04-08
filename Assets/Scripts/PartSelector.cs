using UnityEditor.Tilemaps;
using UnityEngine;

[System.Serializable]
public class AnimationSet
{
    public Sprite[] frames;
}

[System.Serializable]
public class PartOption
{
    public AnimationSet idle;
    public AnimationSet walk;
    public Vector2 offset;
}

public class PartSelector : MonoBehaviour
{
    public PartOption[] options;
    public SpriteRenderer targetRenderer;

    int index = 0;

    private AnimationState currentState = AnimationState.Idle;

    private bool isFlipped = false;

    public void SetFlip(bool flip)
    {
        isFlipped = flip;
        ApplyOffset();
    }

    public void SetState(AnimationState state)
    {
        currentState = state;
    }

    public void Select(int optionIndex)
    {
        index = optionIndex;
        ApplyOffset();
        SetFrame(0);
    }

    public void SetColor(Color color)
    {
        targetRenderer.color = color;
    }

    public void SetFrame(int frame)
    {
        var option = options[index];
        Sprite[] frames = null;

        switch (currentState)
        {
            case AnimationState.Idle:
                frames = option.idle.frames;
                break;
            case AnimationState.Walk:
                frames = option.walk.frames;
                break;
        }

        if(frames == null || frames.Length == 0) return;

        int frameIndex = frame % frames.Length;
        targetRenderer.sprite = frames[frameIndex];
    }

    void ApplyOffset()
    {
        Vector2 offset = options[index].offset;

        if (isFlipped)
        {
            offset.x *= -1f;
        }

        targetRenderer.transform.localPosition = offset;
    }

    public int GetIndex()
    {
        return index;
    }

    public Color GetColor()
    {
        return targetRenderer.color;
    }

    public void SetIndex(int newIndex)
    {
        index = newIndex;
        ApplyOffset();
        SetFrame(0);
    }
}

public enum AnimationState
{
    Idle,
    Walk
}