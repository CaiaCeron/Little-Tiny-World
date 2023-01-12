using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDataPersistence
{
    [SerializeField]
    private float movementSpeed = 0.0f;
    private Vector2 direction;

    private Rigidbody2D rb2d;
    private Animator anim;
    private SpriteRenderer sprite;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        CharacterInput();
    }


    void FixedUpdate()
    {
        CharacterMovement();
    }

    private void CharacterInput()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        CharacterAnimation(direction);

    }

    private void CharacterMovement()
    {
        rb2d.MovePosition(rb2d.position + direction * movementSpeed * Time.fixedDeltaTime);
    }

    private void CharacterAnimation(Vector2 direction)
    {
        anim.SetFloat("MoveX", direction.x);
        sprite.flipX = direction.x >= 1 ? true : false;
        anim.SetFloat("MoveY", direction.y);
        anim.SetFloat("Speed", direction.sqrMagnitude);
    }


    public void LoadGameData(GameData data)
    {
        this.transform.position = data.playerPosition;

    }

    public void SaveGameData(ref GameData data)
    {
        data.playerPosition = this.transform.position;
    }
}
