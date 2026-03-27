using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class TaskItemsController : Controller
    {
        private readonly AppDbContext _context;

        public TaskItemsController(AppDbContext context)
        {
            _context = context;
        }

        // Listar tarefas 
        public async Task<IActionResult> Index(string filter)
        {
            var tasks = _context.Tasks.AsQueryable();

            if (filter == "pendentes")
            {
                tasks = tasks.Where(t => !t.IsCompleted);
            }
            else if (filter == "concluidas")
            {
                tasks = tasks.Where(t => t.IsCompleted);
            }

            return View(await tasks.ToListAsync());
        }

        // Criar tarefa (get)
        public IActionResult Create()
        {
            return View();
        }

        // Criar tarefa (post)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskItem task)
        {
            if (ModelState.IsValid)
            {
                task.CreatedAt = DateTime.Now;

                ModelState.Remove("Description");

                if (string.IsNullOrWhiteSpace(task.Description))
                {
                    task.Description = "-Sem descrição-";
                }
                _context.Add(task);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(task);
        }

        // Editar tarefa (get)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            return View(task);
        }

        // Editar tarefa (post)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TaskItem task)
        {
            if (id != task.Id) return NotFound();

            var existingTask = await _context.Tasks.FindAsync(id);

            if (existingTask == null)
                return NotFound();

            existingTask.Title = task.Title;

            if (string.IsNullOrWhiteSpace(task.Description))
            {
                existingTask.Description = "-Sem descrição-";
            }
            else
            {
                existingTask.Description = task.Description;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Deletar tarefa
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Complete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
                return NotFound();

            task.IsCompleted = true;
            task.CompletedAt = DateTime.Now;

            _context.Update(task);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        // Reabrir tarefas fechadas
        public async Task<IActionResult> Reopen(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
                return NotFound();

            task.IsCompleted = false;
            task.CompletedAt = null;

            _context.Update(task);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}