using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball_Movement : MonoBehaviour
{
	public float speed;
	public float jump;
    public Text scoreText;
    public Text winText;

	private Rigidbody rb;
    private int score;
    private int play;

	void Start ()
    {
		rb = GetComponent<Rigidbody>();
        score = 0;
        play = 1;
        SetScoreText();
        winText.text = "";
	}

	void FixedUpdate ()
    {
		float moveHor = (Input.GetAxis("Horizontal"));
		float moveVert = (Input.GetAxis("Vertical"));

		Vector3 movement = new Vector3(moveHor,0.0f,moveVert);
        if(play == 1)
        {
            rb.AddForce(movement*speed);
        }
		
	}

	void Update ()
    {
	/*	if(Input.GetKeyDown <KeyCode.Space>){
			GetComponent<Rigidbody> ().AddForce(Vector3.up*2000);
		}
	*/
		if (Input.GetButtonDown("Jump"))
		{
			rb.AddForce(Vector3.up * jump);
		}
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            score = score + 1;
            SetScoreText();
        }
    }

    void onCollisionEnter(UnityEngine.Collision other)
    {
        Debug.Log("Hit");
        if (other.gameObject.CompareTag("EndPlat"))
        {
            winText.text = "You Win! \nFinal Score: " + score.ToString();
            play = 0;
            //SetScoreText();
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

}
