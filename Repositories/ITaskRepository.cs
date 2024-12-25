namespace ToDoApp.Repositories
{
    public interface ITaskRepository
    {
        Task<List<Models.Task>> GetTasks(int page);
        Task AddTask(string name, bool isComplete);
        Task MarkAsComplete(string id);
    }
}
