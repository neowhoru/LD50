using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Platformer
{
    public class PlayerColliderPlatformer : MonoBehaviour
    {
        public GameObject deathParticle;
        private Movement _movement;

        public static event Action RestartLevelNotification = delegate {  };
        public static event Action<int> AddHealth = delegate(int healthAmount) {  };
        public static event Action<string> PlayClip = delegate(string clipType) {  };
        public static event Action LevelFinished = delegate {  };
        
        private void Start()
        {
            _movement = GetComponent<Movement>();
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Collide with "+ other.tag);
            switch (other.tag)
            {
                case "Collectable":
                    HandleCollectable(other.gameObject);                
                    break;
            
                case "Death":
                case "Enemy":
                    HandleDeath();
                    break;
                case "Hurtbox":
                    HandleEnemyHit(other);
                    break;
                case "Goal":
                    _movement.canMove = false;
                    LevelFinished();
                    break;
            }
        }

        private void HandleEnemyHit(Collider2D other)
        {
            _movement.BounceJump();
            //other.gameObject.transform.parent.GetComponent<EnemyBase>().KillEnemy();
        }

        private void HandleDeath()
        {
            if (deathParticle != null)
            {
                
                GameObject explode = Instantiate(deathParticle, transform.position, Quaternion.identity);
                PlayClip("DIE");
                RestartLevelNotification();
                Destroy(gameObject);
            
            }
        }

        public void HandleCollectable(GameObject collectable)
        {
            
            switch (collectable.GetComponent<Collectable>().collectType)
            {
                case Collectable.COLLECT_TYPE.HEART:
                    AddHealth(4);
                    PlayClip("COLLECT_HEART");
                    break;
                
            }
            
            // Maybe nice fancy effect
            Destroy(collectable);
        
        }
    }
}
