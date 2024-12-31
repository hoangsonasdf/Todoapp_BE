using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ToDoApp.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IMongoCollection<Models.Task> _taskCollection;

        public TaskRepository(IOptions<DatabaseSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);
            _taskCollection = mongoDatabase.GetCollection<Models.Task>(options.Value.CollectionName);
        }

        public async System.Threading.Tasks.Task AddTask(string name, bool isComplete)
        {
            var newTask = new Models.Task()
            {
                Name = name,
                IsComplete = isComplete
            };

            await _taskCollection.InsertOneAsync(newTask);
        }

        public async Task<List<Models.Task>> GetTasks()
        {
            return await _taskCollection
                .Find(_ => true)
                .ToListAsync();
        }

        public async Task MarkAsComplete(string id)
        {
            var task = await _taskCollection
                .Find(t => t.Id == id)
                .SingleOrDefaultAsync();

            task.IsComplete = true;

            await _taskCollection.ReplaceOneAsync(t => t.Id == id, task);
        }
    }
}
