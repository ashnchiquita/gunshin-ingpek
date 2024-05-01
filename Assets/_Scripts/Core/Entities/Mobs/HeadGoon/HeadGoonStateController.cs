using System;
using UnityEngine;

// TODO: Refactor a bit as EnemyStateController
// Lotsa codes are reusable
[Serializable]
public class HeadGoonStateController : EntityStateController
{
    // Attributes
    private HeadGoon headGoon;
    [HideInInspector] public WeaponState weaponState = WeaponState.IDLE;
    public float detectionDistance = 15f;
    public float attackDistance = 5f;

    // Constructor
    public void Init(HeadGoon headGoon)
    {
        this.headGoon = headGoon;
        headGoon.OnDeathEvent += OnDeath;
    }

    // Functions
    protected override int DetectState()
    {
        // Get movementState
        int movementState = 0; 
        if(DetectJumping())
        {
            movementState = HeadGoonState.JUMPING;
        }
        else if(DetectFalling())
        {
            movementState = HeadGoonState.FALLING;
        }
        else if(DetectSprinting())
        {
            movementState = HeadGoonState.SPRINTING;
        }
        else
        {
            movementState = HeadGoonState.IDLE;
        }

        // Get aiState
        int aiState = 0;
        if(Vector3.Distance(headGoon.Position, GameController.Instance.player.Position) < attackDistance)
        {
            aiState = HeadGoonState.AI_IN_RANGE_STATE;
        }
        else if(Vector3.Distance(headGoon.Position, GameController.Instance.player.Position) < detectionDistance)
        {
            aiState = HeadGoonState.AI_DETECTED_STATE;
        }

        // Get attackState
        int attackState = 0;
        if(DetectAttacking())
        {
            AttackType attackType = weaponState switch
            {
                WeaponState.ATTACK => headGoon.Weapon.attackType,
                WeaponState.ALTERNATE_ATTACK => headGoon.Weapon.alternateAttackType,
                _ => AttackType.NULL
            };

            attackState = attackType switch
            {
                AttackType.RANGED => HeadGoonState.ATTACK_RANGED,
                AttackType.MELEE => HeadGoonState.ATTACK_MELEE,
                _ => HeadGoonState.NULL
            };
        }

        // Combine states
        state = movementState | aiState | attackState;

        return state;
    }

    public void ClearWeaponState()
    {
        weaponState = WeaponState.IDLE;
    }
    public void SetWeaponState(WeaponState state)
    {
        weaponState = state;
    }
    private bool DetectSprinting()
    {
        return headGoon.aiController.nav.velocity.magnitude > 0.5;
    }
    private bool DetectJumping()
    {
        return !headGoon.Grounded && headGoon.Rigidbody.velocity.y > 0;
    }
    private bool DetectFalling()
    {
        return !headGoon.Grounded && headGoon.Rigidbody.velocity.y < 0;
    }
    private bool DetectAttacking()
    {
        return !headGoon.Weapon.CanAttack;
    }
    private void OnDeath()
    {
        state = HeadGoonState.DEAD;
    }


    // Debugging purposes
    public void VisualizeDetection(MonoBehaviour monoBehaviour)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(monoBehaviour.transform.position, detectionDistance);
        Gizmos.DrawWireSphere(monoBehaviour.transform.position, attackDistance);
    }
}