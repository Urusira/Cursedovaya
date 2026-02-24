using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animatorComponent;
    private SpriteRenderer _spriteRendererComponent;

    void Start()
    {
        _animatorComponent = GetComponent<Animator>();
        _spriteRendererComponent = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        WalkingStateController();
        SpriteOrientationSet();
    }

    void WalkingStateController()
    {
        if (PlayerMovement.MoveVector.x != 0 || PlayerMovement.MoveVector.y != 0)
        {
            _animatorComponent.SetBool("moving", true);
        }
        else
        {
            _animatorComponent.SetBool("moving", false);
        }
    }

    void SpriteOrientationSet()
    {
        if (PlayerMovement.MoveVector.x > 0)
        {
            _spriteRendererComponent.flipX = false;
        }
        else if (PlayerMovement.MoveVector.x < 0)
        {
            _spriteRendererComponent.flipX = true;
        }
    }
    
    
}
