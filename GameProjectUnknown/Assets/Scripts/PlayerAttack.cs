using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]private float attackCooldown;
    [SerializeField]private Transform firePoint;
    [SerializeField]private GameObject[] fireballs;
    private Animator animate;
    private PlayerMovements playerMovements;
    private float cooldownTimer = Mathf.Infinity;
    
    private void Awake()
    {
        animate = GetComponent<Animator>();
        playerMovements = GetComponent<PlayerMovements>();
    }
    private void Update()
    {
        if(Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovements.canAttack())
        {
            Attack();
        }    
        cooldownTimer += Time.deltaTime;

    }
    
    private void Attack()
    {
        animate.SetTrigger("attack");
        cooldownTimer = 0;

        fireballs[0].transform.position = firePoint.position;
        fireballs[0].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));

    }

}

