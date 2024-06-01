using UnityEngine;

public class MobMovement : MonoBehaviour
{ 
    [SerializeField] private Rigidbody _rb;
    [SerializeField] public Transform player;

    public float speed = 2.0f;
    public float rotationSpeed = 5f;
    public float detectionRange = 2.0f;
    public string targetTag;
    public Collider wall;
    public string wallTag;

    // Метод, вызываемый при входе другого коллайдера в триггер
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            Destroy(gameObject);
        }
        if (wall.CompareTag(wallTag))
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        _rb.freezeRotation = true;
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
       
            
            if (distanceToPlayer <= detectionRange)
            { 
                
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

                Vector3 direction = (player.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
           
                
            }
        
        }

    }
}
