using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CCbook2.Data;
using CCbook2.Entities;

namespace CCbook2.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Post
        public async Task <IActionResult> ViewAllPost()
        {
            List<Post> list = await(from post in _context.Post
                                    orderby post.CreatedDateTime descending
                                     select new Post 
                                     { 
                                         Id = post.Id,
                                         Comments = post.Comments,
                                         Content = post.Content,
                                         Likes = post.Likes,
                                         CreatedDateTime = post.CreatedDateTime,
                                         UserId = post.UserId,
                                         PostedBy = post.PostedBy
                                     }).ToListAsync();

            return View(list);
        }

        public async Task<IActionResult> Index(string userId)
        {
            List<Post> list = await (from post in _context.Post
                                     where post.UserId == userId
                                     orderby post.CreatedDateTime descending
                                     select new Post
                                     {
                                         Id = post.Id,
                                         Comments = post.Comments,
                                         Content = post.Content,
                                         CreatedDateTime = post.CreatedDateTime,
                                         Likes=post.Likes,
                                         UserId = userId,
                                     }).ToListAsync();
            return View(list);
        }

/*                    return View(await _context.Post.ToListAsync());
*/

/*        public async Task<IActionResult> MyPosts()
        {

        }*/

        // GET: Post/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Post/Create
        /*        public IActionResult Create()
                {

                    return View();
                }*/

        // POST: Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content,CreatedDateTime,Likes,UserId")] Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }*/

        public async Task<IActionResult> Create(string userId)
        {

            String postedBy = "";

            foreach (var item in _context.Users)
            {
                if(item.Id == userId)
                {
                    postedBy = item.Name;
                }
            }


                Post post = new Post
            {
                UserId = userId,
                PostedBy = postedBy
            };

            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content,CreatedDateTime,Likes,UserId,PostedBy")] Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { userId = post.UserId , postedBy = post.PostedBy });
            }
            return View(post);
        }



        // GET: Post/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Content,CreatedDateTime,Likes,UserId")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Post/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id,string userId)
        {
            var post = await _context.Post.FindAsync(id);
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { userId = post.UserId });
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.Id == id);
        }
    }
}
