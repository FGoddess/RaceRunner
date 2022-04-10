using System.Collections.Generic;

public interface IInitializable
{
    public void Initialize(List<BotData> bots, PlayerData player);
}
