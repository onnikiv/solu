using Godot;

public partial class Weapon : Node2D
{
	private Marker2D muzzle;
	private AudioStreamPlayer2D shootAudio;
	private bool canPickUp = false;
	private Player playerInRange;
	private float mousePosition;
	public PackedScene BulletScene = GD.Load<PackedScene>("res://scenes/bullet.tscn");
	private PackedScene gunSmokeScene = GD.Load<PackedScene>("res://scenes/gunsmoke.tscn");
	private Node2D gunSmokeInstance;

	public override void _Ready()
	{
		muzzle = GetNode<Marker2D>("Muzzle");
		shootAudio = GetNode<AudioStreamPlayer2D>("Shoot");
		var area = GetNode<Area2D>("Area2D");
		area.BodyEntered += OnBodyEntered;
		area.BodyExited += OnBodyExited;

		// Instance the gunsmoke and add as child, but keep hidden
		gunSmokeInstance = gunSmokeScene.Instantiate<Node2D>();
		AddChild(gunSmokeInstance);
		gunSmokeInstance.Visible = false;
	}

	private void OnBodyEntered(Node body)
	{
		if (body is Player player)
		{
			canPickUp = true;
			playerInRange = player;
		}
	}

	private void OnBodyExited(Node body)
	{
		if (body == playerInRange)
		{
			canPickUp = false;
			playerInRange = null;
		}
	}

	public override void _Process(double delta)
	{
		if (canPickUp && Input.IsActionJustPressed("ui_accept"))
		{
			playerInRange.EquipWeapon((PackedScene)GD.Load(SceneFilePath));
			QueueFree();
		}

		// Jos parentti ei oo null
		if (GetParent() != null && GetParent().Name == "WeaponSlot")
		{
			UseGun();
		}
	}

	public void UseGun() {

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

			gunSmokeInstance.GlobalPosition = muzzle.GlobalPosition;
			gunSmokeInstance.Rotation = Mathf.Sin(Mathf.DegToRad(mousePosition));
			gunSmokeInstance.Visible = true;

			GetTree().CreateTimer(0.15f).Timeout += () => gunSmokeInstance.Visible = false;
		}
	}
}

