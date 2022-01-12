using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehaviorAI 
{
 Vector3 SetTargetPosition (Vector3 targetPosition);
 Transform GetAgentTransform();
 Vector3 GetTargetPosition();
 GameObject SetTarget(GameObject gameObject);
 GameObject GetTarget();
 Transform GetTransform();
}
