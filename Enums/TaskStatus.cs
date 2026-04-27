using System.ComponentModel;

namespace TaskManagementSystem.API.Enums
{
    public enum TaskStatus
    {
        [Description("Pending")]
        Pending = 0,

        [Description("In Progress")]        
        InProgress = 1,

        [Description("Completed")]
        Completed = 2
    }
}

