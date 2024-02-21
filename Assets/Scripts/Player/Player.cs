using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float currentHealth;
    public float totalHealth = 100;
   

    public float Speed;
    public float RotSpeed;
    private float Rotation;
    public float Gravity;
    public float coliderRadius;

    private bool isAttacking;
    public bool isAlive;


    public Image playerBarLife;
    public GameObject barLifePlayer;
    

    public float EnemyDamage = 30;

    Vector3 moveDirection;

    CharacterController Controller;
    Animator anim;


    List<Transform> EnemiesList = new List<Transform>();

   

    // Start is called before the first frame update
    void Start()
    {
        Controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
      

        currentHealth = totalHealth;
        isAlive = true;

    }

    // Update is called once per frame
    void Update()
    {
        if(EventSystem.current.currentSelectedGameObject == null)
        {
            Move();
            GetMouseInput();
        }
        
    }

    void Move()
    {
        if (Controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (!anim.GetBool("attacking"))
                {
                    anim.SetBool("walking", true);
                    anim.SetInteger("transition", 1);
                    moveDirection = Vector3.forward * Speed;
                    moveDirection = transform.TransformDirection(moveDirection);
                }
                else
                {
                    anim.SetBool("walking", false);
                    moveDirection = Vector3.zero;
                    //StartCoroutine(Attack(1));
                }
            }
               

            if (Input.GetKeyUp(KeyCode.W))
            {
                anim.SetBool("walking", false);
                anim.SetInteger("transition", 0);
                moveDirection = Vector3.zero;
            }
        }

        Rotation += Input.GetAxis("Horizontal") * RotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, Rotation, 0);

        moveDirection.y -= Gravity * Time.deltaTime;
        Controller.Move(moveDirection * Time.deltaTime);
    }


    void GetMouseInput()
    {
        if (Controller.isGrounded)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (anim.GetBool("walking"))
                {
                    anim.SetBool("walking",false);
                    anim.SetInteger("transition", 0);
                }

                if (!anim.GetBool("walking"))
                {
                    //EXECUTA O ATAQUE
                   StartCoroutine("Attack");
                }
            }
        }
    }


    IEnumerator Attack()
    {
        if (!isAttacking && !anim.GetBool("hit"))
        {
            isAttacking = true;
            anim.SetBool("attacking", true);
            anim.SetInteger("transition", 2);

            yield return new WaitForSeconds(.5f);

            GetEnemiesRange();

            foreach (Transform enemies in EnemiesList)
            {
                // executar ação de dano no inimigo
                Enemy enemy = enemies.GetComponent<Enemy>();

                if (enemy != null)
                {
                    enemy.GetHit(EnemyDamage);
                }

            }

            yield return new WaitForSeconds(.8f);

            anim.SetBool("attacking", false);
            anim.SetInteger("transition", 0);
            isAttacking = false;
        }
       

    }


    void GetEnemiesRange()
    {
        EnemiesList.Clear();

        foreach (Collider c in Physics.OverlapSphere((transform.position + transform.forward * coliderRadius), coliderRadius))
        {
            if (c.gameObject.CompareTag("Enemy"))
            {
                EnemiesList.Add(c.transform);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + transform.forward , coliderRadius);
    }

    public void GetHit(float Damage)
    {
        currentHealth -= Damage;

        playerBarLife.fillAmount = currentHealth / totalHealth;

        if (currentHealth > 0)
        {
            // toma hit aqui
            StopCoroutine("Attack");
            anim.SetInteger("transition", 3);
            anim.SetBool("hit", true);
            StartCoroutine(RecoveryFromHit());
           
        }
        else
        {
            //morre aqui
            barLifePlayer.gameObject.SetActive(false);
            anim.SetInteger("transition", 4);
            isAlive = false;
           
        }

    }


    IEnumerator RecoveryFromHit()
    {
        yield return new WaitForSeconds(1.1f);
        anim.SetInteger("transition", 0);
        anim.SetBool("hit", false);
        isAttacking = false;
        anim.SetBool("attacking", false);
    }


    public void IncreaseStats(float Health,float IncreaseSpeed, float Damage)
    {
        currentHealth += Health;
        Speed += IncreaseSpeed;
        EnemyDamage += Damage;
    }

    public void DecreaseStats(float Health, float IncreaseSpeed, float Damage)
    {
        currentHealth -= Health;
        Speed -= IncreaseSpeed;
        EnemyDamage -= Damage;
    }
}
