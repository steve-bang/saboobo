
using MediatR;
using SaBooBo.NotificationService.Domain.Models;

namespace SaBooBo.NotificationService.Application.Features.Commands
{
    public record OrderCompletedCommand(Order Order) : IRequest<bool>;
}