﻿namespace EclipseTasks.Core.Entities
{
    public class Project
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IList<Entities.Task> Tasks { get; }
    }
}