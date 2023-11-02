using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.DTO;
using TaskManagerAPI.Models;
using TaskManagerAPI.Services.Interfaces;
using UsersCRUDAPI.Controllers;

namespace TaskManagerAPI.Controllers
{
    [Authorize]
    public class TagController : Controller
    {
        private readonly ITagService _tagService;
        private readonly ILogger<TaskController> _logger;

        public TagController(ITagService tagService, ILogger<TaskController> logger)
        {
            _tagService = tagService;
            _logger = logger;
        }

        /// <summary>
        /// Получение всех тегов
        /// </summary>
        [HttpGet]
        [Route("api/[controller]/GetTages")]
        public async Task<IActionResult> GetTages()
        {

            var result = _tagService.GetTages();
            if (result == null)
                return NotFound("Нет информации.");

            _logger.LogInformation("Список тегов получен.");
            return Ok(result);
        }

        /// <summary>
        /// Получение тега и всех его задач по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/[controller]/GetTag/{id}")]
        [ProducesResponseType(typeof(Tag), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetTag([FromRoute] int id)
        {
            if (id <= 0)
                return ValidationProblem("TagID должен быть больше 0");
            var result = _tagService.GetTag(id);
            if (result == null)
                return NotFound("Запрошенный тег не найден.");

            _logger.LogInformation("Тег найден.");

            return Ok(result);
        }

        /// <summary>
        /// Добавление тега
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/[controller]/AddTag")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> AddTag([FromBody] TagDTO tag)
        {
            if (tag == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (tag.Name == "")
            {
                ModelState.AddModelError("", "Поле Name пустое.");
                return StatusCode(422, ModelState);
            }

            var newTag = new Tag
            {
                Name = tag.Name
            };

            if (!_tagService.CreateTag(newTag))
            {
                ModelState.AddModelError("", "Ошибка");
                return StatusCode(500, ModelState);
            }

            var createdTag = _tagService.GetTag(tag.Name);

            _logger.LogInformation("Тег успешно создан!");
            return Ok(createdTag);

        }

        /// <summary>
        /// Удаление тега по ID
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        [HttpDelete("api/[controller]/DeleteTag")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteTag(int tagId)
        {
            if (tagId <= 0)
                return ValidationProblem("TagID должен быть больше 0");

            if (!_tagService.TagExists(tagId))
            {
                return NotFound();
            }

            var tag = _tagService.GetTag(tagId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_tagService.DeleteTag(tag))
            {
                ModelState.AddModelError("", "Ошибка!!");
                return StatusCode(500, ModelState);
            }

            _logger.LogInformation("Тег успешно удалён!");
            return Ok("Тег успешно удалён!");
        }
    }
}
