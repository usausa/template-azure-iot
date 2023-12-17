namespace Template.Services;

using Template.Models;

using Smart.Data.Accessor;

using Template.Accessors;

public sealed class SensorService
{
    private readonly ISensorAccessor sensorAccessor;

    public SensorService(IAccessorResolver<ISensorAccessor> sensorAccessor)
    {
        this.sensorAccessor = sensorAccessor.Accessor;
    }

    public ValueTask<int> UpdateSensorAsync(SensorEntity entity) =>
        sensorAccessor.UpdateSensorAsync(entity);

    public ValueTask<List<SensorEntity>> QuerySensorListAsync() =>
        sensorAccessor.QuerySensorListAsync();
}
