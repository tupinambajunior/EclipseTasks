using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace EclipseTasks.Core.VOs
{
    public class HistoryTaskChanges
    {
        public IList<ChangeItem> Changes { get; set; }

        public HistoryTaskChanges(Core.Entities.Task? beforeTask, Core.Entities.Task afterTask)
        {
            Changes = new List<ChangeItem>();

            var properties = typeof(Entities.Task).GetProperties();

            foreach (var property in properties)
            {
                var valueBefore = beforeTask is not null ? property.GetValue(beforeTask) : null;
                var valueAfter = property.GetValue(afterTask);

                if (valueBefore is null && valueAfter is null) continue;

                if (property.PropertyType.Name != "Project" 
                    && (beforeTask is null || valueBefore is null || !(valueBefore.Equals(valueAfter))))
                {
                    var oldValue = beforeTask is null || valueBefore is null ? "empty" : valueBefore?.ToString();
                    var newValue = valueAfter?.ToString();

                    if (property.PropertyType.Name.Equals("DateTime"))
                    {
                        oldValue = beforeTask is null || valueBefore is null ? "empty" : ((DateTime)valueBefore).ToString("O");
                        newValue = ((DateTime)valueAfter).ToString("O");
                    }

                    Changes.Add(new ChangeItem(
                        property.Name,
                        oldValue,
                        newValue
                    ));
                }
            }
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(Changes);
        }

        public record ChangeItem(string Property, string? OldValue, string? NewValue);
    }
}