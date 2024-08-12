namespace SampleArcade
{
    public interface ICharacterConfig
    {
        float Health { get; }
        float Speed { get; }
        float MaxRadiansDelta { get; }
        float Sprint { get; }
    }
}