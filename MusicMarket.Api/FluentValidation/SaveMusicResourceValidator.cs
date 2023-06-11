using FluentValidation;
using MusicMarket.Api.DTO;

namespace MusicMarket.Api.FluentValidation
{
    public class SaveMusicResourceValidator : AbstractValidator<SaveMusicDTO>
    {
        public SaveMusicResourceValidator()
        {
            RuleFor(c=>c.Name).NotEmpty();
            RuleFor(c=> c.ArtistId).NotEmpty().WithMessage("ArtistId should be entered and higher than zero!");
        }
    }
}
