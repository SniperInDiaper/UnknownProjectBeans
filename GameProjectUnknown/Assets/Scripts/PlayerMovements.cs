using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField]private float speed;
    [SerializeField]private float jumpDistance; 
    [SerializeField]private LayerMask groundLayer; //LayerMask are used to separate collision entetities 
    [SerializeField]private LayerMask wallLayer; //LayerMask are used to separate collision entetities
    private Rigidbody2D body; //Used for rigid body and physics over the player's body
    private Animator animate; //Variable for the animation 
    private BoxCollider2D boxCollider; //Initiate for box collider
    private float wallJumpCooldown; //responcible for creating delay between wall jumps
    private float horizantalInput;


    //awake method is activated once at the beginning of the game
    private void Awake(){
        //Grap references for rigidbody and animator from objects
        body = GetComponent<Rigidbody2D>();
        animate = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    


    }
    //update method is for things that must be checked every frame 
    private void Update()
    {
         horizantalInput = Input.GetAxis("Horizontal");

        //flip Player when moving left and right
        if(horizantalInput> 0.01f)
            transform.localScale = Vector3.one;
        else if(horizantalInput< -0.01f)
            transform.localScale = new Vector3(-1,1,1);


       

        //wall jumps logic
        if(wallJumpCooldown > 0.2f)
        {
             body.velocity = new Vector2(horizantalInput * jumpDistance,body.velocity.y);

             if(onWall() && !isGrounded())
             {
                body.gravityScale = 0.75f;
                body.velocity = Vector2.down;
             }else 
                    body.gravityScale = 1.75f;
            
                //jumping command
             if(Input.GetKey(KeyCode.Space) )
                Jump();

        }else 
                wallJumpCooldown += Time.deltaTime;



        //set animator parameters
        animate.SetBool("run", horizantalInput != 0);
        animate.SetBool("grounded", isGrounded());
        


    }
        //Jumping function
    private void Jump()
    {   
        if(isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpDistance);
             animate.SetTrigger("Jump");
        }
        else if(onWall() && !isGrounded())
        {
            if(horizantalInput == 0)
            {
                 body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 0);
                 transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x) , transform.localScale.y, transform.localScale.z);//This is responsible for flipping the direction the player is facing.
            }
            else
                 body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 8, 8); //This is responsible for the jumping of the wall mechanism. 

            wallJumpCooldown = 0;
        }
    }
        /*This is a function to detect if the player is on the ground by checking
         if there is collision between the player and gameObject tagged with Ground*/
    

    private void OnCollisionEnter2D(Collision2D collision){
        
    }

    private bool isGrounded()
    {
        /* RaycastHit2D is used to cast a light of the object to determine something.
        BoxCasting is used to check the collosion box instead of casting a ray.
         the parameters are (Origin of the box, Size of the box, Angle of the box, Direction, distance of the vertiual box from the object, Layer mask)*/
        RaycastHit2D ray = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,Vector2.down, 0.1f, groundLayer);
        return ray.collider != null;
    }
    private bool onWall()
    {
        /* RaycastHit2D is used to cast a light of the object to determine something.
        BoxCasting is used to check the collosion box instead of casting a ray.
         the parameters are (Origin of the box, Size of the box, Angle of the box, Direction, distance of the vertiual box from the object, Layer mask)*/
        RaycastHit2D ray = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        return ray.collider != null;
    }






}
