using Assets.Scripts.AI.FSM;
using Assets.Scripts.Game.Character;
using UnityEngine;

namespace Assets.Labs.FSMTest.Scripts
{
    public class Farmer : SimpleCharacter
    {
        public float MoveSpeed;
        public int BackPack;
        public int MaxBackPack;
        public GameObject Target;
        public float MaxTargetRange;

        private Fsm _stateMachine;
        private Fsm.State _idleState;
        private Fsm.State _harvestState;
        private Fsm.State _moveState;
        private Fsm.State _unloadState;

        private void Start()
        {
            _stateMachine = new Fsm();
            CreateIdleState();
            CreateMoveState();
            CreateHarvestState();
            CreateUnLoadState();

            _stateMachine.PushState(_idleState);
        }

        private void Update()
        {
            _stateMachine.Run(gameObject);
        }

        private void CreateUnLoadState()
        {
            _unloadState = (fsm, fsmGameObject) =>
            {
                // target bonfire is null...
                if (Target == null)
                {
                    Bonfire bonfire = FindObjectOfType<Bonfire>();
                    if (bonfire == null)
                    {
                        fsm.PopState();
                        fsm.PushState(_idleState);
                        return;
                    }

                    Target = bonfire.gameObject;
                }

                // target too far...
                if ((Target.transform.position - transform.position).magnitude > MaxTargetRange)
                {
                    fsm.PushState(_moveState);
                }
                else
                {
                    BackPack = 0;

                    fsm.PopState();
                    fsm.PushState(_idleState);
                }
            };
        }

        private void CreateHarvestState()
        {
            _harvestState = (fsm, fsmGameObject) =>
            {
                // target herb is null...
                if (Target == null)
                {
                    Herb herb = FindObjectOfType<Herb>();
                    if (herb == null)
                    {
                        fsm.PopState();
                        fsm.PushState(_idleState);
                        return;
                    }

                    Target = herb.gameObject;
                }

                // target too far...
                if ((Target.transform.position - transform.position).magnitude > MaxTargetRange)
                {
                    fsm.PushState(_moveState);
                }
                else
                {
                    Destroy(Target);
                    BackPack++;

                    fsm.PopState();
                    fsm.PushState(_idleState);
                }
            };
        }

        private void CreateIdleState()
        {
            _idleState = (fsm, fsmGameObject) =>
            {
                Target = null;
                fsm.PopState();
                fsm.PushState(BackPack < MaxBackPack ? _harvestState : _unloadState);
            };
        }

        private void CreateMoveState()
        {
            _moveState = (fsm, fsmGameObject) =>
            {
                if (MoveTo(Target.transform.position, MoveSpeed))
                {
                    fsm.PopState();
                }
            };
        }



    }
}