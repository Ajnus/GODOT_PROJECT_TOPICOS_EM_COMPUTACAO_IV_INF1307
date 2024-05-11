using Godot;
using System;

public partial class Bullet : Area2D
{
	[Export]
	public int Speed { get; set; } = 10;

	private Vector2 direction = Vector2.Zero;

	private Timer killTimer;

	//private int team = -1;

	[Signal]
	public delegate void HitEventHandler();

	public override void _Ready()
	{
		killTimer = GetNode<Timer>("KillTimer");
		killTimer.Start();
	}

	public override void _PhysicsProcess(double delta)
	{
		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		if (direction != Vector2.Zero)
		{
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").FlipH = true;
			var velocity = direction * Speed;

			GlobalPosition += velocity;
		}
	}

	public void SetDirection(Vector2 direction)
	{
		this.direction = direction;
	}

	private void OnBodyEntered(Node2D body)
	{
		Hide();
		//EmitSignal(SignalName.Hit);
		body.QueueFree();

		// Must be deferred as we can't change physics properties on a physics callback.
		GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);	
	}

	private void OnKillTimerTimeout()
	{
		QueueFree();
	}

	/*private void OnBulletBodyEntered(Node body)
	{
		if (body.HasMethod("HandleHit"))
		{
			GlobalSignals.EmitSignal("bullet_impacted", body.GlobalPosition, direction);
			if (body.HasMethod("GetTeam") && (int)body.Call("GetTeam") != team)
			{
				body.Call("HandleHit");
			}
			QueueFree();
		}
	}*/
}