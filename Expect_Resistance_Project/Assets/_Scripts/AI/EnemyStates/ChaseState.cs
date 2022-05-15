using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace noGame.EnemyBehaviour
{
    internal class ChaseState : EnemyState
    {
        Vector3 targetDirection;
        const float significantYDifference = 0.05f;
        public ChaseState(SimpleEnemy ctx) : base(ctx)
        {
        }

        internal override void Start()
        {
            
        }

        internal override void Update()
        {
            if (ctx.TargetObject == null) targetDirection = Vector3.zero;
            else targetDirection = ctx.TargetObject.transform.position - ctx.gameObject.transform.position;

            ctx.HorizontalInput = Mathf.Clamp(targetDirection.x*ctx.HorizontalInputBoost, -1, 1);
            if(Mathf.Abs(targetDirection.x) < ctx.KillRadius)
            {
                ctx.HorizontalInput = 0;
            }

            ctx.KillCheck();

            HandlePhaseDown();
            HandleJump();
        }

        private void HandlePhaseDown()
        { 
            if(targetDirection.y < -significantYDifference) ctx.PhaseDownInput();
        }

        private void HandleJump()
        {
            ctx.JumpInput(targetDirection.y > significantYDifference && (ctx.IsGrounded() || !ctx.IsFalling()));

        }
    }
}
