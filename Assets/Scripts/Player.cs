using UnityEngine;
// using UnityEngine.InputSystem;

namespace MazeProject
{    public class Player : MonoBehaviour
    {
        public float MoveSpeed = 1f;

        private Rigidbody rigidbody;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateInput();
        }

        private void UpdateInput()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            // Debug.Log("Horizontal:" + horizontal);
            // Debug.Log("Vertical:" + vertical);

            var movement = new Vector3(horizontal, 0, vertical) * MoveSpeed;
            //transform.position += movement * Time.deltaTime;
            rigidbody.linearVelocity = movement;
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
        private void OnTriggerEnter(Collider other){
            var goal = other.GetComponent<Goal>();
            if(goal != null){
                Debug.Log("ゴールに到達!");
            }
        }
    }
} 