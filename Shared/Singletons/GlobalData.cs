using Godot;

namespace sigmarket.Shared.Singletons;

public partial class GlobalData : Node
{
    public static GlobalData Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
    }
    
    public enum PeriodCycle
    {
        Day,
        Night
    };
    
    // alterar o acesso a camera e ao jogador.

    public double PlayerMoney = 30;
    public int CurrentDay = 1;
    public PeriodCycle CurrentPeriod = PeriodCycle.Day;
    public Vector2 FinalPixelPosition = new(160,0);
}