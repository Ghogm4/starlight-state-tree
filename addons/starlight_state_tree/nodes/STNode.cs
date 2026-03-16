using Godot;
namespace StarlightStateTree;
[GlobalClass]
public partial class STNode : Node
{
	[Signal] public delegate void TransitionRequestedEventHandler(string targetStateName);
	public STNode PreviousState { get; internal set; } = null;
	public STNode ParentState { get; private set; } = null;
	public override sealed void _Ready()
	{
		OnReady();
	}
    private void ActivateProcess()
    {
        SetProcess(true);
        SetPhysicsProcess(true);
    }
    private void DeactivateProcess()
    {
        SetProcess(false);
        SetPhysicsProcess(false);
    }
	public override sealed void _EnterTree()
	{
        DeactivateProcess();
		OnEnterTree();
		ParentState = GetParentOrNull<STNode>();
	}
	public override sealed void _PhysicsProcess(double delta)
	{
		OnPhysicsUpdate(delta);
	}
	public override sealed void _Process(double delta)
	{
		OnFrameUpdate(delta);
	}

	public void RequestTransition(string targetStateName) => EmitSignal(SignalName.TransitionRequested, targetStateName);
	public void Enter()
	{
		ActivateProcess();
		OnEnter();
	}

	protected virtual void OnEnter() { }
	public void Exit()
	{
		OnExit();
		DeactivateProcess();
	}
	protected virtual void OnExit() { }
	protected virtual void OnPhysicsUpdate(double delta) { }
	protected virtual void OnFrameUpdate(double delta) { }
	protected virtual void OnReady() { }
	protected virtual void OnEnterTree() { }
}
