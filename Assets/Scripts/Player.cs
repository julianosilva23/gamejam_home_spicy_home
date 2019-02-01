using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;

    public int bombCooldown;

    public int blastCooldown;

    public int resource;

    public int home;

    public string typeInput;

    float direction;

    float bombSpawn;

    bool isMoving;

    bool alive;

    bool hasBomb;

    string charName;

    public GameObject bomb;

    public GameObject builtHome;

    // private RuntimeAnimatorController rac;

    public Animator anim;

    // public AnimationClip[] animationsStop;

    // public AnimationClip[] animationsRun;

    // public Animator animator;
    
    // public AnimatorOverrideController animatorOverrideController;

    // public AnimatorOverrideController teste;

    // public RuntimeAnimatorController teste1;

    // Start is called before the first frame update
    void Start()
    {
        setResource(0);

        setHome(0);

        alive = true;

        hasBomb = true;        
        
    }

    void OnTriggerEnter2D(Collider2D col){
        
        if (col.tag == "Resource"){
            
            setResource(resource + 1);

            //col.gameObject.GetComponent<AudioSource>().Play ();

            Destroy(col.gameObject);
        }

        if (col.tag == "Boom"){
            alive = false;
            StartCoroutine(BlastCooldown(blastCooldown));
        }
    }

    IEnumerator BlastCooldown(int timer){
        int i;
        while (timer > 0){
            i=2;
            while (i > 0){

                gameObject.GetComponent<SpriteRenderer>().enabled = false;

                yield return new WaitForSeconds(0.25f);

                gameObject.GetComponent<SpriteRenderer>().enabled = true;

                yield return new WaitForSeconds(0.25f);

                i--;
            }
            timer --;
        }
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        
        alive = true;
    }

    IEnumerator BombCooldown(int timer){
        while (timer > 0){
            yield return new WaitForSeconds(1);
            timer --;
        }
        hasBomb = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive){
            PlayerMove();
            if (hasBomb){

            if (Input.GetButtonDown("Fire1" + typeInput)){

                bombSpawn = direction / 3;

                hasBomb = false;

                Instantiate(
                    bomb,
                    new Vector2((float)transform.position.x + bombSpawn, transform.position.y),
                    transform.rotation
                );

                StartCoroutine(BombCooldown(bombCooldown));
                }
            }
            if (getResource() >= 5){

                if (Input.GetButtonDown("Fire2" + typeInput)){
                    StartCoroutine(BuildHome(bombCooldown + 3));
            }
        }

        } else {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    IEnumerator BuildHome(int buildTime){

        alive = false;
        GameObject building = Instantiate(
            builtHome,
            new Vector2(transform.position.x + direction, transform.position.y),
            transform.rotation);

        Home house = building.GetComponent<Home>();

        house.owner = gameObject.GetComponent<Player>();

        setResource(getResource() - 3);

        buildTime = buildTime * 2;

        while (buildTime > 0){

            yield return new WaitForSeconds(0.5f);

            if (buildTime % 2 != 0){

                building.GetComponent<SpriteRenderer>().sprite = house.halfDone;

                if (buildTime >= 5 ){

                     building.GetComponent<SpriteRenderer>().enabled = true;  
                }
            } else{

                if (buildTime < 5 ){

                    building.GetComponent<SpriteRenderer>().sprite = house.done;

                } else {

                    building.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
            buildTime --;
        }
        building.GetComponent<SpriteRenderer>().sprite = house.done;
        alive = true;
    }

    void PlayerMove(){

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(

                moveSpeed * Time.fixedDeltaTime * Input.GetAxisRaw("Horizontal" + typeInput),

                moveSpeed * Time.fixedDeltaTime * Input.GetAxisRaw("Vertical" + typeInput));
            
            if (Input.GetAxisRaw("Horizontal" + typeInput) > 0){
                
                direction = 1;

                setIsMoveing(true);

                // animator.SetBool("isMoving", isMoving);
            }
            if (Input.GetAxisRaw("Horizontal" + typeInput) < 0){
                
                direction = -1;

                setIsMoveing(false);

            }
            if (Input.GetAxisRaw("Horizontal" + typeInput) == 0){

                setIsMoveing(false);
            }
            SpriteWork();
    }

    public bool setIsMoveing(bool mov){
        isMoving = mov;

        //animator.SetBool("isMoving", isMoving);

        return isMoving;

    }

    void SpriteWork(){
        if (direction == 1){
            if (gameObject.GetComponent<Transform>().localScale.x < 0){
                gameObject.GetComponent<Transform>().localScale *= new Vector2(-1,1);
            }
        }
        if (direction == -1){
            if (gameObject.GetComponent<Transform>().localScale.x > 0){
                gameObject.GetComponent<Transform>().localScale*= new Vector2(-1,1);
            }
        }
    }

    public int getResource(){
        return resource;
    }

    public int setResource(int value){
        resource = value;

        return resource;
    }

    public int getHome(){

        return home;
    }

    public int setHome(int value){

        home = value;

        return home;
    }

    public void LessHome(){
        home --;
    }

    public string setTypeInput(string type){

        typeInput = type;

        return typeInput;
    }

    public string getTypeInput(){

        return typeInput;
    }

    public string setCharName(string name){

        charName = name;

        return charName;
    }

    public string getCharName(){

        return charName;
    }

    public void setAnimator(RuntimeAnimatorController rac){

        anim = GetComponent<Animator>();

        anim.runtimeAnimatorController = rac as RuntimeAnimatorController;
     
        // animator.runtimeAnimatorController = rac as RuntimeAnimatorController;

        // Debug.Log(animator.runtimeAnimatorController);

        // animator = GetComponent<Animator>();

        // animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);

        // animator.runtimeAnimatorController = animatorOverrideController;

        // animatorOverrideController = teste1 as AnimatorOverrideController;

        // Debug.Log(animatorOverrideController.name);

        // Animator animator = GetComponent<Animator>();

        // animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);

        // animator.runtimeAnimatorController = animatorOverrideController;

        // animatorOverrideController["LandGuy_Main"] = animRun;

        // animatorOverrideController["LandGuy_Stop"] = animStop;

    }
}