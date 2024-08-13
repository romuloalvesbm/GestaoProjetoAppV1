using GestaoProjeto.CrossCutting.Exception.Messages;
using GestaoProjeto.Presentation.Contracts.v1.Models.Funcionarios.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Application.Validation
{
    public class FuncionarioExclusaoValidator : AbstractValidator<FuncionarioExclusaoRequest>
    {
        public FuncionarioExclusaoValidator()
        {
            RuleFor(x => x.FuncionarioId)
               .NotEmpty().WithErrorCode(MessagesExtensions.GetCodeMessage(MessageType.Required)).WithMessage(x => string.Format(MessagesExtensions.GetTextMessage(MessageType.Required), nameof(x.FuncionarioId)));
        }
    }
}

