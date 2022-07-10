namespace Template.Accessors;

using Smart.Data.Accessor.Attributes;

using Template.Models;

[DataAccessor]
public interface ISensorAccessor
{
    [Query]
    ValueTask<List<SensorEntity>> QuerySensorListAsync();

    [Execute]
    ValueTask<int> UpdateSensorAsync(SensorEntity entity);
}
