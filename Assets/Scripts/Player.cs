using UnityEngine;
// using UnityEngine.InputSystem;

namespace MazeProject
{    public class Player : MonoBehaviour
    {
        public float MoveSpeed = 1f;
        public Animator Animator;
        public MainUI MainUI;

        private Rigidbody rigidbody;
        public bool isGoal;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            // isGoal = false;
            if (rigidbody == null)
            {
                Debug.LogError("Player: Rigidbody がこのオブジェクトに付いていません！");
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (MainUI.isFinished)
            {
                // クリア or ゲームオーバー後は表示ロック
                return;
            }
            UpdateInput();
        }

        private void UpdateInput()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            // Debug.Log("Horizontal:" + horizontal);
            // Debug.Log("Vertical:" + vertical);


            var movement = new Vector3(horizontal, 0, vertical) * MoveSpeed;

            if (rigidbody != null)
            {
                rigidbody.linearVelocity = movement;
            }


            if (movement.magnitude > 0)
            {
                transform.localPosition += movement;
                transform.forward = movement;
                Animator.SetBool("IsMove", true);
            }
            else
            {
                Animator.SetBool("IsMove", false);
            }
            // }
            
        }
        private void OnLeft()
        {
            Debug.Log("OnLeft");
        }
        private void OnRight()
        {
            Debug.Log("OnRight:");
        }
        private void OnUp()
        {
            Debug.Log("OnUp:");
        }
        private void OnDown()
        {
            Debug.Log("OnDown:");
        }
        // private void OnHorizontal(InputAction.CallbackContext context)
        // {
        //     Debug.Log("OnHorizontal:" + context.ReadValue<Int>());
        // }
        private void OnTriggerStay(Collider other)
        {
            var goal = other.GetComponent<Goal>();
            if (goal != null)
            {
                isGoal = true;
            }
        }
    }
} 