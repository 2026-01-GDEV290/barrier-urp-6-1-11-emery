using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public Animator anim;

    public CharacterController controller;
    public Transform cam;

    public AudioSource[] sources;

    public float speed = 6f;

    bool canMove = true;
    bool finalHit = false;

    bool windUp = false;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public GameObject SwordOBJ;
    public TrailRenderer TrailRenderer;
    public GameObject HurtBox;
    bool attacking = false;
    public string lastState;

    float yPos;

    private void Start()
    {
        yPos = transform.position.y;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        controller.transform.position = new Vector3(controller.transform.position.x, yPos, controller.transform.position.z);

        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(Horizontal, 0, Vertical).normalized;
        AnimatorStateInfo animInfo = anim.GetCurrentAnimatorStateInfo(0);



        if (dir.magnitude >= 0.1f && canMove)
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
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!finalHit && !windUp)
            {
                anim.SetTrigger("Attack");
            }
            if (!attacking)
            {
                foreach (AudioSource source in sources)
                {
                    source.pitch = Random.Range(1f, 1.5f);
                }
            }
        }



        if (animInfo.IsName("Idle") || animInfo.IsName("WindUp"))
        {
            SwordOBJ.SetActive(animInfo.IsName("WindUp"));
            HurtBox.SetActive(false);
            TrailRenderer.enabled = false;
            canMove = animInfo.IsName("Idle");
            windUp = animInfo.IsName("WindUp");
            attacking = false;
            finalHit = false;
        }
        else if (animInfo.IsName("ATK") || animInfo.IsName("FinalHit"))
        {
            SwordOBJ.SetActive(true);
            HurtBox.SetActive(true);
            TrailRenderer.enabled = true;
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            canMove = false;
            attacking = true;
            finalHit = animInfo.IsName("FinalHit");
        }
    }
}
