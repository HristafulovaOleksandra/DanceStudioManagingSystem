using DanceStudio.Reviews.Api.Controllers;
using DanceStudio.Reviews.Application.DTOs;
using DanceStudio.Reviews.Application.Reviews.Commands.CreateReview;
using DanceStudio.Reviews.Application.Reviews.Commands.DeleteReview;
using DanceStudio.Reviews.Application.Reviews.Commands.UpdateReview;
using DanceStudio.Reviews.Application.Reviews.Queries.GetReviewById;
using DanceStudio.Reviews.Application.Reviews.Queries.GetReviewsByTarget;
using Microsoft.AspNetCore.Mvc;

namespace DanceStudio.Reviews.Api.Controllers
{
    public class ReviewsController : ApiControllerBase
    {
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ReviewDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(string id)
        {
            var review = await Mediator.Send(new GetReviewByIdQuery(id));
            return Ok(review);
        }

        [HttpGet("target/{targetId}")]
        [ProducesResponseType(typeof(IEnumerable<ReviewDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByTarget(string targetId)
        {
            var reviews = await Mediator.Send(new GetReviewsByTargetQuery(targetId));
            return Ok(reviews);
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateReviewCommand command)
        {
            var id = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateReviewCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("ID mismatch");
            }

            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            await Mediator.Send(new DeleteReviewCommand(id));
            return NoContent();
        }
    }
}