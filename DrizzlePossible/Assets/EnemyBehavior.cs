using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
	Idle,
	Chase,
	Attack
}


public class EnemyBehavior : MonoBehaviour
{
	[SerializeField]
	EnemyState _currentState = EnemyState.Idle; // Enemy State
	NavMeshAgent enemy; // Nav Mesh enemy object
	GameObject player; // player object
	public int aggroRange = 10; // When player is within this distance, chase the player
    public int attackRange = 5; // distance away from the agent where the agent will attempt to attack
    public float wanderPositionDelay = 2.0f; // Time between wander destination calculations
    public float maxWanderDistance = 10f; // Max distance a destination can be caluclated to wander to.
	Vector3 wanderPosition; // Position that when patrolling the agent will walk to
	float randX,randY,randZ; // Used to calculate random wander position
	float jumpDelay = 0.3f;
	float lastJump = 0;
	public Rigidbody rb;
	public float jumpStrength = 10f;
	Vector3 jumpTarget;
	public float jumpScalar = 0.5f;
	private NavMeshAgent agent;
	bool isGrounded = true;
	bool agentStatus = true;

	//Known Bug: Enemy just walks... forward?
	
	
	    // Start is called before the first frame update
	    void Start()
	    {
	        enemy = GetComponent<NavMeshAgent>();
	        player = GameObject.FindWithTag("Player");
			agent = GetComponent<NavMeshAgent>();
			rb = GetComponent<Rigidbody>();
	        InvokeRepeating("randomWanderPosition",wanderPositionDelay,wanderPositionDelay);

	    }
	void Update()
	{
		switch(_currentState)
		{
			case EnemyState.Idle:
			//Idle state should switch to Chase state after finding a player
				LookForTargets();
				Patrol();
				break;
			case EnemyState.Chase:
			//Chase state should switch to Attack state if player is within range, or Idle state if player moves out of range
				ChasePlayer();
				break;
			case EnemyState.Attack:
			//After attacking, the agent should switch back to Idle state, so it can check for the player again.
				Attack();
				if (agentStatus)
				{
					_currentState = EnemyState.Chase;
				}
				break;
			default:
			//Here just in case
				break;
		}
	}
	
	void LookForTargets(){
	//Check if the player is within the aggro range of the enemy
	if (Vector3.Distance (this.transform.position, player.transform.position) < aggroRange){
			_currentState = EnemyState.Chase;
		}
	
	}
	
	void Patrol(){
		//agent will be given a new random wander position every wanderPositionDelay seconds
		enemy.SetDestination(wanderPosition);
	}
	
	void randomWanderPosition(){
		//set wanderPosition to a random position within a certain range of the agent.
		//Randomize a num from (-1,1) and multiply that by maxWanderDistance and add to x and y of current position.
		randX = (Random.Range(-1,1)*maxWanderDistance) + this.transform.position.x;
		randZ = (Random.Range(-1,1)*maxWanderDistance) + this.transform.position.z;
		wanderPosition.x = randX;
		wanderPosition.y = this.transform.position.y;
		wanderPosition.z = randZ;
		
	}
	
	void ChasePlayer(){
	//Set enemy location to the player. Nav agent will navigate the enemy toward the player
	enemy.SetDestination(player.transform.position);
	//Check if the player is within attacking range
	if (Vector3.Distance (this.transform.position, player.transform.position) < attackRange){
			_currentState = EnemyState.Attack;
		}
	//Check if the player is out of chasing range
	if (Vector3.Distance (this.transform.position, player.transform.position) > aggroRange){
			_currentState = EnemyState.Idle;
		}
	
	}
	
	void Attack(){
	//Jump at the player 
	//Small delay before jumping

	//Check time delay
	if (Time.time >lastJump - jumpDelay)
		{
			jumpTarget = transform.forward;
			jumpTarget.y = jumpTarget.y + jumpScalar;
            //Disable nav mesh agent real quick
            agent.SetDestination(transform.position);
            // disable the agent
            agent.updatePosition = false;
            agent.updateRotation = false;
            agent.isStopped = true;
			agentStatus = false;
            rb.isKinematic = false;
            //rb.useGravity = true;
            rb.AddRelativeForce(jumpTarget * jumpStrength, ForceMode.Impulse);
			isGrounded = false;

        }
	}

	private void OnCollisionEnter(Collision collision)
	{
        if (collision.collider != null && collision.collider.tag == "Ground")
		{
			if (agentStatus == false)
			{
				agent.updatePosition = true;
				agent.updateRotation = true;
				agent.isStopped = false;
				agentStatus = true;
			}
            isGrounded = true;
        }


    }

}



