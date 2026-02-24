using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject Target { get; set; }
    [SerializeField] private Vector3 offset;
    
    void Start()
    {
        Target = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        offset.y = Target.transform.localScale.y;
        transform.position = Target.transform.position + offset;
    }
}
