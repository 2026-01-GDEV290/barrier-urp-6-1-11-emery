using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public Animator anim;

    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public GameObject trilRenderer;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(Horizontal, 0f, Vertical).normalized;
        AnimatorStateInfo animInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (animInfo.IsName("Idle"))
        {
            trilRenderer.SetActive(false);
        }
        else if(animInfo.IsName("ATK"))
        {
            trilRenderer.SetActive(true);
        }

        if (dir.magnitude >= 0.1f)
        {
            anim.SetBool("Walking", true);
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);


            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
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
