using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Core.Validators
{
    public class AnimalValidator : AbstractValidator<AddAnimalModel>
    {
        public AnimalValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .NotNull()
                .MaximumLength(100)
                .Matches("[A-Z].*").WithMessage("name needs to start with uppercase letter.");
            RuleFor(x => x.Months).NotEmpty()
                .NotNull()
                .GreaterThan(0);
            RuleFor(x => x.GenderId).NotNull()
                .ExclusiveBetween(0, 3);
            RuleFor(x => x.AnimalTypeId).NotNull();
            RuleFor(x => x.Description).MinimumLength(5)
                .MaximumLength(500);
            RuleFor(x => x.ImageUrl).Must(LinkMustBeAUri).WithMessage("Invalid url");
        }

        private static bool LinkMustBeAUri(string? link)
        {
            if (string.IsNullOrWhiteSpace(link))
            {
                return false;
            }

            Uri outUri;
            return Uri.TryCreate(link, UriKind.Absolute, out outUri)
                   && (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps);
        }
    }
}
