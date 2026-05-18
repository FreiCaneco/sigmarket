using Godot;

namespace sigmarket.Scenes.Player;

public partial class PlayerCharacter : CharacterBody2D
{
    private double _gravity = (double)ProjectSettings.GetSetting("physics/2d/default_gravity");

    private AnimationPlayer _anim;
    private Sprite2D _sprite;
    private AudioStreamPlayer _audio;

    [Export] public float MoveSpeed = 50;
    
    public override void _Ready()
    {
        _anim = GetNode<AnimationPlayer>("AnimationPlayer");
        _sprite = GetNode<Sprite2D>("Sprite2D");
        _audio = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
    }

    public override void _Process(double delta)
    {
        UpdateVisuals();
    }

    public override void _PhysicsProcess(double delta)
    {
        Move();
        MoveAndSlide();
    }

    private void Move()
    {
        float direction = Input.GetAxis("move_left", "move_right");
        Velocity = new Vector2(direction * MoveSpeed, Velocity.Y);
    }

    private void UpdateVisuals()
    {
        if (Velocity.X > 0) _sprite.FlipH = false;
        else if (Velocity.X < 0) _sprite.FlipH = true;
        bool isMoving = Velocity.X != 0;
        if (isMoving)
        {
            _anim.Play("move");
            if (!_audio.Playing)
                _audio.Play();
        }
        else
        {
            _anim.Play("idle");
            _audio.Stop();
        }
    }
}