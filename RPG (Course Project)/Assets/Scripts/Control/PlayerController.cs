using System;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {

        void Update()
        {
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
            print("interact with combat and movement unsuccessful");
        }

        private bool InteractWithCombat()
        {
            // When spawn a ray, it stores information of all hit points
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            // Checking if the ray has collided with enemy
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                // if not collided with enemy continue
                if (target == null) continue;

                // if get left mouse button down, attack target
                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target);
                }
                return true;
            }
            // if enemy not found return false
            return false;
        }

        private bool InteractWithMovement()
        {
            // Generates a ray from camera towards the mouse position when left button clicked
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        // return a ray from camera in the direction of mouse position
        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
