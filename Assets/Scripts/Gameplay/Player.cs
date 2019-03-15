using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AnimationModule;

public class Player : MonoBehaviour
{
    public float moveSpeed;

    public int bombCooldown;

    public int blastCooldown;

    public int resource;

    public int home;

    public string typeInput;

    public float direction;

    float bombSpawn;

    bool isMoving;

    public bool alive;

    bool hasBomb;

    bool ghostBuild;

    string charName;

    GameObject ghostHome;

    public GameObject bomb;

    public GameObject resourceDrop;

    public GameObject builtHome;
    public GameObject ghostHomePrefab;

    public Animator anim;

    void Start()
    {
        setResource(0);
        setHome(0);
        alive = true;
        hasBomb = true;    
        ghostBuild = false;
        ghostHome = null;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Resource" && !alive)
        {
            Physics2D.IgnoreCollision(col.collider, GetComponent<Collider2D>());
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        
        if (col.tag == "Resource" && alive && getResource() < 15){
            StartCoroutine(TakeResource(col.gameObject));
        }

        if (col.tag == "Boom"){
            BombHit();
        }
    }

    void BombHit(){
        int drop = 3;
        alive = false;
        if (ghostHome != null){
            ghostBuild = false;
            Destroy(ghostHome);
        }
        gameObject.GetComponent<AudioSource>().Play();
        StartCoroutine(BlastCooldown(blastCooldown));
        while (drop > 0 && getResource() > 0){
            setResource(getResource() -1);
            Instantiate(resourceDrop, transform.position, transform.rotation);
            drop--;
        }
    }

    IEnumerator TakeResource(GameObject res){
        setResource(resource + 1);
        res.GetComponent<AudioSource>().Play ();
        res.SetActive(false);
        yield return new WaitForSeconds(1);
        Destroy(res);
    }

    /*IEnumerator FreezeMove(RigidbodyConstraints2D freeze){
        freeze = RigidbodyConstraints2D.FreezePosition;
        yield return new WaitForSeconds(1);
        freeze = RigidbodyConstraints2D.None;
        freeze = RigidbodyConstraints2D.FreezeRotation;
    }*/

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

    void FixedUpdate()
    {
        if (alive){
            PlayerMove();
            if (hasBomb){

            if (Input.GetButtonDown("Fire1" + typeInput)){
                if (ghostBuild){
                    Destroy(ghostHome);
                    ghostBuild = false;
                }
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
                    if (ghostBuild) {
                        if (ghostHome.GetComponent<GhostHome>().canBuild){
                            StartCoroutine(BuildHome(bombCooldown + 3));
                        }
                    } else {
                        CreateGhostHome();
                    }
                }
            }

        } else {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    void CreateGhostHome(){
        ghostBuild = true;
        ghostHome = Instantiate(
            ghostHomePrefab,
            new Vector2(transform.position.x + direction/1.5f, transform.position.y),
            transform.rotation);
        ghostHome.GetComponent<GhostHome>().owner = gameObject;
    }

    IEnumerator BuildHome(int buildTime){

        alive = false;
        ghostBuild = false;
        Destroy(ghostHome);
        GameObject building = Instantiate(
            builtHome,
            new Vector2(transform.position.x + direction/1.5f, transform.position.y),
            transform.rotation);
        
        SpriteRenderer shadow = building.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        //Debug.Log("Definiu sombra aqui");
        Home house = building.GetComponent<Home>();

        house.owner = gameObject.GetComponent<Player>();

        setResource(getResource() - 5);

        buildTime = buildTime * 2;

        while (buildTime > 0){

            yield return new WaitForSeconds(0.2f);

            if (buildTime % 2 != 0){

                building.GetComponent<SpriteRenderer>().sprite = house.halfDone;
                shadow.enabled = true;

                if (buildTime >= 5 ){

                     building.GetComponent<SpriteRenderer>().enabled = true; 
                     shadow.enabled = true; 
                }
            } else{

                if (buildTime < 5 ){

                    building.GetComponent<SpriteRenderer>().sprite = house.done;
                    shadow.enabled = true;

                } else {

                    building.GetComponent<SpriteRenderer>().enabled = false;
                    shadow.enabled = false;
                    //Debug.Log("Fez sombra sumir aqui");
                }
            }
            buildTime --;
        }
        home ++;
        building.GetComponent<SpriteRenderer>().sprite = house.done;
        shadow.enabled = true;
        alive = true;
    }

    void PlayerMove(){

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(

                moveSpeed * Time.fixedDeltaTime * Input.GetAxisRaw("Horizontal" + typeInput),

                moveSpeed * Time.fixedDeltaTime * Input.GetAxisRaw("Vertical" + typeInput));
            
            if (Input.GetAxisRaw("Horizontal" + typeInput) > 0){
                
                direction = 1;
                //Debug.Log("->");
                setIsMoving(true);

            }
            if (Input.GetAxisRaw("Horizontal" + typeInput) < 0){
                
                direction = -1;
                //Debug.Log("<-");
                setIsMoving(true);

            }
            if (Input.GetAxisRaw("Vertical" + typeInput) != 0){
                //Debug.Log("^v");
                setIsMoving(true);
            }
            if (Input.GetAxisRaw("Horizontal" + typeInput) == 0 && Input.GetAxisRaw("Vertical" + typeInput) == 0){
                //Debug.Log("x");
                setIsMoving(false);
            }
            SpriteWork();
    }

    public bool setIsMoving(bool mov){
        isMoving = mov;
        //Debug.Log(mov);
        gameObject.GetComponent<Animator>().SetBool("isMoving", mov);

        return isMoving;

    }

    void SpriteWork(){
        if (direction == 1){
            if (gameObject.GetComponent<Transform>().localScale.x < 0){
                gameObject.GetComponent<Transform>().localScale *= new Vector2(-1,1);
                gameObject.GetComponent<Transform>().localScale = new Vector3(gameObject.GetComponent<Transform>().localScale.x,gameObject.GetComponent<Transform>().localScale.y,1);
            }
        }
        if (direction == -1){
            if (gameObject.GetComponent<Transform>().localScale.x > 0){
                gameObject.GetComponent<Transform>().localScale *= new Vector2(-1,1);
                gameObject.GetComponent<Transform>().localScale = new Vector3(gameObject.GetComponent<Transform>().localScale.x,gameObject.GetComponent<Transform>().localScale.y,1);
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
    }
}