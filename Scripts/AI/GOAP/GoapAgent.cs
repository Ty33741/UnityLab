using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AI.FSM;
using UnityEngine;

namespace Assets.Scripts.AI.GOAP
{
    public class GoapAgent : MonoBehaviour
    {
        private Fsm _stateMachine;
        private Fsm.State _idleState;
        private Fsm.State _moveState;
        private Fsm.State _performState;
        private GoapAction[] _actions;
        private Stack<GoapAction> _plan;
        private GoapPlanner _planner;
        private IGoap _dataProvider;

        private void Start()
        {
            _planner = new GoapPlanner();

            _stateMachine = new Fsm();
            CreateIdleState();
            CreateMoveState();
            CreatePerformState();

            FindDataProvider();
            LoadActions();

            _stateMachine.PushState(_idleState);
        }

        private void CreateIdleState()
        {
            _idleState = (fsm, fsmGameObject) =>
            {
                HashSet<KeyValuePair<string, object>> worldStates = _dataProvider.GetWorldStates();
                HashSet<KeyValuePair<string, object>> goals = _dataProvider.GetGoals();

                _plan = _planner.Plan(gameObject, _actions, worldStates, goals);

                if (_plan == null)
                {
                    _dataProvider.PlanFailed(goals);
                }
                else
                {
                    _dataProvider.PlanFound(goals, _plan);
                    fsm.PopState();
                    fsm.PushState(_performState);
                }
            };
        }

        private void CreateMoveState()
        {

        }

        private void CreatePerformState()
        {
            _performState = (fsm, fsmGameObject) =>
            {
                Debug.Assert(_plan.Peek() != null);

                if (_plan.Peek().IsDone)
                {
                    _plan.Pop();
                    if (_plan.Peek() == null)
                    {
                        _dataProvider.PlanFinished();
                        fsm.PopState();
                        fsm.PushState(_idleState);
                        return;
                    }
                }

                GoapAction action = _plan.Peek();

                if (!action.Perform(fsmGameObject))
                {
                    _dataProvider.PlanAborted(action);
                    fsm.PopState();
                    fsm.PushState(_idleState);
                }

            };
        }

        private void FindDataProvider()
        {
            _dataProvider = GetComponent<IGoap>();
            //foreach (Component comp in GetComponents<Component>())
            //{
            //    if (comp is IGoap)
            //    {
            //        _dataProvider = comp as IGoap;
            //        return;
            //    }
            //}
        }

        private void LoadActions()
        {
            _actions = GetComponents<GoapAction>();
        }
    }
}