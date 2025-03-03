using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Bot_behavior : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    private int _speed;
    private float SpeedChangeRate = 5f;
    private float distance;
    private float currentSpeed;

    private bool shooting = false;

    private float _gun_animationBlend;
    [SerializeField] private GameObject pfBullet;
    [SerializeField] private Transform target;
    [SerializeField] private Transform spawnBulletPosition;
    private Transform bot;

    private float repeat_time = 1; /* Время в секундах */
    private float curr_time;

    public void DisableAgent()
    {
        agent.enabled = false;
    }
    void StartTimer()
    {
        curr_time = repeat_time;
    }


    void Awake()
    {
        bot = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

    }

    private void Start()
    {
        //animator.SetBool("Aiming", true);
        //animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));
        animator.SetFloat("MotionSpeed", 1f);
        _speed = Animator.StringToHash("Speed");
    }

    void Update()
    {
        agent.destination = target.position;
        currentSpeed = agent.speed;
        distance = Mathf.Sqrt(Mathf.Pow((target.position.x - bot.position.x), 2) + Mathf.Pow((target.position.z - bot.position.z), 2));
        //Debug.Log(distance);
        if (distance >= 12)
        {
            agent.speed = 4;
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
            StartTimer();
        }
        else
        {
            agent.speed = 0;
            //agent.destination = null;
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));
            /*if(!shooting)
				Shoot(1f);
			shooting = true;*/

            curr_time -= Time.deltaTime;

            if (curr_time <= 0)
            {
                Instantiate(pfBullet, spawnBulletPosition.position, Quaternion.LookRotation(target.transform.position - spawnBulletPosition.position, Vector3.up));
                curr_time = repeat_time;
            }
        }
        /*IEnumerator ShootIEnum( float delayTime)
		{
			Instantiate(pfBullet, spawnBulletPosition.position, Quaternion.LookRotation(target.transform.position, Vector3.up));//, transform.forward
			yield return new WaitForSeconds(delayTime);
			shooting = true;
		}

		//You call this function 
		void Shoot( float delayTime)
		{
			StartCoroutine(ShootIEnum( 1f));
		}*/



        //  animator.SetBool("Aiming", false); 
        agent.speed = Mathf.Lerp(currentSpeed, agent.speed, Time.deltaTime * SpeedChangeRate);
        //Debug.Log(_speed);
        //	_gun_animationBlend = Mathf.Lerp(_gun_animationBlend, agent.speed, Time.deltaTime * SpeedChangeRate);
        animator.SetFloat(_speed, agent.speed);

    }
}
