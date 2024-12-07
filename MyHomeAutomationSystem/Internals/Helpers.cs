namespace MyHomeAutomationSystem.Internals
{
    public static class Helpers
    {
        public static BinarySensor BinarySensors(this IEnumerable<IEntity> entities, string entityName) =>
            (BinarySensor)entities.Single(e => e.GetType() == typeof(BinarySensor) && e.Name == entityName);
    }
}
