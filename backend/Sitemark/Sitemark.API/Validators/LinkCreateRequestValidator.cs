using FluentValidation;
using Sitemark.API.Dtos.Requests;

namespace Sitemark.API.Validators
{
    public class LinkCreateRequestValidator : AbstractValidator<LinkCreateRequest>
    {
        public LinkCreateRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(x => x.Platform)
                .NotEmpty().WithMessage("Platform is required.");

            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("Url is required.")
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("Url must be a valid absolute URL.");

            RuleFor(x => x.File)
                .NotNull().WithMessage("File is required.")
                .Must(file => file.Length > 0).WithMessage("File must not be empty.")
                .Must(file => file.ContentType == "image/jpeg" || file.ContentType == "image/png")
                .WithMessage("File must be a valid image (JPEG or PNG).");

        }
    }
}
