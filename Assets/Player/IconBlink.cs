using UnityEngine;

public class IconBlink : MonoBehaviour
{
    private Animator animator;
    private float duration = 0;
    [SerializeField] private float cooldown = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Random.Range(0, 60) == 59 && duration >= 0.2f + cooldown)
        {
            animator.Play("iconBlink");
            duration = 0;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("iconBlink") && duration >= animator.GetCurrentAnimatorStateInfo(0).length)
        {
            animator.Play("iconIdle");
        }
        duration += Time.deltaTime;


    }
}
