using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using names_source;

public class RobotBehavior : MonoBehaviour
{
    [SerializeField] private GameObject pfBullet;

    [SerializeField] private Transform spawnBulletPosition;
    private Transform bot;

    private float repeat_time = 0.4f;
   // private float focus_time = 4f;
    private float curr_time;

    private Animator animator;
    private NavMeshAgent agent;

    [SerializeField] Transform first_place;
    [SerializeField] Transform second_place;
    [SerializeField] Transform player;

    
    float distance;
    private Vector3 target;
    private Vector3 aim_target;
    bool onfirst;
    int name = 1;
   // bool seeing_aim;
    private float currentSpeed;
    private float nextSpeed;
    private float SpeedChangeRate = 5f;
    //private int bot_HP=40;
    // public Source.enemy name.ToString() = new Source.enemy(40, 7, true);
    private Source source;
    
    private Source.enemy robot_1 = new Source.enemy(40, 7, true);
    void Awake()
    {
        bot = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

    }
    void Start()
    {
        nextSpeed = 2.5f;
        onfirst = true;
        curr_time = repeat_time;
        agent.destination = first_place.position;
        target = agent.destination;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (other.name == "shotgun_bullet(Clone)")
        {
            robot_1.health_points -= 12;
            // Debug.Log(bot_HP);
        }
    }
    void Update()
    {
        if (robot_1.health_points <= 0)
        {
            BodyRagdoll.died = true;
            RobotBehavior robot_beh = GetComponent<RobotBehavior>();
            robot_beh.enabled = false;
            GunFollower.unconnected = true;
        }
        
        currentSpeed = agent.speed;
        agent.speed = Mathf.Lerp(currentSpeed, nextSpeed, Time.deltaTime * SpeedChangeRate);
        animator.SetFloat("speed", agent.speed);

        distance = Mathf.Sqrt(Mathf.Pow((target.x - bot.position.x), 2) + Mathf.Pow((target.z - bot.position.z), 2));
        curr_time -= Time.deltaTime;

        if (distance <= 1f && onfirst && !Observer.seeing)
        {

            target = second_place.position;
            agent.destination = second_place.position;
            onfirst = false;
        }
        else if (distance <= 1 && !onfirst && !Observer.seeing)
        {
            target = first_place.position;
            agent.destination = first_place.position;
            onfirst = true;
        }

        if (Observer.observed)
        {
            if (Observer.seeing)
            {
                //focus_time = 4f;
                bot.transform.rotation = Quaternion.LookRotation(player.position - bot.position, Vector3.up);
                // seeing_aim = true;
                nextSpeed = 0f;
                aim_target = player.position;
                agent.destination = aim_target;
                if (curr_time <= 0)
                {
                    Instantiate(pfBullet, spawnBulletPosition.position, Quaternion.LookRotation(player.position + new Vector3(0, 1.3f, 0) - spawnBulletPosition.position, Vector3.up));
                    curr_time = repeat_time;
                   // Debug.Log(spawnBulletPosition.position);
                }
            }
            if (!Observer.seeing)
            {
                nextSpeed = 4.2f;
            }
        }
        else
        {
            //focus_time -= Time.deltaTime;
            // seeing_aim = false;
            if (!Observer.seeing)
            {
                nextSpeed = 2.5f;
                agent.destination = target;
            }
            
                
            
        }

    }
}