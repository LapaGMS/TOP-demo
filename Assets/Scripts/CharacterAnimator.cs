using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    public PartSelector[] parts;

    public PlayerMovement playerMovement;

    public float frameRate = 6f;

    private int currentFrame = 0;
    private float timer = 0f;

    public AnimationState currentState = AnimationState.Idle;

    void Update()
    {
        UpdateState();

        timer += Time.deltaTime;

        if (timer >= 1f / frameRate)
        {
            timer = 0f;
            currentFrame++;
            UpdateFrame();
        }
    }

    void UpdateState()
    {
        Vector2 movement = playerMovement.GetMovement();

        if (movement.magnitude > 0.1f)
        {
            SetState(AnimationState.Walk);
        }
        else
        {
            SetState(AnimationState.Idle);
        }
    }

    void UpdateFrame()
    {
        foreach(var part in parts)
        {
            part.SetState(currentState);
            part.SetFrame(currentFrame);
        }
    }

    public void SetState(AnimationState state)
    {
        if (state == currentState)
        {
            return;
        }

        currentState = state;
        currentFrame = 0;
    }
}
