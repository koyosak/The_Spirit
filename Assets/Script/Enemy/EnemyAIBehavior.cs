using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIBehavior : MonoBehaviour
{
    [SerializeField] float _speed = 3.0f;
    [SerializeField] float _obstacleRange = 5.0f;

    private readonly float _sphereRadius = 0.75f;

    [SerializeField] GameObject _fireballPrefab;
    public GameObject _fireball;

    public void Wander()
    {
        transform.Translate(0, 0, _speed * Time.deltaTime);

        Ray ray = new(transform.position, transform.forward);

        // Cast a sphere in the direction of the ray and see if it collides
        // with the player or with the walls.
        if (Physics.SphereCast(ray, _sphereRadius, out RaycastHit hit))
        {
            GameObject hitObj = hit.transform.gameObject;

            // Shoot a fireball if the ray collided with the player.
            if (hitObj.GetComponent<CharacterController>())
            {
                if (_fireball == null)
                {
                    _fireball = Instantiate(
                        _fireballPrefab,
                        // TransformPoint from local space to global space.
                        // The fireball will be placed in front of the
                        // entity and facing in the same direction.
                        transform.TransformPoint(Vector3.forward * 1.5f),
                        transform.rotation);
                }
            }

            // Otherwise, the ray collided with a wall.
            else if (hit.distance < _obstacleRange)
            {
                float theta = Random.Range(-110, 110);
                transform.Rotate(0, theta, 0);
            }
        }
    }
}
