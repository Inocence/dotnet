using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Mappers;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using api.Models;
using dotnet.Extensions;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController: ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        private readonly UserManager<AppUser> _userManager;
        public CommentController(
            ICommentRepository commentRepo
            , IStockRepository stockRepo
            , UserManager<AppUser> userManager)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var commentList = await _commentRepo.GetAllAsync();
            return Ok(commentList.Select(x => x.RespCommentDto()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id) {
            var comment = await _commentRepo.GetByIdAsync(id);
            if(comment == null) {
                return NotFound();
            }
            return Ok(comment.RespCommentDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentDto createCommentDto) {
            var stock = await _stockRepo.StockExists(createCommentDto.StockId);
            if(!stock) {
                return BadRequest("Stock does not exist");
            }
            var username = User.GetUerName();
            var appUser = await _userManager.FindByNameAsync(username);

            var comment = createCommentDto.ReqCreateCommentDto(appUser.Id);
            await _commentRepo.CreateAsync(comment);
            return CreatedAtAction(nameof(GetById), new {id = comment.Id}, comment.RespCommentDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateCommentDto updateCommentDto) {
            var comment = updateCommentDto.ReqUpdateCommentDto();
            var commentNew = await _commentRepo.UpdateAsync(id, comment);
            if(commentNew == null) {
                return NotFound();
            }
            return Ok(commentNew.RespCommentDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id) {
            var comment = await _commentRepo.DeleteAsync(id);
            if(comment == null) {
                return NotFound();
            }
            return NoContent();
        }
    }
}