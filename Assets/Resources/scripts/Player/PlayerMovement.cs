using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _playerRigidBody;
    private Transform _playerTransform;
    
    public static Vector2 MoveVector;
    public static Vector2 PlayerPosition;

    [Header("Player Stats")]
    public float speed;
    
    // Constant start stats
    private float _baseSpeed;

    private void Start()
    {
        _playerRigidBody = GetComponent<Rigidbody2D>();
        _playerTransform = GetComponent<Transform>();
        
        _baseSpeed = speed;
    }

    private void FixedUpdate()
    {
        MoveVector = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _playerRigidBody.velocity = MoveVector * speed;
        
        PlayerPosition = _playerTransform.position;
    }

    public void addSpeed(float addedSpeed)
    {
        speed += addedSpeed;
    }
}
