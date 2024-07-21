using EclipseTasks.Core.VOs;

namespace EclipseTasks.Core.Test
{
    public class HistoryTaskChangesTest
    {
        [Fact]
        public void CompareTwoTaskEquals()
        {
            var t1 = GetTaskDefault();
            var t2 = GetTaskDefault();

            t2.Id = t1.Id;
            t2.Deadline = t1.Deadline;
            t2.CreatedAt = t1.CreatedAt;

            var historyChanges = new HistoryTaskChanges(t1, t2);

            Assert.Empty(historyChanges.Changes);
        }

        private static Core.Entities.Task GetTaskDefault()
        {
            return new Core.Entities.Task
            {
                Id = Guid.NewGuid(),
                Deadline = DateTime.UtcNow,
                Description = "Miha Descrição 1",
                Priority = Core.Enums.PriorityTask.Low,
                Project = new Core.Entities.Project(),
                Status = Core.Enums.StatusTask.Done,
                Title = "Meu texto",
                CreatedAt = DateTime.UtcNow
            };
        }

        [Fact]
        public void CompareTwoTaskOnlyIdDifferente()
        {
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();

            var t1 = GetTaskDefault();
            t1.Id = id1;

            var t2 = GetTaskDefault();
            t2.Id = id2;
            t2.Deadline = t1.Deadline;
            t2.CreatedAt = t1.CreatedAt;

            var historyChanges = new HistoryTaskChanges(t1, t2);

            Assert.Single(historyChanges.Changes);
            Assert.Equal("Id", historyChanges.Changes[0].Property);
            Assert.Equal(id1.ToString(), historyChanges.Changes[0].OldValue);
            Assert.Equal(id2.ToString(), historyChanges.Changes[0].NewValue);
        }

        [Fact]
        public void CompareTwoTaskOnlyTitleDifferente()
        {
            var t1 = GetTaskDefault();

            var t2 = GetTaskDefault();
            t2.Id = t1.Id;
            t2.Deadline = t1.Deadline;
            t2.CreatedAt = t1.CreatedAt;
            t2.Title = "My updated title";

            var historyChanges = new HistoryTaskChanges(t1, t2);

            Assert.Single(historyChanges.Changes);
            Assert.Equal("Title", historyChanges.Changes[0].Property);
            Assert.Equal(t1.Title, historyChanges.Changes[0].OldValue);
            Assert.Equal(t2.Title, historyChanges.Changes[0].NewValue);
        }

        [Fact]
        public void CompareTwoTaskOnlyDeadlineDifferente()
        {
            var t1 = GetTaskDefault();

            var t2 = GetTaskDefault();
            t2.Id = t1.Id;
            t2.CreatedAt = t1.CreatedAt;
            t2.Deadline = t1.Deadline.AddDays(1);

            var historyChanges = new HistoryTaskChanges(t1, t2);

            Assert.Single(historyChanges.Changes);
            Assert.Equal("Deadline", historyChanges.Changes[0].Property);
            Assert.Equal(t1.Deadline.ToString("O"), historyChanges.Changes[0].OldValue);
            Assert.Equal(t2.Deadline.ToString("O"), historyChanges.Changes[0].NewValue);
        }

        [Fact]
        public void CompareTwoTaskOnlyStatusDifferente()
        {
            var t1 = GetTaskDefault();

            var t2 = GetTaskDefault();
            t2.Id = t1.Id;
            t2.CreatedAt = t1.CreatedAt;
            t2.Deadline = t1.Deadline;
            t2.Status = Core.Enums.StatusTask.Todo;

            var historyChanges = new HistoryTaskChanges(t1, t2);

            Assert.Single(historyChanges.Changes);
            Assert.Equal("Status", historyChanges.Changes[0].Property);
            Assert.Equal(t1.Status.ToString(), historyChanges.Changes[0].OldValue);
            Assert.Equal(t2.Status.ToString(), historyChanges.Changes[0].NewValue);
        }

