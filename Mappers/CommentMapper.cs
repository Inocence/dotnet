using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Dtos.Stock.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto RespCommentDto(this Comment comment) {
            return new CommentDto{
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId
            };
        }

        public static Comment ReqCreateCommentDto(this CreateCommentDto createCommentDto) {
            return new Comment{
                Title = createCommentDto.Title,
                Content = createCommentDto.Content,
                StockId = createCommentDto.StockId
            };
        }

        public static Comment ReqUpdateCommentDto(this UpdateCommentDto updateCommentDto) {
            return new Comment{
                Title = updateCommentDto.Title,
                Content = updateCommentDto.Content,
            };
        }
    }
}