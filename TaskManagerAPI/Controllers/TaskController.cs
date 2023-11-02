using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.DTO;
using TaskManagerAPI.Services.Interfaces;
using Task = TaskManagerAPI.Models.Task;

namespace UsersCRUDAPI.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly ITagService _tagService;
        private readonly ILogger<TaskController> _logger;

        public TaskController(ITaskService taskService, ITagService tagService, ILogger<TaskController> logger)
        {
            _taskService = taskService;
            _tagService = tagService;
            _logger = logger;
        }

        /// <summary>
        /// Получение всех задач
        /// </summary>
        [HttpGet]
        [Route("api/[controller]/GetTasks")]
        public async Task<IActionResult> GetTasks()
        {

            var result = _taskService.GetTasks();
            if (result == null)
                return NotFound("Нет информации.");

            _logger.LogInformation("Список задач получен.");
            return Ok(result);
        }

        /// <summary>
        /// Получение задачи и ее тегов по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/[controller]/GetTask/{id}")]
        [ProducesResponseType(typeof(Task), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetTask([FromRoute] int id)
        {
            if (id <= 0)
                return ValidationProblem("TaskID должен быть больше 0");
            var result = _taskService.GetTask(id);
            if (result == null)
                return NotFound("Запрошенная задача не найдена.");

            _logger.LogInformation("Задача найдена.");

            return Ok(result);
        }


        /// <summary>
        /// Добавление тега задаче
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/[controller]/AddTagToTask")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> AddTagToTask([FromQuery] int id, [FromQuery] string tag)
        {
            if (string.IsNullOrEmpty(tag))
                return ValidationProblem("Тег не должен быть пустым");

            if (id <= 0)
                return ValidationProblem("TaskID должен быть больше 0");

            if (!_taskService.TaskExists(id))
                return NotFound("Задача с таким ID не найдена!");

            if (_tagService.GetAllTagNames().Contains(tag.ToUpper()))
            {
                if (_taskService.AddTagToTask(id, tag))
                {
                    _logger.LogInformation("Teг успешно добавлен!");
                    return Ok("Тег успешно добавлен!");
                }
            }
            else
                return ValidationProblem("Неверный тег.");

            return BadRequest("Произошла ошибка.");
        }

        /// <summary>
        /// Добавление задачи
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/[controller]/AddTask")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> AddTask([FromBody] TaskDTO task)
        {
            if (task == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (task.Name == "")
            {
                ModelState.AddModelError("", "Поле Name пустое.");
                return StatusCode(422, ModelState);
            }

            if (task.Description == "")
            {
                ModelState.AddModelError("", "Поле Description пустое.");
                return StatusCode(422, ModelState);
            }

            var newTask = new Task
            {
                Name = task.Name,
                Description = task.Description
            };

            if (!_taskService.CreateTask(newTask))
            {
                ModelState.AddModelError("", "Ошибка");
                return StatusCode(500, ModelState);
            }

            var createdTask = _taskService.GetTask(task.Name);

            _logger.LogInformation("Задача успешно создана!");
            return Ok(createdTask);
        
        }

        /// <summary>
        /// Удаление задачи по ID
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpDelete("api/[controller]/DeleteTask")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            if (taskId <= 0)
                return ValidationProblem("TaskID должен быть больше 0");

            if (!_taskService.TaskExists(taskId))
            {
                return NotFound();
            }

            var task = _taskService.GetTask(taskId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_taskService.DeleteTask(task))
            {
                ModelState.AddModelError("", "Ошибка!!");
                return StatusCode(500, ModelState);
            }

            _logger.LogInformation("Задача успешно удалена!");
            return Ok("Задача успешно удалена!");
        }
    }
}