        [Fact]
        public void CompareTwoTaskAllsDifferente()
        {
            var t1 = GetTaskDefault();

            var t2 = new Core.Entities.Task
            {
                Id = Guid.NewGuid(),
                Deadline = DateTime.UtcNow,
                Description = "Miha Descrição 2",
                Priority = Core.Enums.PriorityTask.High,
                Project = new Core.Entities.Project(),
                Status = Core.Enums.StatusTask.Todo,
                Title = "Meu texto 2"
            };

            var historyChanges = new HistoryTaskChanges(t1, t2);

            Assert.Equal(7, historyChanges.Changes.Count);
            Assert.Equal("Id", historyChanges.Changes[0].Property);
            Assert.Equal(t1.Id.ToString(), historyChanges.Changes[0].OldValue);
            Assert.Equal(t2.Id.ToString(), historyChanges.Changes[0].NewValue);

            Assert.Equal("Title", historyChanges.Changes[1].Property);
            Assert.Equal(t1.Title, historyChanges.Changes[1].OldValue);
            Assert.Equal(t2.Title, historyChanges.Changes[1].NewValue);

            Assert.Equal("Description", historyChanges.Changes[2].Property);
            Assert.Equal(t1.Description, historyChanges.Changes[2].OldValue);
            Assert.Equal(t2.Description, historyChanges.Changes[2].NewValue);

            Assert.Equal("Deadline", historyChanges.Changes[3].Property);
            Assert.Equal(t1.Deadline.ToString("O"), historyChanges.Changes[3].OldValue);
            Assert.Equal(t2.Deadline.ToString("O"), historyChanges.Changes[3].NewValue);

            Assert.Equal("Status", historyChanges.Changes[4].Property);
            Assert.Equal(t1.Status.ToString(), historyChanges.Changes[4].OldValue);
            Assert.Equal(t2.Status.ToString(), historyChanges.Changes[4].NewValue);

            Assert.Equal("Priority", historyChanges.Changes[5].Property);
            Assert.Equal(t1.Priority.ToString(), historyChanges.Changes[5].OldValue);
            Assert.Equal(t2.Priority.ToString(), historyChanges.Changes[5].NewValue);
        }

        [Fact]
        public void CompareTwoTaskWhereBeforeTaskIsNull()
        {
            var t2 = GetTaskDefault();

            var historyChanges = new HistoryTaskChanges(null, t2);

            Assert.Equal(7, historyChanges.Changes.Count);
            Assert.Equal("Id", historyChanges.Changes[0].Property);
            Assert.Equal("empty", historyChanges.Changes[0].OldValue);
            Assert.Equal(t2.Id.ToString(), historyChanges.Changes[0].NewValue);

            Assert.Equal("Title", historyChanges.Changes[1].Property);
            Assert.Equal("empty", historyChanges.Changes[1].OldValue);
            Assert.Equal(t2.Title, historyChanges.Changes[1].NewValue);

            Assert.Equal("Description", historyChanges.Changes[2].Property);
            Assert.Equal("empty", historyChanges.Changes[2].OldValue);
            Assert.Equal(t2.Description, historyChanges.Changes[2].NewValue);

            Assert.Equal("Deadline", historyChanges.Changes[3].Property);
            Assert.Equal("empty", historyChanges.Changes[3].OldValue);
            Assert.Equal(t2.Deadline.ToString("O"), historyChanges.Changes[3].NewValue);

            Assert.Equal("Status", historyChanges.Changes[4].Property);
            Assert.Equal("empty", historyChanges.Changes[4].OldValue);
            Assert.Equal(t2.Status.ToString(), historyChanges.Changes[4].NewValue);

            Assert.Equal("Priority", historyChanges.Changes[5].Property);
            Assert.Equal("empty", historyChanges.Changes[5].OldValue);
            Assert.Equal(t2.Priority.ToString(), historyChanges.Changes[5].NewValue);

            Assert.Equal("CreatedAt", historyChanges.Changes[6].Property);
            Assert.Equal("empty", historyChanges.Changes[6].OldValue);
            Assert.Equal(t2.CreatedAt.ToString("O"), historyChanges.Changes[6].NewValue);
        }
    }
}