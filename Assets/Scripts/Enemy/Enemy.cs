using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    public float totalHealth = 100;
    public float currentHealth;
    public float attacklDamage;
    public float movementSpeed;
    private bool isAttacking;
    private bool playerisAlive;
    public float coliderRadius;

    private CapsuleCollider cap;
    private Animator anim;

    public float lookRadius = 10;
    public Transform target;
    private NavMeshAgent agent;

    [Header("LifeBar")]
    public Image healthBar;
    public GameObject canvasBar;


    [Header("Path")]
    public List<Transform> PathPoints = new List<Transform>();
    public int currentIndex = 0;

    private void Start()
    {
        anim = GetComponent<Animator>();
        cap = GetComponent<CapsuleCollider>();
        agent = GetComponent<NavMeshAgent>();

        currentHealth = totalHealth;
        playerisAlive = true;
    }

    void MoveToNextPoints()
    {
        if (PathPoints.Count > 0)
        {

            float distance = Vector3.Distance(PathPoints[currentIndex].position, transform.position);
            agent.destination = PathPoints[currentIndex].position;

            if(distance <= 4f)
            {
                //currentIndex++;
                currentIndex = Random.Range(0, PathPoints.Count);
                currentIndex %= PathPoints.Count;
            }
            anim.SetInteger("transition", 1);
            anim.SetBool("walking", true);

        }
    }

    private void Update()
    {
        if (currentHealth > 0)
        {

            float distance = Vector3.Distance(target.position, transform.position);

            if (distance <= lookRadius)
            {
                agent.isStopped = false;
                // O personagem está no raio de ação
                // O inimigo deve seguir o Player/personagem
                if (!anim.GetBool("attacking"))
                {
                    agent.SetDestination(target.position);
                    anim.SetInteger("transition", 1);
                    anim.SetBool("walking", true);
                }



                if (distance <= agent.stoppingDistance)
                {
                    //Aqui o personagem está dentro do raio de ataque
                    //atacar
                    StartCoroutine("Attack");
                    LookTaget();

                }

                if (distance >= agent.stoppingDistance)
                {
                    anim.SetBool("attacking", false);
                }
            }
            else
            {
                // é chamado quando o personagem está completamente fora do raio de ação  
                anim.SetInteger("transition", 0);
                anim.SetBool("walking", false);
                anim.SetBool("attacking", false);
                //agent.isStopped = true;
                MoveToNextPoints();
            }
        }

    }


    IEnumerator Attack()
    {
        if (!isAttacking && playerisAlive && !anim.GetBool("hit"))
        {
            isAttacking = true;
            anim.SetBool("attacking", true);
            anim.SetBool("walking", false);
            anim.SetInteger("transition", 2);
            yield return new WaitForSeconds(1f);
            GetEnemy();
            yield return new WaitForSeconds(1.7f);
            isAttacking = false;
        }
        if(!playerisAlive)
        {
            anim.SetInteger("transition", 0);
            anim.SetBool("walking", false);
            anim.SetBool("attacking", false);
            agent.isStopped = true;
        }


    }

    void GetEnemy()
    {
        foreach (Collider c in Physics.OverlapSphere((transform.position + transform.forward * coliderRadius), coliderRadius))
        {
            if (c.gameObject.CompareTag("Player"))
            {
                //detecta o Player
                c.gameObject.GetComponent<Player>().GetHit(10f);
                playerisAlive = c.gameObject.GetComponent<Player>().isAlive;

            }
        }
    }

    void LookTaget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public void GetHit(float Damage)
    {
        currentHealth -= Damage;

        healthBar.fillAmount = currentHealth / totalHealth;

        if (currentHealth > 0)
        {
            StopCoroutine("Attack");
            anim.SetInteger("transition", 3);
            anim.SetBool("hit", true);
            StartCoroutine(RecoveryFromHit());
            
        }
        else
        {
            // esqueleto morre  
            canvasBar.gameObject.SetActive(false);
            anim.SetInteger("transition", 4);
            cap.enabled = false;
        }
 
    }


    IEnumerator RecoveryFromHit()
    {
        yield return new WaitForSeconds(1f);
        anim.SetInteger("transition", 0);
        anim.SetBool("hit", false);
        isAttacking = false;
    }

   
}
