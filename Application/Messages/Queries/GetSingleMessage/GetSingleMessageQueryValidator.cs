using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages.Queries.GetSingleMessage
{
    public class GetSingleMessageQueryValidator : AbstractValidator<GetSingleMessageQuery>
    {
        public GetSingleMessageQueryValidator() 
        {
            RuleFor(x => x.MessageId)
                .NotNull()
                .WithMessage("Message id cannot be null");
        }
    }
}
