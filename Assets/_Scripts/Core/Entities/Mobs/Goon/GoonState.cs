public class GoonState : DefaultEntityState
{
    public const int AI_DETECTED_STATE = 64;
    public const int AI_IN_RANGE_STATE = 128;
    public const int AI_IN_RANGE_CLOSE_STATE = 256;

    public static int GetAIState(int state)
    {
        return state & (AI_DETECTED_STATE | AI_IN_RANGE_STATE  | AI_IN_RANGE_CLOSE_STATE);
    }
}