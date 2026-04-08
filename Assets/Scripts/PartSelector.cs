using System.Collections;
using System.Collections.Generic;
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

    private AnimationState currentState = AnimationState.Idle;

    private bool isFlipped = false;

    public bool isGame = false;

    public int currentIndex = 0;

    public List<RectTransform> items;
    private List<Vector3> baseScales = new();
    private List<Vector2> basePositions = new();

    public float spacing = 100f;
    public float moveSpeed = 10f;
    public float scaleMultiplier = 1.2f;

    void Start()
    {
        foreach (var item in items)
        {
            baseScales.Add(item.localScale);
            basePositions.Add(item.anchoredPosition);
        }

        UpdatePositions(true);
    }

    void Update()
    {
        if (!isGame)
        {
            UpdatePositions(false);
        }
    }

    public void Next()
    {
        currentIndex++;
        if (currentIndex >= items.Count)
        {
            currentIndex = 0;
        }
    }

    public void Previous()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = items.Count - 1;
        }
    }

    void UpdatePositions(bool instant)
    {
        for (int i = 0; i < items.Count; i++)
        {
            int offset = GetLoopedOffset(i, currentIndex, items.Count);

            Vector2 basePos = basePositions[i];

            Vector2 targetPos = new Vector2(
                offset * spacing,
                basePos.y
            );

            if (instant)
            {
                items[i].anchoredPosition = targetPos;
            }
            else
            {
                items[i].anchoredPosition = Vector2.Lerp(
                    items[i].anchoredPosition,
                    targetPos,
                    Time.deltaTime * moveSpeed
                );
            }

            Vector3 targetScale = baseScales[i] * (i == currentIndex ? scaleMultiplier : 1f);

            items[i].localScale = Vector3.Lerp(
                items[i].localScale,
                targetScale,
                Time.deltaTime * moveSpeed
            );

            if (i == currentIndex)
            {
                items[i].SetAsLastSibling();
            }
        }

        ApplyOffset();
        SetFrame(0);
    }

    int GetLoopedOffset(int index, int center, int count)
    {
        int raw = index - center;

        if (raw > count / 2)
        {
            raw -= count;
        }

        if (raw < -count / 2)
        {
            raw += count;
        }

        return raw;
    }

    public void SetFlip(bool flip)
    {
        isFlipped = flip;
        ApplyOffset();
    }

    public void SetState(AnimationState state)
    {
        currentState = state;
    }

    public void SetColor(Color color)
    {
        targetRenderer.color = color;
    }

    public void SetFrame(int frame)
    {
        var option = options[currentIndex];
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

        if (frames == null || frames.Length == 0) return;

        int frameIndex = frame % frames.Length;
        targetRenderer.sprite = frames[frameIndex];
    }

    void ApplyOffset()
    {
        Vector2 offset = options[currentIndex].offset;

        if (isFlipped)
        {
            offset.x *= -1f;
        }

        targetRenderer.transform.localPosition = offset;
    }

    public int GetIndex()
    {
        return currentIndex;
    }

    public Color GetColor()
    {
        return targetRenderer.color;
    }

    public void SetIndex(int newIndex)
    {
        currentIndex = newIndex;
        ApplyOffset();
        SetFrame(0);
    }
}

public enum AnimationState
{
    Idle,
    Walk
}