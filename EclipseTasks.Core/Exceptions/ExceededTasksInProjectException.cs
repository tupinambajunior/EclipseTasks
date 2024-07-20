using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EclipseTasks.Core.Exceptions
{
    internal class ExceededTasksInProjectException : Exception
    {
        public ExceededTasksInProjectException(string message) : base(message)
        {
        }
    }
}
