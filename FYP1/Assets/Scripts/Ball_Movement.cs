using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 using UnityEngine.SceneManagement;

public class Ball_Movement : MonoBehaviour
{
	public float speed;
	public float jump;
    public Text scoreText;
    public Text winText;
    public Text scoreText2;
    public Text winText2;
    public Camera myCam;
    public Camera myCamNew;
    public Light myLight;
    public GameObject WorldPlane;

    private Rigidbody rb;
    //private Transform cam;
    private GameObject player;
    private int score;
    private int play;
    private Vector3 camDirection;
    private Color colourEasy;
    private Color colourMed;
    private Color colourHard;
    Vector3 angle;

    //Checkpoints
    public GameObject checkP1;
    public GameObject checkP2;

    private Vector3 checkpointTrans;

    private bool grounded;
    private bool canDoubleJump;
    private bool move;
    private bool paused;
    private bool shadowOn;

    void Start ()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
        camDirection = myCam.transform.forward;
        //cam = Camera.main.transform;
        score = 0;
        play = 1;
        move = false;
        paused = false;
        angle = new Vector3(0, 1, 0);
        grounded = true;
        canDoubleJump = false;
        SetScoreText();
        setWinText("");
        shadowOn = true;
        colourEasy = new Vector4(0.61f, 0.64f, 0.69f, 0.5f);
        colourMed = new Vector4(0.24f, 0.37f, 0.59f, 0.5f);
        colourHard = new Vector4(0.08f, 0.18f, 0.34f, 0.5f);
        myCamNew.backgroundColor = colourEasy;
        checkpointTrans = player.transform.position;
    }

	void FixedUpdate ()
    {
		float moveHor = (Input.GetAxis("Horizontal"));
		float moveVert = (Input.GetAxis("Vertical"));
   /*     if (Input.GetKey("1"))
        {
            Rot(1);
        }
        if (Input.GetKey("2"))
        {
            Rot(-1);
        }
     */   Quaternion deltaRotation = Quaternion.Euler(angle * Time.deltaTime);
        Vector3 movement = new Vector3(moveHor,0.0f,moveVert);
        //Vector3 movement2 = new Vector3(cam.TransformDirection);
        if(play == 1)
        {
            if(move)
            {
                rb.MoveRotation(rb.rotation * deltaRotation);
            }
            if (Input.GetKey(KeyCode.P))
            {
                camDirection = myCam.transform.forward;
                movement = camDirection * speed * Time.deltaTime;
                rb.AddForce(movement);
            }
            if (Input.GetKey(KeyCode.O))
            {
                camDirection = myCam.transform.forward*-1;
                movement = camDirection * speed * Time.deltaTime;
                //rigidbody.AddForce(movement);
                rb.AddForce(movement);
                //rb.AddForce(cam.TransformDirection * speed);
                //rb.AddForce(Vector3.forward * speed);
                //transform.position += Vector3.forward * Time.deltaTime * speed;
            }
            rb.AddForce(movement*speed);
            move = false;
        }

		
	}

	void Update ()
    {
        if (player != null)
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (grounded)
                {
                    JumpNew();
                    grounded = false;
                    canDoubleJump = true;
                }
                else if (canDoubleJump)
                {
                    //rb.AddForce(Vector3.up * jump);
                    JumpNew();
                    canDoubleJump = false;
                }
            }
            if (Input.GetKey("1"))
            {
                player.transform.position = checkP1.transform.position;
                player.transform.position += Vector3.up * 3.0f;
                //rb.velocity = new Vector3(0, 0, 0);
            }
            if (Input.GetKey("2"))
            {
                player.transform.position = checkP2.transform.position;
                player.transform.position += Vector3.up * 3.0f;
            }
            if (Input.GetKey("r"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if (Input.GetKey("q"))
            {
                GameOver();
            }
            if (Input.GetKey("p"))
            {
                PauseGame();
            }
            if (Input.GetKey("n"))
            {
                StartCoroutine("changeShadows");
            }
            if (Input.GetKey("f"))
            {
                StartCoroutine("changeFog");
            }
            if (Input.GetKey("h"))
            {
                StartCoroutine("hidePlane");
            }
            if (transform.position.y <= -4f)
            {
                player.transform.position = checkpointTrans;
                score = score - 3;
                rb.velocity = new Vector3(0, 0, 0);
                //GameOver();
            }
            //updateColours();
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (player != null)
        {
            if (other.gameObject.CompareTag("Pick Up"))
            {
                other.gameObject.SetActive(false);
                score = score + 5;
                SetScoreText();
                checkpointTrans = player.transform.position;
            }
            if (other.gameObject.CompareTag("Pick Up 2"))
            {
                other.gameObject.SetActive(false);
                score = score + 8;
                SetScoreText();
                checkpointTrans = player.transform.position;
            }
        }
    }

    void OnCollisionEnter(UnityEngine.Collision other)
    {
        if (player != null)
        {
            if (other.gameObject.CompareTag("EndPlat"))
            {
                //          winText.text = "Final Score: " + score.ToString();
                //         winText2.text = "Final Score: " + score.ToString();
                setWinText("Final Score: " + score.ToString());
                play = 0;
                grounded = true;
                //SetScoreText();
            }
            else if (other.gameObject.CompareTag("Hurdle"))
            {
                score = score - 2;
                SetScoreText();
            }
            else if (other.gameObject.CompareTag("EasyPlat"))
            {
                myCamNew.backgroundColor = colourMed;
                grounded = true;
            }
            else if (other.gameObject.CompareTag("MediumPlat"))
            {
                myCamNew.backgroundColor = colourHard;
                grounded = true;
            }
            else
            {
                grounded = true;
                //checkpointTrans = player.transform.position;
            }
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
        scoreText2.text = "Score: " + score.ToString();
    }

    void setWinText(string message)
    {
        winText.text = message;
        winText2.text = message;
    }

    void JumpNew()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(new Vector3(0, jump, 0));
    }

    void Rot(int degrees)
    {
        move = true;
        angle = new Vector3(0, degrees, 0);
    }

    void GameOver()
    {
        Destroy(player);
        setWinText("Game Over");
        play = 0;
    }

    void PauseGame()
    {
        if (!paused)
        {
            Time.timeScale = 0;
            paused = true;
            setWinText("Game Paused");
        }
        else
        {
            setWinText("");
            Time.timeScale = 1;
            paused = false;
        }
    }

    IEnumerator changeShadows()
    {
        if (shadowOn)
        {
            myLight.shadows = LightShadows.None;
            shadowOn = false;
            setWinText("Shading Off");
            yield return new WaitForSeconds(1);
            setWinText("");
        }
        else
        {
            myLight.shadows = LightShadows.Soft;
            shadowOn = true;
            setWinText("Shading On");
            yield return new WaitForSeconds(1);
            setWinText("");
        }
    }

    IEnumerator changeFog()
    {
        if (RenderSettings.fog == true)
        {
            RenderSettings.fog = false;
            setWinText("Fog Off");
            yield return new WaitForSeconds(1);
            setWinText("");
        }
        else
        {
            RenderSettings.fog = true;
            setWinText("Fog On");
            yield return new WaitForSeconds(1);
            setWinText("");
        }

    }

    IEnumerator hidePlane()
    {
        if (WorldPlane.activeSelf == true)
        {
            WorldPlane.SetActive(false);
            //RenderSettings.fog = false;
            setWinText("World Plane off");
            yield return new WaitForSeconds(1);
            setWinText("");
        }
        else
        {
            WorldPlane.SetActive(true);
            //RenderSettings.fog = false;
            setWinText("World Plane on");
            yield return new WaitForSeconds(1);
            setWinText("");
        }

    }

    /*   void updateColours()
       {
           if(score >= 10 && score <= 19)
           {
               myCamNew.backgroundColor = colour10;
           }
           else if (score >=20 && score <= 29)
           {
               myCamNew.backgroundColor = colour20;
           }
           else if (score >= 30)
           {
               myCamNew.backgroundColor = colour30;
           }
       }*/

}
