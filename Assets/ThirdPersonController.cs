using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public Animator anim;

    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private void Update()
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(Horizontal, 0f, Vertical).normalized;

        if (dir.magnitude >= 0.1f)
        {
            anim.SetBool("Walking", true);
            float targetAngle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            controller.Move(dir * speed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("Walking", false);
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && anim.GetBool("Walking") == false)
        {
            anim.SetTrigger("Attack");
        }
    }
}
