using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField]private float speed;
    [SerializeField]private float jumpDistance;
    private Rigidbody2D body;
    private Animator animate;



    private void Awake(){
        //Grap references for rigidbody and animator from objects
        body = GetComponent<Rigidbody2D>();
        animate = GetComponent<Animator>();
    


    }

    private void Update(){
        float horizantalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizantalInput * speed,body.velocity.y);

        //flip Player when moving left and right
        if(horizantalInput> 0.01f)
            transform.localScale = Vector3.one;
        else if(horizantalInput< -0.01f)
            transform.localScale = new Vector3(-1,1,1);
        //jumping command
        if(Input.GetKey(KeyCode.Space))
            body.velocity = new Vector2(body.velocity.x, jumpDistance);

        //set animator parameters
        animate.SetBool("run", horizantalInput != 0);


    }

    
}
