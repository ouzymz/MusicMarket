using FluentValidation;
using MusicMarket.Api.DTO;

namespace MusicMarket.Api.FluentValidation
{
    public class SaveArtistResourceValidator : AbstractValidator<SaveArtistDTO>
    {
        public SaveArtistResourceValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(20);
        }
    }
}
