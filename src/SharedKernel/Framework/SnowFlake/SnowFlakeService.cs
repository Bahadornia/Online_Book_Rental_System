using SnowflakeGenerator;

namespace Framework.SnowFlake
{
    internal class SnowFlakeService : ISnowFlakeService
    {
        public long CreateId()
        {
            Settings settings = new()
            {
                MachineID = 1,
                CustomEpoch = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero)
            };

            Snowflake snowflake = new Snowflake(settings);
            return snowflake.NextID();
        }
    }
}
