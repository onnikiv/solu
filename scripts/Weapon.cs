using Godot;

public partial class Weapon : Node2D
{
	private Marker2D muzzle;
	private AudioStreamPlayer2D shootAudio;

	public override void _Ready()
	{
		muzzle = GetNode<Marker2D>("Muzzle");
		shootAudio = GetNode<AudioStreamPlayer2D>("Shoot");
	}

	private float mousePosition;
	public PackedScene BulletScene = GD.Load<PackedScene>("res://scenes/bullet.tscn");

	public override void _Process(double delta)
	{
		LookAt(GetGlobalMousePosition());

		mousePosition = (RotationDegrees % 360 + 360) % 360;

		if (mousePosition > 90 && mousePosition < 270)
			Scale = new Vector2(Scale.X, -1);
		else
			Scale = new Vector2(Scale.X, 1);

		if (Input.IsActionJustPressed("fire"))
		{
			var bullet = BulletScene.Instantiate<Node2D>();
			GetTree().CurrentScene.AddChild(bullet);
			bullet.GlobalPosition = muzzle.GlobalPosition;
			bullet.Rotation = Rotation;
			shootAudio.Play();

			// Set player facing based on shoot direction
			var player = GetParent<Player>();
			if (player != null)
			{
				Vector2 shootDir = (GetGlobalMousePosition() - GlobalPosition).Normalized();
				player.SetFacingByShootDirection(shootDir);
			}
		}
	}
}
