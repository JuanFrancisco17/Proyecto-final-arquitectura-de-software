using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyState
{
    public enum STATE
    {
        PATROL, PURSUE, ATTACK
    };

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    };

    public STATE name;
    protected EVENT stage;

    protected GameObject target;
    protected NavMeshAgent agent;
    protected int routine;
    protected float counter;
    protected Quaternion angle;
    protected float grade;
    protected EnemyState nextState;
    protected bool isAttacking;

    public EnemyState(GameObject _target, NavMeshAgent _agent, int _routine, float _counter, Quaternion _angle, float _grade)
    {
        target = _target;
        agent = _agent;
        stage = EVENT.ENTER;
        routine = _routine;
        counter = _counter;
        angle = _angle;
        grade = _grade;
    }

    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

    public EnemyState Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }

    public class Patrol : EnemyState
    {

        public Patrol(GameObject _target, NavMeshAgent _agent, int _routine, float _counter, Quaternion _angle, float _grade) : base(_target, _agent, _routine, _counter, _angle, _grade)
        {
            name = STATE.PATROL;
        }

        public override void Enter()
        {
            
            base.Enter();
        }

        public override void Update()
        {
            //Si la distancia con el jugador es mayor a 13
            if (Vector3.Distance(agent.transform.position, target.transform.position) > 13)
            {
                //El contado empieza a contar y cuando llegue a 2 segundos sacamos un numero aleatorio (0,1)
                counter += 1 * Time.deltaTime;
                if (counter >= 2)
                {
                    routine = Random.Range(0, 3);
                    counter = 0;
                }

                switch (routine)
                {
                    case 0:
                        //Si sale 0 el enemigo se queda quieto

                        break;

                    case 1:
                        //Si sale uno el enemigo se mueve a una posicion aleatoria
                        grade = Random.Range(0, 360);
                        angle = Quaternion.Euler(0, grade, 0);
                        routine++;
                        break;

                    case 2:
                        //Si sale uno el enemigo se mueve a una posicion aleatoria
                        grade = Random.Range(0, 360);
                        angle = Quaternion.Euler(0, grade, 0);
                        routine++;
                        break;

                    case 3:
                        agent.transform.rotation = Quaternion.RotateTowards(agent.transform.rotation, angle, 0.5f);
                        agent.transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                        //Refrescamos el destino del agente para que vuelva a caminar si ha salido de perseguir al jugador
                        agent.SetDestination(agent.transform.position);
                        break;
                }
            }
            else if(Vector3.Distance(agent.transform.position, target.transform.position) < 8 && Vector3.Distance(agent.transform.position, target.transform.position) > 1)
            {
                nextState = new Pursue(target, agent, routine, counter, angle, grade);
                stage = EVENT.EXIT;
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }

    public class Pursue : EnemyState
    {

        public Pursue(GameObject _target, NavMeshAgent _agent, int _routine, float _counter, Quaternion _angle, float _grade) : base(_target, _agent, _routine, _counter, _angle, _grade)
        {
            name = STATE.PURSUE;
        }

        public override void Enter()
        {

            base.Enter();
        }

        public override void Update()
        {
            agent.destination = target.transform.position;

            if ((Vector3.Distance(agent.transform.position, target.transform.position) > 13))
            {
                nextState = new Patrol(target, agent, routine, counter, angle, grade);
                stage = EVENT.EXIT;
            }
            if ((Vector3.Distance(agent.transform.position, target.transform.position) <= 1.4))
            {
                nextState = new Attack(target, agent, routine, counter, angle, grade);
                stage = EVENT.EXIT;
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }

    public class Attack : EnemyState
    {

        public Attack(GameObject _target, NavMeshAgent _agent, int _routine, float _counter, Quaternion _angle, float _grade) : base(_target, _agent, _routine, _counter, _angle, _grade)
        {
            name = STATE.ATTACK;
        }

        public override void Enter()
        {

            base.Enter();
        }

        public override void Update()
        {
            Debug.Log("Entrando en estado de atacar");
            PlayerController.Instance.Dead();

            if ((Vector3.Distance(agent.transform.position, target.transform.position) > 1.4))
            {
                nextState = new Pursue(target, agent, routine, counter, angle, grade);
                stage = EVENT.EXIT;
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }

}
