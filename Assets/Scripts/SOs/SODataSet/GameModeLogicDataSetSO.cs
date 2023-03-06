namespace Game.DataSet
{
    public enum GameModeID
    {
        BattleRoyal = 0,
        TargetScore = 1,
        HighestScore = 2,
        HighestKills = 3,
    }
    
    public class GameModeLogicDataSetSO : DataSetSO<GameModeID, GameModeLogicSO> { }
}