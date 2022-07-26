using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField]private float speed;
    [SerializeField]private float jumpDistance; 
    private Rigidbody2D body; //Used for rigid body and physics over the player's body
    private Animator animate; //Variable for the animation 
    private bool grounded; //true if the player is on the ground and false otherwise


    //awake method is activated once at the beginning of the game
    private void Awake(){
        //Grap references for rigidbody and animator from objects
        body = GetComponent<Rigidbody2D>();
        animate = GetComponent<Animator>();
    


    }
    //update method is for things that must be checked every frame 
    private void Update(){
        float horizantalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizantalInput * speed,body.velocity.y);

        //flip Player when moving left and right
        if(horizantalInput> 0.01f)
            transform.localScale = Vector3.one;
        else if(horizantalInput< -0.01f)
            transform.localScale = new Vector3(-1,1,1);


        //jumping command
        if(Input.GetKey(KeyCode.Space) && grounded)
           Jump();



        //set animator parameters
        animate.SetBool("run", horizantalInput != 0);
        animate.SetBool("grounded", grounded);
        


    }
        //Jumping function
    private void Jump(){
         body.velocity = new Vector2(body.velocity.x, jumpDistance);
         animate.SetTrigger("Jump");
         grounded = false;
    }
        /*This is a function to detect if the player is on the ground by checking
         if there is collision between the player and gameObject tagged with Ground*/
    

    private void OnCollisionEnter2D(Collision2D collision){
        
        if(collision.gameObject.tag == "Ground")
            grounded = true;
    }
}
