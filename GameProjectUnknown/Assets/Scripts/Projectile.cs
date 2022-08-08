using UnityEngine;

public class Projectile : MonoBehaviour
{
   [SerializeField]private float speed;
    private float direction;
    private bool hit;
    private float movementSpeed;

    private BoxCollider2D boxCollider;
    private Animator animate;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animate = GetComponent<Animator>();
    }

    private void Update()
    {
        if(hit) return;
        movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0,0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        animate.SetTrigger("Explode");
    }

    public void SetDirection(float _direction)
    {
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }


    private void Deactivate()
    {
        gameObject.SetActive(false);
    }


}
