using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed  = 1;
    public Transform target;
    public Animator animator;

    private void Start()
    {
        Vector3 targetDirection = target.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0, targetAngle+90);
    }
    private void Update()
    {
        if(target == null) { return; }
        if(Vector2.Distance(transform.position, target.position) < .2f)
        {
            animator.SetBool("Attack", true);
            return;
        }
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed*Time.deltaTime);
    }
}
