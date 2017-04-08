﻿using UnityEngine;

/// <summary>
/// When an agent has this movement behaviour it will actively desire to move
/// directly away from the location of the target.
/// </summary>
public class AvoidMovementBehaviour : DirectMovementBehaviour
{
    /// <summary>
    /// Constructor for AvoidMovementBehaviour instances.
    /// </summary>
    /// <param name="movementBehaviour">The movement behaviour to decorate.</param>
    /// <param name="agent">The GameObject that desires this movement behaviour.</param>
    /// <param name="target">The target of this movement behaviour.</param>
    /// <param name="radius">The radius at which this behaviour is completed.</param>
    public AvoidMovementBehaviour(AbstractMovementBehaviour movementBehaviour, GameObject agent, GameObject target, float radius)
         : base(movementBehaviour, agent, target, radius) { }

    /// <summary>
    /// The velocity desired by this movement behaviour.
    /// </summary>
    /// <returns>The optimal velocity vector to accomplish this movement behaviour.</returns>
    public override Vector2 CalculateDesiredVelocity()
    {
        var desiredVelocity = CalculateMaximumVelocity(target.transform.position, agent.transform.position);

        var distance = Vector2.Distance(agent.transform.position, target.transform.position);
        var radiusFactor = Mathf.Clamp((1.0f - (distance / radius))*2, 0.0f, 2.0f);
        desiredVelocity *= radiusFactor;

        Debug.DrawRay(agent.transform.position, desiredVelocity * 10, Color.red);
        return desiredVelocity + movementBehaviour.CalculateDesiredVelocity();
    }
}
