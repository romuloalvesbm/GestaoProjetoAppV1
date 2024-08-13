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
    public class FuncionarioEdicaoValidator : AbstractValidator<FuncionarioEdicaoRequest>
    {
        public FuncionarioEdicaoValidator()
        {
            RuleFor(x => x.FuncionarioId)
                .NotEmpty().WithErrorCode(MessagesExtensions.GetCodeMessage(MessageType.Required)).WithMessage(x => string.Format(MessagesExtensions.GetTextMessage(MessageType.Required), nameof(x.FuncionarioId)));

            RuleFor(x => x.Nome)
                .NotEmpty().WithErrorCode(MessagesExtensions.GetCodeMessage(MessageType.Required)).WithMessage(x => string.Format(MessagesExtensions.GetTextMessage(MessageType.Required), nameof(x.Nome)))
                .Length(1, 100).WithErrorCode(MessagesExtensions.GetCodeMessage(MessageType.Leght)).WithMessage(x => string.Format(MessagesExtensions.GetTextMessage(MessageType.Leght), nameof(x.Nome), 1, 100));
        }
    }
}
